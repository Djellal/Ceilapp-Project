using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Compensations
{
    public partial class AddCompensation
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

        protected override async Task OnInitializedAsync()
        {
            compensation = new Ceilapp.Models.ceilapp.Compensation();

            var courseRegistrations = (await ceilappService.GetCourseRegistrations()).ToList();

            registrationDisplayItems = courseRegistrations.Select(r => new RegistrationDisplayItem
            {
                Id = r.Id,
                DisplayText = $"{r.LastName} {r.FirstName} - {r.Course?.Name} ({r.InscriptionCode})"
            }).ToList();
        }
        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.Compensation compensation;

        protected List<RegistrationDisplayItem> registrationDisplayItems;

        public class RegistrationDisplayItem
        {
            public int Id { get; set; }
            public string DisplayText { get; set; }
        }

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.CreateCompensation(compensation);
                DialogService.Close(compensation);
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
    }
}
