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
    public partial class States
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

        protected IEnumerable<Ceilapp.Models.ceilapp.State> states;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.State> grid0;
        protected override async Task OnInitializedAsync()
        {
            states = await ceilappService.GetStates();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddState>("Add State", null);
            await grid0.Reload();
        }

        protected async Task EditRow(DataGridRowMouseEventArgs<Ceilapp.Models.ceilapp.State> args)
        {
            await DialogService.OpenAsync<EditState>("Edit State", new Dictionary<string, object> { {"Id", args.Data.Id} });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.State state)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteState(state.Id);

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
                    Detail = $"Unable to delete State"
                });
            }
        }

        protected Ceilapp.Models.ceilapp.State stateChild;
        protected async Task GetChildData(Ceilapp.Models.ceilapp.State args)
        {
            stateChild = args;
            var MunicipalitiesResult = await ceilappService.GetMunicipalities(new Query { Filter = $@"i => i.StateId == ""{args.Id}""", Expand = "State" });
            if (MunicipalitiesResult != null)
            {
                args.Municipalities = MunicipalitiesResult.ToList();
            }
        }
        protected Ceilapp.Models.ceilapp.Municipality municipalityMunicipalities;

        protected IEnumerable<Ceilapp.Models.ceilapp.State> statesForStateIdMunicipalities;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.Municipality> MunicipalitiesDataGrid;

        protected async Task MunicipalitiesAddButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.State data)
        {

            var dialogResult = await DialogService.OpenAsync<AddMunicipality>("Add Municipalities", new Dictionary<string, object> { {"StateId" , data.Id} });
            await GetChildData(data);
            await MunicipalitiesDataGrid.Reload();

        }

        protected async Task MunicipalitiesRowSelect(Ceilapp.Models.ceilapp.Municipality args, Ceilapp.Models.ceilapp.State data)
        {
            var dialogResult = await DialogService.OpenAsync<EditMunicipality>("Edit Municipalities", new Dictionary<string, object> { {"Id", args.Id} });
            await GetChildData(data);
            await MunicipalitiesDataGrid.Reload();
        }

        protected async Task MunicipalitiesDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Municipality municipality)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteMunicipality(municipality.Id);

                    await GetChildData(stateChild);

                    if (deleteResult != null)
                    {
                        await MunicipalitiesDataGrid.Reload();
                    }
                }
            }
            catch (System.Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete Municipality"
                });
            }
        }

        string lastFilter;

        [Inject]
        protected SecurityService Security { get; set; }
        protected async void Grid0Render(DataGridRenderEventArgs<Ceilapp.Models.ceilapp.State> args)
        {
            if (grid0.Query.Filter != lastFilter)
            {
                stateChild = grid0.View.FirstOrDefault();
            }

            if (grid0.Query.Filter != lastFilter && stateChild != null)
            {
                await grid0.SelectRow(stateChild);
            }

            lastFilter = grid0.Query.Filter;
        }
    }
}