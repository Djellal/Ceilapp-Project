using Ceilapp.Components.Pages.Appsettings;
using Ceilapp.Components.Pages.CourseRegistrations;
using Ceilapp.Models.ceilapp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using System.Net.Http;

namespace Ceilapp.Components.Layout
{
    public partial class MainLayout
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

        private bool sidebarExpanded = true;

        [Inject]
        protected SecurityService Security { get; set; }

        [Inject]
        protected ceilappService ceilappdb { get; set; }
        public AppSetting AppSettings { get; private set; }
       
protected override async Task OnInitializedAsync()
        {
            try
            {
                AppSettings = await ceilappdb.GetAppSettingById(1);               
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = ex.Message });
            }         
           
        }
        public string Logout = "Logout";

        void SidebarToggleClick()
        {
            sidebarExpanded = !sidebarExpanded;
        }

        protected void ProfileMenuClick(RadzenProfileMenuItem args)
        {
            if (args.Value == "Logout")
            {
                Security.Logout();
            }
        }

       protected async Task NewCourseRegistration()
        {
            // await DialogService.OpenAsync<EditCourseRegistration>("Edit CourseRegistration", new Dictionary<string, object> { { "mode", "create" } });
            NavigationManager.NavigateTo($"/new-course-registration/create", true);
        }

        protected async System.Threading.Tasks.Task PanelMenu0Click(Radzen.MenuItemEventArgs args)
        {
           if (args.Value == "Logout")
            {
                Security.Logout();
            }
        }

        protected async System.Threading.Tasks.Task OpenSocialLink(string link)
        {
            await JSRuntime.InvokeVoidAsync("open", link);
        }

        protected async System.Threading.Tasks.Task Image0Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NavigationManager.NavigateTo($"/");
        }
    }
}
