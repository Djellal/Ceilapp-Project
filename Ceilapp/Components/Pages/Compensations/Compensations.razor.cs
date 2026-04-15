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
    public partial class Compensations
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

        protected IEnumerable<Ceilapp.Models.ceilapp.Compensation> compensations;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.Compensation> grid0;

        [Inject]
        protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            compensations = await ceilappService.GetCompensations(new Query { Expand = "CourseRegistration" });
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCompensation>("Ajouter une compensation", options: new DialogOptions { Resizable = false, Draggable = false });
            await grid0.Reload();
        }

        protected async Task EditRow(Ceilapp.Models.ceilapp.Compensation args)
        {
            await DialogService.OpenAsync<EditCompensation>("Modifier la compensation", new Dictionary<string, object> { {"Id", args.Id} }, new DialogOptions { Resizable = false, Draggable = false });
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Compensation compensation)
        {
            try
            {
                if (await DialogService.Confirm("Êtes-vous sûr de vouloir supprimer cet enregistrement ?") == true)
                {
                    var deleteResult = await ceilappService.DeleteCompensation(compensation.Id);

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
                    Summary = $"Erreur",
                    Detail = $"Impossible de supprimer la compensation"
                });
            }
        }
    }
}