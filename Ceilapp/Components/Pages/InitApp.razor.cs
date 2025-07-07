using System;
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

        public bool initfinished { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            initfinished = false;
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


            var Langid = ceilappdb.dbContext.CourseTypes.FirstOrDefault(ct => ct.Name == "Language").Id;

            var atlid = ceilappdb.dbContext.CourseTypes.FirstOrDefault(ct => ct.Name == "Atelier").Id;

            if (!ceilappdb.dbContext.Courses.Any())
            {
                var c =  await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code= "ENG",
                    Name = "Anglais",
                    NameAr = "اللغة الانجليزية",   
                    Image="",
                    CourseTypeId = Langid,
                    IsActive = true,
                });

                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel {CourseId =c.Id,  Name = "Pré-A1", NameAr = "Pré-A1" ,IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A1.1", NameAr = "A1.1" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A1.2", NameAr = "A1.2" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A2.2", NameAr = "A2.2" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B1.1", NameAr = "B1.1" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B1.2", NameAr = "B1.2" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B2.1", NameAr = "B2.1" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B2.2", NameAr = "B2.2" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "C1.1", NameAr = "C1.1" , IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "C1.2", NameAr = "C1.2" , IsActive = true });

                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "FRA",
                    Name = "Français",
                    NameAr = "اللغة الفرنسية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                });

                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "Pré-A1", NameAr = "Pré-A1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A1.1", NameAr = "A1.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A1.2", NameAr = "A1.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A2.2", NameAr = "A2.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B1.1", NameAr = "B1.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B1.2", NameAr = "B1.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B2.1", NameAr = "B2.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B2.2", NameAr = "B2.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "C1.1", NameAr = "C1.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "C1.2", NameAr = "C1.2", IsActive = true });

              c =  await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "ESP",
                    Name = "Espagnol",
                    NameAr = "اللغة الاسبانية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "Pré-A1", NameAr = "Pré-A1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A1.1", NameAr = "A1.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A1.2", NameAr = "A1.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "A2.2", NameAr = "A2.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B1.1", NameAr = "B1.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B1.2", NameAr = "B1.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B2.1", NameAr = "B2.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "B2.2", NameAr = "B2.2", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "C1.1", NameAr = "C1.1", IsActive = true });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "C1.2", NameAr = "C1.2", IsActive = true });
               
                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "RUS",
                    Name = "Russe",
                    NameAr = "اللغة الروسية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                });

                await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "TUR",
                    Name = "Turc",
                    NameAr = "اللغة التركية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true, 

                });

                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code= "ALM",
                    Name = "Allemand",
                    NameAr= "اللغة الالمانية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                });
                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "ARAB",
                    Name = "Arabe",
                    NameAr = "اللغة العربية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                });
                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "ITL",
                    Name = "Italien",
                    NameAr= "اللغة الايطالية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                });

                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "SPKENS",
                    Name = "Workshops Speaking For Teachers",
                    NameAr= "ورشة محادثة للمدرسين",
                    Image = "",
                    CourseTypeId = atlid,
                    IsActive = true,
                });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "-", NameAr = "-", IsActive = true });
                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "SPK",
                    Name = "WORKSHOP SPEAKING",
                    NameAr= "ورشة محادثة",
                    Image = "",
                    CourseTypeId = atlid,
                    IsActive = true,
                });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "-", NameAr = "-", IsActive = true });
                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "ACOF",
                    Name = "Atelier communication orale français",
                    NameAr= "ورشة التواصل الشفوي بالفرنسية",
                    Image = "",
                    CourseTypeId = atlid,
                    IsActive = true,
                });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "-", NameAr = "-", IsActive = true });
            }

            initfinished = true;
        }
    }
}