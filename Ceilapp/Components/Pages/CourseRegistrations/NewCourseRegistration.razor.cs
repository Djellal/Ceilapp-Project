using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.CourseRegistrations
{
    public partial class NewCourseRegistration
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
            if(isnew)
            {
                courseRegistration = new Ceilapp.Models.ceilapp.CourseRegistration();

            }
            else 
            {
                courseRegistration = await ceilappService.GetCourseRegistrationById(Convert.ToInt32(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).Segments.Last()));
            }
            

            statesForBirthStateId = await ceilappService.GetStates();

            municipalitiesForBirthMunicipalityId = await ceilappService.GetMunicipalities();

            professionsForProfessionId = await ceilappService.GetProfessions();

            coursesForCourseId = await ceilappService.GetCourses();

            courseLevelsForCourseLevelId = await ceilappService.GetCourseLevels();

            sessionsForSessionId = await ceilappService.GetSessions();
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
        [Parameter]
        public bool isnew { get; set; } = true;

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.CreateCourseRegistration(courseRegistration);
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
    }
}