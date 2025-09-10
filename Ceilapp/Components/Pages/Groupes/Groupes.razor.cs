using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Groupes
{
    public partial class Groupes
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

        protected IEnumerable<Ceilapp.Models.ceilapp.Groupe> groupes;
protected IEnumerable<Ceilapp.Models.ceilapp.Course> courses;
protected IEnumerable<Ceilapp.Models.ceilapp.CourseLevel> courseLevels;

protected int? selectedCourseId;
protected int? selectedCourseLevelId;

protected RadzenDataGrid<Ceilapp.Models.ceilapp.Groupe> grid0;

[Inject]
protected SecurityService Security { get; set; }

protected override async Task OnInitializedAsync()
{
    await LoadCourses();
    await LoadCourseLevels();
    await LoadGroupes();
}

protected async Task LoadCourses()
{
    courses = await ceilappService.GetCourses();
}

protected async Task LoadCourseLevels()
{
    courseLevels = await ceilappService.GetCourseLevels(new Query { Expand = "Course" });
}

protected async Task LoadGroupes()
{
    var query = new Query { Expand = "Course,CourseLevel,Session" };
    
    if (selectedCourseId.HasValue)
    {
        query.Filter = $"i => i.CourseId == {selectedCourseId.Value}";
    }
    
    if (selectedCourseLevelId.HasValue)
    {
        if (!string.IsNullOrEmpty(query.Filter))
        {
            query.Filter += $" && i.CourseLevelId == {selectedCourseLevelId.Value}";
        }
        else
        {
            query.Filter = $"i => i.CourseLevelId == {selectedCourseLevelId.Value}";
        }
    }

    groupes = await ceilappService.GetGroupes(query);
}

protected async Task OnCourseFilterChange(object value)
{
   try
   {
     courseLevels = await ceilappService.GetCourseLevels(new Radzen.Query { Filter = "i => i.CourseId == @0", FilterParameters = new object[] { value }, OrderBy = "LevelOrder asc" });
     selectedCourseId = value as int?;
     await LoadGroupes();
     await grid0.Reload();
   }
   catch (System.Exception ex)
   {
    NotificationService.Notify(new NotificationMessage
    {
        Severity = NotificationSeverity.Error,
        Summary = $"Error",
        Detail = $"Unable to load course levels: {ex.Message}"
    });   
   }
}

protected async Task OnCourseLevelFilterChange(object value)
{
    selectedCourseLevelId = value as int?;
    await LoadGroupes();
    await grid0.Reload();
}

protected async Task ClearFilters()
{
    selectedCourseId = null;
    selectedCourseLevelId = null;
    await LoadGroupes();
    await grid0.Reload();
}

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddGroupe>("Add Groupe", null);
            await grid0.Reload();
        }

        protected async Task EditRow(Ceilapp.Models.ceilapp.Groupe args)
        {
            await DialogService.OpenAsync<EditGroupe>("Edit Groupe", new Dictionary<string, object> { {"Id", args.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Groupe groupe)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteGroupe(groupe.Id);

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
                    Detail = $"Unable to delete Groupe"
                });
            }
        }
    }
}