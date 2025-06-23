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
    public partial class CourseLevels
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

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseLevel> courseLevels;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.CourseLevel> grid0;
        protected override async Task OnInitializedAsync()
        {
            courseLevels = await ceilappService.GetCourseLevels(new Query { Expand = "Course,CourseLevel1" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCourseLevel>("Add CourseLevel", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<Ceilapp.Models.ceilapp.CourseLevel> args)
        {
            await DialogService.OpenAsync<EditCourseLevel>("Edit CourseLevel", new Dictionary<string, object> { {"Id", args.Data.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseLevel courseLevel)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteCourseLevel(courseLevel.Id);

                    if (deleteResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CourseLevel"
                });
            }
        }

        protected Ceilapp.Models.ceilapp.CourseLevel courseLevelChild;
        protected async Task GetChildData(Ceilapp.Models.ceilapp.CourseLevel args)
        {
            courseLevelChild = args;
        }

        string lastFilter;

        [Inject]
        protected SecurityService Security { get; set; }
        protected async void Grid0Render(DataGridRenderEventArgs<Ceilapp.Models.ceilapp.CourseLevel> args)
        {
            if (grid0.Query.Filter != lastFilter)
            {
                courseLevelChild = grid0.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter && courseLevelChild != null)
            {
                await grid0.SelectRow(courseLevelChild);
            }

            lastFilter = grid0.Query.Filter;
        }
    }
}