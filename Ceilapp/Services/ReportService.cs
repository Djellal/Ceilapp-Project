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
                    termsImage = await RenderHtmlToImageAsync(AppSettings.TermsAndConditions, 800, 300);
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
                        page.Margin(1f, Unit.Centimetre);
                        page.PageColor(Colors.White);

                        page.Content()
                            .PaddingVertical(3, Unit.Centimetre)
                            .Column(column =>
                            {
                                column.Spacing(15);
                                
                                // Header with border
                                column.Item()
                                    .Border(1)
                                    .BorderColor(Colors.Blue.Darken2)
                                    .Padding(10)
                                    .Text("FICHE D'INSCRIPTION")
                                    .AlignCenter()
                                    .FontSize(16)
                                    .Bold()
                                    .FontColor(Colors.Blue.Darken2);
                                
                                // Registration Details in a table-like format
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(1);
                                        columns.RelativeColumn(2);
                                    });
                                    
                                    AddTableRow(table, "Session", registration.Session?.SessionName);
                                    AddTableRow(table, "N° d'inscription", registration.InscriptionCode);
                                    AddTableRow(table, "Nom complet", $"{registration.LastName} {registration.FirstName}");
                                    AddTableRow(table, "Date de naissance", 
                                        $"{registration.BirthDate:dd/MM/yyyy} à {registration.Municipality?.Name} - {registration.State?.Name}");
                                });
                                
                                // Course Information in a highlighted box
                                column.Item()
                                    .Background(Colors.Grey.Lighten3)
                                    .Padding(10)
                                    .Table(table =>
                                    {
                                        table.ColumnsDefinition(columns =>
                                        {
                                            columns.RelativeColumn(1);
                                            columns.RelativeColumn(2);
                                        });
                                        
                                        AddTableRow(table, "Cours", registration.Course?.Name);
                                        AddTableRow(table, "Niveau", registration.CourseLevel?.Name);

                                        var feeValue = "";
                                        if(registration.Profession != null)
                                        {
                                            if(registration.Profession.FeeValue>=0)
                                            feeValue = registration.Profession.FeeValue.ToString("N2", CultureInfo.InvariantCulture) + " DA";
                                            else
                                            feeValue = "Indéfini";
                                        }else
                                        {
                                            feeValue = "Indéfini";
                                        }   

                                        AddTableRow(table, "Profession", 
                                            $"{registration.Profession?.Name} - Droits d'inscriptions: {feeValue}");
                                    });
                                
                                // Terms and Conditions section
                                column.Item().ShowEntire().Column(tc =>
                                {
                                                                     
                                    if (termsImage != null)
                                    {
                                        tc.Item()
                                            .Border(1)
                                            .BorderColor(Colors.Grey.Lighten2)
                                            .Padding(2)
                                            .Background(Colors.Grey.Lighten5)
                                            .Image(termsImage)
                                            .FitWidth(); // Use FitWidth() instead of FitToWidth()
                                    }
                                   
                                });
                                
                                // Signature section
                                // column.Item()
                                //     .AlignRight()
                                //     .Text(text =>
                                //     {
                                //         text.Span("Date: ").SemiBold();
                                //         text.Span(DateTime.Now.ToString("dd/MM/yyyy"));
                                //         text.EmptyLine();
                                //         text.EmptyLine();
                                //         text.Span("Signature: ").SemiBold();
                                //         text.EmptyLine();
                                //         text.EmptyLine();
                                //     });
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

        private static void AddTableRow(TableDescriptor table, string label, string value)
        {
            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                .Text(label).SemiBold();
            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).Padding(5)
                .Text(value ?? "");
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
