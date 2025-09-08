using System.Net.Http;
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

namespace Ceilapp.Components.Pages
{
    public partial class Index
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
        protected SecurityService Security { get; set; }

        [Inject]
        protected Ceilapp.ceilappService ceilappService { get; set; }

        protected System.Linq.IQueryable<Ceilapp.Models.ceilapp.Course> courses;

         protected string redirectUrl;
        protected string error;
        protected string info;
        protected bool errorVisible;
        protected bool infoVisible;
        

        protected async System.Threading.Tasks.Task InscriptionClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NavigationManager.NavigateTo($"/register-application-user");

        }
        protected async System.Threading.Tasks.Task reinscriptionClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NavigationManager.NavigateTo($"/edit-course-registration/true/true");

        }

        protected async System.Threading.Tasks.Task LoginButtonClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NavigationManager.NavigateTo($"/login");
        }

        protected override async Task OnInitializedAsync()
        {
            courses = await ceilappService.GetCourses(new Radzen.Query { OrderBy = "Order asc", Expand = "CourseType" });

             var query = System.Web.HttpUtility.ParseQueryString(new Uri(NavigationManager.ToAbsoluteUri(NavigationManager.Uri).ToString()).Query);

            error = query.Get("error");

            info = query.Get("info");

            redirectUrl = query.Get("redirectUrl");

            errorVisible = !string.IsNullOrEmpty(error);

            infoVisible = !string.IsNullOrEmpty(info);
        }

        protected async System.Threading.Tasks.Task RegisterButtonClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NavigationManager.NavigateTo($"/register-application-user");
        }
        protected async Task ResetPassword()
        {
            var result = await DialogService.OpenAsync<ResetPassword>("Reset password");

            if (result == true)
            {
                infoVisible = true;

                info = "Password reset successfully. Please check your email for further instructions.";
            }
        }

        protected async System.Threading.Tasks.Task Button0Click(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            NavigationManager.NavigateTo($"/register-application-user");
        }

    protected async Task Register()
        {
            var result = await DialogService.OpenAsync<RegisterApplicationUser>("S'enregistrer");

            // if (result == true)
            // {
            //     infoVisible = true;

            //     info = "Registration accepted. Please check your email for further instructions.";
            // }
        }
    }
}