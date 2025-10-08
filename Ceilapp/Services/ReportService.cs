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
                        
                        .Include(cr => cr.CourseLevel)
                        .Include(cr => cr.Profession)
                        .Include(cr => cr.Municipality)
                        .Include(cr => cr.State)
                        .Include(cr => cr.Course)
                        .ThenInclude(cr=>cr.CourseType)
                        .FirstOrDefault(cr => cr.Id == registrationId);
                if (registration == null)
                {
                    return null;
                }

                var AppSettings = await ceilappService.GetAppSettingById(1);
                
                // Render terms and conditions as image
                //byte[] termsImage = null;
                //if (!string.IsNullOrEmpty(AppSettings?.TermsAndConditions))
                //{
                //    termsImage = await RenderHtmlToImageAsync(AppSettings.TermsAndConditions, 800, 300);
                //}

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
                                var title=registration.IsReregistration?"FICHE DE RÉINSCRIPTION":"FICHE D'INSCRIPTION";
                                column.Item()
                                    .Border(1)
                                    .BorderColor(Colors.Blue.Darken2)
                                    .Padding(10)
                                    .Text(title)
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
                                    AddTableRow(table, "N° Tél", registration.Tel);
                                    
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
                                        if(registration.IsReregistration) AddTableRow(table, "Niveau", registration.CourseLevel?.Name);

                                        var feeValue = "";

                                        if(registration.Course?.CourseType.Name == "Atelier")
                                        {
                                            feeValue = "Indéfini";
                                        }
                                        else
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
                                //column.Item().ShowEntire().Column(tc =>
                                //{

                                //    if (termsImage != null)
                                //    {
                                //        tc.Item()
                                //            .Border(1)
                                //            .BorderColor(Colors.Grey.Lighten2)
                                //            .Padding(2)
                                //            .Background(Colors.Grey.Lighten5)
                                //            .Image(termsImage)
                                //            .FitWidth(); // Use FitWidth() instead of FitToWidth()
                                //    }

                                //});

                                //Signature section
                                 column.Item()
                                     .AlignRight()
                                     .Text(text =>
                                     {
                                         text.Span("Date: ").SemiBold();
                                         text.Span(DateTime.Now.ToString("dd/MM/yyyy"));
                                         text.EmptyLine();
                                         text.EmptyLine();
                                         text.Span("Signature: ").SemiBold();
                                         text.EmptyLine();
                                         text.EmptyLine();
                                     });
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

        public async Task<byte[]> GenererStatisticsReportAsync(int sessionId)
        {
            try
            {
                // Get the session with its registrations
                var session = await ceilappService.dbContext.Sessions
                    .Include(s => s.CourseRegistrations)
                    .ThenInclude(cr => cr.Course)
                    .ThenInclude(c => c.CourseType)
                    .Include(s => s.CourseRegistrations)
                    .ThenInclude(cr => cr.Profession)
                    .Include(s => s.CourseRegistrations)
                    .ThenInclude(cr => cr.State)
                    .Include(s => s.CourseRegistrations)
                    .ThenInclude(cr => cr.Municipality)
                    .FirstOrDefaultAsync(s => s.Id == sessionId);
                
                if (session == null)
                {
                    return null;
                }

                // Get all registrations for this session
                var allRegistrations = session.CourseRegistrations.ToList();
                var validatedRegistrations = allRegistrations.Where(r => r.RegistrationValidated).ToList();

                // Calculate statistics
                var totalRegistrations = allRegistrations.Count;
                var validatedRegistrationsCount = validatedRegistrations.Count;
                var totalFeesCollected = validatedRegistrations.Sum(r => r.PaidFeeValue);
                var totalExpectedFees = validatedRegistrations.Sum(r => r.FeeValue);
                var unpaidFees = totalExpectedFees - totalFeesCollected;
                
                // Group validated registrations by course type
                var registrationsByCourseType = validatedRegistrations
                    .GroupBy(r => r.Course?.CourseType?.Name ?? "Unknown")
                    .Select(g => new { CourseTypeName = g.Key, Count = g.Count() })
                    .ToList();
                
                // Group validated registrations by profession
                var registrationsByProfession = validatedRegistrations
                    .Where(r => r.Profession != null)
                    .GroupBy(r => r.Profession.Name)
                    .Select(g => new { ProfessionName = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .Take(5) // Top 5 professions
                    .ToList();
                
                // Group validated registrations by origin state
                var registrationsByState = validatedRegistrations
                    .Where(r => r.State != null)
                    .GroupBy(r => r.State.Name)
                    .Select(g => new { StateName = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .Take(5) // Top 5 states
                    .ToList();

                // Count new registrations vs re-registrations (for validated only)
                var newRegistrations = validatedRegistrations.Count(r => !r.IsReregistration);
                var reRegistrations = validatedRegistrations.Count(r => r.IsReregistration);
                
                // Group validated registrations by course
                var registrationsByCourse = validatedRegistrations
                    .Where(r => r.Course != null)
                    .GroupBy(r => r.Course.Name)
                    .Select(g => new { CourseName = g.Key, Count = g.Count() })
                    .OrderByDescending(g => g.Count)
                    .ToList();

                QuestPDF.Settings.License = LicenseType.Community;

                return Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        
                        page.Header()
                            .Text($"Statistiques de la session: {session.SessionName}")
                            .SemiBold()
                            .FontSize(16)
                            .FontColor(Colors.Blue.Darken2)
                            .AlignCenter();

                        page.Content()
                            .PaddingVertical(1, Unit.Centimetre)
                            .Column(x =>
                            {
                                x.Spacing(20);

                                // Session information
                                x.Item().Element(ContainerSessionInfo);
                                
                                // Statistics summary
                                x.Item().Element(ContainerStatsSummary);
                                
                                // Registrations by course type
                                x.Item().Element(ContainerCourseTypeStats);
                                
                                // Registrations by course
                                x.Item().Element(ContainerCourseStats);
                                
                                // Registrations by profession (top 5)
                                x.Item().Element(ContainerProfessionStats);
                                
                                // Registrations by state (top 5)
                                x.Item().Element(ContainerStateStats);
                                
                                // Registration type breakdown
                                x.Item().Element(ContainerRegistrationTypeStats);
                                
                                // Footer with generation date
                                x.Item().AlignCenter().Text($"Rapport généré le: {DateTime.Now:dd/MM/yyyy HH:mm}").FontSize(10);
                            });
                    });
                }).GeneratePdf();

                // Local methods for creating different sections of the report
                void ContainerSessionInfo(IContainer container)
                {
                    container.Background(Colors.Grey.Lighten3)
                        .Padding(10)
                        .Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100);
                                columns.RelativeColumn(3);
                            });

                            AddTableRow(table, "Code Session", session.SessionCode);
                            AddTableRow(table, "Nom Session", session.SessionName);
                            AddTableRow(table, "Date Début", session.StartDate.ToString("dd/MM/yyyy"));
                            AddTableRow(table, "Date Fin", session.EndDate.ToString("dd/MM/yyyy"));
                        });
                }

                void ContainerStatsSummary(IContainer container)
                {
                    container.Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        // Row 1: Total registrations and validated registrations
                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Background(Colors.Blue.Lighten5).Padding(5)
                            .AlignCenter().Column(col =>
                            {
                                col.Item().Text("Inscriptions Totales").SemiBold();
                                col.Item().Text(totalRegistrations.ToString()).FontSize(14).SemiBold();
                            });

                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Background(Colors.Green.Lighten5).Padding(5)
                            .AlignCenter().Column(col =>
                            {
                                col.Item().Text("Inscriptions Validées").SemiBold();
                                col.Item().Text(validatedRegistrationsCount.ToString()).FontSize(14).SemiBold();
                            });

                        table.Cell().Border(1).BorderColor(Colors.Grey.Lighten2).Background(Colors.Red.Lighten5).Padding(5)
                            .AlignCenter().Column(col =>
                            {
                                col.Item().Text("Frais Impayés (DA)").SemiBold();
                                col.Item().Text(unpaidFees.ToString("N2", CultureInfo.InvariantCulture)).FontSize(14).SemiBold();
                            });
                    });
                }

                void ContainerCourseTypeStats(IContainer container)
                {
                    container.PaddingVertical(5).Column(column =>
                    {
                        column.Item().PaddingBottom(5).Text("Répartition par Type de Cours (Inscriptions Validées)").FontSize(12).SemiBold();
                        
                        if (registrationsByCourseType.Any())
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                // Header
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Type de Cours").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("Nombre").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Pourcentage").SemiBold();

                                foreach (var item in registrationsByCourseType)
                                {
                                    var percentage = validatedRegistrationsCount > 0 ? (double)item.Count / validatedRegistrationsCount * 100 : 0;
                                    
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.CourseTypeName);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text(item.Count.ToString());
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"{percentage:F1}%");
                                }
                            });
                        }
                        else
                        {
                            column.Item().Padding(10).Element(EmptyDataMessage);
                        }
                    });
                }

                void ContainerCourseStats(IContainer container)
                {
                    container.PaddingVertical(5).Column(column =>
                    {
                        column.Item().PaddingBottom(5).Text("Répartition par Cours (Inscriptions Validées)").FontSize(12).SemiBold();
                        
                        if (registrationsByCourse.Any())
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                // Header
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Cours").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("Nombre").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Pourcentage").SemiBold();

                                foreach (var item in registrationsByCourse)
                                {
                                    var percentage = validatedRegistrationsCount > 0 ? (double)item.Count / validatedRegistrationsCount * 100 : 0;
                                    
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.CourseName);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text(item.Count.ToString());
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"{percentage:F1}%");
                                }
                            });
                        }
                        else
                        {
                            column.Item().Padding(10).Element(EmptyDataMessage);
                        }
                    });
                }

                void ContainerProfessionStats(IContainer container)
                {
                    container.PaddingVertical(5).Column(column =>
                    {
                        column.Item().PaddingBottom(5).Text("Top 5 des Professions par Inscriptions (Validées)").FontSize(12).SemiBold();
                        
                        if (registrationsByProfession.Any())
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                // Header
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Profession").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("Nombre").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Pourcentage").SemiBold();

                                foreach (var item in registrationsByProfession)
                                {
                                    var percentage = validatedRegistrationsCount > 0 ? (double)item.Count / validatedRegistrationsCount * 100 : 0;
                                    
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.ProfessionName);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text(item.Count.ToString());
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"{percentage:F1}%");
                                }
                            });
                        }
                        else
                        {
                            column.Item().Padding(10).Element(EmptyDataMessage);
                        }
                    });
                }

                void ContainerStateStats(IContainer container)
                {
                    container.PaddingVertical(5).Column(column =>
                    {
                        column.Item().PaddingBottom(5).Text("Top 5 des États par Origine (Inscriptions Validées)").FontSize(12).SemiBold();
                        
                        if (registrationsByState.Any())
                        {
                            column.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(1);
                                    columns.RelativeColumn(1);
                                });

                                // Header
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("État").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("Nombre").SemiBold();
                                table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Pourcentage").SemiBold();

                                foreach (var item in registrationsByState)
                                {
                                    var percentage = validatedRegistrationsCount > 0 ? (double)item.Count / validatedRegistrationsCount * 100 : 0;
                                    
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text(item.StateName);
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text(item.Count.ToString());
                                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"{percentage:F1}%");
                                }
                            });
                        }
                        else
                        {
                            column.Item().Padding(10).Element(EmptyDataMessage);
                        }
                    });
                }

                void ContainerRegistrationTypeStats(IContainer container)
                {
                    container.PaddingVertical(5).Column(column =>
                    {
                        column.Item().PaddingBottom(5).Text("Répartition des Types d'Inscriptions (Validées)").FontSize(12).SemiBold();
                        
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            // Header
                            table.Cell().Background(Colors.Grey.Lighten2).Padding(5).Text("Type d'Inscription").SemiBold();
                            table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignCenter().Text("Nombre").SemiBold();
                            table.Cell().Background(Colors.Grey.Lighten2).Padding(5).AlignRight().Text("Pourcentage").SemiBold();

                            // New registrations
                            var newPercentage = validatedRegistrationsCount > 0 ? (double)newRegistrations / validatedRegistrationsCount * 100 : 0;
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text("Nouvelle Inscription");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text(newRegistrations.ToString());
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"{newPercentage:F1}%");

                            // Re-registrations
                            var rePercentage = validatedRegistrationsCount > 0 ? (double)reRegistrations / validatedRegistrationsCount * 100 : 0;
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).Text("Réinscription");
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignCenter().Text(reRegistrations.ToString());
                            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3).Padding(5).AlignRight().Text($"{rePercentage:F1}%");
                        });
                    });
                }

                void EmptyDataMessage(IContainer container)
                {
                    container.Background(Colors.Grey.Lighten4)
                        .Padding(10)
                        .AlignCenter()
                        .Text("Aucune donnée disponible")
                        .Italic()
                        .FontColor(Colors.Grey.Darken2);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#endif
                return null;
            }
        }
    }
}
