using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.testpages
{
    public partial class CourseRegistrations2
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

            protected IEnumerable<Ceilapp.Models.ceilapp.State> statesForBirthStateId;

            protected IEnumerable<Ceilapp.Models.ceilapp.Municipality> municipalitiesForBirthMunicipalityId;

            protected IEnumerable<Ceilapp.Models.ceilapp.Profession> professionsForProfessionId;

            protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseId;

            protected IEnumerable<Ceilapp.Models.ceilapp.CourseLevel> courseLevelsForCourseLevelId;

            protected IEnumerable<Ceilapp.Models.ceilapp.Session> sessionsForSessionId;

            protected IEnumerable<Ceilapp.Models.ceilapp.Groupe> groupesForGroupId;

            [Inject]
            protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            courseRegistrations = await ceilappService.GetCourseRegistrations(new Query { Expand = "State,Municipality,Profession,Course,CourseLevel,Session,Groupe" });

            statesForBirthStateId = await ceilappService.GetStates();

            municipalitiesForBirthMunicipalityId = await ceilappService.GetMunicipalities();

            professionsForProfessionId = await ceilappService.GetProfessions();

            coursesForCourseId = await ceilappService.GetCourses();

            courseLevelsForCourseLevelId = await ceilappService.GetCourseLevels();

            sessionsForSessionId = await ceilappService.GetSessions();

            groupesForGroupId = await ceilappService.GetGroupes();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new Ceilapp.Models.ceilapp.CourseRegistration());
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

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ceilappService.ExportCourseRegistrationsToCSV(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "State,Municipality,Profession,Course,CourseLevel,Session,Groupe",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "CourseRegistrations");
            }

            if (args == null || args.Value == "xlsx")
            {
                await ceilappService.ExportCourseRegistrationsToExcel(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "State,Municipality,Profession,Course,CourseLevel,Session,Groupe",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "CourseRegistrations");
            }
        }

        protected async Task GridRowUpdate(Ceilapp.Models.ceilapp.CourseRegistration args)
        {
            try
            {
                await ceilappService.UpdateCourseRegistration(args.Id, args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to update CourseRegistration"
                });
            }
        }

        protected async Task GridRowCreate(Ceilapp.Models.ceilapp.CourseRegistration args)
        {
            try
            {
                await ceilappService.CreateCourseRegistration(args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to create CourseRegistration"
                });
            }
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseRegistration data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseRegistration data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseRegistration data)
        {
            grid0.CancelEditRow(data);
            await ceilappService.CancelCourseRegistrationChanges(data);
        }
    }
}