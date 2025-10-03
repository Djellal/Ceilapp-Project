using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Microsoft.EntityFrameworkCore;

namespace Ceilapp.Components.Pages.Locations
{
    public partial class AddState2
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
            state = new Ceilapp.Models.ceilapp.State();
            state.Id = await GenerateNewStateId();
            state.NameAr = "";
        }

        private async Task<string> GenerateNewStateId()
        {
            var states = await ceilappService.GetStates();
            int nbr = states.Count()+100;

            while (states.Any(s => s.Id == nbr.ToString()))
            {
                nbr++;
            }

            return nbr.ToString();

        }

        protected bool errorVisible;
        protected Ceilapp.Models.ceilapp.State state;

        [Inject]
        protected SecurityService Security { get; set; }

        protected async Task FormSubmit()
        {
            try
            {
                await ceilappService.CreateState(state);
                DialogService.Close(state);
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