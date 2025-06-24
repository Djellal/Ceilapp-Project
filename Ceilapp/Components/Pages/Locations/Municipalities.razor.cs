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
    public partial class Municipalities
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

        protected IEnumerable<Ceilapp.Models.ceilapp.Municipality> municipalities;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.Municipality> grid0;
        protected override async Task OnInitializedAsync()
        {
            municipalities = await ceilappService.GetMunicipalities(new Query { Expand = "State" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddMunicipality>("Add Municipality", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<Ceilapp.Models.ceilapp.Municipality> args)
        {
            await DialogService.OpenAsync<EditMunicipality>("Edit Municipality", new Dictionary<string, object> { {"Id", args.Data.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Municipality municipality)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteMunicipality(municipality.Id);

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
                    Detail = $"Unable to delete Municipality"
                });
            }
        }

        protected Ceilapp.Models.ceilapp.Municipality municipalityChild;
        protected async Task GetChildData(Ceilapp.Models.ceilapp.Municipality args)
        {
            municipalityChild = args;
        }

        string lastFilter;

        [Inject]
        protected SecurityService Security { get; set; }
        protected async void Grid0Render(DataGridRenderEventArgs<Ceilapp.Models.ceilapp.Municipality> args)
        {
            if (grid0.Query.Filter != lastFilter)
            {
                municipalityChild = grid0.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter && municipalityChild != null)
            {
                await grid0.SelectRow(municipalityChild);
            }

            lastFilter = grid0.Query.Filter;
        }
    }
}