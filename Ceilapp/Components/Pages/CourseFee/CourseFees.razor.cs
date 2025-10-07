using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.CourseFee
{
    public partial class CourseFees
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

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseFee> courseFees;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.CourseFee> grid0;

            protected IEnumerable<Ceilapp.Models.ceilapp.Profession> professionsForProfessionId;

            protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseId;

            protected int? SelectedCourse=null;

            [Inject]
            protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            courseFees = await ceilappService.GetCourseFees(new Query { Expand = "Profession,Course" });

            professionsForProfessionId = await ceilappService.GetProfessions();

            coursesForCourseId = await ceilappService.GetCourses();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {

           if(SelectedCourse.HasValue) await grid0.InsertRow(new Ceilapp.Models.ceilapp.CourseFee { CourseId=(int)SelectedCourse.Value});
            else await grid0.InsertRow(new Ceilapp.Models.ceilapp.CourseFee {});

        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseFee courseFee)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteCourseFee(courseFee.Id);

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
                    Detail = $"Unable to delete CourseFee"
                });
            }
        }

        protected async Task ExportClick(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await ceilappService.ExportCourseFeesToCSV(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "Profession,Course",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "CourseFees");
            }

            if (args == null || args.Value == "xlsx")
            {
                await ceilappService.ExportCourseFeesToExcel(new Query
                {
                    Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}",
                    OrderBy = $"{grid0.Query.OrderBy}",
                    Expand = "Profession,Course",
                    Select = string.Join(",", grid0.ColumnsCollection.Where(c => c.GetVisible() && !string.IsNullOrEmpty(c.Property)).Select(c => c.Property.Contains(".") ? c.Property + " as " + c.Property.Replace(".", "") : c.Property))
                }, "CourseFees");
            }
        }

        protected async Task GridRowUpdate(Ceilapp.Models.ceilapp.CourseFee args)
        {
            try
            {
                await ceilappService.UpdateCourseFee(args.Id, args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to update CourseFee"
                });
            }
        }

        protected async Task GridRowCreate(Ceilapp.Models.ceilapp.CourseFee args)
        {
            try
            {
                if(ceilappService.dbContext.CourseFees.Any(cf=>cf.CourseId==args.CourseId && args.ProfessionId == cf.ProfessionId)){
                     NotificationService.Notify(new NotificationMessage
                        {
                            Severity = NotificationSeverity.Error,
                            Summary = $"Erreur",
                            Detail = $"Le prix de ce cours avec cette profession existe dèjà"
                        });
                        grid0.CancelEditRow(args);
                 return;
                }
                await ceilappService.CreateCourseFee(args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to create CourseFee"
                });
            }
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseFee data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseFee data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.CourseFee data)
        {
            grid0.CancelEditRow(data);
            await ceilappService.CancelCourseFeeChanges(data);
        }

        protected async System.Threading.Tasks.Task CoursDropDownChange(System.Object args)
        {
            if (@args == null)
            {
                courseFees = await ceilappService.GetCourseFees(new Query { Expand = "Profession,Course" });
            }
            else{
                courseFees = await ceilappService.GetCourseFees(new Radzen.Query { Filter = "i => i.CourseId == @0", FilterParameters = new object[] { @SelectedCourse }, Expand = "Profession,Course" });
            }
            
        }
    }
}