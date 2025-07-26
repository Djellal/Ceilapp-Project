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
using Microsoft.EntityFrameworkCore;


namespace Ceilapp.Components.Pages
{
    public partial class StudentDashboard
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

        [Inject]protected ceilappService ceilappService { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }
        
        public Session CurrentSession { get; private set; }

        private List<CourseRegistration> currentRegistrations;
        private List<CourseRegistration> previousRegistrations;
        private string studentId;
        public AppSetting AppSetting { get; private set; }

        // ...

        protected override async Task OnInitializedAsync()
        {
            AppSetting = await ceilappService.GetAppSettingById(1);
            if (AppSetting == null)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "AppSetting not found. Please contact the administrator.", Duration = 5000 });
                NavigationManager.NavigateTo("/");
                return;
            }

            CurrentSession = AppSetting.CurrentSessionId.HasValue ? await ceilappService.GetSessionById(AppSetting.CurrentSessionId.Value) : null;
            if (CurrentSession == null)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Current session not found. Please contact the administrator.", Duration = 5000 });
                NavigationManager.NavigateTo("/");
                return;
            }

            var studentId = Security.User?.Id;

            if (!string.IsNullOrEmpty(studentId))
            {
                // Fix CS8072: Remove null-propagating operator in expression tree
                // Fix CS1061: Use Microsoft.EntityFrameworkCore for ToListAsync
                currentRegistrations = await ceilappService.dbContext.CourseRegistrations.Include(r=>r.Course).Include(r=>r.CourseLevel)
                    .Where(r => r.UserId == studentId && r.SessionId == CurrentSession.Id)
                    .ToListAsync();

                previousRegistrations = await ceilappService.dbContext.CourseRegistrations.Include(r=>r.Course).Include(r=>r.CourseLevel)
                    .Where(r => r.UserId == studentId && r.SessionId != CurrentSession.Id)
                    .ToListAsync();
            }
        }



        protected async System.Threading.Tasks.Task Button1Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NavigationManager.NavigateTo($"/edit-course-registration/false");
        }
    }
}