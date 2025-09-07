using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages
{
    public partial class ApplicationUsers
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

        protected IEnumerable<Ceilapp.Models.ApplicationUser> users;
        protected RadzenDataGrid<Ceilapp.Models.ApplicationUser> grid0;
        protected string error;
        protected bool errorVisible;

        [Inject]
        protected SecurityService Security { get; set; }

        protected IEnumerable<Ceilapp.Models.ApplicationRole> roles;
        protected string selectedRole;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
            roles = await Security.GetRoles();
        }

        protected async Task LoadData()
        {
            users = await Security.GetUsers(selectedRole);
        }

        protected async Task OnRoleFilterChange(string role)
        {
            selectedRole = role;
            await LoadData();
        }

        // Update all methods that refresh user data to use LoadData()
        protected async Task AddClick()
        {
            await DialogService.OpenAsync<AddApplicationUser>("Add Application User");
            await LoadData();
        }

        protected async Task RowSelect(Ceilapp.Models.ApplicationUser user)
        {
            await DialogService.OpenAsync<EditApplicationUser>("Edit Application User", new Dictionary<string, object>{ {"Id", user.Id} });
            await LoadData();
        }

        protected async Task DeleteClick(Ceilapp.Models.ApplicationUser user)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this user?") == true)
                {
                    await Security.DeleteUser($"{user.Id}");
                    await LoadData();
                }
            }
            catch (Exception ex)
            {
                errorVisible = true;
                error = ex.Message;
            }
        }
    }
}