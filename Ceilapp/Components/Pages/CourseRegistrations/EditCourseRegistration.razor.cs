using Ceilapp.Components.Pages.Sessions;
using Ceilapp.Models.ceilapp;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            
        }

        protected async Task CancelButtonClick(MouseEventArgs args)
        {
            DialogService.Close(null);
        }
        protected async Task InitNewRegistration()
        {
            courseRegistration = new Models.ceilapp.CourseRegistration();
            courseRegistration.RegistrationDate = DateTime.Now;
            courseRegistration.BirthDate = DateTime.Now.AddYears(-20);
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
            }else
            {
                FeeValue = 0.ToString("C");
            }
        }

        protected async System.Threading.Tasks.Task CourseIdChange(System.Object args)
        {
            try
            {
                courseLevelsForCourseLevelId = await ceilappService.GetCourseLevels(new Radzen.Query { Filter = "i => i.CourseId == @0", FilterParameters = new object[] { args } });
                if (isnew)
                {
                    courseRegistration.CourseLevelId = courseLevelsForCourseLevelId?.FirstOrDefault()?.Id ?? 0;
                }
                else
                    await GetNextLevel();
            }
            catch (Exception ex)
            {

                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = ex.Message + "\r\n" + ex.InnerException?.Message,
                    Duration = 4000
                });
            }
        }
        protected async System.Threading.Tasks.Task GetNextLevel()
        {
            NotificationService.Notify(new NotificationMessage { 
                Severity = NotificationSeverity.Info, 
                Summary = "Info", 
                Detail = "This feature is not implemented yet.",
                Duration = 4000
            });
        }

        protected async System.Threading.Tasks.Task BirthStateIdChange(System.Object args)
        {
           try
           { 
            municipalitiesForBirthMunicipalityId = await ceilappService.GetMunicipalities(new Radzen.Query { Filter = "i => i.StateId == @0", FilterParameters = new object[] { @args } });
            
           }
           catch (Exception ex)
           {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Erreur", Detail = ex.Message });
           }
          
        }



        protected async System.Threading.Tasks.Task Button1Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            try
            {
                if (isnew)
                {
                    courseRegistration.InscriptionCode = GenerateInscriptionCode();
                    await ceilappService.CreateCourseRegistration(courseRegistration);
                    NavigationManager.NavigateTo($"/student-dashboard");
                }
                else
                {
                    await ceilappService.UpdateCourseRegistration(Id, courseRegistration);
                }
                
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", ex.Message + "\r\n" + ex.InnerException?.Message, 5000);
            }
        }

        private string GenerateInscriptionCode()
        {
           
            if (CurrentSession == null)
            {
                return string.Empty; // or throw an exception if preferred
            }

            int count = ceilappService.dbContext.CourseRegistrations.Count(x => x.SessionId == CurrentSession.Id) + 1;



            string code = $"{CurrentSession.SessionCode}-{count:0000}";

            while (ceilappService.dbContext.CourseRegistrations.Any(reg => reg.InscriptionCode == code))
            {
                code = $"{CurrentSession.SessionCode}-{++count:0000}";
            }

            return code;
        }
    }


}