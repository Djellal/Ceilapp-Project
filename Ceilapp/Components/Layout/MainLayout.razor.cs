using Ceilapp.Components.Pages.CourseRegistrations;
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

        private bool sidebarExpanded ;

        [Inject]
        protected SecurityService Security { get; set; }
        public bool RolesNotExists { get; private set; } = false;

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

        protected override async Task OnInitializedAsync()
        {
            var roles = await Security.GetRoles(); // Await the Task to get the actual IEnumerable<ApplicationRole>

            RolesNotExists = !roles.Any();
           
        }

        protected async Task NewCourseRegistration()
        {
            // await DialogService.OpenAsync<EditCourseRegistration>("Edit CourseRegistration", new Dictionary<string, object> { { "mode", "create" } });
            NavigationManager.NavigateTo($"/new-course-registration/create", true);
        }
    }
}
