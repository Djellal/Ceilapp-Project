using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Compensations
{
    public partial class AddCompensation
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

        protected override async Task OnInitializedAsync()
        {
            compensation = new Ceilapp.Models.ceilapp.Compensation
            {
                AbsenceDate = DateTime.Today,
                AbsenceFrom = new TimeSpan(8, 0, 0),
                AbsenceTo = new TimeSpan(9, 30, 0),
                MakeupDate = DateTime.Today,
                MakeupFrom = new TimeSpan(8, 0, 0),
                MakeupTo = new TimeSpan(9, 30, 0),
            };

            var courseRegistrations = (await ceilappService.GetCourseRegistrations()).ToList();

            registrationDisplayItems = courseRegistrations.Select(r => new RegistrationDisplayItem
            {
                Id = r.Id,
                DisplayText = $"{r.LastName} {r.FirstName} - {r.Course?.Name} ({r.InscriptionCode})"
            }).ToList();

            teacherNames = (await Security.GetUsers(Constants.TEACHER)).Select(t => t.Name).ToList();

            var appSetting = await ceilappService.GetAppSettingById(1);
            maxCompensationsPerCourse = appSetting?.MaxComponsationsPerCourse ?? 0;
        }
        protected bool errorVisible;
        protected string errorMessage = "Impossible d'enregistrer la séance de rattrapage";
        protected int maxCompensationsPerCourse;
        protected Ceilapp.Models.ceilapp.Compensation compensation;

        protected List<RegistrationDisplayItem> registrationDisplayItems;
        protected List<string> teacherNames;

        public class RegistrationDisplayItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; }
        }

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                errorVisible = false;

                var approvedCount = await ceilappService.dbContext.Compensations
                    .CountAsync(c => c.CourseRegistrationId == compensation.CourseRegistrationId && c.IsApproved);

                if (approvedCount >= maxCompensationsPerCourse)
                {
                    errorVisible = true;
                    errorMessage = $"Cet étudiant a atteint le nombre maximum de séances de rattrapage approuvées ({maxCompensationsPerCourse}) pour ce cours.";
                    return;
                }

                await ceilappService.CreateCompensation(compensation);
                DialogService.Close(compensation);
            }
            catch (Exception ex)
            {
                errorVisible = true;
                errorMessage = "Impossible d'enregistrer la séance de rattrapage.";
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
    }
}
