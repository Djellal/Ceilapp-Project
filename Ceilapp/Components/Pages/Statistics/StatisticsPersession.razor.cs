using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Radzen;
using Radzen.Blazor;
using Ceilapp.Models.ceilapp;

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

            var registrations = await ceilappService.dbContext.CourseRegistrations
                .Include(r => r.Course)
                .Include(r => r.CourseLevel)
                .Include(r => r.Profession)
                .Include(r => r.Session)
                .Where(r => r.SessionId == selectedSessionId.Value).AsNoTracking()
                .ToListAsync();

            // Calculate session-wide statistics
            sessionStats = new SessionStatistics
            {
                TotalRegistrations = registrations.Count,
                ValidatedRegistrations = registrations.Count(r => r.RegistrationValidated),
                PendingRegistrations = registrations.Count(r => !r.RegistrationValidated),
                ReregistrationCount = registrations.Count(r => r.IsReregistration),
                NewRegistrationCount = registrations.Count(r => !r.IsReregistration),
                TotalFees = registrations.Sum(r => r.PaidFeeValue),
                ExpectedFees = registrations.Sum(r => r.Profession?.FeeValue ?? 0),
                PaidFees = registrations.Sum(r => r.PaidFeeValue > 0 ? r.PaidFeeValue : 0),
                UnpaidCount = registrations.Count(r => r.PaidFeeValue == 0)
            };

            // Course statistics
            courseStats = registrations
                .GroupBy(r => r.Course)
                .Select(g => new CourseStatistic
                {
                    CourseName = g.Key?.Name ?? "Unknown",
                    TotalRegistrations = g.Count(),
                    ValidatedRegistrations = g.Count(r => r.RegistrationValidated),
                    PendingRegistrations = g.Count(r => !r.RegistrationValidated),
                    TotalFees = g.Sum(r => r.PaidFeeValue)
                })
                .OrderByDescending(c => c.TotalRegistrations)
                .ToList();

            // Profession statistics
            professionStats = registrations
                .GroupBy(r => r.Profession)
                .Select(g => new ProfessionStatistic
                {
                    ProfessionName = g.Key?.Name ?? "Unknown",
                    TotalRegistrations = g.Count(),
                    ValidatedRegistrations = g.Count(r => r.RegistrationValidated),
                    ExpectedFeeValue = g.Key?.FeeValue ?? 0,
                    TotalFees = g.Sum(r => r.PaidFeeValue)
                })
                .OrderByDescending(p => p.TotalRegistrations)
                .ToList();

            // Course level statistics
            courseLevelStats = registrations
                .GroupBy(r => new { r.Course, r.CourseLevel })
                .Select(g => new CourseLevelStatistic
                {
                    CourseName = g.Key.Course?.Name ?? "Unknown",
                    LevelName = g.Key.CourseLevel?.Name ?? "Unknown",
                    TotalRegistrations = g.Count(),
                    ValidatedRegistrations = g.Count(r => r.RegistrationValidated),
                    TotalFees = g.Sum(r => r.PaidFeeValue)
                })
                .OrderBy(cl => cl.CourseName)
                .ThenBy(cl => cl.LevelName)
                .ToList();

            StateHasChanged();
        }

        protected async Task ExportToExcel()
        {
            // TODO: Implement Excel export functionality
            NotificationService.Notify(new NotificationMessage 
            { 
                Severity = NotificationSeverity.Info, 
                Summary = "Export", 
                Detail = "Fonctionnalité d'export Excel à implémenter", 
                Duration = 3000 
            });
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
        public decimal ValidationRate => TotalRegistrations > 0 ? (decimal)ValidatedRegistrations / TotalRegistrations * 100 : 0;
    }

    public class ProfessionStatistic
    {
        public string ProfessionName { get; set; }
        public int TotalRegistrations { get; set; }
        public int ValidatedRegistrations { get; set; }
        public decimal ExpectedFeeValue { get; set; }
        public decimal TotalFees { get; set; }
        public decimal ValidationRate => TotalRegistrations > 0 ? (decimal)ValidatedRegistrations / TotalRegistrations * 100 : 0;
    }

    public class CourseLevelStatistic
    {
        public string CourseName { get; set; }
        public string LevelName { get; set; }
        public int TotalRegistrations { get; set; }
        public int ValidatedRegistrations { get; set; }
        public decimal TotalFees { get; set; }
        public decimal ValidationRate => TotalRegistrations > 0 ? (decimal)ValidatedRegistrations / TotalRegistrations * 100 : 0;
    }
}