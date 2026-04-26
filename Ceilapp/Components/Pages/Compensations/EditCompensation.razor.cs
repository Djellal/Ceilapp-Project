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
    public partial class EditCompensation
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

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            compensation = await ceilappService.GetCompensationById(Id);

            var courseRegistrations = (await ceilappService.GetCourseRegistrations()).ToList();

            registrationDisplayItems = courseRegistrations.Select(r => new RegistrationDisplayItem
            {
                Id = r.Id,
                DisplayText = $"{r.LastName} {r.FirstName} - {r.Course?.Name} ({r.InscriptionCode})"
            }).ToList();

            teacherNames = (await Security.GetUsers(Constants.TEACHER)).Select(t => t.Name).ToList();

            var existingCompensations = (await ceilappService.GetCompensations()).ToList();
            courseLevelSuggestions = existingCompensations.Select(c => c.CourseLevel).Where(v => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();
            originGroupSuggestions = existingCompensations.Select(c => c.OriginGroup).Where(v => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();
            recipientGroupSuggestions = existingCompensations.Select(c => c.RecipientGroup).Where(v => !string.IsNullOrWhiteSpace(v)).Distinct().ToList();

            var appSetting = await ceilappService.GetAppSettingById(1);
            maxCompensationsPerCourse = appSetting?.MaxComponsationsPerCourse ?? 0;
        }
        protected bool errorVisible;
        protected string errorMessage = "Impossible d'enregistrer la séance de rattrapage";
        protected int maxCompensationsPerCourse;
        protected Ceilapp.Models.ceilapp.Compensation compensation;

        protected List<RegistrationDisplayItem> registrationDisplayItems;
        protected List<string> teacherNames;
        protected List<string> courseLevelSuggestions;
        protected List<string> originGroupSuggestions;
        protected List<string> recipientGroupSuggestions;

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

                if (compensation.IsApproved)
                {
                    var approvedCount = await ceilappService.dbContext.Compensations
                        .CountAsync(c => c.CourseRegistrationId == compensation.CourseRegistrationId
                            && c.IsApproved && c.Id != Id);

                    if (approvedCount >= maxCompensationsPerCourse)
                    {
                        errorVisible = true;
                        errorMessage = $"Cet étudiant a atteint le nombre maximum de séances de rattrapage approuvées ({maxCompensationsPerCourse}) pour ce cours.";
                        return;
                    }
                }

                await ceilappService.UpdateCompensation(Id, compensation);
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

        protected void SetAbsenceTimeSlot(int fromHour, int toHour)
        {
            compensation.AbsenceFrom = new TimeSpan(fromHour, 0, 0);
            compensation.AbsenceTo = new TimeSpan(toHour, 0, 0);
        }

        protected void SetMakeupTimeSlot(int fromHour, int toHour)
        {
            compensation.MakeupFrom = new TimeSpan(fromHour, 0, 0);
            compensation.MakeupTo = new TimeSpan(toHour, 0, 0);
        }

        protected async Task PrintCompensationPdf()
        {
            try
            {
                errorVisible = false;

                if (compensation.IsApproved)
                {
                    var approvedCount = await ceilappService.dbContext.Compensations
                        .CountAsync(c => c.CourseRegistrationId == compensation.CourseRegistrationId
                            && c.IsApproved && c.Id != Id);

                    if (approvedCount >= maxCompensationsPerCourse)
                    {
                        errorVisible = true;
                        errorMessage = $"Cet étudiant a atteint le nombre maximum de séances de rattrapage approuvées ({maxCompensationsPerCourse}) pour ce cours.";
                        return;
                    }
                }

                await ceilappService.UpdateCompensation(Id, compensation);
            }
            catch (Exception)
            {
                errorVisible = true;
                errorMessage = "Impossible d'enregistrer la séance de rattrapage.";
                return;
            }

            await JSRuntime.InvokeVoidAsync("open", $"/Document/CompensationPdf?id={Id}", "_blank");
        }
    }
}
