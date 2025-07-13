using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Evaluations
{
    public partial class EditEvaluation
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
            evaluation = await ceilappService.GetEvaluationById(Id);

            courseRegistrationsForCourseRegistrationId = await ceilappService.GetCourseRegistrations();

            coursesForCourseComponentId = await ceilappService.GetCourses();
        }
        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.Evaluation evaluation;

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseRegistration> courseRegistrationsForCourseRegistrationId;

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseComponentId;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.UpdateEvaluation(Id, evaluation);
                DialogService.Close(evaluation);
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