using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Locations
{
    public partial class AddMunicipality
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

            statesForStateId = await ceilappService.GetStates();
        }
        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.Municipality municipality;

        protected IEnumerable<Ceilapp.Models.ceilapp.State> statesForStateId;

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.CreateMunicipality(municipality);
                DialogService.Close(municipality);
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





        bool hasStateIdValue;

        [Parameter]
        public string StateId { get; set; }

        [Inject]
        protected SecurityService Security { get; set; }
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            municipality = new Ceilapp.Models.ceilapp.Municipality();

            hasStateIdValue = parameters.TryGetValue<string>("StateId", out var hasStateIdResult);

            if (hasStateIdValue)
            {
                municipality.StateId = hasStateIdResult;
            }
            await base.SetParametersAsync(parameters);
        }
    }
}