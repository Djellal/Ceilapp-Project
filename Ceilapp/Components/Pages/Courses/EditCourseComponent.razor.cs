using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Courses
{
    public partial class EditCourseComponent
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
            courseComponent = await ceilappService.GetCourseComponentById(Id);

            coursesForCourseId = await ceilappService.GetCourses();
        }
        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.CourseComponent courseComponent;

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseId;

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.UpdateCourseComponent(Id, courseComponent);
                DialogService.Close(courseComponent);
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





        bool hasCourseIdValue;

        [Parameter]
        public int CourseId { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            courseComponent = new Ceilapp.Models.ceilapp.CourseComponent();

            hasCourseIdValue = parameters.TryGetValue<int>("CourseId", out var hasCourseIdResult);

            if (hasCourseIdValue)
            {
                courseComponent.CourseId = hasCourseIdResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}