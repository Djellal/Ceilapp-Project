using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ceilapp.Models.ceilapp;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ceilapp
{
    public class ReportService
    {
        private ceilappService ceilappService;

        public ReportService(ceilappService ceilappService)
        {
            this.ceilappService = ceilappService;
        }
        
        public async Task<byte[]> GenererFicheInscriptionAsync(int registrationId)
        {
            try
            {
                var registration = ceilappService.dbContext.CourseRegistrations
                        .Include(cr => cr.Session)
                        .Include(cr => cr.Course)
                        .Include(cr => cr.CourseLevel)
                        .Include(cr => cr.Profession)
                        .Include(cr => cr.Municipality)
                        .Include(cr => cr.State)
                        .FirstOrDefault(cr => cr.Id == registrationId);
                if (registration == null)
                {
                    return null;
                }

                var AppSettings = await ceilappService.GetAppSettingById(1);
                
                // Render terms and conditions as image
                byte[] termsImage = null;
                if (!string.IsNullOrEmpty(AppSettings?.TermsAndConditions))
                {
                    termsImage = await RenderHtmlToImageAsync(AppSettings.TermsAndConditions, 800, 400);
                }

                QuestPDF.Settings.License = LicenseType.Community;

                return Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Background()
                            .Layers(layer =>
                            {
                                layer.PrimaryLayer();
                                layer.Layer()
                                    .AlignCenter()
                                    .AlignMiddle()
                                    .Image("wwwroot/images/fichinsc.png")
                                    .FitArea();
                            });
                        
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);

                        page.Content()
                            .PaddingTop(2, Unit.Centimetre)
                            .Column(column =>
                            {
                                column.Spacing(7);
                                
                                // Header
                                column.Item().Text("FICHE D'INSCRIPTION")
                                    .AlignCenter()
                                    .FontSize(20)
                                    .SemiBold()
                                    .FontColor(Colors.Blue.Darken2);
                                
                                // Registration Details
                                column.Item().Text($"Session:\t {registration.Session?.SessionName}");
                                column.Item().Text($"N° d'inscription:\t {registration.InscriptionCode}");
                                column.Item().Text($"Nom:\t {registration.LastName} {registration.FirstName}");
                                column.Item().Text($"Date de naissance:\t {registration.BirthDate:dd/MM/yyyy} à {registration.Municipality?.Name} - {registration.State?.Name}");
                                
                                column.Item().LineHorizontal(2);
                                
                                // Course Information
                                column.Item().Text($"Cours: \t{registration.Course?.Name}");
                                column.Item().Text($"Niveau: \t{registration.CourseLevel?.Name}");
                                column.Item().Text($"Profession:\t {registration.Profession?.Name} , (Droits d'inscriptions :{registration.Profession?.FeeValue:N2})");
                                
                                column.Item().LineHorizontal(2);
                                
                                // Terms and Conditions as Image
                                column.Item().Text("Conditions d'inscription:").SemiBold();
                                if (termsImage != null)
                                {
                                    column.Item().Image(termsImage).FitWidth();
                                }
                                else
                                {
                                    column.Item().Text("Aucune condition spécifiée").FontColor("#808080");
                                }
                            });
                    });
                }).GeneratePdf();
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
                return null;
            }
        }

        public async Task<byte[]> RenderHtmlToImageAsync(string htmlContent, int width = 800, int height = 600)
        {
            await new BrowserFetcher().DownloadAsync();

            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                Args = new[] { "--no-sandbox", "--disable-setuid-sandbox" }
            });

            await using var page = await browser.NewPageAsync();
            
            await page.SetViewportAsync(new ViewPortOptions
            {
                Width = width,
                Height = height
            });

            await page.SetContentAsync(htmlContent);
            await Task.Delay(1000);

            var screenshotOptions = new ScreenshotOptions
            {
                Type = ScreenshotType.Png,
                FullPage = true,
                OmitBackground = false
            };

            return await page.ScreenshotDataAsync(screenshotOptions);
        }

        public async Task<string> RenderHtmlToImageFileAsync(string htmlContent, string outputPath, int width = 800, int height = 600)
        {
            var imageBytes = await RenderHtmlToImageAsync(htmlContent, width, height);
            await File.WriteAllBytesAsync(outputPath, imageBytes);
            return outputPath;
        }
    }
}
