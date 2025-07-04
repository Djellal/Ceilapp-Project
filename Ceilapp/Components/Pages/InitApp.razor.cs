﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages
{
    public partial class InitApp
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
        protected ceilappService ceilappdb { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var roles = await Security.GetRoles(); // Await the Task to get the actual IEnumerable<ApplicationRole>

            foreach (var item in Constants.Roles)
            {
                if (!roles.Any(role => role.Name == item)) // Now 'roles' is IEnumerable<ApplicationRole>, so 'Any' can be used
                {
                    await Security.CreateRole(new Models.ApplicationRole { Name = item });
                }
            }
            var adminmail = "djellal@univ-setif.dz";
            var admin = (await Security.GetUsers()).FirstOrDefault(u => u.Name == adminmail);

            if (admin == null)
            {
                admin = new Models.ApplicationUser
                {
                    Name = adminmail,
                    Email = adminmail,
                };
                admin.Password = "DhB@571982";
                admin.ConfirmPassword = "DhB@571982";
                admin.Roles = new List<Models.ApplicationRole>
                {
                    new Models.ApplicationRole { Name = Constants.ADMIN } // Assuming the first role is Admin
                };
                var result = await Security.CreateUser(admin);
            }

            if (!ceilappdb.dbContext.CourseTypes.Any())
            {
                await ceilappdb.CreateCourseType(new Models.ceilapp.CourseType { Name = "Language",NameAr="لغات" });
                await ceilappdb.CreateCourseType(new Models.ceilapp.CourseType { Name = "Atelier", NameAr = "ورشة" });
            }

            if (!ceilappdb.dbContext.Professions.Any())
            {
                await ceilappdb.CreateProfession(new Models.ceilapp.Profession { Name = "Etudiant", NameAr = "طالب", FeeValue = 4000 });
                await ceilappdb.CreateProfession(new Models.ceilapp.Profession { Name = "Enseignant", NameAr = "استاذ", FeeValue = 8000 });
                await ceilappdb.CreateProfession(new Models.ceilapp.Profession { Name = "Externe", NameAr = "خارجي", FeeValue = 6000 });
            }
           
        }
    }
}