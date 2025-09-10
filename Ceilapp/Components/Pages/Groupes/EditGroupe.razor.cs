using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Ceilapp.Models.ceilapp;

namespace Ceilapp.Components.Pages.Groupes
{
    public partial class EditGroupe
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

        public AppSetting AppSetting { get; private set; }

        protected IEnumerable<Ceilapp.Models.ApplicationUser> Teachers;

        protected override async Task OnInitializedAsync()
        {
            groupe = await ceilappService.GetGroupeById(Id);

            coursesForCourseId = await ceilappService.GetCourses();

            courseLevelsForCourseLevelId = await ceilappService.GetCourseLevels();

            sessionsForCurrentSessionId = await ceilappService.GetSessions();
            
            Teachers = await Security.GetUsers(Constants.TEACHER);

             AppSetting = await ceilappService.GetAppSettingById(1);
          


        }
        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.Groupe groupe;

        protected IEnumerable<Ceilapp.Models.ceilapp.Course> coursesForCourseId;

        protected IEnumerable<Ceilapp.Models.ceilapp.CourseLevel> courseLevelsForCourseLevelId;

        protected IEnumerable<Ceilapp.Models.ceilapp.Session> sessionsForCurrentSessionId;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.UpdateGroupe(Id, groupe);
                DialogService.Close(groupe);
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

        protected async System.Threading.Tasks.Task CourseIdChange(System.Object args)
        {
           courseLevelsForCourseLevelId = await ceilappService.GetCourseLevels(new Radzen.Query { Filter = "i => i.CourseId == @0", FilterParameters = new object[] { args }, OrderBy = "LevelOrder asc" });
        }
    }
}