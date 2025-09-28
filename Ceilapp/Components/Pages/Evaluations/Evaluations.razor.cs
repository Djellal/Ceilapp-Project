using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Evaluations
{
    public partial class Evaluations
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

        protected IEnumerable<Ceilapp.Models.ceilapp.Evaluation> evaluations;

        protected RadzenDataGrid<Ceilapp.Models.ceilapp.Evaluation> grid0;

            protected IEnumerable<Ceilapp.Models.ceilapp.CourseRegistration> courseRegistrationsForCourseRegistrationId;

            protected IEnumerable<Ceilapp.Models.ceilapp.CourseComponent> courseComponentsForCourseComponentId;

            [Inject]
            protected SecurityService Security { get; set; }
        protected override async Task OnInitializedAsync()
        {
            evaluations = await ceilappService.GetEvaluations(new Query { Expand = "CourseRegistration,CourseComponent" });

            courseRegistrationsForCourseRegistrationId = await ceilappService.GetCourseRegistrations();

            courseComponentsForCourseComponentId = await ceilappService.GetCourseComponents();
        }

        protected async Task AddButtonClick(MouseEventArgs args)
        {
            await grid0.InsertRow(new Ceilapp.Models.ceilapp.Evaluation());
        }

        protected async Task GridDeleteButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Evaluation evaluation)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var deleteResult = await ceilappService.DeleteEvaluation(evaluation.Id);

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
                    Detail = $"Unable to delete Evaluation"
                });
            }
        }

        protected async Task GridRowUpdate(Ceilapp.Models.ceilapp.Evaluation args)
        {
            try
            {
                await ceilappService.UpdateEvaluation(args.Id, args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to update Evaluation"
                });
            }
        }

        protected async Task GridRowCreate(Ceilapp.Models.ceilapp.Evaluation args)
        {
            try
            {
                await ceilappService.CreateEvaluation(args);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage
                {
                      Severity = NotificationSeverity.Error,
                      Summary = $"Error",
                      Detail = $"Unable to create Evaluation"
                });
            }
            await grid0.Reload();
        }

        protected async Task EditButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Evaluation data)
        {
            await grid0.EditRow(data);
        }

        protected async Task SaveButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Evaluation data)
        {
            await grid0.UpdateRow(data);
        }

        protected async Task CancelButtonClick(MouseEventArgs args, Ceilapp.Models.ceilapp.Evaluation data)
        {
            grid0.CancelEditRow(data);
            await ceilappService.CancelEvaluationChanges(data);
        }
    }
}