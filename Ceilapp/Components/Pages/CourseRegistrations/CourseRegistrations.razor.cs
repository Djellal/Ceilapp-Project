using Ceilapp.Components.Pages.Appsettings;
using Ceilapp.Models.ceilapp;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ceilapp.Components.Pages.CourseRegistrations
{
    public partial class CourseRegistrations
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

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseRegistration> courseRegistrations;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.CourseRegistration> grid0;

        [Inject]
        protected SecurityService Security { get; set; }

        protected System.Linq.IQueryable<Ceilapp.Models.ceilapp.Session> sessions;
        protected int? SelectedSession = null;

        protected System.Linq.IQueryable<Ceilapp.Models.ceilapp.Course> courses;
        protected int? SelectedCourse = null;
        protected int? SelectedLevel = null;
        protected bool? Validated = null;


        protected System.Linq.IQueryable<Ceilapp.Models.ceilapp.CourseLevel> courseLevels;

        public AppSetting AppSettings { get; private set; }
        public Session CurrentSession { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            courseRegistrations = await ceilappService.GetCourseRegistrations(new Query { Expand = "State,Municipality,Profession,Course,CourseLevel,Session" });
            sessions = await ceilappService.GetSessions();
            courses = await ceilappService.GetCourses();
            courseLevels = await ceilappService.GetCourseLevels();

            AppSettings = await ceilappService.GetAppSettingById(1);

            if (AppSettings != null)
            {
                CurrentSession = AppSettings.CurrentSessionId.HasValue ? await ceilappService.GetSessionById(AppSettings.CurrentSessionId.Value) : null;
            }

            SelectedSession = CurrentSession?.Id;

        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
           
        }

        protected async Task EditRow(Ceilapp.Models.ceilapp.CourseRegistration args)
        {
           // await DialogService.OpenAsync<EditCourseRegistration>("Edit CourseRegistration", new Dictionary<string, object> { {"Id", args.Id} });
            NavigationManager.NavigateTo($"edit-course-registration/false/{args.Id}/true"); 
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseRegistration courseRegistration)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteCourseRegistration(courseRegistration.Id);

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
                    Detail = $"Unable to delete CourseRegistration"
                });
            }
        }

        protected async Task Filter()
        {
            try
            {

                IEnumerable<Ceilapp.Models.ceilapp.CourseRegistration> TempcourseRegistrations;
                if (SelectedSession.HasValue)
                {
                    TempcourseRegistrations = await ceilappService.GetCourseRegistrations(new Radzen.Query { Filter = "i => i.SessionId == @0", FilterParameters = new object[] { SelectedSession.Value }, Expand = "State,Municipality,Profession,Course,CourseLevel,Session" });
                }
                else
                {
                    TempcourseRegistrations = await ceilappService.GetCourseRegistrations(new Query { Expand = "State,Municipality,Profession,Course,CourseLevel,Session" });
                }

                if(Validated.HasValue)
                {
                    TempcourseRegistrations = TempcourseRegistrations.Where(cr => cr.RegistrationValidated == Validated.Value);
                }

                if (SelectedCourse.HasValue)
                {
                    TempcourseRegistrations = courseRegistrations.Where(cr => cr.CourseId == SelectedCourse.Value);
                }

                if (SelectedLevel.HasValue)
                {
                    TempcourseRegistrations = courseRegistrations.Where(cr => cr.CourseLevelId == SelectedLevel.Value);
                }

                courseRegistrations = TempcourseRegistrations;
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = "Error",
                    Detail = "Unable to filter Course Registrations"
                });
                Console.WriteLine($"Error filtering Course Registrations: {ex.Message}");
            }
        }

        protected async System.Threading.Tasks.Task DropDown0Change(System.Object args)
        {
            await Filter();
        }

        protected async System.Threading.Tasks.Task DropDown1Change(System.Object args)
        {
           if(SelectedCourse.HasValue) courseLevels = await ceilappService.GetCourseLevels(new Radzen.Query { Filter = "i => i.CourseId == @0", FilterParameters = new object[] { SelectedCourse } });
            await Filter();
        }

        protected async System.Threading.Tasks.Task DropDown2Change(System.Object args)
        {
            await Filter();
        }

        protected async System.Threading.Tasks.Task Button0Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            SelectedSession = CurrentSession?.Id;
        }

        


        protected async System.Threading.Tasks.Task ValidesSelectBarChange(bool? args)
        {
            await Filter();
        }
    }
}