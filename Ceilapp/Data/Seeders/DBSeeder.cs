using Ceilapp.Models.ceilapp;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ceilapp.Data.Seeders
{
    public static class DBSeeder
    {
        public static async Task SeedAppData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var ceilappdb = scope.ServiceProvider.GetRequiredService<ceilappService>();

            var session = await ceilappdb.GetSessionById(1);
            if (session == null)
            {
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


            if (!ceilappdb.dbContext.CourseTypes.Any())
            {
                await ceilappdb.CreateCourseType(new Models.ceilapp.CourseType { Name = "Language", NameAr = "لغات" });
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
                var c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "ENG",
                    Name = "Anglais",
                    NameAr = "اللغة الانجليزية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                    Order = 1
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
                    Code = "FRA",
                    Name = "Français",
                    NameAr = "اللغة الفرنسية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                    Order = 2
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
                    Code = "ESP",
                    Name = "Espagnol",
                    NameAr = "اللغة الاسبانية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                    Order = 3
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
                    Order = 6
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


                await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "TUR",
                    Name = "Turc",
                    NameAr = "اللغة التركية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                    Order = 5

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
                    Code = "ALM",
                    Name = "Allemand",
                    NameAr = "اللغة الالمانية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                    Order = 4
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
                    Code = "ARAB",
                    Name = "Arabe",
                    NameAr = "اللغة العربية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                    Order = 7
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
                    Code = "ITL",
                    Name = "Italien",
                    NameAr = "اللغة الايطالية",
                    Image = "",
                    CourseTypeId = Langid,
                    IsActive = true,
                    Order = 8
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
                    Code = "SPKENS",
                    Name = "Workshops Speaking For Teachers",
                    NameAr = "ورشة محادثة للمدرسين",
                    Image = "",
                    CourseTypeId = atlid,
                    IsActive = true,
                    Order = 9
                });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "-", NameAr = "-", IsActive = true });
                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "SPK",
                    Name = "WORKSHOP SPEAKING",
                    NameAr = "ورشة محادثة",
                    Image = "",
                    CourseTypeId = atlid,
                    IsActive = true,
                    Order = 10
                });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "-", NameAr = "-", IsActive = true });
                c = await ceilappdb.CreateCourse(new Models.ceilapp.Course
                {
                    Code = "ACOF",
                    Name = "Atelier communication orale français",
                    NameAr = "ورشة التواصل الشفوي بالفرنسية",
                    Image = "",
                    CourseTypeId = atlid,
                    IsActive = true,
                    Order = 11
                });
                await ceilappdb.CreateCourseLevel(new Models.ceilapp.CourseLevel { CourseId = c.Id, Name = "-", NameAr = "-", IsActive = true });
            }
        }
        public static void SeedAlgerianStates(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ceilappContext>();

            // Check if states already exist
            if (!context.States.Any())
            {
                var states = new List<State>
                {
                    new State { Id = "01", Name = "Adrar", NameAr = "أدرار" },
                    new State { Id = "02", Name = "Chlef", NameAr = "الشلف" },
                    new State { Id = "03", Name = "Laghouat", NameAr = "الأغواط" },
                    new State { Id = "04", Name = "Oum El Bouaghi", NameAr = "أم البواقي" },
                    new State { Id = "05", Name = "Batna", NameAr = "باتنة" },
                    new State { Id = "06", Name = "Béjaïa", NameAr = "بجاية" },
                    new State { Id = "07", Name = "Biskra", NameAr = "بسكرة" },
                    new State { Id = "08", Name = "Béchar", NameAr = "بشار" },
                    new State { Id = "09", Name = "Blida", NameAr = "البليدة" },
                    new State { Id = "10", Name = "Bouira", NameAr = "البويرة" },
                    new State { Id = "11", Name = "Tamanrasset", NameAr = "تمنراست" },
                    new State { Id = "12", Name = "Tébessa", NameAr = "تبسة" },
                    new State { Id = "13", Name = "Tlemcen", NameAr = "تلمسان" },
                    new State { Id = "14", Name = "Tiaret", NameAr = "تيارت" },
                    new State { Id = "15", Name = "Tizi Ouzou", NameAr = "تيزي وزو" },
                    new State { Id = "16", Name = "Alger", NameAr = "الجزائر" },
                    new State { Id = "17", Name = "Djelfa", NameAr = "الجلفة" },
                    new State { Id = "18", Name = "Jijel", NameAr = "جيجل" },
                    new State { Id = "19", Name = "Sétif", NameAr = "سطيف" },
                    new State { Id = "20", Name = "Saïda", NameAr = "سعيدة" },
                    new State { Id = "21", Name = "Skikda", NameAr = "سكيكدة" },
                    new State { Id = "22", Name = "Sidi Bel Abbès", NameAr = "سيدي بلعباس" },
                    new State { Id = "23", Name = "Annaba", NameAr = "عنابة" },
                    new State { Id = "24", Name = "Guelma", NameAr = "قالمة" },
                    new State { Id = "25", Name = "Constantine", NameAr = "قسنطينة" },
                    new State { Id = "26", Name = "Médéa", NameAr = "المدية" },
                    new State { Id = "27", Name = "Mostaganem", NameAr = "مستغانم" },
                    new State { Id = "28", Name = "M'Sila", NameAr = "المسيلة" },
                    new State { Id = "29", Name = "Mascara", NameAr = "معسكر" },
                    new State { Id = "30", Name = "Ouargla", NameAr = "ورقلة" },
                    new State { Id = "31", Name = "Oran", NameAr = "وهران" },
                    new State { Id = "32", Name = "El Bayadh", NameAr = "البيض" },
                    new State { Id = "33", Name = "Illizi", NameAr = "إليزي" },
                    new State { Id = "34", Name = "Bordj Bou Arréridj", NameAr = "برج بوعريريج" },
                    new State { Id = "35", Name = "Boumerdès", NameAr = "بومرداس" },
                    new State { Id = "36", Name = "El Tarf", NameAr = "الطارف" },
                    new State { Id = "37", Name = "Tindouf", NameAr = "تندوف" },
                    new State { Id = "38", Name = "Tissemsilt", NameAr = "تيسمسيلت" },
                    new State { Id = "39", Name = "El Oued", NameAr = "الوادي" },
                    new State { Id = "40", Name = "Khenchela", NameAr = "خنشلة" },
                    new State { Id = "41", Name = "Souk Ahras", NameAr = "سوق أهراس" },
                    new State { Id = "42", Name = "Tipaza", NameAr = "تيبازة" },
                    new State { Id = "43", Name = "Mila", NameAr = "ميلة" },
                    new State { Id = "44", Name = "Aïn Defla", NameAr = "عين الدفلى" },
                    new State { Id = "45", Name = "Naâma", NameAr = "النعامة" },
                    new State { Id = "46", Name = "Aïn Témouchent", NameAr = "عين تموشنت" },
                    new State { Id = "47", Name = "Ghardaïa", NameAr = "غرداية" },
                    new State { Id = "48", Name = "Relizane", NameAr = "غليزان" },
                    new State { Id = "49", Name = "Timimoun", NameAr = "تيميمون" },
                    new State { Id = "50", Name = "Bordj Badji Mokhtar", NameAr = "برج باجي مختار" },
                    new State { Id = "51", Name = "Ouled Djellal", NameAr = "أولاد جلال" },
                    new State { Id = "52", Name = "Béni Abbès", NameAr = "بني عباس" },
                    new State { Id = "53", Name = "In Salah", NameAr = "عين صالح" },
                    new State { Id = "54", Name = "In Guezzam", NameAr = "عين قزام" },
                    new State { Id = "55", Name = "Touggourt", NameAr = "تقرت" },
                    new State { Id = "56", Name = "Djanet", NameAr = "جانت" },
                    new State { Id = "57", Name = "El M'Ghair", NameAr = "المغير" },
                    new State { Id = "58", Name = "El Meniaa", NameAr = "المنيعة" }
                };

                context.States.AddRange(states);
                context.SaveChanges();

                Console.WriteLine("Algerian states seeded successfully.");
            }
            else
            {
                Console.WriteLine("Algerian states already exist in the database.");
            }
        }

        public static void SeedAlgerianMunicipalities(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ceilappContext>();

            // Check if municipalities already exist
            if (!context.Municipalities.Any())
            {
                // Make sure states exist first
                if (!context.States.Any())
                {
                    Console.WriteLine("Cannot seed municipalities: States don't exist yet.");
                    return;
                }

                foreach (var state in context.States)
                {
                    context.Municipalities.Add(new Municipality
                    {
                        Name = "-",
                        NameAr = "-",
                        StateId = state.Id
                    });
                }
                context.SaveChanges();

                var municipalities = new List<Municipality>
                {
                    // Adrar (01)
                    new Municipality { Name = "Adrar", NameAr = "أدرار", StateId = "01" },
                    new Municipality { Name = "Tamest", NameAr = "تامست", StateId = "01" },
                    new Municipality { Name = "Reggane", NameAr = "رقان", StateId = "01" },
                    new Municipality { Name = "In Zghmir", NameAr = "إن زغمير", StateId = "01" },
                    new Municipality { Name = "Tit", NameAr = "تيت", StateId = "01" },
                    new Municipality { Name = "Tsabit", NameAr = "تسابيت", StateId = "01" },
                    new Municipality { Name = "Zaouiet Kounta", NameAr = "زاوية كنتة", StateId = "01" },
                    new Municipality { Name = "Aoulef", NameAr = "أولف", StateId = "01" },
                    new Municipality { Name = "Tamekten", NameAr = "تامقتن", StateId = "01" },
                    new Municipality { Name = "Tamantit", NameAr = "تمنطيط", StateId = "01" },
                    new Municipality { Name = "Fenoughil", NameAr = "فنوغيل", StateId = "01" },
                    new Municipality { Name = "Sali", NameAr = "سالي", StateId = "01" },
                    new Municipality { Name = "Akabli", NameAr = "اقبلي", StateId = "01" },
                    new Municipality { Name = "Sebaa", NameAr = "السبع", StateId = "01" },
                    new Municipality { Name = "Ouled Ahmed Timmi", NameAr = "أولاد أحمد تيمي", StateId = "01" },
                    new Municipality { Name = "Bouda", NameAr = "بودة", StateId = "01" },
                    new Municipality { Name = "Aougrout", NameAr = "أوقروت", StateId = "01" },
                    new Municipality { Name = "Deldoul", NameAr = "دلدول", StateId = "01" },
                    new Municipality { Name = "Metarfa", NameAr = "المطارفة", StateId = "01" },
                    new Municipality { Name = "Ouled Aissa", NameAr = "أولاد عيسى", StateId = "01" },
                    new Municipality { Name = "Timiaouine", NameAr = "تيمياوين", StateId = "01" },
                    new Municipality { Name = "Ouled Said", NameAr = "أولاد السعيد", StateId = "01" },
                    new Municipality { Name = "Timimoun", NameAr = "تيميمون", StateId = "01" },
                    new Municipality { Name = "Ksar Kaddour", NameAr = "قصر قدور", StateId = "01" },
                    new Municipality { Name = "Tinerkouk", NameAr = "تنركوك", StateId = "01" },
                    new Municipality { Name = "Charouine", NameAr = "شروين", StateId = "01" },
                    new Municipality { Name = "Talmine", NameAr = "طالمين", StateId = "01" },
                    new Municipality { Name = "Bordj Badji Mokhtar", NameAr = "برج باجي مختار", StateId = "01" },
                    
                    // Chlef (02)
                    new Municipality { Name = "Chlef", NameAr = "الشلف", StateId = "02" },
                    new Municipality { Name = "Ténès", NameAr = "تنس", StateId = "02" },
                    new Municipality { Name = "Bénairia", NameAr = "بني راشد", StateId = "02" },
                    new Municipality { Name = "El Karimia", NameAr = "الكريمية", StateId = "02" },
                    new Municipality { Name = "Tadjena", NameAr = "تاجنة", StateId = "02" },
                    new Municipality { Name = "Taougrite", NameAr = "تاوقريت", StateId = "02" },
                    new Municipality { Name = "Beni Haoua", NameAr = "بني حواء", StateId = "02" },
                    new Municipality { Name = "Sobha", NameAr = "الصبحة", StateId = "02" },
                    new Municipality { Name = "Harchoun", NameAr = "الحرشون", StateId = "02" },
                    new Municipality { Name = "Ouled Fares", NameAr = "أولاد فارس", StateId = "02" },
                    new Municipality { Name = "Sidi Akkacha", NameAr = "سيدي عكاشة", StateId = "02" },
                    new Municipality { Name = "Boukadir", NameAr = "بوقادير", StateId = "02" },
                    new Municipality { Name = "Beni Bouattab", NameAr = "بني بوعتاب", StateId = "02" },
                    new Municipality { Name = "Oued Goussine", NameAr = "وادي قوسين", StateId = "02" },
                    new Municipality { Name = "El Marsa", NameAr = "المرسى", StateId = "02" },
                    new Municipality { Name = "Chettia", NameAr = "الشطية", StateId = "02" },
                    new Municipality { Name = "Sidi Abderrahmane", NameAr = "سيدي عبد الرحمن", StateId = "02" },
                    new Municipality { Name = "Moussadek", NameAr = "مصدق", StateId = "02" },
                    new Municipality { Name = "El Hadjadj", NameAr = "الحجاج", StateId = "02" },
                    new Municipality { Name = "Dahra", NameAr = "الظهرة", StateId = "02" },
                    new Municipality { Name = "Ouled Abbes", NameAr = "أولاد عباس", StateId = "02" },
                    new Municipality { Name = "Sendjas", NameAr = "سنجاس", StateId = "02" },
                    new Municipality { Name = "Zeboudja", NameAr = "الزبوجة", StateId = "02" },
                    new Municipality { Name = "Oued Sly", NameAr = "وادي سلي", StateId = "02" },
                    new Municipality { Name = "Abou El Hassan", NameAr = "أبو الحسن", StateId = "02" },
                    new Municipality { Name = "El Beidha", NameAr = "البيضة", StateId = "02" },
                    new Municipality { Name = "Ouled Ben Abdelkader", NameAr = "أولاد بن عبد القادر", StateId = "02" },
                    new Municipality { Name = "Labiod Medjadja", NameAr = "الأبيض مجاجة", StateId = "02" },
                    new Municipality { Name = "Oum Drou", NameAr = "أم الدروع", StateId = "02" },
                    new Municipality { Name = "Breira", NameAr = "بريرة", StateId = "02" },
                    new Municipality { Name = "Beni Rached", NameAr = "بني راشد", StateId = "02" },
                    new Municipality { Name = "Talassa", NameAr = "تلعصة", StateId = "02" },
                    new Municipality { Name = "Herenfa", NameAr = "الهرنفة", StateId = "02" },
                    new Municipality { Name = "Ain Merane", NameAr = "عين مران", StateId = "02" },
                    new Municipality { Name = "Oued Fodda", NameAr = "وادي الفضة", StateId = "02" },
                    
                    // Laghouat (03)
                    new Municipality { Name = "Laghouat", NameAr = "الأغواط", StateId = "03" },
                    new Municipality { Name = "Ksar El Hirane", NameAr = "قصر الحيران", StateId = "03" },
                    new Municipality { Name = "Benacer Benchohra", NameAr = "بن ناصر بن شهرة", StateId = "03" },
                    new Municipality { Name = "Sidi Makhlouf", NameAr = "سيدي مخلوف", StateId = "03" },
                    new Municipality { Name = "Hassi Delaa", NameAr = "حاسي الدلاعة", StateId = "03" },
                    new Municipality { Name = "Hassi R'Mel", NameAr = "حاسي الرمل", StateId = "03" },
                    new Municipality { Name = "Ain Madhi", NameAr = "عين ماضي", StateId = "03" },
                    new Municipality { Name = "Tadjemout", NameAr = "تاجموت", StateId = "03" },
                    new Municipality { Name = "El Assafia", NameAr = "العسافية", StateId = "03" },
                    new Municipality { Name = "El Ghicha", NameAr = "الغيشة", StateId = "03" },
                    new Municipality { Name = "Sebgag", NameAr = "سبقاق", StateId = "03" },
                    new Municipality { Name = "Tadjrouna", NameAr = "تاجرونة", StateId = "03" },
                    new Municipality { Name = "Aflou", NameAr = "أفلو", StateId = "03" },
                    new Municipality { Name = "El Houaita", NameAr = "الحويطة", StateId = "03" },
                    new Municipality { Name = "Gueltat Sidi Saad", NameAr = "قلتة سيدي سعد", StateId = "03" },
                    new Municipality { Name = "Ain Sidi Ali", NameAr = "عين سيدي علي", StateId = "03" },
                    new Municipality { Name = "Beidha", NameAr = "البيضاء", StateId = "03" },
                    new Municipality { Name = "Brida", NameAr = "بريدة", StateId = "03" },
                    new Municipality { Name = "El Kheneg", NameAr = "الخنق", StateId = "03" },
                    new Municipality { Name = "Ain Mahdi", NameAr = "عين ماضي", StateId = "03" },
                    new Municipality { Name = "Oued Morra", NameAr = "وادي مرة", StateId = "03" },
                    new Municipality { Name = "Oued M'Zi", NameAr = "وادي مزي", StateId = "03" },
                    new Municipality { Name = "Sidi Bouzid", NameAr = "سيدي بوزيد", StateId = "03" },
                    new Municipality { Name = "Hadj Mechri", NameAr = "الحاج مشري", StateId = "03" },
                    
                    // Oum El Bouaghi (04)
                    new Municipality { Name = "Oum El Bouaghi", NameAr = "أم البواقي", StateId = "04" },
                    new Municipality { Name = "Aïn Beïda", NameAr = "عين البيضاء", StateId = "04" },
                    new Municipality { Name = "Aïn M'lila", NameAr = "عين مليلة", StateId = "04" },
                    new Municipality { Name = "Aïn Kercha", NameAr = "عين كرشة", StateId = "04" },
                    new Municipality { Name = "Aïn Babouche", NameAr = "عين ببوش", StateId = "04" },
                    new Municipality { Name = "Aïn Zitoun", NameAr = "عين الزيتون", StateId = "04" },
                    new Municipality { Name = "Behir Chergui", NameAr = "بحير الشرقي", StateId = "04" },
                    new Municipality { Name = "Berriche", NameAr = "بريش", StateId = "04" },
                    new Municipality { Name = "Dhalaa", NameAr = "الضلعة", StateId = "04" },
                    new Municipality { Name = "El Amiria", NameAr = "العامرية", StateId = "04" },
                    new Municipality { Name = "El Belala", NameAr = "البلالة", StateId = "04" },
                    new Municipality { Name = "El Djazia", NameAr = "الجازية", StateId = "04" },
                    new Municipality { Name = "El Fedjoudj Boughrara Saoudi", NameAr = "الفجوج بوغرارة السعودي", StateId = "04" },
                    new Municipality { Name = "El Harmilia", NameAr = "الحرملية", StateId = "04" },
                    new Municipality { Name = "Fkirina", NameAr = "فكيرينة", StateId = "04" },
                    new Municipality { Name = "Hanchir Toumghani", NameAr = "هنشير تومغني", StateId = "04" },
                    new Municipality { Name = "Ksar Sbahi", NameAr = "قصر الصباحي", StateId = "04" },
                    new Municipality { Name = "Meskiana", NameAr = "مسكيانة", StateId = "04" },
                    new Municipality { Name = "Ouled Gacem", NameAr = "أولاد قاسم", StateId = "04" },
                    new Municipality { Name = "Ouled Hamla", NameAr = "أولاد حملة", StateId = "04" },
                    new Municipality { Name = "Ouled Zouai", NameAr = "أولاد زواي", StateId = "04" },
                    new Municipality { Name = "Oued Nini", NameAr = "وادي نيني", StateId = "04" },
                    new Municipality { Name = "Rahia", NameAr = "الرحية", StateId = "04" },
                    new Municipality { Name = "Sigus", NameAr = "سيقوس", StateId = "04" },
                    new Municipality { Name = "Souk Naamane", NameAr = "سوق نعمان", StateId = "04" },
                    new Municipality { Name = "Zorg", NameAr = "الزرق", StateId = "04" },
                    new Municipality { Name = "Bir Chouhada", NameAr = "بئر الشهداء", StateId = "04" },
                    new Municipality { Name = "Ouled Achour", NameAr = "أولاد عاشور", StateId = "04" },
                    new Municipality { Name = "Oued El Chaaba", NameAr = "وادي الشعبة", StateId = "04" },
                    
                    // Batna (05)
                    new Municipality { Name = "Batna", NameAr = "باتنة", StateId = "05" },
                    new Municipality { Name = "Tazoult", NameAr = "تازولت", StateId = "05" },
                    new Municipality { Name = "N'Gaous", NameAr = "نقاوس", StateId = "05" },
                    new Municipality { Name = "Arris", NameAr = "أريس", StateId = "05" },
                    new Municipality { Name = "Theniet El Abed", NameAr = "ثنية العابد", StateId = "05" },
                    new Municipality { Name = "Ain Touta", NameAr = "عين التوتة", StateId = "05" },
                    new Municipality { Name = "Barika", NameAr = "بريكة", StateId = "05" },
                    new Municipality { Name = "Merouana", NameAr = "مروانة", StateId = "05" },
                    new Municipality { Name = "Seriana", NameAr = "سريانة", StateId = "05" },
                    new Municipality { Name = "Menaa", NameAr = "منعة", StateId = "05" },
                    new Municipality { Name = "El Madher", NameAr = "المعذر", StateId = "05" },
                    new Municipality { Name = "Timgad", NameAr = "تيمقاد", StateId = "05" },
                    new Municipality { Name = "Ras El Aioun", NameAr = "رأس العيون", StateId = "05" },
                    new Municipality { Name = "Chemora", NameAr = "الشمرة", StateId = "05" },
                    new Municipality { Name = "Oued Chaaba", NameAr = "وادي الشعبة", StateId = "05" },
                    new Municipality { Name = "Oued El Ma", NameAr = "وادي الماء", StateId = "05" },
                    new Municipality { Name = "Ouled Si Slimane", NameAr = "أولاد سي سليمان", StateId = "05" },
                    new Municipality { Name = "Talkhamt", NameAr = "تالخمت", StateId = "05" },
                    new Municipality { Name = "Boulhilat", NameAr = "بولهيلات", StateId = "05" },
                    new Municipality { Name = "Larbaâ", NameAr = "لارباع", StateId = "05" },
                    
                    // Béjaïa (06)
                    new Municipality { Name = "Béjaïa", NameAr = "بجاية", StateId = "06" },
                    new Municipality { Name = "Akbou", NameAr = "أقبو", StateId = "06" },
                    new Municipality { Name = "Seddouk", NameAr = "صدوق", StateId = "06" },
                    new Municipality { Name = "Tichy", NameAr = "تيشي", StateId = "06" },
                    new Municipality { Name = "Chemini", NameAr = "شميني", StateId = "06" },
                    new Municipality { Name = "Amizour", NameAr = "أميزور", StateId = "06" },
                    new Municipality { Name = "Souk El Ténine", NameAr = "سوق الإثنين", StateId = "06" },
                    new Municipality { Name = "Timezrit", NameAr = "تيمزريت", StateId = "06" },
                    new Municipality { Name = "Tazmalt", NameAr = "تازمالت", StateId = "06" },
                    new Municipality { Name = "Adekar", NameAr = "أدكار", StateId = "06" },
                    new Municipality { Name = "Akfadou", NameAr = "أكفادو", StateId = "06" },
                    new Municipality { Name = "Sidi Aïch", NameAr = "سيدي عيش", StateId = "06" },
                    new Municipality { Name = "Aokas", NameAr = "أوقاس", StateId = "06" },
                    new Municipality { Name = "Barbacha", NameAr = "برباشة", StateId = "06" },
                    new Municipality { Name = "Ighil Ali", NameAr = "إغيل علي", StateId = "06" },
                    new Municipality { Name = "Kendira", NameAr = "كنديرة", StateId = "06" },
                    new Municipality { Name = "Kherrata", NameAr = "خراطة", StateId = "06" },
                    new Municipality { Name = "Beni Maouche", NameAr = "بني معوش", StateId = "06" },
                    new Municipality { Name = "Oued Ghir", NameAr = "وادي غير", StateId = "06" },
                    new Municipality { Name = "Bouhamza", NameAr = "بوحمزة", StateId = "06" },
                    new Municipality { Name = "Beni Ksila", NameAr = "بني كسيلة", StateId = "06" },
                    new Municipality { Name = "Toudja", NameAr = "توجة", StateId = "06" },
                    new Municipality { Name = "Darguina", NameAr = "درقينة", StateId = "06" },
                    new Municipality { Name = "Sidi-Ayad", NameAr = "سيدي عياد", StateId = "06" },
                    new Municipality { Name = "Tamokra", NameAr = "تامقرة", StateId = "06" },
                    
                    // Biskra (07)
                    new Municipality { Name = "Biskra", NameAr = "بسكرة", StateId = "07" },
                    new Municipality { Name = "Sidi Okba", NameAr = "سيدي عقبة", StateId = "07" },
                    new Municipality { Name = "Tolga", NameAr = "طولقة", StateId = "07" },
                    new Municipality { Name = "Ouled Djellal", NameAr = "أولاد جلال", StateId = "07" },
                    new Municipality { Name = "Zeribet El Oued", NameAr = "زريبة الوادي", StateId = "07" },
                    new Municipality { Name = "El Kantara", NameAr = "القنطرة", StateId = "07" },
                    new Municipality { Name = "El Outaya", NameAr = "الوطاية", StateId = "07" },
                    new Municipality { Name = "Djemorah", NameAr = "جمورة", StateId = "07" },
                    new Municipality { Name = "Branis", NameAr = "برانيس", StateId = "07" },
                    new Municipality { Name = "El Hadjeb", NameAr = "الحاجب", StateId = "07" },
                    new Municipality { Name = "Ain Zaatout", NameAr = "عين زعطوط", StateId = "07" },
                    new Municipality { Name = "Lichana", NameAr = "ليشانة", StateId = "07" },
                    new Municipality { Name = "Foughala", NameAr = "فوغالة", StateId = "07" },
                    new Municipality { Name = "Bordj Ben Azzouz", NameAr = "برج بن عزوز", StateId = "07" },
                    new Municipality { Name = "M'Chouneche", NameAr = "مشونش", StateId = "07" },
                    new Municipality { Name = "El Ghrous", NameAr = "الغروس", StateId = "07" },
                    new Municipality { Name = "M'Lili", NameAr = "مليلي", StateId = "07" },
                    new Municipality { Name = "Ourlal", NameAr = "أورلال", StateId = "07" },
                    new Municipality { Name = "Oumache", NameAr = "أوماش", StateId = "07" },
                    new Municipality { Name = "Ain Naga", NameAr = "عين الناقة", StateId = "07" },
                    new Municipality { Name = "Chetma", NameAr = "شتمة", StateId = "07" },
                    new Municipality { Name = "Bouchagroun", NameAr = "بوشقرون", StateId = "07" },
                    new Municipality { Name = "Mekhadma", NameAr = "مخادمة", StateId = "07" },
                    new Municipality { Name = "Lioua", NameAr = "ليوة", StateId = "07" },
                    new Municipality { Name = "El Feidh", NameAr = "الفيض", StateId = "07" },
                    new Municipality { Name = "Khanguet Sidi Nadji", NameAr = "خنقة سيدي ناجي", StateId = "07" },
                    new Municipality { Name = "M'Ziraa", NameAr = "المزيرعة", StateId = "07" },
                    new Municipality { Name = "Doucen", NameAr = "الدوسن", StateId = "07" },
                    new Municipality { Name = "Chaiba", NameAr = "الشعيبة", StateId = "07" },
                    new Municipality { Name = "Sidi Khaled", NameAr = "سيدي خالد", StateId = "07" },
                    new Municipality { Name = "Besbes", NameAr = "بسباس", StateId = "07" },
                    new Municipality { Name = "Ras El Miaad", NameAr = "رأس الميعاد", StateId = "07" },
                    
                    
                    // Béchar (08)
                    new Municipality { Name = "Béchar", NameAr = "بشار", StateId = "08" },
                    new Municipality { Name = "Erg Ferradj", NameAr = "عرق فراج", StateId = "08" },
                    new Municipality { Name = "Ouled Khoudir", NameAr = "أولاد خضير", StateId = "08" },
                    new Municipality { Name = "Meridja", NameAr = "المريجة", StateId = "08" },
                    new Municipality { Name = "Timoudi", NameAr = "تيمودي", StateId = "08" },
                    new Municipality { Name = "Lahmar", NameAr = "لحمر", StateId = "08" },
                    new Municipality { Name = "Béni Abbès", NameAr = "بني عباس", StateId = "08" },
                    new Municipality { Name = "Tabelbala", NameAr = "تبلبالة", StateId = "08" },
                    new Municipality { Name = "Taghit", NameAr = "تاغيت", StateId = "08" },
                    new Municipality { Name = "El Ouata", NameAr = "الواتة", StateId = "08" },
                    new Municipality { Name = "Boukais", NameAr = "بوكايس", StateId = "08" },
                    new Municipality { Name = "Mogheul", NameAr = "موغل", StateId = "08" },
                    new Municipality { Name = "Abadla", NameAr = "العبادلة", StateId = "08" },
                    new Municipality { Name = "Kerzaz", NameAr = "كرزاز", StateId = "08" },
                    new Municipality { Name = "Kenadsa", NameAr = "القنادسة", StateId = "08" },
                    new Municipality { Name = "Igli", NameAr = "إقلي", StateId = "08" },
                    new Municipality { Name = "Beni Ikhlef", NameAr = "بني يخلف", StateId = "08" },
                    new Municipality { Name = "Mechraa Houari Boumedienne", NameAr = "مشرع هواري بومدين", StateId = "08" },
                    new Municipality { Name = "Tamtert", NameAr = "تامترت", StateId = "08" },
                    new Municipality { Name = "Beni Ounif", NameAr = "بني ونيف", StateId = "08" },
                    new Municipality { Name = "Ksiksou", NameAr = "كسيكسو", StateId = "08" },

                    // Blida (09)
                    new Municipality { Name = "Blida", NameAr = "البليدة", StateId = "09" },
                    new Municipality { Name = "Bouinan", NameAr = "بوعينان", StateId = "09" },
                    new Municipality { Name = "Oued El Alleug", NameAr = "وادي العلايق", StateId = "09" },
                    new Municipality { Name = "Ouled Yaich", NameAr = "أولاد يعيش", StateId = "09" },
                    new Municipality { Name = "Chréa", NameAr = "الشريعة", StateId = "09" },
                    new Municipality { Name = "El Affroun", NameAr = "العفرون", StateId = "09" },
                    new Municipality { Name = "Chiffa", NameAr = "الشفة", StateId = "09" },
                    new Municipality { Name = "Hammam Melouane", NameAr = "حمام ملوان", StateId = "09" },
                    new Municipality { Name = "Beni Tamou", NameAr = "بني تامو", StateId = "09" },
                    new Municipality { Name = "Bouarfa", NameAr = "بوعرفة", StateId = "09" },
                    new Municipality { Name = "Beni Mered", NameAr = "بني مراد", StateId = "09" },
                    new Municipality { Name = "Boufarik", NameAr = "بوفاريك", StateId = "09" },
                    new Municipality { Name = "Soumaa", NameAr = "الصومعة", StateId = "09" },
                    new Municipality { Name = "Guerrouaou", NameAr = "قرواو", StateId = "09" },
                    new Municipality { Name = "Ain Romana", NameAr = "عين الرمانة", StateId = "09" },
                    new Municipality { Name = "Mouzaia", NameAr = "موزاية", StateId = "09" },
                    new Municipality { Name = "Souhane", NameAr = "صوحان", StateId = "09" },
                    new Municipality { Name = "Meftah", NameAr = "مفتاح", StateId = "09" },
                    new Municipality { Name = "Djebabra", NameAr = "جبابرة", StateId = "09" },
                    new Municipality { Name = "Bougara", NameAr = "بوقرة", StateId = "09" },
                    new Municipality { Name = "Larbaa", NameAr = "الأربعاء", StateId = "09" },
                    new Municipality { Name = "Oued Djer", NameAr = "وادي جر", StateId = "09" },
                    new Municipality { Name = "Beni Tamou", NameAr = "بني تامو", StateId = "09" },
                    new Municipality { Name = "Chebli", NameAr = "الشبلي", StateId = "09" },
                    new Municipality { Name = "Ain Romana", NameAr = "عين الرمانة", StateId = "09" },

                    // Bouira (10)
                    new Municipality { Name = "Bouira", NameAr = "البويرة", StateId = "10" },
                    new Municipality { Name = "Ain Bessem", NameAr = "عين بسام", StateId = "10" },
                    new Municipality { Name = "Ain Laloui", NameAr = "عين العلوي", StateId = "10" },
                    new Municipality { Name = "Ahl El Ksar", NameAr = "أهل القصر", StateId = "10" },
                    new Municipality { Name = "Aomar", NameAr = "أعمر", StateId = "10" },
                    new Municipality { Name = "Ath Mansour", NameAr = "آث منصور", StateId = "10" },
                    new Municipality { Name = "Bechloul", NameAr = "بشلول", StateId = "10" },
                    new Municipality { Name = "Bir Ghbalou", NameAr = "بئر غبالو", StateId = "10" },
                    new Municipality { Name = "Bordj Okhriss", NameAr = "برج أوخريص", StateId = "10" },
                    new Municipality { Name = "Bouderbala", NameAr = "بودربالة", StateId = "10" },
                    new Municipality { Name = "Boukram", NameAr = "بوكرم", StateId = "10" },
                    new Municipality { Name = "Chorfa", NameAr = "الشرفة", StateId = "10" },
                    new Municipality { Name = "Dechmia", NameAr = "الدشمية", StateId = "10" },
                    new Municipality { Name = "Dirah", NameAr = "ديرة", StateId = "10" },
                    new Municipality { Name = "Djebahia", NameAr = "جباحية", StateId = "10" },
                    new Municipality { Name = "El Adjiba", NameAr = "العجيبة", StateId = "10" },
                    new Municipality { Name = "El Asnam", NameAr = "الأسنام", StateId = "10" },
                    new Municipality { Name = "El Hachimia", NameAr = "الهاشمية", StateId = "10" },
                    new Municipality { Name = "El Khabouzia", NameAr = "الخبوزية", StateId = "10" },
                    new Municipality { Name = "El Mokrani", NameAr = "المقراني", StateId = "10" },
                    new Municipality { Name = "Guerrouma", NameAr = "قرومة", StateId = "10" },
                    new Municipality { Name = "Haizer", NameAr = "حيزر", StateId = "10" },
                    new Municipality { Name = "Kadiria", NameAr = "القادرية", StateId = "10" },
                    new Municipality { Name = "Lakhdaria", NameAr = "الأخضرية", StateId = "10" },
                    new Municipality { Name = "M'Chedallah", NameAr = "مشد الله", StateId = "10" },
                    new Municipality { Name = "Maala", NameAr = "معلة", StateId = "10" },
                    new Municipality { Name = "Mezdour", NameAr = "مزدور", StateId = "10" },
                    new Municipality { Name = "Oued El Berdi", NameAr = "وادي البردي", StateId = "10" },
                    new Municipality { Name = "Raouraoua", NameAr = "روراوة", StateId = "10" },
                    new Municipality { Name = "Ridane", NameAr = "ريدان", StateId = "10" },
                    new Municipality { Name = "Saharidj", NameAr = "سحاريج", StateId = "10" },
                    new Municipality { Name = "Sour El Ghouzlane", NameAr = "سور الغزلان", StateId = "10" },
                    new Municipality { Name = "Souk El Khemis", NameAr = "سوق الخميس", StateId = "10" },
                    new Municipality { Name = "Taghzout", NameAr = "تاغزوت", StateId = "10" },
                    new Municipality { Name = "Taksert", NameAr = "تاكسرت", StateId = "10" },
                    new Municipality { Name = "Zbarbar", NameAr = "زبربر", StateId = "10" },

                    // Tamanrasset (11)
                    new Municipality { Name = "Tamanrasset", NameAr = "تمنراست", StateId = "11" },
                    new Municipality { Name = "Abalessa", NameAr = "أبلسة", StateId = "11" },
                    new Municipality { Name = "In Amguel", NameAr = "عين امقل", StateId = "11" },
                    new Municipality { Name = "In Salah", NameAr = "عين صالح", StateId = "11" },
                    new Municipality { Name = "In Ghar", NameAr = "عين غار", StateId = "11" },
                    new Municipality { Name = "Idles", NameAr = "إدلس", StateId = "11" },
                    new Municipality { Name = "Tazrouk", NameAr = "تاظروك", StateId = "11" },
                    new Municipality { Name = "Tin Zaouatine", NameAr = "تين زواتين", StateId = "11" },
                    new Municipality { Name = "In Guezzam", NameAr = "عين قزام", StateId = "11" },
                    new Municipality { Name = "Foggaret Ezzaouia", NameAr = "فقارة الزوى", StateId = "11" },

                    // Tébessa (12)
                    new Municipality { Name = "Tébessa", NameAr = "تبسة", StateId = "12" },
                    new Municipality { Name = "Bir el-Ater", NameAr = "بئر العاتر", StateId = "12" },
                    new Municipality { Name = "Cheria", NameAr = "الشريعة", StateId = "12" },
                    new Municipality { Name = "Stah Guentis", NameAr = "سطح قنطيس", StateId = "12" },
                    new Municipality { Name = "El Aouinet", NameAr = "العوينات", StateId = "12" },
                    new Municipality { Name = "El Houidjbet", NameAr = "الحويجبات", StateId = "12" },
                    new Municipality { Name = "El Kouif", NameAr = "الكويف", StateId = "12" },
                    new Municipality { Name = "El Ma Labiodh", NameAr = "الماء الابيض", StateId = "12" },
                    new Municipality { Name = "El Meridj", NameAr = "المريج", StateId = "12" },
                    new Municipality { Name = "El Ogla", NameAr = "العقلة", StateId = "12" },
                    new Municipality { Name = "El Ogla El Malha", NameAr = "العقلة المالحة", StateId = "12" },
                    new Municipality { Name = "Ferkane", NameAr = "فركان", StateId = "12" },
                    new Municipality { Name = "Hammamet", NameAr = "حمامات", StateId = "12" },
                    new Municipality { Name = "Morsott", NameAr = "مرسط", StateId = "12" },
                    new Municipality { Name = "Negrine", NameAr = "نقرين", StateId = "12" },
                    new Municipality { Name = "Oum Ali", NameAr = "أم علي", StateId = "12" },
                    new Municipality { Name = "Safsaf El Ouesra", NameAr = "صفصاف الوسرى", StateId = "12" },
                    new Municipality { Name = "Thlidjene", NameAr = "ثليجان", StateId = "12" },
                    new Municipality { Name = "Ain Zerga", NameAr = "عين الزرقاء", StateId = "12" },
                    new Municipality { Name = "Bedjene", NameAr = "بجن", StateId = "12" },
                    new Municipality { Name = "Bekkaria", NameAr = "بكارية", StateId = "12" },
                    new Municipality { Name = "Bir Mokkadem", NameAr = "بئر مقدم", StateId = "12" },
                    new Municipality { Name = "Boulhaf Dir", NameAr = "بولحاف الدير", StateId = "12" },
                    new Municipality { Name = "Boukhadra", NameAr = "بوخضرة", StateId = "12" },
                    new Municipality { Name = "Ouenza", NameAr = "الونزة", StateId = "12" },
                    new Municipality { Name = "Gourigueur", NameAr = "قريقر", StateId = "12" },
                    new Municipality { Name = "El Mazraa", NameAr = "المزرعة", StateId = "12" },
                    new Municipality { Name = "El Mezeraa", NameAr = "المزرعة", StateId = "12" },

                    // Tlemcen (13)
                    new Municipality { Name = "Tlemcen", NameAr = "تلمسان", StateId = "13" },
                    new Municipality { Name = "Beni Mester", NameAr = "بني مستر", StateId = "13" },
                    new Municipality { Name = "Ain Tallout", NameAr = "عين تالوت", StateId = "13" },
                    new Municipality { Name = "Remchi", NameAr = "الرمشي", StateId = "13" },
                    new Municipality { Name = "El Fehoul", NameAr = "الفحول", StateId = "13" },
                    new Municipality { Name = "Sabra", NameAr = "صبرة", StateId = "13" },
                    new Municipality { Name = "Ghazaouet", NameAr = "الغزوات", StateId = "13" },
                    new Municipality { Name = "Souani", NameAr = "السواني", StateId = "13" },
                    new Municipality { Name = "Djebala", NameAr = "جبالة", StateId = "13" },
                    new Municipality { Name = "El Gor", NameAr = "القور", StateId = "13" },
                    new Municipality { Name = "Oued Chouly", NameAr = "وادي الشولي", StateId = "13" },
                    new Municipality { Name = "Ain Fezza", NameAr = "عين فزة", StateId = "13" },
                    new Municipality { Name = "Ouled Mimoun", NameAr = "أولاد ميمون", StateId = "13" },
                    new Municipality { Name = "Amieur", NameAr = "عمير", StateId = "13" },
                    new Municipality { Name = "Ain Youcef", NameAr = "عين يوسف", StateId = "13" },
                    new Municipality { Name = "Zenata", NameAr = "زناتة", StateId = "13" },
                    new Municipality { Name = "Beni Snous", NameAr = "بني سنوس", StateId = "13" },
                    new Municipality { Name = "Bab El Assa", NameAr = "باب العسة", StateId = "13" },
                    new Municipality { Name = "Dar Yaghmouracene", NameAr = "دار يغمراسن", StateId = "13" },
                    new Municipality { Name = "Fellaoucene", NameAr = "فلاوسن", StateId = "13" },
                    new Municipality { Name = "Azails", NameAr = "العزايل", StateId = "13" },
                    new Municipality { Name = "Sebaa Chioukh", NameAr = "سبعة شيوخ", StateId = "13" },
                    new Municipality { Name = "Terny Beni Hediel", NameAr = "تيرني بني هديل", StateId = "13" },
                    new Municipality { Name = "Bensekrane", NameAr = "بن سكران", StateId = "13" },
                    new Municipality { Name = "Ain Nehala", NameAr = "عين النحالة", StateId = "13" },
                    new Municipality { Name = "Hennaya", NameAr = "الحناية", StateId = "13" },
                    new Municipality { Name = "Maghnia", NameAr = "مغنية", StateId = "13" },
                    new Municipality { Name = "Hammam Boughrara", NameAr = "حمام بوغرارة", StateId = "13" },
                    new Municipality { Name = "Souahlia", NameAr = "السواحلية", StateId = "13" },
                    new Municipality { Name = "Msirda Fouaga", NameAr = "مسيردة الفواقة", StateId = "13" },
                    new Municipality { Name = "Ain Fetah", NameAr = "عين فتاح", StateId = "13" },
                    new Municipality { Name = "El Aricha", NameAr = "العريشة", StateId = "13" },
                    new Municipality { Name = "Souk Tlata", NameAr = "سوق الثلاثاء", StateId = "13" },
                    new Municipality { Name = "Sidi Abdelli", NameAr = "سيدي العبدلي", StateId = "13" },
                    new Municipality { Name = "Sebdou", NameAr = "سبدو", StateId = "13" },
                    new Municipality { Name = "Beni Ouarsous", NameAr = "بني وارسوس", StateId = "13" },
                    new Municipality { Name = "Sidi Medjahed", NameAr = "سيدي مجاهد", StateId = "13" },
                    new Municipality { Name = "Beni Boussaid", NameAr = "بني بوسعيد", StateId = "13" },
                    new Municipality { Name = "Marsa Ben M'Hidi", NameAr = "مرسى بن مهيدي", StateId = "13" },
                    new Municipality { Name = "Nedroma", NameAr = "ندرومة", StateId = "13" },
                    new Municipality { Name = "Sidi Djillali", NameAr = "سيدي الجيلالي", StateId = "13" },
                    new Municipality { Name = "Beni Bahdel", NameAr = "بني بهدل", StateId = "13" },
                    new Municipality { Name = "El Bouihi", NameAr = "البويهي", StateId = "13" },
                    new Municipality { Name = "Honaine", NameAr = "هنين", StateId = "13" },
                    new Municipality { Name = "Tianet", NameAr = "تيانت", StateId = "13" },
                    new Municipality { Name = "Ouled Riyah", NameAr = "أولاد رياح", StateId = "13" },
                    new Municipality { Name = "Bouhlou", NameAr = "بوحلو", StateId = "13" },
                    new Municipality { Name = "Souk El Khemis", NameAr = "سوق الخميس", StateId = "13" },
                    new Municipality { Name = "Ain Ghoraba", NameAr = "عين غرابة", StateId = "13" },
                    new Municipality { Name = "Chetouane", NameAr = "شتوان", StateId = "13" },
                    new Municipality { Name = "Mansourah", NameAr = "منصورة", StateId = "13" },
                    new Municipality { Name = "Beni Semiel", NameAr = "بني صميل", StateId = "13" },

                    // Tiaret (14)
                    new Municipality { Name = "Tiaret", NameAr = "تيارت", StateId = "14" },
                    new Municipality { Name = "Medroussa", NameAr = "مدروسة", StateId = "14" },
                    new Municipality { Name = "Ain Bouchekif", NameAr = "عين بوشقيف", StateId = "14" },
                    new Municipality { Name = "Sidi Ali Mellal", NameAr = "سيدي علي ملال", StateId = "14" },
                    new Municipality { Name = "Ain Zarit", NameAr = "عين زاريت", StateId = "14" },
                    new Municipality { Name = "Ain Deheb", NameAr = "عين الذهب", StateId = "14" },
                    new Municipality { Name = "Sidi Bakhti", NameAr = "سيدي بختي", StateId = "14" },
                    new Municipality { Name = "Medrissa", NameAr = "مدريسة", StateId = "14" },
                    new Municipality { Name = "Zmalet El Emir Abdelkader", NameAr = "زمالة الأمير عبد القادر", StateId = "14" },
                    new Municipality { Name = "Madna", NameAr = "مادنة", StateId = "14" },
                    new Municipality { Name = "Sebt", NameAr = "السبت", StateId = "14" },
                    new Municipality { Name = "Mellakou", NameAr = "ملاكو", StateId = "14" },
                    new Municipality { Name = "Dahmouni", NameAr = "دحموني", StateId = "14" },
                    new Municipality { Name = "Rahouia", NameAr = "الرحوية", StateId = "14" },
                    new Municipality { Name = "Mahdia", NameAr = "مهدية", StateId = "14" },
                    new Municipality { Name = "Sougueur", NameAr = "السوقر", StateId = "14" },
                    new Municipality { Name = "Si Abdelghani", NameAr = "سي عبد الغني", StateId = "14" },
                    new Municipality { Name = "Ain El Hadid", NameAr = "عين الحديد", StateId = "14" },
                    new Municipality { Name = "Oued Lilli", NameAr = "وادي ليلي", StateId = "14" },
                    new Municipality { Name = "Tidda", NameAr = "تيدة", StateId = "14" },
                    new Municipality { Name = "Sidi Hosni", NameAr = "سيدي حسني", StateId = "14" },
                    new Municipality { Name = "Djillali Ben Amar", NameAr = "جيلالي بن عمار", StateId = "14" },
                    new Municipality { Name = "Sebaine", NameAr = "السبعين", StateId = "14" },
                    new Municipality { Name = "Tousnina", NameAr = "توسنينة", StateId = "14" },
                    new Municipality { Name = "Frenda", NameAr = "فرندة", StateId = "14" },
                    new Municipality { Name = "Ain Kermes", NameAr = "عين كرمس", StateId = "14" },
                    new Municipality { Name = "Ksar Chellala", NameAr = "قصر الشلالة", StateId = "14" },
                    new Municipality { Name = "Rechaiga", NameAr = "الرشايقة", StateId = "14" },
                    new Municipality { Name = "Nadorah", NameAr = "الناظورة", StateId = "14" },
                    new Municipality { Name = "Tagdempt", NameAr = "تاقدمت", StateId = "14" },
                    new Municipality { Name = "Oued Essalem", NameAr = "وادي السلام", StateId = "14" },
                    new Municipality { Name = "Sidi Abderrahmane", NameAr = "سيدي عبد الرحمن", StateId = "14" },
                    new Municipality { Name = "Serghine", NameAr = "سرغين", StateId = "14" },
                    new Municipality { Name = "Bougara", NameAr = "بوقرة", StateId = "14" },
                    new Municipality { Name = "Hamadia", NameAr = "حمادية", StateId = "14" },
                    new Municipality { Name = "Chehaima", NameAr = "شحيمة", StateId = "14" },
                    new Municipality { Name = "Takhemaret", NameAr = "تخمرت", StateId = "14" },
                    new Municipality { Name = "Sidi Abderahmane", NameAr = "سيدي عبد الرحمن", StateId = "14" },
                    new Municipality { Name = "Mechraa Safa", NameAr = "مشرع الصفا", StateId = "14" },
                    new Municipality { Name = "Djebel Messaad", NameAr = "جبل مساعد", StateId = "14" },
                    new Municipality { Name = "Sidi Boutouchent", NameAr = "سيدي بوتوشنت", StateId = "14" },
                    new Municipality { Name = "Faidja", NameAr = "الفايجة", StateId = "14" },

                    // Tizi Ouzou (15)
                    new Municipality { Name = "Tizi Ouzou", NameAr = "تيزي وزو", StateId = "15" },
                    new Municipality { Name = "Ain El Hammam", NameAr = "عين الحمام", StateId = "15" },
                    new Municipality { Name = "Akbil", NameAr = "اقبيل", StateId = "15" },
                    new Municipality { Name = "Freha", NameAr = "فريحة", StateId = "15" },
                    new Municipality { Name = "Souamaa", NameAr = "صوامع", StateId = "15" },
                    new Municipality { Name = "Mechtrass", NameAr = "مشطراس", StateId = "15" },
                    new Municipality { Name = "Irdjen", NameAr = "إيرجن", StateId = "15" },
                    new Municipality { Name = "Timizart", NameAr = "تيميزار", StateId = "15" },
                    new Municipality { Name = "Makouda", NameAr = "ماكودة", StateId = "15" },
                    new Municipality { Name = "Draa El Mizan", NameAr = "ذراع الميزان", StateId = "15" },
                    new Municipality { Name = "Tizi Gheniff", NameAr = "تيزي غنيف", StateId = "15" },
                    new Municipality { Name = "Bounouh", NameAr = "بونوح", StateId = "15" },
                    new Municipality { Name = "Ait Chafaa", NameAr = "آيت شافع", StateId = "15" },
                    new Municipality { Name = "Frikat", NameAr = "فريقات", StateId = "15" },
                    new Municipality { Name = "Beni Aissi", NameAr = "بني عيسي", StateId = "15" },
                    new Municipality { Name = "Beni Zmenzer", NameAr = "بني زمنزر", StateId = "15" },
                    new Municipality { Name = "Iferhounene", NameAr = "إفرحونن", StateId = "15" },
                    new Municipality { Name = "Azazga", NameAr = "عزازقة", StateId = "15" },
                    new Municipality { Name = "Illoula Oumalou", NameAr = "إيلولة أومالو", StateId = "15" },
                    new Municipality { Name = "Yakouren", NameAr = "ياقورن", StateId = "15" },
                    new Municipality { Name = "Larba Nath Irathen", NameAr = "الأربعاء ناث إيراثن", StateId = "15" },
                    new Municipality { Name = "Tizi Rached", NameAr = "تيزي راشد", StateId = "15" },
                    new Municipality { Name = "Zekri", NameAr = "زكري", StateId = "15" },
                    new Municipality { Name = "Ouaguenoun", NameAr = "واقنون", StateId = "15" },
                    new Municipality { Name = "Ain Zaouia", NameAr = "عين الزاوية", StateId = "15" },
                    new Municipality { Name = "Mkira", NameAr = "مكيرة", StateId = "15" },
                    new Municipality { Name = "Ait Yahia", NameAr = "آيت يحيى", StateId = "15" },
                    new Municipality { Name = "Ait Mahmoud", NameAr = "آيت محمود", StateId = "15" },
                    new Municipality { Name = "Maatkas", NameAr = "معاتقة", StateId = "15" },
                    new Municipality { Name = "Ait Boumahdi", NameAr = "آيت بومهدي", StateId = "15" },
                    new Municipality { Name = "Abi Youcef", NameAr = "أبي يوسف", StateId = "15" },
                    new Municipality { Name = "Beni Douala", NameAr = "بني دوالة", StateId = "15" },
                    new Municipality { Name = "Illilten", NameAr = "إيليلتن", StateId = "15" },
                    new Municipality { Name = "Bouzeguene", NameAr = "بوزقن", StateId = "15" },
                    new Municipality { Name = "Ait Aggouacha", NameAr = "آيت عقواشة", StateId = "15" },
                    new Municipality { Name = "Ouadhias", NameAr = "واضية", StateId = "15" },
                    new Municipality { Name = "Azeffoun", NameAr = "أزفون", StateId = "15" },
                    new Municipality { Name = "Tigzirt", NameAr = "تيقزيرت", StateId = "15" },
                    new Municipality { Name = "Ait Aissa Mimoun", NameAr = "آيت عيسى ميمون", StateId = "15" },
                    new Municipality { Name = "Boghni", NameAr = "بوغني", StateId = "15" },
                    new Municipality { Name = "Ifigha", NameAr = "إيفيغاء", StateId = "15" },
                    new Municipality { Name = "Ait Oumalou", NameAr = "آيت أومالو", StateId = "15" },
                    new Municipality { Name = "Tirmitine", NameAr = "تيرمتين", StateId = "15" },
                    new Municipality { Name = "Akerrou", NameAr = "أقرو", StateId = "15" },
                    new Municipality { Name = "Yatafene", NameAr = "يطافن", StateId = "15" },
                    new Municipality { Name = "Beni Ziki", NameAr = "بني زيكي", StateId = "15" },
                    new Municipality { Name = "Draa Ben Khedda", NameAr = "ذراع بن خدة", StateId = "15" },
                    new Municipality { Name = "Ouacif", NameAr = "واسيف", StateId = "15" },
                    new Municipality { Name = "Idjeur", NameAr = "إيجار", StateId = "15" },
                    new Municipality { Name = "Mekla", NameAr = "مقلع", StateId = "15" },
                    new Municipality { Name = "Tizi Nthlata", NameAr = "تيزي نثلاثة", StateId = "15" },
                    new Municipality { Name = "Beni Yenni", NameAr = "بني يني", StateId = "15" },
                    new Municipality { Name = "Aghribs", NameAr = "أغريب", StateId = "15" },
                    new Municipality { Name = "Iflissen", NameAr = "إفليسن", StateId = "15" },
                    new Municipality { Name = "Boudjima", NameAr = "بوجيمة", StateId = "15" },
                    new Municipality { Name = "Ait Ouacif", NameAr = "آيت واسيف", StateId = "15" },
                    new Municipality { Name = "Assi Youcef", NameAr = "آسي يوسف", StateId = "15" },
                    new Municipality { Name = "Ait Toudert", NameAr = "آيت تودرت", StateId = "15" },
                    new Municipality { Name = "Agouni Gueghrane", NameAr = "أقني قغران", StateId = "15" },
                    new Municipality { Name = "Mizrana", NameAr = "ميزرانة", StateId = "15" },
                    new Municipality { Name = "Imsouhal", NameAr = "إمسوحال", StateId = "15" },
                    new Municipality { Name = "Tadmait", NameAr = "تادمايت", StateId = "15" },
                    new Municipality { Name = "Ait Bouaddou", NameAr = "آيت بوعدو", StateId = "15" },
                    new Municipality { Name = "Souk El Thenine", NameAr = "سوق الإثنين", StateId = "15" },
                    new Municipality { Name = "Ait Khelili", NameAr = "آيت خليلي", StateId = "15" },
                    new Municipality { Name = "Sidi Namane", NameAr = "سيدي نعمان", StateId = "15" },
                    new Municipality { Name = "Iboudraren", NameAr = "إبودرارن", StateId = "15" },
                    new Municipality { Name = "Aghni Goughrane", NameAr = "أغني قوغران", StateId = "15" },
                    new Municipality { Name = "Ait Yahia Moussa", NameAr = "آيت يحيى موسى", StateId = "15" },

                    // Alger (16)
                    new Municipality { Name = "Alger Centre", NameAr = "الجزائر الوسطى", StateId = "16" },
                    new Municipality { Name = "Sidi M'Hamed", NameAr = "سيدي امحمد", StateId = "16" },
                    new Municipality { Name = "El Madania", NameAr = "المدنية", StateId = "16" },
                    new Municipality { Name = "Belouizdad", NameAr = "بلوزداد", StateId = "16" },
                    new Municipality { Name = "Bab El Oued", NameAr = "باب الوادي", StateId = "16" },
                    new Municipality { Name = "Bologhine", NameAr = "بولوغين", StateId = "16" },
                    new Municipality { Name = "Casbah", NameAr = "القصبة", StateId = "16" },
                    new Municipality { Name = "Oued Koriche", NameAr = "وادي قريش", StateId = "16" },
                    new Municipality { Name = "Bir Mourad Raïs", NameAr = "بئر مراد رايس", StateId = "16" },
                    new Municipality { Name = "El Biar", NameAr = "الأبيار", StateId = "16" },
                    new Municipality { Name = "Bouzareah", NameAr = "بوزريعة", StateId = "16" },
                    new Municipality { Name = "Birkhadem", NameAr = "بئر خادم", StateId = "16" },
                    new Municipality { Name = "El Harrach", NameAr = "الحراش", StateId = "16" },
                    new Municipality { Name = "Baraki", NameAr = "براقي", StateId = "16" },
                    new Municipality { Name = "Oued Smar", NameAr = "وادي السمار", StateId = "16" },
                    new Municipality { Name = "Bachdjarah", NameAr = "باش جراح", StateId = "16" },
                    new Municipality { Name = "Hussein Dey", NameAr = "حسين داي", StateId = "16" },
                    new Municipality { Name = "Kouba", NameAr = "القبة", StateId = "16" },
                    new Municipality { Name = "Bourouba", NameAr = "بوروبة", StateId = "16" },
                    new Municipality { Name = "Dar El Beïda", NameAr = "الدار البيضاء", StateId = "16" },
                    new Municipality { Name = "Bab Ezzouar", NameAr = "باب الزوار", StateId = "16" },
                    new Municipality { Name = "Ben Aknoun", NameAr = "بن عكنون", StateId = "16" },
                    new Municipality { Name = "Dely Ibrahim", NameAr = "دالي ابراهيم", StateId = "16" },
                    new Municipality { Name = "El Hammamet", NameAr = "الحمامات", StateId = "16" },
                    new Municipality { Name = "Raïs Hamidou", NameAr = "الرايس حميدو", StateId = "16" },
                    new Municipality { Name = "Djasr Kasentina", NameAr = "جسر قسنطينة", StateId = "16" },
                    new Municipality { Name = "El Mouradia", NameAr = "المرادية", StateId = "16" },
                    new Municipality { Name = "Hydra", NameAr = "حيدرة", StateId = "16" },
                    new Municipality { Name = "Mohammadia", NameAr = "المحمدية", StateId = "16" },
                    new Municipality { Name = "Bordj El Kiffan", NameAr = "برج الكيفان", StateId = "16" },
                    new Municipality { Name = "El Magharia", NameAr = "المغارية", StateId = "16" },
                    new Municipality { Name = "Beni Messous", NameAr = "بني مسوس", StateId = "16" },
                    new Municipality { Name = "Les Eucalyptus", NameAr = "الكاليتوس", StateId = "16" },
                    new Municipality { Name = "Birtouta", NameAr = "بئر توتة", StateId = "16" },
                    new Municipality { Name = "Tessala El Merdja", NameAr = "تسالة المرجة", StateId = "16" },
                    new Municipality { Name = "Ouled Chebel", NameAr = "اولاد شبل", StateId = "16" },
                    new Municipality { Name = "Sidi Moussa", NameAr = "سيدي موسى", StateId = "16" },
                    new Municipality { Name = "Ain Taya", NameAr = "عين طاية", StateId = "16" },
                    new Municipality { Name = "Bordj El Bahri", NameAr = "برج البحري", StateId = "16" },
                    new Municipality { Name = "El Marsa", NameAr = "المرسى", StateId = "16" },
                    new Municipality { Name = "H'raoua", NameAr = "هراوة", StateId = "16" },
                    new Municipality { Name = "Rouiba", NameAr = "الرويبة", StateId = "16" },
                    new Municipality { Name = "Reghaïa", NameAr = "رغاية", StateId = "16" },
                    new Municipality { Name = "Ain Benian", NameAr = "عين بنيان", StateId = "16" },
                    new Municipality { Name = "Staoueli", NameAr = "سطاوالي", StateId = "16" },
                    new Municipality { Name = "Zeralda", NameAr = "زرالدة", StateId = "16" },
                    new Municipality { Name = "Mahelma", NameAr = "المحالمة", StateId = "16" },
                    new Municipality { Name = "Rahmania", NameAr = "الرحمانية", StateId = "16" },
                    new Municipality { Name = "Souidania", NameAr = "السويدانية", StateId = "16" },
                    new Municipality { Name = "Cheraga", NameAr = "الشراقة", StateId = "16" },
                    new Municipality { Name = "Ouled Fayet", NameAr = "اولاد فايت", StateId = "16" },
                    new Municipality { Name = "El Achour", NameAr = "العاشور", StateId = "16" },
                    new Municipality { Name = "Draria", NameAr = "الدرارية", StateId = "16" },
                    new Municipality { Name = "Douera", NameAr = "الدويرة", StateId = "16" },
                    new Municipality { Name = "Baba Hassen", NameAr = "بابا حسن", StateId = "16" },
                    new Municipality { Name = "Khraicia", NameAr = "الخرايسية", StateId = "16" },
                    new Municipality { Name = "Saoula", NameAr = "السحاولة", StateId = "16" },

                    // Djelfa (17)
                    new Municipality { Name = "Djelfa", NameAr = "الجلفة", StateId = "17" },
                    new Municipality { Name = "Ain El Ibel", NameAr = "عين الإبل", StateId = "17" },
                    new Municipality { Name = "Ain Oussera", NameAr = "عين وسارة", StateId = "17" },
                    new Municipality { Name = "Benhar", NameAr = "بنهار", StateId = "17" },
                    new Municipality { Name = "Birine", NameAr = "بيرين", StateId = "17" },
                    new Municipality { Name = "Bouira Lahdab", NameAr = "بويرة الأحداب", StateId = "17" },
                    new Municipality { Name = "Charef", NameAr = "الشارف", StateId = "17" },
                    new Municipality { Name = "Dar Chioukh", NameAr = "دار الشيوخ", StateId = "17" },
                    new Municipality { Name = "Deldoul", NameAr = "دلدول", StateId = "17" },
                    new Municipality { Name = "El Guedid", NameAr = "القديد", StateId = "17" },
                    new Municipality { Name = "El Idrissia", NameAr = "الإدريسية", StateId = "17" },
                    new Municipality { Name = "El Khemis", NameAr = "الخميس", StateId = "17" },
                    new Municipality { Name = "Faidh El Botma", NameAr = "فيض البطمة", StateId = "17" },
                    new Municipality { Name = "Guernini", NameAr = "قرنيني", StateId = "17" },
                    new Municipality { Name = "Had Sahary", NameAr = "حد الصحاري", StateId = "17" },
                    new Municipality { Name = "Hassi Bahbah", NameAr = "حاسي بحبح", StateId = "17" },
                    new Municipality { Name = "Hassi El Euch", NameAr = "حاسي العش", StateId = "17" },
                    new Municipality { Name = "Hassi Fedoul", NameAr = "حاسي فدول", StateId = "17" },
                    new Municipality { Name = "Messaad", NameAr = "مسعد", StateId = "17" },
                    new Municipality { Name = "M'Liliha", NameAr = "مليليحة", StateId = "17" },
                    new Municipality { Name = "Moudjebara", NameAr = "مجبارة", StateId = "17" },
                    new Municipality { Name = "Oum Laadham", NameAr = "أم العظام", StateId = "17" },
                    new Municipality { Name = "Sed Rahal", NameAr = "سد رحال", StateId = "17" },
                    new Municipality { Name = "Selmana", NameAr = "سلمانة", StateId = "17" },
                    new Municipality { Name = "Sidi Baizid", NameAr = "سيدي بايزيد", StateId = "17" },
                    new Municipality { Name = "Sidi Ladjel", NameAr = "سيدي لعجال", StateId = "17" },
                    new Municipality { Name = "Tadmit", NameAr = "تعظميت", StateId = "17" },
                    new Municipality { Name = "Zaafrane", NameAr = "زعفران", StateId = "17" },
                    new Municipality { Name = "Zaccar", NameAr = "زكار", StateId = "17" },
                    new Municipality { Name = "Ain Chouhada", NameAr = "عين الشهداء", StateId = "17" },
                    new Municipality { Name = "Douis", NameAr = "دويس", StateId = "17" },
                    new Municipality { Name = "Ain Feka", NameAr = "عين فقه", StateId = "17" },
                    new Municipality { Name = "Amourah", NameAr = "عمورة", StateId = "17" },
                    new Municipality { Name = "El Gahra", NameAr = "القهرة", StateId = "17" },
                    new Municipality { Name = "Sidi Laadjel", NameAr = "سيدي لعجال", StateId = "17" },
                    new Municipality { Name = "El Maalba", NameAr = "المعالبة", StateId = "17" },

                    // Jijel (18)
                    new Municipality { Name = "Jijel", NameAr = "جيجل", StateId = "18" },
                    new Municipality { Name = "Eraguene", NameAr = "أراقن", StateId = "18" },
                    new Municipality { Name = "El Aouana", NameAr = "العوانة", StateId = "18" },
                    new Municipality { Name = "Ziama Mansouriah", NameAr = "زيامة منصورية", StateId = "18" },
                    new Municipality { Name = "Taher", NameAr = "الطاهير", StateId = "18" },
                    new Municipality { Name = "Emir Abdelkader", NameAr = "الأمير عبد القادر", StateId = "18" },
                    new Municipality { Name = "Chekfa", NameAr = "الشقفة", StateId = "18" },
                    new Municipality { Name = "Chahna", NameAr = "الشحنة", StateId = "18" },
                    new Municipality { Name = "El Milia", NameAr = "الميلية", StateId = "18" },
                    new Municipality { Name = "Sidi Marouf", NameAr = "سيدي معروف", StateId = "18" },
                    new Municipality { Name = "Settara", NameAr = "السطارة", StateId = "18" },
                    new Municipality { Name = "El Ancer", NameAr = "العنصر", StateId = "18" },
                    new Municipality { Name = "Sidi Abdelaziz", NameAr = "سيدي عبد العزيز", StateId = "18" },
                    new Municipality { Name = "Kaous", NameAr = "قاوس", StateId = "18" },
                    new Municipality { Name = "Ghebala", NameAr = "غبالة", StateId = "18" },
                    new Municipality { Name = "Bouraoui Belhadef", NameAr = "بوراوي بلهادف", StateId = "18" },
                    new Municipality { Name = "Djimla", NameAr = "جيملة", StateId = "18" },
                    new Municipality { Name = "Selma Benziada", NameAr = "سلمى بن زيادة", StateId = "18" },
                    new Municipality { Name = "Boucif Ouled Askeur", NameAr = "بوسيف أولاد عسكر", StateId = "18" },
                    new Municipality { Name = "El Kennar Nouchfi", NameAr = "القنار نوشفي", StateId = "18" },
                    new Municipality { Name = "Ouled Yahia Khedrouche", NameAr = "أولاد يحيى خدروش", StateId = "18" },
                    new Municipality { Name = "Djemaa Beni Habibi", NameAr = "جمعة بني حبيبي", StateId = "18" },
                    new Municipality { Name = "Bordj Taher", NameAr = "برج الطهر", StateId = "18" },
                    new Municipality { Name = "Ouled Rabah", NameAr = "أولاد رابح", StateId = "18" },
                    new Municipality { Name = "Texenna", NameAr = "تاكسنة", StateId = "18" },
                    new Municipality { Name = "Kaous", NameAr = "قاوس", StateId = "18" },
                    new Municipality { Name = "Djemaa Beni Habibi", NameAr = "جمعة بني حبيبي", StateId = "18" },
                    new Municipality { Name = "Boudriaa Ben Yadjis", NameAr = "بودريعة بني  ياجيس", StateId = "18" },
                    
                    // Sétif (19)
                    new Municipality { Name = "Sétif", NameAr = "سطيف", StateId = "19" },
                    new Municipality { Name = "Ain El Kebira", NameAr = "عين الكبيرة", StateId = "19" },
                    new Municipality { Name = "Bougaa", NameAr = "بوقاعة", StateId = "19" },
                    new Municipality { Name = "El Eulma", NameAr = "العلمة", StateId = "19" },
                    new Municipality { Name = "Ain Oulmene", NameAr = "عين ولمان", StateId = "19" },
                    new Municipality { Name = "Djemila", NameAr = "جميلة", StateId = "19" },
                    new Municipality { Name = "Ain Arnat", NameAr = "عين أرنات", StateId = "19" },
                    new Municipality { Name = "Ain Azel", NameAr = "عين أزال", StateId = "19" },
                    new Municipality { Name = "Guenzet", NameAr = "قنزات", StateId = "19" },
                    new Municipality { Name = "Beni Aziz", NameAr = "بني عزيز", StateId = "19" },
                    new Municipality { Name = "Beni Ouartilane", NameAr = "بني ورتيلان", StateId = "19" },
                    new Municipality { Name = "Ouled Saber", NameAr = "أولاد صابر", StateId = "19" },
                    new Municipality { Name = "Guidjel", NameAr = "قجال", StateId = "19" },
                    new Municipality { Name = "Ouled Addouane", NameAr = "أولاد عدوان", StateId = "19" },
                    new Municipality { Name = "Belaa", NameAr = "بلاعة", StateId = "19" },
                    new Municipality { Name = "Ain Legradj", NameAr = "عين لقراج", StateId = "19" },
                    new Municipality { Name = "Beni Chebana", NameAr = "بني شبانة", StateId = "19" },
                    new Municipality { Name = "Beni Mouhli", NameAr = "بني موحلي", StateId = "19" },
                    new Municipality { Name = "Ain Roua", NameAr = "عين الروى", StateId = "19" },
                    new Municipality { Name = "Draa Kebila", NameAr = "ذراع قبيلة", StateId = "19" },
                    new Municipality { Name = "Bir Haddada", NameAr = "بئر حدادة", StateId = "19" },
                    new Municipality { Name = "Babor", NameAr = "بابور", StateId = "19" },
                    new Municipality { Name = "Serdj El Ghoul", NameAr = "سرج الغول", StateId = "19" },
                    new Municipality { Name = "El Ouricia", NameAr = "أوريسيا", StateId = "19" },
                    new Municipality { Name = "Tizi N'Bechar", NameAr = "تيزي نبشار", StateId = "19" },
                    new Municipality { Name = "Salah Bey", NameAr = "صالح باي", StateId = "19" },
                    new Municipality { Name = "Ain Sebt", NameAr = "عين السبت", StateId = "19" },
                    new Municipality { Name = "Amoucha", NameAr = "عموشة", StateId = "19" },
                    new Municipality { Name = "Ain Abessa", NameAr = "عين عباسة", StateId = "19" },
                    new Municipality { Name = "Maouklane", NameAr = "ماوكلان", StateId = "19" },
                    new Municipality { Name = "Tala Ifacene", NameAr = "تالة إيفاسن", StateId = "19" },
                    new Municipality { Name = "Bousselam", NameAr = "بوسلام", StateId = "19" },
                    new Municipality { Name = "El Ouldja", NameAr = "الولجة", StateId = "19" },
                    new Municipality { Name = "Taya", NameAr = "الطاية", StateId = "19" },
                    new Municipality { Name = "Hammam Guergour", NameAr = "حمام قرقور", StateId = "19" },
                    new Municipality { Name = "Ait Naoual Mezada", NameAr = "آيت نوال مزادة", StateId = "19" },
                    new Municipality { Name = "Ait Tizi", NameAr = "آيت تيزي", StateId = "19" },
                    new Municipality { Name = "Boutaleb", NameAr = "بوطالب", StateId = "19" },
                    new Municipality { Name = "Ain Lahdjar", NameAr = "عين الحجر", StateId = "19" },
                    new Municipality { Name = "Ksar El Abtal", NameAr = "قصر الأبطال", StateId = "19" },
                    new Municipality { Name = "El Rasfa", NameAr = "الرصفة", StateId = "19" },
                    new Municipality { Name = "Ain Legraj", NameAr = "عين لقراج", StateId = "19" },
                    new Municipality { Name = "Beidha Bordj", NameAr = "بيضاء برج", StateId = "19" },
                    new Municipality { Name = "Bouandas", NameAr = "بوعنداس", StateId = "19" },
                    new Municipality { Name = "Bazer Sakhra", NameAr = "بازر سكرة", StateId = "19" },
                    new Municipality { Name = "Djemila", NameAr = "جميلة", StateId = "19" },
                    new Municipality { Name = "Tachouda", NameAr = "تاشودة", StateId = "19" },
                    new Municipality { Name = "El Hammadia", NameAr = "الحمادية", StateId = "19" },
                    new Municipality { Name = "Maaouia", NameAr = "معاوية", StateId = "19" },
                    new Municipality { Name = "Oued El Bared", NameAr = "واد البارد", StateId = "19" },
                    new Municipality { Name = "Harbil", NameAr = "حربيل", StateId = "19" },
                    
                    // Saïda (20)
                    new Municipality { Name = "Saïda", NameAr = "سعيدة", StateId = "20" },
                    new Municipality { Name = "Doui Thabet", NameAr = "دوي ثابت", StateId = "20" },
                    new Municipality { Name = "Aïn El Hadjar", NameAr = "عين الحجر", StateId = "20" },
                    new Municipality { Name = "Ouled Khaled", NameAr = "أولاد خالد", StateId = "20" },
                    new Municipality { Name = "Moulay Larbi", NameAr = "مولاي العربي", StateId = "20" },
                    new Municipality { Name = "Youb", NameAr = "يوب", StateId = "20" },
                    new Municipality { Name = "Hounet", NameAr = "هونت", StateId = "20" },
                    new Municipality { Name = "Sidi Amar", NameAr = "سيدي عمر", StateId = "20" },
                    new Municipality { Name = "Sidi Boubekeur", NameAr = "سيدي بوبكر", StateId = "20" },
                    new Municipality { Name = "El Hassasna", NameAr = "الحساسنة", StateId = "20" },
                    new Municipality { Name = "Maamora", NameAr = "المعمورة", StateId = "20" },
                    new Municipality { Name = "Sidi Ahmed", NameAr = "سيدي أحمد", StateId = "20" },
                    new Municipality { Name = "Aïn Sekhouna", NameAr = "عين السخونة", StateId = "20" },
                    new Municipality { Name = "Ouled Brahim", NameAr = "أولاد براهيم", StateId = "20" },
                    new Municipality { Name = "Tircine", NameAr = "تيرسين", StateId = "20" },
                    new Municipality { Name = "Aïn Soltane", NameAr = "عين السلطان", StateId = "20" },

                    // Skikda (21)
                    new Municipality { Name = "Skikda", NameAr = "سكيكدة", StateId = "21" },
                    new Municipality { Name = "Azzaba", NameAr = "عزابة", StateId = "21" },
                    new Municipality { Name = "El Hadaiek", NameAr = "الحدائق", StateId = "21" },
                    new Municipality { Name = "Ramdane Djamel", NameAr = "رمضان جمال", StateId = "21" },
                    new Municipality { Name = "Collo", NameAr = "القل", StateId = "21" },
                    new Municipality { Name = "Tamalous", NameAr = "تمالوس", StateId = "21" },
                    new Municipality { Name = "Ain Kechra", NameAr = "عين قشرة", StateId = "21" },
                    new Municipality { Name = "Oum Toub", NameAr = "أم الطوب", StateId = "21" },
                    new Municipality { Name = "Benazouz", NameAr = "بن عزوز", StateId = "21" },
                    new Municipality { Name = "El Harrouch", NameAr = "الحروش", StateId = "21" },
                    new Municipality { Name = "Zitouna", NameAr = "الزيتونة", StateId = "21" },
                    new Municipality { Name = "Ouled Attia", NameAr = "أولاد عطية", StateId = "21" },
                    new Municipality { Name = "Oued Zhour", NameAr = "وادي الزهور", StateId = "21" },
                    new Municipality { Name = "Emjez Edchich", NameAr = "مجاز الدشيش", StateId = "21" },
                    new Municipality { Name = "Ain Charchar", NameAr = "عين شرشار", StateId = "21" },
                    new Municipality { Name = "Bekkouche Lakhdar", NameAr = "بكوش لخضر", StateId = "21" },
                    new Municipality { Name = "Ben El Ouiden", NameAr = "بني ولدان", StateId = "21" },
                    new Municipality { Name = "Bin El Ouiden", NameAr = "بين الويدان", StateId = "21" },
                    new Municipality { Name = "Bouchtata", NameAr = "بوشطاطة", StateId = "21" },
                    new Municipality { Name = "Cheraia", NameAr = "الشرايع", StateId = "21" },
                    new Municipality { Name = "El Ghedir", NameAr = "الغدير", StateId = "21" },
                    new Municipality { Name = "El Marsa", NameAr = "المرسى", StateId = "21" },
                    new Municipality { Name = "Filfila", NameAr = "فلفلة", StateId = "21" },
                    new Municipality { Name = "Hammadi Krouma", NameAr = "حمادي كرومة", StateId = "21" },
                    new Municipality { Name = "Kanoua", NameAr = "قنواع", StateId = "21" },
                    new Municipality { Name = "Kerkera", NameAr = "كركرة", StateId = "21" },
                    new Municipality { Name = "Khenag Maoune", NameAr = "خناق مايون", StateId = "21" },
                    new Municipality { Name = "Ouled Hbaba", NameAr = "أولاد حبابة", StateId = "21" },
                    new Municipality { Name = "Salah Bouchaour", NameAr = "صالح بوالشعور", StateId = "21" },
                    new Municipality { Name = "Sidi Mezghiche", NameAr = "سيدي مزغيش", StateId = "21" },
                    new Municipality { Name = "Ain Bouziane", NameAr = "عين بوزيان", StateId = "21" },
                    new Municipality { Name = "Beni Zid", NameAr = "بني زيد", StateId = "21" },
                    new Municipality { Name = "Es Sebt", NameAr = "السبت", StateId = "21" },

                    // Sidi Bel Abbès (22)
                    new Municipality { Name = "Sidi Bel Abbès", NameAr = "سيدي بلعباس", StateId = "22" },
                    new Municipality { Name = "Tessala", NameAr = "تسالة", StateId = "22" },
                    new Municipality { Name = "Sidi Brahim", NameAr = "سيدي ابراهيم", StateId = "22" },
                    new Municipality { Name = "Mostefa Ben Brahim", NameAr = "مصطفى بن ابراهيم", StateId = "22" },
                    new Municipality { Name = "Telagh", NameAr = "تلاغ", StateId = "22" },
                    new Municipality { Name = "Sidi Ali Boussidi", NameAr = "سيدي علي بوسيدي", StateId = "22" },
                    new Municipality { Name = "Marhoum", NameAr = "مرحوم", StateId = "22" },
                    new Municipality { Name = "Sidi Ali Ben Youb", NameAr = "سيدي علي بن يوب", StateId = "22" },
                    new Municipality { Name = "Sfisef", NameAr = "سفيزف", StateId = "22" },
                    new Municipality { Name = "Ain El Berd", NameAr = "عين البرد", StateId = "22" },
                    new Municipality { Name = "Sidi Lahcene", NameAr = "سيدي لحسن", StateId = "22" },
                    new Municipality { Name = "Sidi Yacoub", NameAr = "سيدي يعقوب", StateId = "22" },
                    new Municipality { Name = "Sidi Hamadouche", NameAr = "سيدي حمادوش", StateId = "22" },
                    new Municipality { Name = "Sidi Chaib", NameAr = "سيدي شعيب", StateId = "22" },
                    new Municipality { Name = "Ain Thrid", NameAr = "عين الثريد", StateId = "22" },
                    new Municipality { Name = "Makedra", NameAr = "مكدرة", StateId = "22" },
                    new Municipality { Name = "Tenira", NameAr = "تنيرة", StateId = "22" },
                    new Municipality { Name = "Moulay Slissen", NameAr = "مولاي سليسن", StateId = "22" },
                    new Municipality { Name = "Ben Badis", NameAr = "بن باديس", StateId = "22" },
                    new Municipality { Name = "Hassi Zahana", NameAr = "حاسي زهانة", StateId = "22" },

                    // Annaba (23)
                    new Municipality { Name = "Annaba", NameAr = "عنابة", StateId = "23" },
                    new Municipality { Name = "Berrahal", NameAr = "برحال", StateId = "23" },
                    new Municipality { Name = "El Bouni", NameAr = "البوني", StateId = "23" },
                    new Municipality { Name = "El Hadjar", NameAr = "الحجار", StateId = "23" },
                    new Municipality { Name = "Eulma", NameAr = "العلمة", StateId = "23" },
                    new Municipality { Name = "Chetaibi", NameAr = "شطايبي", StateId = "23" },
                    new Municipality { Name = "Seraidi", NameAr = "سرايدي", StateId = "23" },
                    new Municipality { Name = "Ain Berda", NameAr = "عين الباردة", StateId = "23" },
                    new Municipality { Name = "Oued El Aneb", NameAr = "وادي العنب", StateId = "23" },
                    new Municipality { Name = "Cheurfa", NameAr = "الشرفة", StateId = "23" },
                    new Municipality { Name = "Treat", NameAr = "التريعات", StateId = "23" },
                    new Municipality { Name = "Sidi Amar", NameAr = "سيدي عمار", StateId = "23" },

                    // Guelma (24)
                    new Municipality { Name = "Guelma", NameAr = "قالمة", StateId = "24" },
                    new Municipality { Name = "Ain Ben Beida", NameAr = "عين بن بيضاء", StateId = "24" },
                    new Municipality { Name = "Ain Larbi", NameAr = "عين العربي", StateId = "24" },
                    new Municipality { Name = "Ain Makhlouf", NameAr = "عين مخلوف", StateId = "24" },
                    new Municipality { Name = "Ain Reggada", NameAr = "عين رقادة", StateId = "24" },
                    new Municipality { Name = "Ain Sandel", NameAr = "عين صندل", StateId = "24" },
                    new Municipality { Name = "Belkheir", NameAr = "بلخير", StateId = "24" },
                    new Municipality { Name = "Bendjarah", NameAr = "بن جراح", StateId = "24" },
                    new Municipality { Name = "Beni Mezline", NameAr = "بني مزلين", StateId = "24" },
                    new Municipality { Name = "Bordj Sabath", NameAr = "برج صباط", StateId = "24" },
                    new Municipality { Name = "Bouati Mahmoud", NameAr = "بوعاتي محمود", StateId = "24" },
                    new Municipality { Name = "Bouchegouf", NameAr = "بوشقوف", StateId = "24" },
                    new Municipality { Name = "Bouhamdane", NameAr = "بوحمدان", StateId = "24" },
                    new Municipality { Name = "Dahouara", NameAr = "الدهوارة", StateId = "24" },
                    new Municipality { Name = "Djeballah Khemissi", NameAr = "جبالة الخميسي", StateId = "24" },
                    new Municipality { Name = "El Fedjoudj", NameAr = "الفجوج", StateId = "24" },
                    new Municipality { Name = "Hammam Debagh", NameAr = "حمام دباغ", StateId = "24" },
                    new Municipality { Name = "Hammam N'Bail", NameAr = "حمام النبايل", StateId = "24" },
                    new Municipality { Name = "Heliopolis", NameAr = "هيليوبوليس", StateId = "24" },
                    new Municipality { Name = "Houari Boumediene", NameAr = "هواري بومدين", StateId = "24" },
                    new Municipality { Name = "Khezarra", NameAr = "لخزارة", StateId = "24" },
                    new Municipality { Name = "Medjez Amar", NameAr = "مجاز عمار", StateId = "24" },
                    new Municipality { Name = "Medjez Sfa", NameAr = "مجاز الصفاء", StateId = "24" },
                    new Municipality { Name = "Nechmaya", NameAr = "نشماية", StateId = "24" },
                    new Municipality { Name = "Oued Cheham", NameAr = "وادي الشحم", StateId = "24" },
                    new Municipality { Name = "Oued Fragha", NameAr = "وادي فراغة", StateId = "24" },
                    new Municipality { Name = "Oued Zenati", NameAr = "وادي الزناتي", StateId = "24" },
                    new Municipality { Name = "Ras El Agba", NameAr = "رأس العقبة", StateId = "24" },
                    new Municipality { Name = "Roknia", NameAr = "الركنية", StateId = "24" },
                    new Municipality { Name = "Sellaoua Announa", NameAr = "سلاوة عنونة", StateId = "24" },
                    new Municipality { Name = "Tamlouka", NameAr = "تاملوكة", StateId = "24" },
                    new Municipality { Name = "Taya", NameAr = "الطاية", StateId = "24" },
                    new Municipality { Name = "Boumahra Ahmed", NameAr = "بومهرة أحمد", StateId = "24" },
                    new Municipality { Name = "Guelaat Bou Sbaa", NameAr = "قلعة بوصبع", StateId = "24" },

// Constantine (25)
new Municipality { Name = "Constantine", NameAr = "قسنطينة", StateId = "25" },
new Municipality { Name = "Hamma Bouziane", NameAr = "حامة بوزيان", StateId = "25" },
new Municipality { Name = "El Khroub", NameAr = "الخروب", StateId = "25" },
new Municipality { Name = "Aïn Smara", NameAr = "عين سمارة", StateId = "25" },
new Municipality { Name = "Didouche Mourad", NameAr = "ديدوش مراد", StateId = "25" },
new Municipality { Name = "Ibn Ziad", NameAr = "ابن زياد", StateId = "25" },
new Municipality { Name = "Zighoud Youcef", NameAr = "زيغود يوسف", StateId = "25" },
new Municipality { Name = "Beni Hamiden", NameAr = "بني حميدان", StateId = "25" },
new Municipality { Name = "Ouled Rahmoune", NameAr = "أولاد رحمون", StateId = "25" },
new Municipality { Name = "Ain Abid", NameAr = "عين عبيد", StateId = "25" },
new Municipality { Name = "Messaoud Boudjeriou", NameAr = "مسعود بوجريو", StateId = "25" },
new Municipality { Name = "Ibn Badis", NameAr = "ابن باديس", StateId = "25" },

// Médéa (26)
new Municipality { Name = "Médéa", NameAr = "المدية", StateId = "26" },
new Municipality { Name = "Ouzera", NameAr = "وزرة", StateId = "26" },
new Municipality { Name = "Ain Boucif", NameAr = "عين بوسيف", StateId = "26" },
new Municipality { Name = "Ouamri", NameAr = "عوامري", StateId = "26" },
new Municipality { Name = "El Omaria", NameAr = "العمارية", StateId = "26" },
new Municipality { Name = "Draa Smar", NameAr = "ذراع السمار", StateId = "26" },
new Municipality { Name = "Ouled Antar", NameAr = "أولاد عنتر", StateId = "26" },
new Municipality { Name = "Ain Bensultan", NameAr = "عين بن سلطان", StateId = "26" },
new Municipality { Name = "Si Mahdjoub", NameAr = "سي المحجوب", StateId = "26" },
new Municipality { Name = "Ksar El Boukhari", NameAr = "قصر البخاري", StateId = "26" },
new Municipality { Name = "El Azizia", NameAr = "العزيزية", StateId = "26" },
new Municipality { Name = "Tablat", NameAr = "تابلاط", StateId = "26" },
new Municipality { Name = "El Hamdania", NameAr = "الحمدانية", StateId = "26" },
new Municipality { Name = "Beni Slimane", NameAr = "بني سليمان", StateId = "26" },
new Municipality { Name = "Berrouaghia", NameAr = "البرواقية", StateId = "26" },
new Municipality { Name = "Seghouane", NameAr = "سغوان", StateId = "26" },
new Municipality { Name = "Mezghena", NameAr = "مزغنة", StateId = "26" },
new Municipality { Name = "Bouaichoune", NameAr = "بوعيشون", StateId = "26" },
new Municipality { Name = "Sidi Naamane", NameAr = "سيدي نعمان", StateId = "26" },
new Municipality { Name = "Boghar", NameAr = "بوغار", StateId = "26" },
new Municipality { Name = "Ouled Bouachra", NameAr = "أولاد بوعشرة", StateId = "26" },
new Municipality { Name = "Sidi Zahar", NameAr = "سيدي زهار", StateId = "26" },
new Municipality { Name = "Oued Harbil", NameAr = "واد حربيل", StateId = "26" },
new Municipality { Name = "Bouaiche", NameAr = "بوعيش", StateId = "26" },
new Municipality { Name = "Chahbounia", NameAr = "شهبونية", StateId = "26" },


// Mostaganem (27)
new Municipality { Name = "Mostaganem", NameAr = "مستغانم", StateId = "27" },
new Municipality { Name = "Ain Nouissy", NameAr = "عين نويسي", StateId = "27" },
new Municipality { Name = "Hassi Mameche", NameAr = "حاسي ماماش", StateId = "27" },
new Municipality { Name = "Ain Tedles", NameAr = "عين تادلس", StateId = "27" },
new Municipality { Name = "Sour", NameAr = "سور", StateId = "27" },
new Municipality { Name = "Oued El Kheir", NameAr = "وادي الخير", StateId = "27" },
new Municipality { Name = "Sidi Belattar", NameAr = "سيدي بلعطار", StateId = "27" },
new Municipality { Name = "Kheir Eddine", NameAr = "خير الدين", StateId = "27" },
new Municipality { Name = "Sayada", NameAr = "صيادة", StateId = "27" },
new Municipality { Name = "Fornaka", NameAr = "فرناقة", StateId = "27" },
new Municipality { Name = "Stidia", NameAr = "ستيدية", StateId = "27" },
new Municipality { Name = "Ain Boudinar", NameAr = "عين بودينار", StateId = "27" },
new Municipality { Name = "Tazgait", NameAr = "تزقايت", StateId = "27" },
new Municipality { Name = "Sidi Ali", NameAr = "سيدي علي", StateId = "27" },
new Municipality { Name = "Sidi Lakhdar", NameAr = "سيدي لخضر", StateId = "27" },
new Municipality { Name = "Hadjadj", NameAr = "حجاج", StateId = "27" },
new Municipality { Name = "Benabdelmalek Ramdane", NameAr = "بن عبد المالك رمضان", StateId = "27" },
new Municipality { Name = "Achaacha", NameAr = "عشعاشة", StateId = "27" },
new Municipality { Name = "Khadra", NameAr = "خضرة", StateId = "27" },
new Municipality { Name = "Nekmaria", NameAr = "نكمارية", StateId = "27" },
new Municipality { Name = "Sidi Bellater", NameAr = "سيدي بلاتر", StateId = "27" },
new Municipality { Name = "Ouled Boughalem", NameAr = "أولاد بوغالم", StateId = "27" },
new Municipality { Name = "Ouled Maallah", NameAr = "أولاد مع الله", StateId = "27" },
new Municipality { Name = "Mezghrane", NameAr = "مزغران", StateId = "27" },
new Municipality { Name = "Bouguirat", NameAr = "بوقيراط", StateId = "27" },
new Municipality { Name = "Sirat", NameAr = "سيرات", StateId = "27" },
new Municipality { Name = "Souaflia", NameAr = "السوافلية", StateId = "27" },
new Municipality { Name = "Safsaf", NameAr = "صفصاف", StateId = "27" },
new Municipality { Name = "Mansourah", NameAr = "منصورة", StateId = "27" },
new Municipality { Name = "Touahria", NameAr = "الطواهرية", StateId = "27" },
new Municipality { Name = "El Hassiane", NameAr = "الحسيان", StateId = "27" },
new Municipality { Name = "Ain Sidi Cherif", NameAr = "عين سيدي شريف", StateId = "27" },

// M'Sila (28)
new Municipality { Name = "M'Sila", NameAr = "المسيلة", StateId = "28" },
new Municipality { Name = "Hammam Dalaa", NameAr = "حمام الضلعة", StateId = "28" },
new Municipality { Name = "Ouled Derradj", NameAr = "أولاد دراج", StateId = "28" },
new Municipality { Name = "Chellal", NameAr = "شلال", StateId = "28" },
new Municipality { Name = "Magra", NameAr = "مقرة", StateId = "28" },
new Municipality { Name = "Berhoum", NameAr = "برهوم", StateId = "28" },
new Municipality { Name = "Ain El Hadjel", NameAr = "عين الحجل", StateId = "28" },
new Municipality { Name = "Sidi Aissa", NameAr = "سيدي عيسى", StateId = "28" },
new Municipality { Name = "Ben Srour", NameAr = "بن سرور", StateId = "28" },
new Municipality { Name = "Bou Saada", NameAr = "بوسعادة", StateId = "28" },
new Municipality { Name = "Ouled Sidi Brahim", NameAr = "أولاد سيدي ابراهيم", StateId = "28" },
new Municipality { Name = "Sidi Ameur", NameAr = "سيدي عامر", StateId = "28" },
new Municipality { Name = "Tamsa", NameAr = "تامسة", StateId = "28" },
new Municipality { Name = "Ain El Melh", NameAr = "عين الملح", StateId = "28" },
new Municipality { Name = "Medjedel", NameAr = "امجدل", StateId = "28" },
new Municipality { Name = "Ain Errich", NameAr = "عين الريش", StateId = "28" },
new Municipality { Name = "Ain Fares", NameAr = "عين فارس", StateId = "28" },
new Municipality { Name = "Sidi M'Hamed", NameAr = "سيدي امحمد", StateId = "28" },
new Municipality { Name = "Ouanougha", NameAr = "ونوغة", StateId = "28" },
new Municipality { Name = "Bou Saada", NameAr = "بوسعادة", StateId = "28" },
new Municipality { Name = "Ouled Mansour", NameAr = "أولاد منصور", StateId = "28" },
new Municipality { Name = "Maarif", NameAr = "معاريف", StateId = "28" },
new Municipality { Name = "Dehahna", NameAr = "دهاهنة", StateId = "28" },
new Municipality { Name = "Bouti Sayah", NameAr = "بوطي السايح", StateId = "28" },
new Municipality { Name = "Khoubana", NameAr = "خبانة", StateId = "28" },
new Municipality { Name = "M'Tarfa", NameAr = "المطارفة", StateId = "28" },
new Municipality { Name = "M'Cif", NameAr = "مسيف", StateId = "28" },
new Municipality { Name = "Zarzour", NameAr = "زرزور", StateId = "28" },
new Municipality { Name = "Ouled Atia", NameAr = "أولاد عطية", StateId = "28" },
new Municipality { Name = "Belaiba", NameAr = "بلعايبة", StateId = "28" },
new Municipality { Name = "Ben Zouh", NameAr = "بن زوه", StateId = "28" },
new Municipality { Name = "Ain El Khadra", NameAr = "عين الخضراء", StateId = "28" },
new Municipality { Name = "Ouled Slimane", NameAr = "أولاد سليمان", StateId = "28" },
new Municipality { Name = "El Houamed", NameAr = "الحوامد", StateId = "28" },
new Municipality { Name = "El Hamel", NameAr = "الهامل", StateId = "28" },
new Municipality { Name = "Ouled Madhi", NameAr = "أولاد ماضي", StateId = "28" },
new Municipality { Name = "Souamaa", NameAr = "السوامع", StateId = "28" },
new Municipality { Name = "Ain El Melh", NameAr = "عين الملح", StateId = "28" },
new Municipality { Name = "Sidi Hadjeres", NameAr = "سيدي هجرس", StateId = "28" },
new Municipality { Name = "Tarmount", NameAr = "تارمونت", StateId = "28" },
new Municipality { Name = "Mohammed Boudiaf", NameAr = "محمد بوضياف", StateId = "28" },
new Municipality { Name = "Benzouh", NameAr = "بن زوه", StateId = "28" },
new Municipality { Name = "Bir Foda", NameAr = "بئر فضة", StateId = "28" },
new Municipality { Name = "Ain Fares", NameAr = "عين فارس", StateId = "28" },
new Municipality { Name = "Sidi Ameur", NameAr = "سيدي عامر", StateId = "28" },
new Municipality { Name = "Ouled Addi Guebala", NameAr = "أولاد عدي لقبالة", StateId = "28" },

// Mascara (29)
new Municipality { Name = "Mascara", NameAr = "معسكر", StateId = "29" },
new Municipality { Name = "Bouhanifia", NameAr = "بوحنيفية", StateId = "29" },
new Municipality { Name = "Ghriss", NameAr = "غريس", StateId = "29" },
new Municipality { Name = "El Hachem", NameAr = "الحشم", StateId = "29" },
new Municipality { Name = "Maoussa", NameAr = "ماوسة", StateId = "29" },
new Municipality { Name = "Tighennif", NameAr = "تيغنيف", StateId = "29" },
new Municipality { Name = "El Keurt", NameAr = "القرط", StateId = "29" },
new Municipality { Name = "Oued El Abtal", NameAr = "وادي الأبطال", StateId = "29" },
new Municipality { Name = "Ain Fekan", NameAr = "عين فكان", StateId = "29" },
new Municipality { Name = "Ain Fares", NameAr = "عين فارس", StateId = "29" },
new Municipality { Name = "Froha", NameAr = "فروحة", StateId = "29" },
new Municipality { Name = "Matemore", NameAr = "المطمور", StateId = "29" },
new Municipality { Name = "Makdha", NameAr = "مقدة", StateId = "29" },
new Municipality { Name = "Sidi Kada", NameAr = "سيدي قادة", StateId = "29" },
new Municipality { Name = "Zahana", NameAr = "زهانة", StateId = "29" },
new Municipality { Name = "El Menaouer", NameAr = "المنور", StateId = "29" },
new Municipality { Name = "El Bordj", NameAr = "البرج", StateId = "29" },
new Municipality { Name = "Khalouia", NameAr = "خلوية", StateId = "29" },
new Municipality { Name = "El Ghomri", NameAr = "الغمري", StateId = "29" },
new Municipality { Name = "Sehailia", NameAr = "السهايلية", StateId = "29" },
new Municipality { Name = "Nesmoth", NameAr = "نسموط", StateId = "29" },
new Municipality { Name = "Sidi Boussaid", NameAr = "سيدي بوسعيد", StateId = "29" },
new Municipality { Name = "Benian", NameAr = "بنيان", StateId = "29" },
new Municipality { Name = "Ain Ferah", NameAr = "عين فراح", StateId = "29" },
new Municipality { Name = "Oggaz", NameAr = "عقاز", StateId = "29" },
new Municipality { Name = "Alaimia", NameAr = "العلايمية", StateId = "29" },
new Municipality { Name = "El Gueitena", NameAr = "القطنة", StateId = "29" },
new Municipality { Name = "Sidi Abdelmoumen", NameAr = "سيدي عبد المؤمن", StateId = "29" },
new Municipality { Name = "Mocta Douz", NameAr = "مقطع الدوز", StateId = "29" },
new Municipality { Name = "Ferraguig", NameAr = "فراقيق", StateId = "29" },
new Municipality { Name = "Chorfa", NameAr = "الشرفاء", StateId = "29" },
new Municipality { Name = "Ras El Ain Amirouche", NameAr = "رأس عين عميروش", StateId = "29" },
new Municipality { Name = "Sedjerara", NameAr = "سجرارة", StateId = "29" },
new Municipality { Name = "Mamounia", NameAr = "المأمونية", StateId = "29" },
new Municipality { Name = "Oued Taria", NameAr = "وادي التاغية", StateId = "29" },
new Municipality { Name = "Zelamta", NameAr = "زلامطة", StateId = "29" },
new Municipality { Name = "Hacine", NameAr = "حسين", StateId = "29" },
new Municipality { Name = "El Gaada", NameAr = "القعدة", StateId = "29" },
new Municipality { Name = "Sidi Abdeldjebar", NameAr = "سيدي عبد الجبار", StateId = "29" },
new Municipality { Name = "Guerdjoum", NameAr = "قرجوم", StateId = "29" },
new Municipality { Name = "Aouf", NameAr = "عوف", StateId = "29" },
new Municipality { Name = "Ain Frass", NameAr = "عين أفرص", StateId = "29" },
new Municipality { Name = "Sig", NameAr = "سيق", StateId = "29" },
new Municipality { Name = "Bou Henni", NameAr = "بوهني", StateId = "29" },
new Municipality { Name = "Zahana", NameAr = "زهانة", StateId = "29" },
new Municipality { Name = "El Gaada", NameAr = "القعدة", StateId = "29" },

// Ouargla (30)
new Municipality { Name = "Ouargla", NameAr = "ورقلة", StateId = "30" },
new Municipality { Name = "Rouissat", NameAr = "الرويسات", StateId = "30" },
new Municipality { Name = "Ain Beida", NameAr = "عين البيضاء", StateId = "30" },
new Municipality { Name = "N'Goussa", NameAr = "انقوسة", StateId = "30" },
new Municipality { Name = "Hassi Messaoud", NameAr = "حاسي مسعود", StateId = "30" },
new Municipality { Name = "Sidi Khouiled", NameAr = "سيدي خويلد", StateId = "30" },
new Municipality { Name = "Hassi Ben Abdellah", NameAr = "حاسي بن عبد الله", StateId = "30" },
new Municipality { Name = "El Borma", NameAr = "البرمة", StateId = "30" },
new Municipality { Name = "Zaouia El Abidia", NameAr = "الزاوية العابدية", StateId = "30" },
new Municipality { Name = "N'Goussa", NameAr = "انقوسة", StateId = "30" },


                    // Oran (31)
                    new Municipality { Name = "Oran", NameAr = "وهران", StateId = "31" },
                    new Municipality { Name = "Bir El Djir", NameAr = "بئر الجير", StateId = "31" },
                    new Municipality { Name = "Es Senia", NameAr = "السانية", StateId = "31" },
                    new Municipality { Name = "Arzew", NameAr = "أرزيو", StateId = "31" },
                    new Municipality { Name = "Bethioua", NameAr = "بطيوة", StateId = "31" },
                    new Municipality { Name = "Ain El Turk", NameAr = "عين الترك", StateId = "31" },
                    new Municipality { Name = "Oued Tlelat", NameAr = "وادي تليلات", StateId = "31" },
                    new Municipality { Name = "Gdyel", NameAr = "قديل", StateId = "31" },
                    new Municipality { Name = "Ben Freha", NameAr = "بن فريحة", StateId = "31" },
                    new Municipality { Name = "Hassi Bounif", NameAr = "حاسي بونيف", StateId = "31" },
                    new Municipality { Name = "Hassi Ben Okba", NameAr = "حاسي بن عقبة", StateId = "31" },
                    new Municipality { Name = "Sidi Chami", NameAr = "سيدي الشحمي", StateId = "31" },
                    new Municipality { Name = "Boufatis", NameAr = "بوفاتيس", StateId = "31" },
                    new Municipality { Name = "Mers El Kebir", NameAr = "المرسى الكبير", StateId = "31" },
                    new Municipality { Name = "Bousfer", NameAr = "بوسفر", StateId = "31" },
                    new Municipality { Name = "El Kerma", NameAr = "الكرمة", StateId = "31" },
                    new Municipality { Name = "El Braya", NameAr = "البراية", StateId = "31" },
                    new Municipality { Name = "Hassi Mefsoukh", NameAr = "حاسي مفسوخ", StateId = "31" },
                    new Municipality { Name = "Sidi Ben Yebka", NameAr = "سيدي بن يبقى", StateId = "31" },
                    new Municipality { Name = "Misserghin", NameAr = "مسرغين", StateId = "31" },
                    new Municipality { Name = "Boutlelis", NameAr = "بوتليليس", StateId = "31" },
                    new Municipality { Name = "Ain El Kerma", NameAr = "عين الكرمة", StateId = "31" },
                    new Municipality { Name = "Ain El Bia", NameAr = "عين البية", StateId = "31" },
                    new Municipality { Name = "Tafraoui", NameAr = "طفراوي", StateId = "31" },
                    new Municipality { Name = "Ben Freha", NameAr = "بن فريحة", StateId = "31" },
                    new Municipality { Name = "Marsat El Hadjadj", NameAr = "مرسى الحجاج", StateId = "31" },
                    

                    // El Bayadh (32)
                    new Municipality { Name = "El Bayadh", NameAr = "البيض", StateId = "32" },
                    new Municipality { Name = "Rogassa", NameAr = "رقاصة", StateId = "32" },
                    new Municipality { Name = "Stitten", NameAr = "ستيتن", StateId = "32" },
                    new Municipality { Name = "Brezina", NameAr = "بريزينة", StateId = "32" },
                    new Municipality { Name = "Ghassoul", NameAr = "الغاسول", StateId = "32" },
                    new Municipality { Name = "Boualem", NameAr = "بوعلام", StateId = "32" },
                    new Municipality { Name = "El Abiodh Sidi Cheikh", NameAr = "الأبيض سيدي الشيخ", StateId = "32" },
                    new Municipality { Name = "Ain El Orak", NameAr = "عين العراك", StateId = "32" },
                    new Municipality { Name = "Arbaouat", NameAr = "اربوات", StateId = "32" },
                    new Municipality { Name = "Bougtoub", NameAr = "بوقطب", StateId = "32" },
                    new Municipality { Name = "El Kheither", NameAr = "الخيثر", StateId = "32" },
                    new Municipality { Name = "Kef El Ahmar", NameAr = "الكاف الأحمر", StateId = "32" },
                    new Municipality { Name = "Boussemghoun", NameAr = "بوسمغون", StateId = "32" },
                    new Municipality { Name = "Chellala", NameAr = "شلالة", StateId = "32" },
                    new Municipality { Name = "Kraakda", NameAr = "كراكدة", StateId = "32" },
                    new Municipality { Name = "El Mehara", NameAr = "المحرة", StateId = "32" },
                    new Municipality { Name = "Tousmouline", NameAr = "توسمولين", StateId = "32" },
                    new Municipality { Name = "Sidi Ameur", NameAr = "سيدي عامر", StateId = "32" },
                    new Municipality { Name = "El Bnoud", NameAr = "البنود", StateId = "32" },
                    new Municipality { Name = "Cheguig", NameAr = "الشقيق", StateId = "32" },
                    new Municipality { Name = "Sidi Slimane", NameAr = "سيدي سليمان", StateId = "32" },
                    new Municipality { Name = "Sidi Tifour", NameAr = "سيدي طيفور", StateId = "32" },

                    // Illizi (33)
                    new Municipality { Name = "Illizi", NameAr = "إليزي", StateId = "33" },
                    new Municipality { Name = "Djanet", NameAr = "جانت", StateId = "33" },
                    new Municipality { Name = "Debdeb", NameAr = "دبداب", StateId = "33" },
                    new Municipality { Name = "Bordj Omar Driss", NameAr = "برج عمر إدريس", StateId = "33" },
                    new Municipality { Name = "Bordj El Houasse", NameAr = "برج الحواس", StateId = "33" },
                    new Municipality { Name = "In Amenas", NameAr = "عين أمناس", StateId = "33" },

                    // Bordj Bou Arréridj (34)
                    new Municipality { Name = "Bordj Bou Arréridj", NameAr = "برج بوعريريج", StateId = "34" },
                    new Municipality { Name = "Ras El Oued", NameAr = "رأس الوادي", StateId = "34" },
                    new Municipality { Name = "Bordj Zemoura", NameAr = "برج زمورة", StateId = "34" },
                    new Municipality { Name = "Mansoura", NameAr = "المنصورة", StateId = "34" },
                    new Municipality { Name = "El Achir", NameAr = "العشير", StateId = "34" },
                    new Municipality { Name = "Ain Taghrout", NameAr = "عين تاغروت", StateId = "34" },
                    new Municipality { Name = "Bordj Ghedir", NameAr = "برج الغدير", StateId = "34" },
                    new Municipality { Name = "Sidi Embarek", NameAr = "سيدي امبارك", StateId = "34" },
                    new Municipality { Name = "El Hamadia", NameAr = "الحمادية", StateId = "34" },
                    new Municipality { Name = "Belimour", NameAr = "بليمور", StateId = "34" },
                    new Municipality { Name = "El Main", NameAr = "الماين", StateId = "34" },
                    new Municipality { Name = "El Anseur", NameAr = "العناصر", StateId = "34" },
                    new Municipality { Name = "Tesmart", NameAr = "تسمرت", StateId = "34" },
                    new Municipality { Name = "Djaafra", NameAr = "جعافرة", StateId = "34" },
                    new Municipality { Name = "El M'hir", NameAr = "المهير", StateId = "34" },
                    new Municipality { Name = "Ain Tesra", NameAr = "عين تسرة", StateId = "34" },
                    new Municipality { Name = "Khelil", NameAr = "خليل", StateId = "34" },
                    new Municipality { Name = "Ksour", NameAr = "القصور", StateId = "34" },
                    new Municipality { Name = "Ouled Brahem", NameAr = "أولاد ابراهم", StateId = "34" },
                    new Municipality { Name = "Taglait", NameAr = "تقلعيت", StateId = "34" },
                    new Municipality { Name = "Colla", NameAr = "القلة", StateId = "34" },
                    new Municipality { Name = "Tixter", NameAr = "تيكستار", StateId = "34" },
                    new Municipality { Name = "Hasnaoua", NameAr = "حسناوة", StateId = "34" },
                    new Municipality { Name = "Medjana", NameAr = "مجانة", StateId = "34" },
                    new Municipality { Name = "Teniet En Nasr", NameAr = "ثنية النصر", StateId = "34" },
                    new Municipality { Name = "Ben Daoud", NameAr = "بن داود", StateId = "34" },
                    new Municipality { Name = "Ouled Sidi Brahim", NameAr = "أولاد سيدي ابراهيم", StateId = "34" },
                    new Municipality { Name = "Bir Kasdali", NameAr = "بئر قاصد علي", StateId = "34" },
                    new Municipality { Name = "Ghailasa", NameAr = "غيلاسة", StateId = "34" },
                    new Municipality { Name = "Rabta", NameAr = "الرابطة", StateId = "34" },
                    new Municipality { Name = "Haraza", NameAr = "حرازة", StateId = "34" },
                    new Municipality { Name = "El Euch", NameAr = "العش", StateId = "34" },
                    new Municipality { Name = "El Annasseur", NameAr = "العناصر", StateId = "34" },
                    new Municipality { Name = "Tafreg", NameAr = "تفرق", StateId = "34" },

                    // Boumerdès (35)
                    new Municipality { Name = "Boumerdès", NameAr = "بومرداس", StateId = "35" },
                    new Municipality { Name = "Boudouaou", NameAr = "بودواو", StateId = "35" },
                    new Municipality { Name = "Afir", NameAr = "أعفير", StateId = "35" },
                    new Municipality { Name = "Bordj Menaiel", NameAr = "برج منايل", StateId = "35" },
                    new Municipality { Name = "Baghlia", NameAr = "بغلية", StateId = "35" },
                    new Municipality { Name = "Sidi Daoud", NameAr = "سيدي داود", StateId = "35" },
                    new Municipality { Name = "Naciria", NameAr = "الناصرية", StateId = "35" },
                    new Municipality { Name = "Djinet", NameAr = "جنات", StateId = "35" },
                    new Municipality { Name = "Isser", NameAr = "يسر", StateId = "35" },
                    new Municipality { Name = "Zemmouri", NameAr = "زموري", StateId = "35" },
                    new Municipality { Name = "Si Mustapha", NameAr = "سي مصطفى", StateId = "35" },
                    new Municipality { Name = "Tidjelabine", NameAr = "تيجلابين", StateId = "35" },
                    new Municipality { Name = "Chabet el Ameur", NameAr = "شعبة العامر", StateId = "35" },
                    new Municipality { Name = "Thenia", NameAr = "الثنية", StateId = "35" },
                    new Municipality { Name = "Corso", NameAr = "قورصو", StateId = "35" },
                    new Municipality { Name = "Ouled Moussa", NameAr = "أولاد موسى", StateId = "35" },
                    new Municipality { Name = "Larbatache", NameAr = "الاربعطاش", StateId = "35" },
                    new Municipality { Name = "Bouzegza Keddara", NameAr = "بوزقزة قدارة", StateId = "35" },
                    new Municipality { Name = "Taourga", NameAr = "تاورقة", StateId = "35" },
                    new Municipality { Name = "Ouled Aissa", NameAr = "أولاد عيسى", StateId = "35" },
                    new Municipality { Name = "Ben Choud", NameAr = "بن شود", StateId = "35" },
                    new Municipality { Name = "Dellys", NameAr = "دلس", StateId = "35" },
                    new Municipality { Name = "Ammal", NameAr = "عمال", StateId = "35" },
                    new Municipality { Name = "Beni Amrane", NameAr = "بني عمران", StateId = "35" },
                    new Municipality { Name = "Souk El Had", NameAr = "سوق الحد", StateId = "35" },
                    new Municipality { Name = "Hammedi", NameAr = "حمادي", StateId = "35" },
                    new Municipality { Name = "Khemis El Khechna", NameAr = "خميس الخشنة", StateId = "35" },
                    new Municipality { Name = "Legata", NameAr = "لقاطة", StateId = "35" },
                    new Municipality { Name = "Boudouaou El Bahri", NameAr = "بودواو البحري", StateId = "35" },
                    new Municipality { Name = "Ouled Hedadj", NameAr = "أولاد هداج", StateId = "35" },
                    new Municipality { Name = "Leghata", NameAr = "لغاطة", StateId = "35" },
                    new Municipality { Name = "Timezrit", NameAr = "تيمزريت", StateId = "35" },


                    // El Taref (36)
                    new Municipality { Name = "El Tarf", NameAr = "الطارف", StateId = "36" },
                    new Municipality { Name = "Bouhadjar", NameAr = "بوحجار", StateId = "36" },
                    new Municipality { Name = "Ben M'Hidi", NameAr = "بن مهيدي", StateId = "36" },
                    new Municipality { Name = "Bougous", NameAr = "بوقوس", StateId = "36" },
                    new Municipality { Name = "El Kala", NameAr = "القالة", StateId = "36" },
                    new Municipality { Name = "Ain El Assel", NameAr = "عين العسل", StateId = "36" },
                    new Municipality { Name = "El Aioun", NameAr = "العيون", StateId = "36" },
                    new Municipality { Name = "Bouteldja", NameAr = "بوثلجة", StateId = "36" },
                    new Municipality { Name = "Souarekh", NameAr = "السوارخ", StateId = "36" },
                    new Municipality { Name = "Berrihane", NameAr = "بريحان", StateId = "36" },
                    new Municipality { Name = "Lac des Oiseaux", NameAr = "بحيرة الطيور", StateId = "36" },
                    new Municipality { Name = "Chefia", NameAr = "الشافية", StateId = "36" },
                    new Municipality { Name = "Drean", NameAr = "الذرعان", StateId = "36" },
                    new Municipality { Name = "Chihani", NameAr = "شحاني", StateId = "36" },
                    new Municipality { Name = "Chebaita Mokhtar", NameAr = "شبيطة مختار", StateId = "36" },
                    new Municipality { Name = "Besbes", NameAr = "البسباس", StateId = "36" },
                    new Municipality { Name = "Asfour", NameAr = "عصفور", StateId = "36" },
                    new Municipality { Name = "Echatt", NameAr = "الشط", StateId = "36" },
                    new Municipality { Name = "Zerizer", NameAr = "زريزر", StateId = "36" },
                    new Municipality { Name = "Zitouna", NameAr = "الزيتونة", StateId = "36" },
                    new Municipality { Name = "Ain Kerma", NameAr = "عين كرمة", StateId = "36" },
                    new Municipality { Name = "Oued Zitoune", NameAr = "وادي الزيتون", StateId = "36" },
                    new Municipality { Name = "Hammam Beni Salah", NameAr = "حمام بني صالح", StateId = "36" },
                    new Municipality { Name = "Raml Souk", NameAr = "رمل السوق", StateId = "36" },
                    // Tindouf (37)
                    new Municipality { Name = "Tindouf", NameAr = "تندوف", StateId = "37" },
                    new Municipality { Name = "Oum El Assel", NameAr = "أم العسل", StateId = "37" },

                    // Tissemsilt (38)
                    new Municipality { Name = "Tissemsilt", NameAr = "تيسمسيلت", StateId = "38" },
                    new Municipality { Name = "Bordj Bounaama", NameAr = "برج بونعامة", StateId = "38" },
                    new Municipality { Name = "Theniet El Had", NameAr = "ثنية الاحد", StateId = "38" },
                    new Municipality { Name = "Lazharia", NameAr = "الأزهرية", StateId = "38" },
                    new Municipality { Name = "Beni Chaib", NameAr = "بني شعيب", StateId = "38" },
                    new Municipality { Name = "Larbaa", NameAr = "الأربعاء", StateId = "38" },
                    new Municipality { Name = "Maâcem", NameAr = "المعاصم", StateId = "38" },
                    new Municipality { Name = "Sidi Lantri", NameAr = "سيدي العنتري", StateId = "38" },
                    new Municipality { Name = "Bordj El Emir Abdelkader", NameAr = "برج الأمير عبد القادر", StateId = "38" },
                    new Municipality { Name = "Youssoufia", NameAr = "اليوسفية", StateId = "38" },
                    new Municipality { Name = "Khemisti", NameAr = "خميستي", StateId = "38" },
                    new Municipality { Name = "Ouled Bessem", NameAr = "أولاد بسام", StateId = "38" },
                    new Municipality { Name = "Ammari", NameAr = "عماري", StateId = "38" },
                    new Municipality { Name = "Sidi Boutouchent", NameAr = "سيدي بوتوشنت", StateId = "38" },
                    new Municipality { Name = "Layoune", NameAr = "العيون", StateId = "38" },
                    new Municipality { Name = "Tamalaht", NameAr = "تملاحت", StateId = "38" },
                    new Municipality { Name = "Sidi Abed", NameAr = "سيدي عابد", StateId = "38" },
                    new Municipality { Name = "Boucaid", NameAr = "بوقائد", StateId = "38" },
                    new Municipality { Name = "Lardjem", NameAr = "لرجام", StateId = "38" },
                    new Municipality { Name = "Melaab", NameAr = "الملعب", StateId = "38" },
                    new Municipality { Name = "Sidi Slimane", NameAr = "سيدي سليمان", StateId = "38" },
                    new Municipality { Name = "Beni Lahcene", NameAr = "بني لحسن", StateId = "38" },


                    // El Oued (39)
                    new Municipality { Name = "El Oued", NameAr = "الوادي", StateId = "39" },
                    new Municipality { Name = "Robbah", NameAr = "الرباح", StateId = "39" },
                    new Municipality { Name = "Oued El Alenda", NameAr = "وادي العلندة", StateId = "39" },
                    new Municipality { Name = "Bayadha", NameAr = "البياضة", StateId = "39" },
                    new Municipality { Name = "Nakhla", NameAr = "النخلة", StateId = "39" },
                    new Municipality { Name = "Guemar", NameAr = "قمار", StateId = "39" },
                    new Municipality { Name = "Kouinine", NameAr = "كوينين", StateId = "39" },
                    new Municipality { Name = "Reguiba", NameAr = "الرقيبة", StateId = "39" },
                    new Municipality { Name = "Hamraia", NameAr = "الحمراية", StateId = "39" },
                    new Municipality { Name = "Taghzout", NameAr = "تغزوت", StateId = "39" },
                    new Municipality { Name = "Debila", NameAr = "الدبيلة", StateId = "39" },
                    new Municipality { Name = "Hassani Abdelkrim", NameAr = "حساني عبد الكريم", StateId = "39" },
                    new Municipality { Name = "Hassi Khalifa", NameAr = "حاسي خليفة", StateId = "39" },
                    new Municipality { Name = "Taleb Larbi", NameAr = "الطالب العربي", StateId = "39" },
                    new Municipality { Name = "Douar El Ma", NameAr = "دوار الماء", StateId = "39" },
                    new Municipality { Name = "Sidi Aoun", NameAr = "سيدي عون", StateId = "39" },
                    new Municipality { Name = "Trifaoui", NameAr = "الطريفاوي", StateId = "39" },
                    new Municipality { Name = "Magrane", NameAr = "المقرن", StateId = "39" },
                    new Municipality { Name = "Ben Guecha", NameAr = "بن قشة", StateId = "39" },
                    new Municipality { Name = "Ourmes", NameAr = "ورماس", StateId = "39" },
                    new Municipality { Name = "Still", NameAr = "سطيل", StateId = "39" },
                    new Municipality { Name = "M'Rara", NameAr = "المرارة", StateId = "39" },
                    new Municipality { Name = "Sidi Khellil", NameAr = "سيدي خليل", StateId = "39" },
                    new Municipality { Name = "Tendla", NameAr = "تندلة", StateId = "39" },
                    new Municipality { Name = "El Ogla", NameAr = "العقلة", StateId = "39" },
                    new Municipality { Name = "Mih Ouansa", NameAr = "اميه وانسة", StateId = "39" },
                    new Municipality { Name = "El Mghair", NameAr = "المغير", StateId = "39" },
                    new Municipality { Name = "Djamaa", NameAr = "جامعة", StateId = "39" },
                    new Municipality { Name = "Oum Touyour", NameAr = "أم الطيور", StateId = "39" },
                    new Municipality { Name = "Sidi Amrane", NameAr = "سيدي عمران", StateId = "39" },

                    // Khenchela (40)
                    new Municipality { Name = "Khenchela", NameAr = "خنشلة", StateId = "40" },
                    new Municipality { Name = "M'Toussa", NameAr = "متوسة", StateId = "40" },
                    new Municipality { Name = "Kais", NameAr = "قايس", StateId = "40" },
                    new Municipality { Name = "Baghai", NameAr = "بغاي", StateId = "40" },
                    new Municipality { Name = "El Hamma", NameAr = "الحامة", StateId = "40" },
                    new Municipality { Name = "Ain Touila", NameAr = "عين الطويلة", StateId = "40" },
                    new Municipality { Name = "Taouzianat", NameAr = "تاوزيانت", StateId = "40" },
                    new Municipality { Name = "Bouhmama", NameAr = "بوحمامة", StateId = "40" },
                    new Municipality { Name = "El Oueldja", NameAr = "الولجة", StateId = "40" },
                    new Municipality { Name = "Remila", NameAr = "الرميلة", StateId = "40" },
                    new Municipality { Name = "Cherchar", NameAr = "ششار", StateId = "40" },
                    new Municipality { Name = "Djellal", NameAr = "جلال", StateId = "40" },
                    new Municipality { Name = "Babar", NameAr = "بابار", StateId = "40" },
                    new Municipality { Name = "Tamza", NameAr = "طامزة", StateId = "40" },
                    new Municipality { Name = "Ensigha", NameAr = "انسيغة", StateId = "40" },
                    new Municipality { Name = "Yabous", NameAr = "يابوس", StateId = "40" },
                    new Municipality { Name = "Khirane", NameAr = "خيران", StateId = "40" },
                    new Municipality { Name = "M'Sara", NameAr = "مصارة", StateId = "40" },
                    new Municipality { Name = "Chelia", NameAr = "شلية", StateId = "40" },
                    new Municipality { Name = "Ouled Rechache", NameAr = "أولاد رشاش", StateId = "40" },
                    new Municipality { Name = "El Mahmal", NameAr = "المحمل", StateId = "40" },

                    // Souk Ahras (41)
                    new Municipality { Name = "Souk Ahras", NameAr = "سوق أهراس", StateId = "41" },
                    new Municipality { Name = "Sedrata", NameAr = "سدراتة", StateId = "41" },
                    new Municipality { Name = "Mechroha", NameAr = "المشروحة", StateId = "41" },
                    new Municipality { Name = "Ouled Driss", NameAr = "أولاد إدريس", StateId = "41" },
                    new Municipality { Name = "Tiffech", NameAr = "تيفاش", StateId = "41" },
                    new Municipality { Name = "Zaarouria", NameAr = "الزعرورية", StateId = "41" },
                    new Municipality { Name = "Taoura", NameAr = "تاورة", StateId = "41" },
                    new Municipality { Name = "Drea", NameAr = "الدريعة", StateId = "41" },
                    new Municipality { Name = "Haddada", NameAr = "الحدادة", StateId = "41" },
                    new Municipality { Name = "Khedara", NameAr = "الخضارة", StateId = "41" },
                    new Municipality { Name = "Merahna", NameAr = "المراهنة", StateId = "41" },
                    new Municipality { Name = "Ouled Moumen", NameAr = "أولاد مومن", StateId = "41" },
                    new Municipality { Name = "Bir Bouhouche", NameAr = "بئر بوحوش", StateId = "41" },
                    new Municipality { Name = "M'Daourouch", NameAr = "مداوروش", StateId = "41" },
                    new Municipality { Name = "Oum El Adhaim", NameAr = "أم العظايم", StateId = "41" },
                    new Municipality { Name = "Ain Zana", NameAr = "عين الزانة", StateId = "41" },
                    new Municipality { Name = "Ain Soltane", NameAr = "عين سلطان", StateId = "41" },
                    new Municipality { Name = "Ouillen", NameAr = "ويلان", StateId = "41" },
                    new Municipality { Name = "Sidi Fredj", NameAr = "سيدي فرج", StateId = "41" },
                    new Municipality { Name = "Safel El Ouiden", NameAr = "سافل الويدان", StateId = "41" },
                    new Municipality { Name = "Ragouba", NameAr = "الراقوبة", StateId = "41" },
                    new Municipality { Name = "Khemissa", NameAr = "خميسة", StateId = "41" },
                    new Municipality { Name = "Terraguelt", NameAr = "ترقالت", StateId = "41" },
                    new Municipality { Name = "Zouabi", NameAr = "الزوابي", StateId = "41" },
                    new Municipality { Name = "Lakhdara", NameAr = "لخضارة", StateId = "41" },
                    new Municipality { Name = "Oued Keberit", NameAr = "وادي الكبريت", StateId = "41" },


                    // Tipaza (42)
                    new Municipality { Name = "Tipaza", NameAr = "تيبازة", StateId = "42" },
                    new Municipality { Name = "Menaceur", NameAr = "مناصر", StateId = "42" },
                    new Municipality { Name = "Larhat", NameAr = "لارهاط", StateId = "42" },
                    new Municipality { Name = "Douaouda", NameAr = "دواودة", StateId = "42" },
                    new Municipality { Name = "Bouharoun", NameAr = "بوهارون", StateId = "42" },
                    new Municipality { Name = "Khemisti", NameAr = "خميستي", StateId = "42" },
                    new Municipality { Name = "Bou Ismaïl", NameAr = "بو اسماعيل", StateId = "42" },
                    new Municipality { Name = "Aghbal", NameAr = "أغبال", StateId = "42" },
                    new Municipality { Name = "Hadjout", NameAr = "حجوط", StateId = "42" },
                    new Municipality { Name = "Sidi Amar", NameAr = "سيدي عمار", StateId = "42" },
                    new Municipality { Name = "Gouraya", NameAr = "قوراية", StateId = "42" },
                    new Municipality { Name = "Nador", NameAr = "الناظور", StateId = "42" },
                    new Municipality { Name = "Chaiba", NameAr = "الشعيبة", StateId = "42" },
                    new Municipality { Name = "Ain Tagourait", NameAr = "عين تاقورايت", StateId = "42" },
                    new Municipality { Name = "Cherchell", NameAr = "شرشال", StateId = "42" },
                    new Municipality { Name = "Damous", NameAr = "داموس", StateId = "42" },
                    new Municipality { Name = "Merad", NameAr = "مراد", StateId = "42" },
                    new Municipality { Name = "Fouka", NameAr = "فوكة", StateId = "42" },
                    new Municipality { Name = "Beni Mileuk", NameAr = "بني ميلك", StateId = "42" },
                    new Municipality { Name = "Messelmoun", NameAr = "مسلمون", StateId = "42" },
                    new Municipality { Name = "Sidi Ghiles", NameAr = "سيدي غيلاس", StateId = "42" },
                    new Municipality { Name = "Koléa", NameAr = "القليعة", StateId = "42" },
                    new Municipality { Name = "Attatba", NameAr = "الحطاطبة", StateId = "42" },
                    new Municipality { Name = "Sidi Rached", NameAr = "سيدي راشد", StateId = "42" },
                    new Municipality { Name = "Ahmer El Ain", NameAr = "أحمر العين", StateId = "42" },
                    new Municipality { Name = "Bourkika", NameAr = "بورقيقة", StateId = "42" },
                    new Municipality { Name = "Sidi Semiane", NameAr = "سيدي سميان", StateId = "42" },
                    new Municipality { Name = "Hadjeret Ennous", NameAr = "حجرة النص", StateId = "42" },

                    // Mila (43)
                    new Municipality { Name = "Mila", NameAr = "ميلة", StateId = "43" },
                    new Municipality { Name = "Ferdjioua", NameAr = "فرجيوة", StateId = "43" },
                    new Municipality { Name = "Chelghoum Laid", NameAr = "شلغوم العيد", StateId = "43" },
                    new Municipality { Name = "Sidi Merouane", NameAr = "سيدي مروان", StateId = "43" },
                    new Municipality { Name = "Tassala Lemtai", NameAr = "تسالة لمطاعي", StateId = "43" },
                    new Municipality { Name = "Terrai Bainen", NameAr = "ترعي باينان", StateId = "43" },
                    new Municipality { Name = "Grarem Gouga", NameAr = "القرارم قوقة", StateId = "43" },
                    new Municipality { Name = "Tassadane Haddada", NameAr = "تسدان حدادة", StateId = "43" },
                    new Municipality { Name = "Oued Endja", NameAr = "وادي النجاء", StateId = "43" },
                    new Municipality { Name = "Ahmed Rachedi", NameAr = "أحمد راشدي", StateId = "43" },
                    new Municipality { Name = "Ouled Khalouf", NameAr = "أولاد خلوف", StateId = "43" },
                    new Municipality { Name = "Oued Seguen", NameAr = "وادي سقان", StateId = "43" },
                    new Municipality { Name = "Tadjenanet", NameAr = "تاجنانت", StateId = "43" },
                    new Municipality { Name = "Benyahia Abderrahmane", NameAr = "بن يحي عبد الرحمن", StateId = "43" },
                    new Municipality { Name = "Yahia Beni Guecha", NameAr = "يحي بني قشة", StateId = "43" },
                    new Municipality { Name = "Chigara", NameAr = "الشيقارة", StateId = "43" },
                    new Municipality { Name = "Hamala", NameAr = "حمالة", StateId = "43" },
                    new Municipality { Name = "Sidi Khelifa", NameAr = "سيدي خليفة", StateId = "43" },
                    new Municipality { Name = "Zeghaia", NameAr = "زغاية", StateId = "43" },
                    new Municipality { Name = "El Mechira", NameAr = "المشيرة", StateId = "43" },
                    new Municipality { Name = "Amira Arres", NameAr = "اعميرة اراس", StateId = "43" },
                    new Municipality { Name = "Tiberguent", NameAr = "تيبرقنت", StateId = "43" },
                    new Municipality { Name = "Bouhatem", NameAr = "بوحاتم", StateId = "43" },
                    new Municipality { Name = "Derrahi Bouslah", NameAr = "دراحي بوصلاح", StateId = "43" },
                    new Municipality { Name = "Minar Zarza", NameAr = "مينار زارزة", StateId = "43" },
                    new Municipality { Name = "Rouached", NameAr = "الرواشد", StateId = "43" },
                    new Municipality { Name = "Tassala", NameAr = "تسالة", StateId = "43" },
                    new Municipality { Name = "Ain Tine", NameAr = "عين التين", StateId = "43" },
                    new Municipality { Name = "El Ayadi Barbes", NameAr = "العياضي برباس", StateId = "43" },
                    new Municipality { Name = "Telaghma", NameAr = "التلاغمة", StateId = "43" },
                    new Municipality { Name = "Oued Athmenia", NameAr = "وادي العثمانية", StateId = "43" },
                    new Municipality { Name = "Ain Melouk", NameAr = "عين الملوك", StateId = "43" },
// Aïn Defla (44)
new Municipality { Name = "Aïn Defla", NameAr = "عين الدفلى", StateId = "44" },
new Municipality { Name = "Miliana", NameAr = "مليانة", StateId = "44" },
new Municipality { Name = "Khemis Miliana", NameAr = "خميس مليانة", StateId = "44" },
new Municipality { Name = "Hammam Righa", NameAr = "حمام ريغة", StateId = "44" },
new Municipality { Name = "Aïn Benian", NameAr = "عين البنيان", StateId = "44" },
new Municipality { Name = "El Abadia", NameAr = "العبادية", StateId = "44" },
new Municipality { Name = "Djendel", NameAr = "جندل", StateId = "44" },
new Municipality { Name = "Oued Chorfa", NameAr = "وادي الشرفاء", StateId = "44" },
new Municipality { Name = "El Amra", NameAr = "العامرة", StateId = "44" },
new Municipality { Name = "Bourached", NameAr = "بوراشد", StateId = "44" },
new Municipality { Name = "El Attaf", NameAr = "العطاف", StateId = "44" },
new Municipality { Name = "El Maine", NameAr = "الماين", StateId = "44" },
new Municipality { Name = "Tacheta Zougagha", NameAr = "تاشتة زقاغة", StateId = "44" },
new Municipality { Name = "Aïn Bouyahia", NameAr = "عين بويحيى", StateId = "44" },
new Municipality { Name = "Aïn Torki", NameAr = "عين التركي", StateId = "44" },
new Municipality { Name = "Aïn Soltane", NameAr = "عين السلطان", StateId = "44" },
new Municipality { Name = "Bir Ould Khelifa", NameAr = "بئر ولد خليفة", StateId = "44" },
new Municipality { Name = "Djelida", NameAr = "جليدة", StateId = "44" },
new Municipality { Name = "Djemaa Ouled Cheikh", NameAr = "جمعة أولاد الشيخ", StateId = "44" },
new Municipality { Name = "Mekhatria", NameAr = "المخاطرية", StateId = "44" },
new Municipality { Name = "Bathia", NameAr = "بطحية", StateId = "44" },
new Municipality { Name = "Rouina", NameAr = "الروينة", StateId = "44" },
new Municipality { Name = "Zeddine", NameAr = "زدين", StateId = "44" },
new Municipality { Name = "Ben Allal", NameAr = "بن علال", StateId = "44" },
new Municipality { Name = "Aïn Lechiakh", NameAr = "عين الاشياخ", StateId = "44" },
new Municipality { Name = "Oued Djemaa", NameAr = "واد الجمعة", StateId = "44" },
new Municipality { Name = "Belaas", NameAr = "بلعاص", StateId = "44" },
new Municipality { Name = "Bordj Emir Khaled", NameAr = "برج الأمير خالد", StateId = "44" },
new Municipality { Name = "Hoceinia", NameAr = "الحسينية", StateId = "44" },
new Municipality { Name = "Birbouche", NameAr = "بربوش", StateId = "44" },
new Municipality { Name = "Sidi Lakhdar", NameAr = "سيدي لخضر", StateId = "44" },
new Municipality { Name = "Tiberkanine", NameAr = "تبركانين", StateId = "44" },
new Municipality { Name = "Arrib", NameAr = "عريب", StateId = "44" },
new Municipality { Name = "Sidi Saad", NameAr = "سيدي سعد", StateId = "44" },
new Municipality { Name = "El Hassania", NameAr = "الحسانية", StateId = "44" },
new Municipality { Name = "Aïn Lehdjel", NameAr = "عين لحجل", StateId = "44" },

// Naâma (45)
new Municipality { Name = "Naâma", NameAr = "النعامة", StateId = "45" },
new Municipality { Name = "Mecheria", NameAr = "المشرية", StateId = "45" },
new Municipality { Name = "Ain Sefra", NameAr = "عين الصفراء", StateId = "45" },
new Municipality { Name = "Tiout", NameAr = "تيوت", StateId = "45" },
new Municipality { Name = "Sfissifa", NameAr = "سفيسيفة", StateId = "45" },
new Municipality { Name = "Moghrar", NameAr = "مغرار", StateId = "45" },
new Municipality { Name = "Asla", NameAr = "عسلة", StateId = "45" },
new Municipality { Name = "Djeniene Bourezg", NameAr = "جنين بورزق", StateId = "45" },
new Municipality { Name = "Ain Ben Khelil", NameAr = "عين بن خليل", StateId = "45" },
new Municipality { Name = "Makmen Ben Amer", NameAr = "مكمن بن عمار", StateId = "45" },
new Municipality { Name = "Kasdir", NameAr = "القصدير", StateId = "45" },
new Municipality { Name = "El Biod", NameAr = "البيوض", StateId = "45" },

// Aïn Témouchent (46)
new Municipality { Name = "Aïn Témouchent", NameAr = "عين تموشنت", StateId = "46" },
new Municipality { Name = "Chaabat El Leham", NameAr = "شعبة اللحم", StateId = "46" },
new Municipality { Name = "Aïn Kihal", NameAr = "عين الكيحل", StateId = "46" },
new Municipality { Name = "Hammam Bouhadjar", NameAr = "حمام بوحجر", StateId = "46" },
new Municipality { Name = "Bou Zedjar", NameAr = "بوزجار", StateId = "46" },
new Municipality { Name = "Oued Berkeche", NameAr = "وادي برقش", StateId = "46" },
new Municipality { Name = "Aghlal", NameAr = "أغلال", StateId = "46" },
new Municipality { Name = "Terga", NameAr = "تارقة", StateId = "46" },
new Municipality { Name = "Aïn El Arbaa", NameAr = "عين الأربعاء", StateId = "46" },
new Municipality { Name = "Tamzoura", NameAr = "تامزورة", StateId = "46" },
new Municipality { Name = "Chentouf", NameAr = "شنتوف", StateId = "46" },
new Municipality { Name = "Sidi Ben Adda", NameAr = "سيدي بن عدة", StateId = "46" },
new Municipality { Name = "Aoubellil", NameAr = "عقب الليل", StateId = "46" },
new Municipality { Name = "El Malah", NameAr = "المالح", StateId = "46" },
new Municipality { Name = "Sidi Boumedienne", NameAr = "سيدي بومدين", StateId = "46" },
new Municipality { Name = "Oued Sabah", NameAr = "وادي الصباح", StateId = "46" },
new Municipality { Name = "Ouled Boudjemaa", NameAr = "أولاد بوجمعة", StateId = "46" },
new Municipality { Name = "El Amria", NameAr = "العامرية", StateId = "46" },
new Municipality { Name = "Hassi El Ghella", NameAr = "حاسي الغلة", StateId = "46" },
new Municipality { Name = "Hassasna", NameAr = "الحساسنة", StateId = "46" },
new Municipality { Name = "Ouled Kihal", NameAr = "أولاد الكيحل", StateId = "46" },
new Municipality { Name = "Sidi Safi", NameAr = "سيدي صافي", StateId = "46" },
new Municipality { Name = "El Messaid", NameAr = "المساعيد", StateId = "46" },
new Municipality { Name = "Sidi Ouriache", NameAr = "سيدي ورياش", StateId = "46" },
new Municipality { Name = "Emir Abdelkader", NameAr = "الأمير عبد القادر", StateId = "46" },
new Municipality { Name = "Beni Saf", NameAr = "بني صاف", StateId = "46" },
new Municipality { Name = "Oulhaça El Gheraba", NameAr = "ولهاصة الغرابة", StateId = "46" },
new Municipality { Name = "Tadmaya", NameAr = "تادماية", StateId = "46" },

                    // Ghardaïa (47)
                    new Municipality { Name = "Ghardaïa", NameAr = "غرداية", StateId = "47" },
                    new Municipality { Name = "El Menia", NameAr = "المنيعة", StateId = "47" },
                    new Municipality { Name = "Dhayet Bendhahoua", NameAr = "ضاية بن ضحوة", StateId = "47" },
                    new Municipality { Name = "Berriane", NameAr = "بريان", StateId = "47" },
                    new Municipality { Name = "Metlili", NameAr = "متليلي", StateId = "47" },
                    new Municipality { Name = "El Guerrara", NameAr = "القرارة", StateId = "47" },
                    new Municipality { Name = "El Atteuf", NameAr = "العطف", StateId = "47" },
                    new Municipality { Name = "Zelfana", NameAr = "زلفانة", StateId = "47" },
                    new Municipality { Name = "Sebseb", NameAr = "سبسب", StateId = "47" },
                    new Municipality { Name = "Bounoura", NameAr = "بونورة", StateId = "47" },
                    new Municipality { Name = "Hassi Fehal", NameAr = "حاسي الفحل", StateId = "47" },
                    new Municipality { Name = "Mansoura", NameAr = "المنصورة", StateId = "47" },
                    new Municipality { Name = "Hassi Gara", NameAr = "حاسي القارة", StateId = "47" },
                   
// Relizane (48)
new Municipality { Name = "Relizane", NameAr = "غليزان", StateId = "48" },
new Municipality { Name = "Oued Rhiou", NameAr = "وادي رهيو", StateId = "48" },
new Municipality { Name = "Belaassel Bouzegza", NameAr = "بلعسل بوزقزة", StateId = "48" },
new Municipality { Name = "Sidi Saada", NameAr = "سيدي سعادة", StateId = "48" },
new Municipality { Name = "Ouled Aiche", NameAr = "أولاد يعيش", StateId = "48" },
new Municipality { Name = "Sidi Lazreg", NameAr = "سيدي لزرق", StateId = "48" },
new Municipality { Name = "El Hamadna", NameAr = "الحمادنة", StateId = "48" },
new Municipality { Name = "Sidi M'Hamed Ben Ali", NameAr = "سيدي امحمد بن علي", StateId = "48" },
new Municipality { Name = "Mediouna", NameAr = "مديونة", StateId = "48" },
new Municipality { Name = "Sidi Khettab", NameAr = "سيدي خطاب", StateId = "48" },
new Municipality { Name = "Ammi Moussa", NameAr = "عمي موسى", StateId = "48" },
new Municipality { Name = "Zemmoura", NameAr = "زمورة", StateId = "48" },
new Municipality { Name = "Beni Dergoun", NameAr = "بني درقن", StateId = "48" },
new Municipality { Name = "Djidiouia", NameAr = "جديوية", StateId = "48" },
new Municipality { Name = "El Guettar", NameAr = "القطار", StateId = "48" },
new Municipality { Name = "El Matmar", NameAr = "المطمر", StateId = "48" },
new Municipality { Name = "Had Echkalla", NameAr = "حد الشكالة", StateId = "48" },
new Municipality { Name = "Kalaa", NameAr = "القلعة", StateId = "48" },
new Municipality { Name = "Ain Tarek", NameAr = "عين طارق", StateId = "48" },
new Municipality { Name = "Oued El Djemaa", NameAr = "وادي الجمعة", StateId = "48" },
new Municipality { Name = "Ouarizane", NameAr = "واريزان", StateId = "48" },
new Municipality { Name = "Mazouna", NameAr = "مازونة", StateId = "48" },
new Municipality { Name = "Yellel", NameAr = "يلل", StateId = "48" },
new Municipality { Name = "Bendaoud", NameAr = "بن داود", StateId = "48" },
new Municipality { Name = "El Hassi", NameAr = "الحاسي", StateId = "48" },
new Municipality { Name = "Lahlef", NameAr = "لحلاف", StateId = "48" },
new Municipality { Name = "Merdja Sidi Abed", NameAr = "مرجة سيدي عابد", StateId = "48" },
new Municipality { Name = "Mendes", NameAr = "منداس", StateId = "48" },
new Municipality { Name = "Oued Essalem", NameAr = "وادي السلام", StateId = "48" },
new Municipality { Name = "Dar Ben Abdellah", NameAr = "دار بن عبد الله", StateId = "48" },
new Municipality { Name = "Hamri", NameAr = "حمري", StateId = "48" },
new Municipality { Name = "Ramka", NameAr = "الرمكة", StateId = "48" },
new Municipality { Name = "Souk El Had", NameAr = "سوق الحد", StateId = "48" },
new Municipality { Name = "Ain Rahma", NameAr = "عين الرحمة", StateId = "48" },
new Municipality { Name = "Belacel", NameAr = "بلعسل", StateId = "48" },
new Municipality { Name = "Sidi M'Hamed Benaouda", NameAr = "سيدي امحمد بن عودة", StateId = "48" },
new Municipality { Name = "El Ouldja", NameAr = "الولجة", StateId = "48" },
new Municipality { Name = "Kerakda", NameAr = "القراقدة", StateId = "48" },
                    
                    // Timimoun (49)
                    new Municipality { Name = "Timimoun", NameAr = "تيميمون", StateId = "49" },
                    new Municipality { Name = "Ouled Said", NameAr = "أولاد السعيد", StateId = "49" },
                    new Municipality { Name = "Tinerkouk", NameAr = "تينركوك", StateId = "49" },
                    new Municipality { Name = "Aougrout", NameAr = "أوقروت", StateId = "49" },
                    new Municipality { Name = "Metarfa", NameAr = "المطارفة", StateId = "49" },
                    new Municipality { Name = "Talmine", NameAr = "طالمين", StateId = "49" },
                    new Municipality { Name = "Ouled Aissa", NameAr = "أولاد عيسى", StateId = "49" },
                    new Municipality { Name = "Charouine", NameAr = "شروين", StateId = "49" },
                    new Municipality { Name = "Deldoul", NameAr = "دلدول", StateId = "49" },
                    new Municipality { Name = "Ksar Kaddour", NameAr = "قصر قدور", StateId = "49" },

                    // Bordj Badji Mokhtar (52)
                    new Municipality { Name = "Bordj Badji Mokhtar", NameAr = "برج باجي مختار", StateId = "52" },
                    new Municipality { Name = "Timiaouine", NameAr = "تيمياوين", StateId = "52" },

                    // Ouled Djellal (51)
                    new Municipality { Name = "Ouled Djellal", NameAr = "أولاد جلال", StateId = "51" },
                    new Municipality { Name = "Doucen", NameAr = "الدوسن", StateId = "51" },
                    new Municipality { Name = "Chaiba", NameAr = "الشعيبة", StateId = "51" },
                    new Municipality { Name = "Sidi Khaled", NameAr = "سيدي خالد", StateId = "51" },
                    new Municipality { Name = "Besbes", NameAr = "بسباس", StateId = "51" },
                    new Municipality { Name = "Ras El Miaad", NameAr = "رأس الميعاد", StateId = "51" },
                    new Municipality { Name = "Ouled Djellal", NameAr = "أولاد جلال", StateId = "51" },
                    new Municipality { Name = "Doucen", NameAr = "الدوسن", StateId = "51" },
                    new Municipality { Name = "Chaiba", NameAr = "الشعيبة", StateId = "51" },
                    new Municipality { Name = "Sidi Khaled", NameAr = "سيدي خالد", StateId = "51" },
                    new Municipality { Name = "Besbes", NameAr = "بسباس", StateId = "51" },
                    new Municipality { Name = "Ras El Miaad", NameAr = "رأس الميعاد", StateId = "51" },
                    new Municipality { Name = "Ouled Djellal", NameAr = "أولاد جلال", StateId = "51" },
                    new Municipality { Name = "Doucen", NameAr = "الدوسن", StateId = "51" },
                    new Municipality { Name = "Chaiba", NameAr = "الشعيبة", StateId = "51" },
                    new Municipality { Name = "Sidi Khaled", NameAr = "سيدي خالد", StateId = "51" },
                    new Municipality { Name = "Besbes", NameAr = "بسباس", StateId = "51" },
                    new Municipality { Name = "Ras El Miaad", NameAr = "رأس الميعاد", StateId = "51" },

                    // Béni Abbès (52)
                    new Municipality { Name = "Béni Abbès", NameAr = "بني عباس", StateId = "52" },
                    new Municipality { Name = "Tamtert", NameAr = "تامترت", StateId = "52" },
                    new Municipality { Name = "Igli", NameAr = "إقلي", StateId = "52" },
                    new Municipality { Name = "El Ouata", NameAr = "الواتة", StateId = "52" },
                    new Municipality { Name = "Kerzaz", NameAr = "كرزاز", StateId = "52" },
                    new Municipality { Name = "Timoudi", NameAr = "تيمودي", StateId = "52" },
                    new Municipality { Name = "Beni Ikhlef", NameAr = "بن يخلف", StateId = "52" },
                    new Municipality { Name = "Ksabi", NameAr = "القصابي", StateId = "52" },
                    new Municipality { Name = "Ouled Khodeir", NameAr = "أولاد خضير", StateId = "52" },
                    new Municipality { Name = "Tabelbala", NameAr = "تبلبالة", StateId = "52" },

                    // In Salah (53)
                    new Municipality { Name = "In Salah", NameAr = "عين صالح", StateId = "53" },
                    new Municipality { Name = "Foggaret Ezzaouia", NameAr = "فقارة الزوى", StateId = "53" },
                    new Municipality { Name = "In Ghar", NameAr = "عين غار", StateId = "53" },

                    // In Guezzam
                    new Municipality { Name = "In Guezzam", NameAr = "عين قزام", StateId = "54" },
                    new Municipality { Name = "Tin Zaouatine", NameAr = "تين زواتين", StateId = "54" },

                    // Touggourt (55)
                    new Municipality { Name = "Touggourt", NameAr = "تقرت", StateId = "55" },
                    new Municipality { Name = "Nezla", NameAr = "النزلة", StateId = "55" },
                    new Municipality { Name = "Zaouia El Abidia", NameAr = "الزاوية العابدية", StateId = "55" },
                    new Municipality { Name = "Tebesbest", NameAr = "تبسبست", StateId = "55" },
                    new Municipality { Name = "Megarine", NameAr = "المقارين", StateId = "55" },
                    new Municipality { Name = "Sidi Slimane", NameAr = "سيدي سليمان", StateId = "55" },
                    new Municipality { Name = "El Alia", NameAr = "العالية", StateId = "55" },
                    new Municipality { Name = "Blidet Amor", NameAr = "بليدة عامر", StateId = "55" },
                    new Municipality { Name = "Tamacine", NameAr = "تماسين", StateId = "55" },
                    new Municipality { Name = "M'Naguer", NameAr = "المنقر", StateId = "55" },
                    new Municipality { Name = "El Hadjira", NameAr = "الحجيرة", StateId = "55" },
                    new Municipality { Name = "Benaceur", NameAr = "بن ناصر", StateId = "55" },
                    new Municipality { Name = "Taibet", NameAr = "الطيبات", StateId = "55" },

                    // Djanet (56)
                    new Municipality { Name = "Djanet", NameAr = "جانت", StateId = "56" },
                    new Municipality { Name = "Bordj El Houasse", NameAr = "برج الحواس", StateId = "56" },

                    // El M'Ghair (57)
                    new Municipality { Name = "El M'Ghair", NameAr = "المغير", StateId = "57" },
                    new Municipality { Name = "Still", NameAr = "سطيل", StateId = "57" },
                    new Municipality { Name = "Sidi Khelil", NameAr = "سيدي خليل", StateId = "57" },
                    new Municipality { Name = "Oum Touyour", NameAr = "أم الطيور", StateId = "57" },
                    new Municipality { Name = "Djamaâ", NameAr = "جامعة", StateId = "57" },
                    new Municipality { Name = "M'Rara", NameAr = "المرارة", StateId = "57" },
                    new Municipality { Name = "Tendla", NameAr = "تندلة", StateId = "57" },

                    // El Meniaa (58)
                    new Municipality { Name = "El Meniaa", NameAr = "المنيعة", StateId = "58" },
                    new Municipality { Name = "Hassi Gara", NameAr = "حاسي القارة", StateId = "58" }
                };

                context.Municipalities.AddRange(municipalities);
                context.SaveChanges();

                Console.WriteLine("Algerian municipalities seeded successfully.");
            }
            else
            {
                Console.WriteLine("Algerian municipalities already exist in the database.");
            }
        }

        // Method to call both seeders in the correct order
        public static void SeedAlgerianLocations(WebApplication app)
        {
            SeedAlgerianStates(app);
            SeedAlgerianMunicipalities(app);
        }

        public static void SeedAppRoles(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var identitydb = scope.ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>();

            foreach (var item in Constants.Roles)
            {
                if (!identitydb.Roles.Any(r => r.Name == item))
                {
                    var role = new Microsoft.AspNetCore.Identity.IdentityRole(item);
                    identitydb.Roles.Add(new Models.ApplicationRole { Name = role.Name, NormalizedName = role.Name.ToUpper() });
                    identitydb.SaveChanges();
                    Console.WriteLine($"Role {item} seeded successfully.");
                }
                else
                {
                    Console.WriteLine($"Role {item} already exists.");
                }

            }

            // Seed admin user
            if (!identitydb.Users.Any(u => u.UserName == "djellal"))
            {
                var adminUser = new Models.ApplicationUser
                {
                    UserName = "djellal",
                    Email = "djellal@univ-setif.dz",
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                var userManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<Models.ApplicationUser>>();
                var result = userManager.CreateAsync(adminUser, "dhb571982").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(adminUser, Constants.ADMIN).Wait();
                    Console.WriteLine("Admin user seeded successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to seed admin user.");
                }
            }
            else
            {
                Console.WriteLine("Admin user already exists.");
            }

        }


    }

}