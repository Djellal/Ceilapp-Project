using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;
using Ceilapp.Models.ceilapp;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Colors = QuestPDF.Helpers.Colors;

namespace Ceilapp.Components.Pages.Statistics
{
    public partial class StatisticsPersession
    {
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject]
        protected TooltipService TooltipService { get; set; }

        [Inject]
        protected ContextMenuService ContextMenuService { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        public ceilappService ceilappService { get; set; }

        protected IQueryable<Session> sessions;
        protected int? selectedSessionId;
        protected Session currentSession;

        // Statistics data
        protected SessionStatistics sessionStats;
        protected List<CourseStatistic> courseStats = new List<CourseStatistic>();
        protected List<ProfessionStatistic> professionStats = new List<ProfessionStatistic>();
        protected List<CourseLevelStatistic> courseLevelStats = new List<CourseLevelStatistic>();

        protected override async Task OnInitializedAsync()
        {
            sessions = await ceilappService.GetSessions();
            
            // Get current session
            var appSettings = await ceilappService.GetAppSettingById(1);
            if (appSettings?.CurrentSessionId.HasValue == true)
            {
                currentSession = await ceilappService.GetSessionById(appSettings.CurrentSessionId.Value);
                selectedSessionId = currentSession?.Id;
            }

            if (selectedSessionId.HasValue)
            {
                await LoadStatistics();
            }
        }

        protected async Task OnSessionChange(object args)
        {
            selectedSessionId = (int?)args;
            await LoadStatistics();
        }

        protected async Task LoadStatistics()
        {
            if (!selectedSessionId.HasValue) return;

            var registrations = ceilappService.dbContext.CourseRegistrations
                .Include(r => r.Course)
                .Include(r => r.CourseLevel)
                .Include(r => r.Profession)
                .Include(r => r.Session)
                .Include(r => r.Course.CourseFees) // Add CourseFees to get correct fee values
                .Where(r => r.SessionId == selectedSessionId.Value).AsNoTracking()
                .ToList();

            // Calculate session-wide statistics
            sessionStats = new SessionStatistics
            {
                TotalRegistrations = registrations.Count,
                ValidatedRegistrations = registrations.Count(r => r.RegistrationValidated),
                PendingRegistrations = registrations.Count(r => !r.RegistrationValidated),
                ReregistrationCount = registrations.Count(r => r.IsReregistration),
                NewRegistrationCount = registrations.Count(r => !r.IsReregistration),
                TotalFees = registrations.Sum(r => r.PaidFeeValue),
                ExpectedFees = CalculateExpectedFeesForRegistrations(registrations), // Use correct method
                PaidFees = registrations.Sum(r => r.PaidFeeValue > 0 ? r.PaidFeeValue : 0),
                UnpaidCount = registrations.Count(r => r.PaidFeeValue == 0)
            };

            // Course statistics
            courseStats = registrations
                .GroupBy(r => r.CourseId) // Group by CourseId instead of Course entity
                .Select(g =>
                {
                    var firstReg = g.First();
                    var course = firstReg.Course; // Get the course from the first registration in the group
                    var courseRegs = g.ToList();
                    return new CourseStatistic
                    {
                        CourseName = course?.Name ?? "Unknown",
                        TotalRegistrations = g.Count(),
                        ValidatedRegistrations = g.Count(r => r.RegistrationValidated),
                        PendingRegistrations = g.Count(r => !r.RegistrationValidated),
                        TotalFees = g.Sum(r => r.PaidFeeValue),
                        ExpectedFees = CalculateExpectedFeesForCourseRegistrations(courseRegs)
                    };
                })
                .OrderByDescending(c => c.TotalRegistrations)
                .ToList();

            // Profession statistics
            professionStats = registrations
                .GroupBy(r => r.ProfessionId) // Group by ProfessionId instead of Profession entity
                .Select(g =>
                {
                    var firstReg = g.First();
                    var profession = firstReg.Profession; // Get the profession from the first registration in the group
                    var professionRegs = g.ToList();
                    return new ProfessionStatistic
                    {
                        ProfessionName = profession?.Name ?? "Unknown",
                        TotalRegistrations = g.Count(),
                        ValidatedRegistrations = g.Count(r => r.RegistrationValidated),
                        ExpectedFeeValue = profession?.FeeValue ?? 0,
                        TotalFees = g.Sum(r => r.PaidFeeValue),
                        ExpectedFees = CalculateExpectedFeesForProfessionRegistrations(professionRegs)
                    };
                })
                .OrderByDescending(p => p.TotalRegistrations)
                .ToList();

            // Course level statistics
            courseLevelStats = registrations
                .GroupBy(r => new { r.CourseId, r.CourseLevelId }) // Group by IDs instead of entity objects
                .Select(g =>
                {
                    var firstReg = g.First();
                    var course = firstReg.Course;
                    var level = firstReg.CourseLevel;
                    var levelRegs = g.ToList();
                    return new CourseLevelStatistic
                    {
                        CourseName = course?.Name ?? "Unknown",
                        LevelName = level?.Name ?? "Unknown",
                        TotalRegistrations = g.Count(),
                        ValidatedRegistrations = g.Count(r => r.RegistrationValidated),
                        TotalFees = g.Sum(r => r.PaidFeeValue),
                        ExpectedFees = CalculateExpectedFeesForLevelRegistrations(levelRegs)
                    };
                })
                .OrderBy(cl => cl.CourseName)
                .ThenBy(cl => cl.LevelName)
                .ToList();

            StateHasChanged();
        }

        // Helper method to calculate expected fees for registrations
        private decimal CalculateExpectedFeesForRegistrations(List<CourseRegistration> registrations)
        {
            decimal totalExpected = 0;
            foreach (var registration in registrations)
            {
                // Look for specific course fee based on profession if available
                var courseFee = registration.Course?.CourseFees?
                    .FirstOrDefault(cf => cf.ProfessionId == registration.ProfessionId);
                if (courseFee != null)
                {
                    totalExpected += courseFee.FeeValue;
                }
                else
                {
                    // Fallback to profession fee if no specific course fee exists
                    totalExpected += registration.Profession?.FeeValue ?? 0;
                }
            }
            return totalExpected;
        }

        // Helper method to calculate expected fees for course registrations
        private decimal CalculateExpectedFeesForCourseRegistrations(List<CourseRegistration> registrations)
        {
            decimal totalExpected = 0;
            foreach (var registration in registrations)
            {
                var courseFee = registration.Course?.CourseFees?
                    .FirstOrDefault(cf => cf.ProfessionId == registration.ProfessionId);
                if (courseFee != null)
                {
                    totalExpected += courseFee.FeeValue;
                }
                else
                {
                    totalExpected += registration.Profession?.FeeValue ?? 0;
                }
            }
            return totalExpected;
        }

        // Helper method to calculate expected fees for profession registrations
        private decimal CalculateExpectedFeesForProfessionRegistrations(List<CourseRegistration> registrations)
        {
            decimal totalExpected = 0;
            foreach (var registration in registrations)
            {
                var courseFee = registration.Course?.CourseFees?
                    .FirstOrDefault(cf => cf.ProfessionId == registration.ProfessionId);
                if (courseFee != null)
                {
                    totalExpected += courseFee.FeeValue;
                }
                else
                {
                    totalExpected += registration.Profession?.FeeValue ?? 0;
                }
            }
            return totalExpected;
        }

        // Helper method to calculate expected fees for level registrations
        private decimal CalculateExpectedFeesForLevelRegistrations(List<CourseRegistration> registrations)
        {
            decimal totalExpected = 0;
            foreach (var registration in registrations)
            {
                var courseFee = registration.Course?.CourseFees?
                    .FirstOrDefault(cf => cf.ProfessionId == registration.ProfessionId);
                if (courseFee != null)
                {
                    totalExpected += courseFee.FeeValue;
                }
                else
                {
                    totalExpected += registration.Profession?.FeeValue ?? 0;
                }
            }
            return totalExpected;
        }


        protected async Task ExportToPDFReport() 
        { 
                        try
            {

                var uri = $"Document/StatisticsPersession?sessionid={selectedSessionId}";
                NavigationManager.NavigateTo(uri, true);

            }
            catch (Exception ex)
            {

                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = $"Erreur lors de la génération du PDF: {ex.Message}"
                });
#if DEBUG
                throw ex;
#endif
            }
        }
        // StatisticsPersession.CreateSessionStatsSection(IContainer container)
        private void CreateSessionStatsSection(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                // Section title
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Statistiques de la Session").SemiBold().FontSize(14);
                        col.Item().PaddingVertical(5).LineHorizontal(Colors.Black);
                    });
                });

                if (sessionStats != null)
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3f);  // Libellé
                            columns.RelativeColumn(1f);  // Valeur
                        });

                        // Total
                        table.Cell().Element(HeaderCellStyle).Text("Total Inscriptions");
                        table.Cell().Element(CellStyle).Text(sessionStats.TotalRegistrations.ToString());

                        // Validées
                        table.Cell().Element(HeaderCellStyle).Text("Inscriptions Validées");
                        table.Cell().Element(CellStyle).Text(sessionStats.ValidatedRegistrations.ToString());

                        // En attente
                        table.Cell().Element(HeaderCellStyle).Text("Inscriptions en Attente");
                        table.Cell().Element(CellStyle).Text(sessionStats.PendingRegistrations.ToString());

                        // Réinscriptions
                        table.Cell().Element(HeaderCellStyle).Text("Réinscriptions");
                        table.Cell().Element(CellStyle).Text(sessionStats.ReregistrationCount.ToString());

                        // Nouvelles inscriptions
                        table.Cell().Element(HeaderCellStyle).Text("Nouvelles Inscriptions");
                        table.Cell().Element(CellStyle).Text(sessionStats.NewRegistrationCount.ToString());

                        // Frais payés
                        table.Cell().Element(HeaderCellStyle).Text("Frais Totaux Payés");
                        table.Cell().Element(CellStyle).Text(sessionStats.PaidFees.ToString("C"));

                        // Frais attendus
                        table.Cell().Element(HeaderCellStyle).Text("Frais Attendus");
                        table.Cell().Element(CellStyle).Text(sessionStats.ExpectedFees.ToString("C"));

                        // Non payés
                        table.Cell().Element(HeaderCellStyle).Text("Inscriptions Non Payées");
                        table.Cell().Element(CellStyle).Text(sessionStats.UnpaidCount.ToString());

                        // Taux de validation
                        table.Cell().Element(HeaderCellStyle).Text("Taux de Validation (%)");
                        table.Cell().Element(CellStyle).Text(sessionStats.ValidationRate.ToString("F2"));

                        // Taux de paiement
                        table.Cell().Element(HeaderCellStyle).Text("Taux de Paiement (%)");
                        table.Cell().Element(CellStyle).Text(sessionStats.PaymentRate.ToString("F2"));
                    });
                }
                else
                {
                    column.Item().Element(EmptyDataStyle).Text("Aucune donnée disponible");
                }
            });
        }

        // StatisticsPersession.CreateCourseStatsSection(IContainer container)
        private void CreateCourseStatsSection(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Statistiques par Cours").SemiBold().FontSize(14);
                        col.Item().PaddingVertical(5).LineHorizontal(Colors.Black);
                    });
                });
        
                if (courseStats.Any())
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                        });
        
                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("Cours").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Total").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Validées").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("En Attente").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Frais Payés").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Frais Attendus").SemiBold();
                        });
        
                        foreach (var stat in courseStats)
                        {
                            table.Cell().Element(CellStyle).Text(stat.CourseName);
                            table.Cell().Element(CellStyle).Text(stat.TotalRegistrations.ToString());
                            table.Cell().Element(CellStyle).Text(stat.ValidatedRegistrations.ToString());
                            table.Cell().Element(CellStyle).Text(stat.PendingRegistrations.ToString());
                            table.Cell().Element(CellStyle).Text(stat.TotalFees.ToString("C"));
                            table.Cell().Element(CellStyle).Text(stat.ExpectedFees.ToString("C"));
                        }
                    });
                }
                else
                {
                    column.Item().Element(EmptyDataStyle).Text("Aucune donnée disponible");
                }
            });
        }

        // StatisticsPersession.CreateProfessionStatsSection(IContainer container)
        private void CreateProfessionStatsSection(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Statistiques par Profession").SemiBold().FontSize(14);
                        col.Item().PaddingVertical(5).LineHorizontal(Colors.Black);
                    });
                });

                if (professionStats.Any())
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("Profession").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Total").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Validées").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Frais Payés").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Frais Attendus").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Frais de Base").SemiBold();
                        });

                        foreach (var stat in professionStats)
                        {
                            table.Cell().Element(CellStyle).Text(stat.ProfessionName);
                            table.Cell().Element(CellStyle).Text(stat.TotalRegistrations.ToString());
                            table.Cell().Element(CellStyle).Text(stat.ValidatedRegistrations.ToString());
                            table.Cell().Element(CellStyle).Text(stat.TotalFees.ToString("C"));
                            table.Cell().Element(CellStyle).Text(stat.ExpectedFees.ToString("C"));
                            table.Cell().Element(CellStyle).Text(stat.ExpectedFeeValue.ToString("C"));
                        }
                    });
                }
                else
                {
                    column.Item().Element(EmptyDataStyle).Text("Aucune donnée disponible");
                }
            });
        }

        // StatisticsPersession.CreateCourseLevelStatsSection(IContainer container)
        private void CreateCourseLevelStatsSection(IContainer container)
        {
            container.PaddingVertical(10).Column(column =>
            {
                column.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("Statistiques par Niveau de Cours").SemiBold().FontSize(14);
                        col.Item().PaddingVertical(5).LineHorizontal(Colors.Black);
                    });
                });

                if (courseLevelStats.Any())
                {
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2f);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1f);
                            columns.RelativeColumn(1.5f);
                            columns.RelativeColumn(1.5f);
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(HeaderCellStyle).Text("Cours").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Niveau").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Total").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Validées").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("En Attente").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Frais Payés").SemiBold();
                            header.Cell().Element(HeaderCellStyle).Text("Frais Attendus").SemiBold();
                        });

                        foreach (var stat in courseLevelStats)
                        {
                            table.Cell().Element(CellStyle).Text(stat.CourseName);
                            table.Cell().Element(CellStyle).Text(stat.LevelName);
                            table.Cell().Element(CellStyle).Text(stat.TotalRegistrations.ToString());
                            table.Cell().Element(CellStyle).Text(stat.ValidatedRegistrations.ToString());
                            table.Cell().Element(CellStyle).Text((stat.TotalRegistrations - stat.ValidatedRegistrations).ToString());
                            table.Cell().Element(CellStyle).Text(stat.TotalFees.ToString("C"));
                            table.Cell().Element(CellStyle).Text(stat.ExpectedFees.ToString("C"));
                        }
                    });
                }
                else
                {
                    column.Item().Element(EmptyDataStyle).Text("Aucune donnée disponible");
                }
            });
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container.Border(1).BorderColor(Colors.Grey.Lighten1).Background(Colors.Grey.Lighten3).PaddingHorizontal(5).PaddingVertical(2);
        }

        private static IContainer HeaderCellStyle(IContainer container)
        {
            return container.Border(1).BorderColor(Colors.Grey.Medium).Background(Colors.Grey.Medium).PaddingHorizontal(5).PaddingVertical(2);
        }

        private static IContainer EmptyDataStyle(IContainer container)
        {
            return container.Background(Colors.Grey.Lighten3).Padding(10).AlignCenter();
        }
    }

    public class SessionStatistics
    {
        public int TotalRegistrations { get; set; }
        public int ValidatedRegistrations { get; set; }
        public int PendingRegistrations { get; set; }
        public int ReregistrationCount { get; set; }
        public int NewRegistrationCount { get; set; }
        public decimal TotalFees { get; set; }
        public decimal ExpectedFees { get; set; }
        public decimal PaidFees { get; set; }
        public int UnpaidCount { get; set; }
        public decimal ValidationRate => TotalRegistrations > 0 ? (decimal)ValidatedRegistrations / TotalRegistrations * 100 : 0;
        public decimal PaymentRate => ExpectedFees > 0 ? PaidFees / ExpectedFees * 100 : 0;
    }

    public class CourseStatistic
    {
        public string CourseName { get; set; }
        public int TotalRegistrations { get; set; }
        public int ValidatedRegistrations { get; set; }
        public int PendingRegistrations { get; set; }
        public decimal TotalFees { get; set; }
        public decimal ExpectedFees { get; set; }
        public decimal ValidationRate => TotalRegistrations > 0 ? (decimal)ValidatedRegistrations / TotalRegistrations * 100 : 0;
    }

    public class ProfessionStatistic
    {
        public string ProfessionName { get; set; }
        public int TotalRegistrations { get; set; }
        public int ValidatedRegistrations { get; set; }
        public decimal ExpectedFeeValue { get; set; }
        public decimal TotalFees { get; set; }
        public decimal ExpectedFees { get; set; }
        public decimal ValidationRate => TotalRegistrations > 0 ? (decimal)ValidatedRegistrations / TotalRegistrations * 100 : 0;
    }

    public class CourseLevelStatistic
    {
        public string CourseName { get; set; }
        public string LevelName { get; set; }
        public int TotalRegistrations { get; set; }
        public int ValidatedRegistrations { get; set; }
        public decimal TotalFees { get; set; }
        public decimal ExpectedFees { get; set; }
        public decimal ValidationRate => TotalRegistrations > 0 ? (decimal)ValidatedRegistrations / TotalRegistrations * 100 : 0;
    }
}