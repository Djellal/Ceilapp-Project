using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Ceilapp.Models.ceilapp;

namespace Ceilapp.Components.Pages.CourseRegistrations
{
    public partial class EditCourseRegistration
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

        public string FeeValue { get; set; } = 0.ToString("C");

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public bool isnew { get; set; } = true;

        protected override async Task OnInitializedAsync()
        {



            statesForBirthStateId = await ceilappService.GetStates();

            municipalitiesForBirthMunicipalityId = await ceilappService.GetMunicipalities();

            professionsForProfessionId = await ceilappService.GetProfessions();

            coursesForCourseId = await ceilappService.GetCourses();

            courseLevelsForCourseLevelId = await ceilappService.GetCourseLevels();

            sessionsForSessionId = await ceilappService.GetSessions();

            AppSetting = await ceilappService.GetAppSettingById(1);
            if (AppSetting == null)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "AppSetting not found. Please contact the administrator.", Duration = 5000 });
                NavigationManager.NavigateTo("/");
                return;
            }

            CurrentSession = AppSetting.CurrentSessionId.HasValue ? await ceilappService.GetSessionById(AppSetting.CurrentSessionId.Value) : null;
            if (CurrentSession == null) 
            { 
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Current session not found. Please contact the administrator.", Duration = 5000 });
                NavigationManager.NavigateTo("/");
                return;
            }
            if (isnew)
            {
               
                await InitNewRegistration();

            }
            else
            {
                courseRegistration = await ceilappService.GetCourseRegistrationById(Id);

            }
        }
        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.CourseRegistration courseRegistration;

        protected IEnumerable<Ceilapp.Models.ceilapp.State> statesForBirthStateId;

        protected IEnumerable<Ceilapp.Models.ceilapp.Municipality> municipalitiesForBirthMunicipalityId;

        protected IEnumerable<Ceilapp.Models.ceilapp.Profession> professionsForProfessionId;

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseId;

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseLevel> courseLevelsForCourseLevelId;

        protected IEnumerable<Ceilapp.Models.ceilapp.Session> sessionsForSessionId;

        [Inject]
        protected SecurityService Security { get; set; }
        public AppSetting AppSetting { get; private set; }
        public Session CurrentSession { get; private set; }

        protected async Task FormSubmit()
        {
            try
            {
                if (isnew)
                {
                    await ceilappService.CreateCourseRegistration(courseRegistration);
                }
                else
                {
                    await ceilappService.UpdateCourseRegistration(Id, courseRegistration);
                }
                DialogService.Close(courseRegistration);
            }
            catch (Exception ex)
            {
                errorVisible = true;
            }
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
        protected async Task InitNewRegistration()
        {
            courseRegistration = new Models.ceilapp.CourseRegistration();
            courseRegistration.RegistrationDate = DateTime.Now;
            courseRegistration.UserId = Security.User.Id;
            courseRegistration.SessionId = CurrentSession?.Id ?? 0; // Ensure SessionId is set to the current session
            courseRegistration.InscriptionCode = CurrentSession?.SessionCode + "/..";

        }

        protected async System.Threading.Tasks.Task ProfessionIdChange(System.Object args)
        {
            var selectedprofession = professionsForProfessionId.FirstOrDefault(x => x.Id == (int)args);
            if (selectedprofession != null)
            {
                FeeValue = selectedprofession.FeeValue.ToString("C");
            }
        }
    }


}