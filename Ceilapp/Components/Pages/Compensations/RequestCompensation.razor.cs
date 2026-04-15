using Ceilapp.Models.ceilapp;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ceilapp.Components.Pages.Compensations
{
    public partial class RequestCompensation
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
        public ceilappService ceilappService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }

        protected bool errorVisible;
        protected bool successVisible;
        protected string errorMessage = "Impossible d'envoyer la demande de compensation.";
        protected Ceilapp.Models.ceilapp.Compensation compensation;
        protected IEnumerable<RegistrationDisplayItem> studentRegistrations;
        protected int maxCompensationsPerCourse;

        public class RegistrationDisplayItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; }
        }

        protected override async Task OnInitializedAsync()
        {
            compensation = new Ceilapp.Models.ceilapp.Compensation();

            var studentId = Security.User?.Id;
            if (string.IsNullOrEmpty(studentId))
            {
                NavigationManager.NavigateTo("/");
                return;
            }

            var appSetting = await ceilappService.GetAppSettingById(1);
            if (appSetting?.CurrentSessionId == null)
            {
                errorVisible = true;
                errorMessage = "Aucune session active n'est configurée. Veuillez contacter l'administration.";
                studentRegistrations = Enumerable.Empty<RegistrationDisplayItem>();
                return;
            }

            maxCompensationsPerCourse = appSetting.MaxComponsationsPerCourse;

            var registrations = await ceilappService.dbContext.CourseRegistrations
                .Include(r => r.Course)
                .Include(r => r.CourseLevel)
                .Include(r => r.Compensations)
                .Where(r => r.UserId == studentId && r.SessionId == appSetting.CurrentSessionId.Value && r.RegistrationValidated)
                .ToListAsync();

            studentRegistrations = registrations
                .Where(r => r.Compensations == null || r.Compensations.Count < maxCompensationsPerCourse)
                .Select(r => new RegistrationDisplayItem
                {
                    Id = r.Id,
                    DisplayText = $"{r.Course?.Name} - {r.CourseLevel?.Name} ({r.InscriptionCode}) — {r.Compensations?.Count ?? 0}/{maxCompensationsPerCourse}"
                }).ToList();
        }

        protected async Task FormSubmit()
        {
            try
            {
                errorVisible = false;
                successVisible = false;

                var existingCount = await ceilappService.dbContext.Compensations
                    .CountAsync(c => c.CourseRegistrationId == compensation.CourseRegistrationId);

                if (existingCount >= maxCompensationsPerCourse)
                {
                    errorVisible = true;
                    errorMessage = $"Vous avez atteint le nombre maximum de compensations autorisées ({maxCompensationsPerCourse}) pour ce cours.";
                    return;
                }

                compensation.MakeupTeacherId = "";

                await ceilappService.CreateCompensation(compensation);

                successVisible = true;
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Success,
                    Summary = "Succès",
                    Detail = "Votre demande de compensation a été envoyée avec succès.",
                    Duration = 5000
                });

                compensation = new Ceilapp.Models.ceilapp.Compensation();
            }
            catch (Exception ex)
            {
                errorVisible = true;
                errorMessage = "Impossible d'envoyer la demande de compensation.";
            }
        }

        protected void GoToDashboard()
        {
            NavigationManager.NavigateTo("/student-dashboard");
        }
    }
}
