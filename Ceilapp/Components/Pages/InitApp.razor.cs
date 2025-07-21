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

            var session = await ceilappdb.GetSessionById(1);
            if(session == null){
                session = new Ceilapp.Models.ceilapp.Session
                {
                    Id = 1,
                    SessionCode = "SE001",
                    SessionName = "Septembre 2025",
                    SessionNameAr = "سبتمبر 2025",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddMonths(2),

                };
                await ceilappdb.CreateSession(session);
            }




            var appSetting = await ceilappdb.GetAppSettingById(1);
            if (appSetting == null)
            {
                appSetting = new Ceilapp.Models.ceilapp.AppSetting
                {
                    Id = 1,
                    OrganizationName = "CEIL UFAS1",
                    Address = "Université Sétif -1- Campus El Bez, Ex-Faculté de Droit (Actuellement Département d'Agronomie)",
                    Tel = " (+213) 036.62.09.96",
                    Email = " ceil@univ-setif.dz",
                    WebSite = " https://ceil.univ-setif.dz",
                    Fb = "https://www.facebook.com/CEIL.SETIF1UNIVERSITY",
                    LinkedIn = "https://www.linkedin.com/school/universite-ferhat-abbas-setif",
                    Youtube = "https://www.youtube.com/channel/UCjU0ehPWCFlvCHrfgUt3DOQ",
                    Instagram = "https://www.instagram.com/universite_ferhat_abbas_setif/?hl=fr",
                    X = "https://x.com/UnivFerhatAbbas",
                    Logo = "https://example.com/logo.png",
                   
                    IsRegistrationOpened = false,
                    MaxRegistrationPerSession = 2,
                    CurrentSessionId = 1,
                    TermsAndConditions = @"<table style=""width: 100%; border-collapse: collapse; font-size: 14px; font-family: Arial, Tahoma, sans-serif;"">
    <thead>
        <tr>
            <th style=""background-color: #34495e; color: white; padding: 15px; text-align: right; direction: rtl; font-weight: bold; font-size: 16px; width: 50%; border: 1px solid #2c3e50;"">العربية</th>
            <th style=""background-color: #34495e; color: white; padding: 15px; text-align: left; direction: ltr; font-weight: bold; font-size: 16px; width: 50%; border: 1px solid #2c3e50;"">Français</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                يجب الالتزام بالقانون الداخلي لجامعة فرحات عباس وأي إخلال به يؤدي إلى الإقصاء المباشر من المركز.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Le respect du règlement intérieur de l'université Ferhat Abbas est obligatoire ; toute infraction entraîne l'exclusion immédiate du centre.
            </td>
        </tr>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                لا يمكن بأي حال من الأحوال التحويل بين الأفواج بعد انتهاء الآجال المخصصة لذلك.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Aucun transfert entre groupes n'est autorisé après l'expiration des délais prévus à cet effet.
            </td>
        </tr>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                لا يمكن استرداد مبلغ التسجيل لأي سبب من الأسباب كما لا يمكن الرجاء التسجيل في دورة أخرى.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Aucun remboursement des frais d'inscription n'est possible pour quelque raison que ce soit ; aucune nouvelle inscription à une autre session n'est acceptée.
            </td>
        </tr>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                تؤدي الغيابات المتكررة إلى الإقصاء المباشر دون أي تعويض.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Les absences répétées entraînent l'exclusion immédiate sans compensation.
            </td>
        </tr>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                يتم الاعتماد عن المعدل لتحديد المستوى إلى تصنيف المعني في المستوى الأدنى ولا يعاد الامتحان إلا إذا تم تبرير معقول.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Le classement de l'étudiant dans un niveau inférieur est basé sur la moyenne, et aucun examen de rattrapage ne sera organisé sauf en cas de justification valable.
            </td>
        </tr>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                يكون اختيار الأفواج حسب الحصص المتوفرة والتي تكون موزعة.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Le choix des groupes se fait selon les places disponibles et leur répartition.
            </td>
        </tr>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                لا يمكن تغيير الفوج عند نفس الأستاذ ونفس المستوى فقط.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Il est interdit de changer de groupe avec le même enseignant et au même niveau.
            </td>
        </tr>
        <tr>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: right; direction: rtl; background-color: #fafbfc;"">
                يتم التكوين بشهادة مستوى للطالب الناجح وشهادة مشاركة للطالب الراسب.
            </td>
            <td style=""padding: 15px; vertical-align: top; border: 1px solid #ecf0f1; text-align: left; direction: ltr; background-color: #ffffff;"">
                Une attestation de niveau est délivrée à l'étudiant ayant réussi, et une attestation de participation à l'étudiant ajourné.
            </td>
        </tr>
    </tbody>
</table>",

                };
                await ceilappdb.CreateAppSetting(appSetting);
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