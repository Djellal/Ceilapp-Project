using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using Ceilapp.Models.ceilapp;

namespace Ceilapp.Components.Pages.CourseRegistrations
{
    public partial class ChooseGroup
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
        protected SecurityService Security { get; set; }

        [Inject]
        public ceilappService ceilappService { get; set; }

        protected Ceilapp.Models.ceilapp.CourseRegistration courseRegistration { get; set; }
        protected IEnumerable<Ceilapp.Models.ceilapp.Groupe> availableGroups { get; set; }
        protected int? selectedGroupId { get; set; }

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            courseRegistration = await ceilappService.GetCourseRegistrationById(Id);
            if (courseRegistration == null)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Inscription introuvable.", Duration = 5000 });
                NavigationManager.NavigateTo("/student-dashboard");
                return;
            }

            // Load groups filtered by the course and course level of the registration
            await LoadAvailableGroups();
        }

        protected async Task LoadAvailableGroups()
        {
            try
            {
                // Create query to filter groups by course and course level
                var query = new Query 
                { 
                    Filter = $"g=>g.CourseId == {courseRegistration.CourseId} and g=>g.CourseLevelId   == {courseRegistration.CourseLevelId}",
                    Expand = "Course,CourseLevel,Session"
                };

                availableGroups = await ceilappService.GetGroupes(query);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage 
                { 
                    Severity = NotificationSeverity.Error, 
                    Summary = "Error", 
                    Detail = "Erreur lors du chargement des groupes.", 
                    Duration = 5000 
                });
                Console.WriteLine($"Error loading groups: {ex.Message}");
            }
        }

        protected async Task AssignGroup()
        {
            if (!selectedGroupId.HasValue)
            {
                NotificationService.Notify(new NotificationMessage 
                { 
                    Severity = NotificationSeverity.Warning, 
                    Summary = "Attention", 
                    Detail = "Veuillez sélectionner un groupe.", 
                    Duration = 5000 
                });
                return;
            }

            try
            {
                // Update the course registration with the selected group
                courseRegistration.GroupId = selectedGroupId.Value;
                await ceilappService.UpdateCourseRegistration(courseRegistration.Id, courseRegistration);

                NotificationService.Notify(new NotificationMessage 
                { 
                    Severity = NotificationSeverity.Success, 
                    Summary = "Succès", 
                    Detail = "Groupe assigné avec succès.", 
                    Duration = 5000 
                });

                NavigationManager.NavigateTo("/student-dashboard");
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage 
                { 
                    Severity = NotificationSeverity.Error, 
                    Summary = "Error", 
                    Detail = "Erreur lors de l'assignation du groupe.", 
                    Duration = 5000 
                });
                Console.WriteLine($"Error assigning group: {ex.Message}");
            }
        }

        protected void Cancel()
        {
            NavigationManager.NavigateTo("/student-dashboard");
        }
    }
}