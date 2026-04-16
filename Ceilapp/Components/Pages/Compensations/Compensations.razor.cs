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
        protected IEnumerable<Ceilapp.Models.ceilapp.Compensation> filteredCompensations;
        protected string searchText;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.Compensation> grid0;

        [Inject]
        protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            compensations = await ceilappService.GetCompensations(new Query { Expand = "CourseRegistration,CourseRegistration.Course" });
            filteredCompensations = compensations;
        }

        protected void OnSearch(string value)
        {
            searchText = value;
            if (string.IsNullOrWhiteSpace(searchText))
            {
                filteredCompensations = compensations;
            }
            else
            {
                var term = searchText.ToLower();
                filteredCompensations = compensations.Where(c =>
                    (c.CourseRegistration?.LastName?.ToLower().Contains(term) == true) ||
                    (c.CourseRegistration?.FirstName?.ToLower().Contains(term) == true) ||
                    (c.CourseRegistration?.InscriptionCode?.ToLower().Contains(term) == true) ||
                    (c.CourseRegistration?.Course?.Name?.ToLower().Contains(term) == true) ||
                    (c.MakeupTeacherId?.ToLower().Contains(term) == true));
            }
        }

        protected void ClearSearch(MouseEventArgs args)
        {
            searchText = "";
            filteredCompensations = compensations;
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await DialogService.OpenAsync<AddCompensation>("Ajouter une séance de rattrapage", options: new DialogOptions { Resizable = false, Draggable = false });
            compensations = await ceilappService.GetCompensations(new Query { Expand = "CourseRegistration,CourseRegistration.Course" });
            OnSearch(searchText);
        }

        protected async Task EditRow(Ceilapp.Models.ceilapp.Compensation args)
        {
            await DialogService.OpenAsync<EditCompensation>("Modifier la séance de rattrapage", new Dictionary<string, object> { {"Id", args.Id} }, new DialogOptions { Resizable = false, Draggable = false });
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