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
    public partial class Courses
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

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> courses;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.Course> grid0;
        protected override async Task OnInitializedAsync()
        {
            courses = await ceilappService.GetCourses(new Query { Expand = "CourseType" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCourse>("Add Course", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<Ceilapp.Models.ceilapp.Course> args)
        {
            await DialogService.OpenAsync<EditCourse>("Edit Course", new Dictionary<string, object> { {"Id", args.Data.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Course course)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteCourse(course.Id);

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
                    Detail = $"Unable to delete Course"
                });
            }
        }

        protected Ceilapp.Models.ceilapp.Course courseChild;
        protected async Task GetChildData(Ceilapp.Models.ceilapp.Course args)
        {
            courseChild = args;
            var CourseLevelsResult = await ceilappService.GetCourseLevels(new Query { Filter = $@"i => i.CourseId == {args.Id}", Expand = "Course,CourseLevel1" });
            
            if (CourseLevelsResult != null)
            {
                args.CourseLevels = CourseLevelsResult.OrderBy(c=>c.LevelOrder).ToList();
            }
            var CourseComponentsResult = await ceilappService.GetCourseComponents(new Query { Filter = $@"i => i.CourseId == {args.Id}", Expand = "Course" });
            if (CourseComponentsResult != null)
            {
                args.CourseComponents = CourseComponentsResult.ToList();
            }
        }
        protected Ceilapp.Models.ceilapp.CourseLevel courseLevelCourseLevels;

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseIdCourseLevels;

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseLevel> courseLevelsForNextLevelIdCourseLevels;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.CourseLevel> CourseLevelsDataGrid;

        protected async Task CourseLevelsAddButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Course data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCourseLevel>("Add CourseLevels", new Dictionary<string, object> { {"CourseId" , data.Id} });
            await GetChildData(data);
            await CourseLevelsDataGrid.Reload();

        }

        protected async Task CourseLevelsRowSelect(Ceilapp.Models.ceilapp.CourseLevel args, Ceilapp.Models.ceilapp.Course data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCourseLevel>("Edit CourseLevels", new Dictionary<string, object> { {"Id", args.Id} });
            await GetChildData(data);
            await CourseLevelsDataGrid.Reload();
        }

        protected async Task CourseLevelsDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseLevel courseLevel)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteCourseLevel(courseLevel.Id);

                    await GetChildData(courseChild);

                    if (deleteResult != null)
                    {
                        await CourseLevelsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CourseLevel"
                });
            }
        }
        protected Ceilapp.Models.ceilapp.CourseComponent courseComponentCourseComponents;

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseIdCourseComponents;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.CourseComponent> CourseComponentsDataGrid;

        protected async Task CourseComponentsAddButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Course data)
        {

            var dialogResult = await DialogService.OpenAsync<AddCourseComponent>("Add CourseComponents", new Dictionary<string, object> { {"CourseId" , data.Id} });
            await GetChildData(data);
            await CourseComponentsDataGrid.Reload();

        }

        protected async Task CourseComponentsRowSelect(Ceilapp.Models.ceilapp.CourseComponent args, Ceilapp.Models.ceilapp.Course data)
        {
            var dialogResult = await DialogService.OpenAsync<EditCourseComponent>("Edit CourseComponents", new Dictionary<string, object> { {"Id", args.Id} });
            await GetChildData(data);
            await CourseComponentsDataGrid.Reload();
        }

        protected async Task CourseComponentsDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseComponent courseComponent)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteCourseComponent(courseComponent.Id);

                    await GetChildData(courseChild);

                    if (deleteResult != null)
                    {
                        await CourseComponentsDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete CourseComponent"
                });
            }
        }

        string lastFilter;

        [Inject]
        protected SecurityService Security { get; set; }
        protected async void Grid0Render(DataGridRenderEventArgs<Ceilapp.Models.ceilapp.Course> args)
        {
            if (grid0.Query.Filter != lastFilter)
            {
                courseChild = grid0.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter && courseChild != null)
            {
                await grid0.SelectRow(courseChild);
            }

            lastFilter = grid0.Query.Filter;
        }
    }
}