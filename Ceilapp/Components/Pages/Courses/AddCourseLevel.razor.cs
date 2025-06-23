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
    public partial class AddCourseLevel
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

            coursesForCourseId = await ceilappService.GetCourses();

            courseLevelsForNextLevelId = await ceilappService.GetCourseLevels();
        }
        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.CourseLevel courseLevel;

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseId;

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseLevel> courseLevelsForNextLevelId;

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.CreateCourseLevel(courseLevel);
                DialogService.Close(courseLevel);
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

        bool hasNextLevelIdValue;

        [Parameter]
        public int? NextLevelId { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            courseLevel = new Ceilapp.Models.ceilapp.CourseLevel();

            hasCourseIdValue = parameters.TryGetValue<int>("CourseId", out var hasCourseIdResult);

            if (hasCourseIdValue)
            {
                courseLevel.CourseId = hasCourseIdResult;
            }

            hasNextLevelIdValue = parameters.TryGetValue<int?>("NextLevelId", out var hasNextLevelIdResult);

            if (hasNextLevelIdValue)
            {
                courseLevel.NextLevelId = hasNextLevelIdResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}