using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Professions
{
    public partial class Professions
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

        protected IEnumerable<Ceilapp.Models.ceilapp.Profession> professions;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.Profession> grid0;

        [Inject]
        protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            professions = await ceilappService.GetProfessions();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new Ceilapp.Models.ceilapp.Profession());
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Profession profession)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteProfession(profession.Id);

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
                    Detail = $"Unable to delete Profession"
                });
            }
        }

        protected async Task GridRowUpdate(Ceilapp.Models.ceilapp.Profession args)
        {
            try
            {
                await ceilappService.UpdateProfession(args.Id, args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to update Profession"
                });
            }
        }

        protected async Task GridRowCreate(Ceilapp.Models.ceilapp.Profession args)
        {
            try
            {
                await ceilappService.CreateProfession(args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to create Profession"
                });
            }
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Profession data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Profession data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Profession data)
        {
            grid0.CancelEditRow(data);
            await ceilappService.CancelProfessionChanges(data);
        }
    }
}