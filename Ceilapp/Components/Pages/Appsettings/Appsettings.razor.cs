using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace Ceilapp.Components.Pages.Appsettings
{
    public partial class Appsettings
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

        protected Ceilapp.Models.ceilapp.AppSetting appSetting = new Ceilapp.Models.ceilapp.AppSetting();
        [Inject]
        public ceilappService ceilappdb { get; set; }

        [Inject]
        protected Ceilapp.ceilappService ceilappService { get; set; }

        protected System.Linq.IQueryable<Ceilapp.Models.ceilapp.Session> sessions;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                appSetting = await ceilappdb.GetAppSettingById(1);
                if (appSetting == null)
                {
                    appSetting = new Ceilapp.Models.ceilapp.AppSetting
                    {
                        Id = 1,
                        OrganizationName = "Default Organization",
                        Address = "Default Setting",
                        Tel = "Default Value",
                        Email = "",
                        WebSite = "https://example.com",
                        Fb = "https://facebook.com/example",
                        LinkedIn = "https://linkedin.com/in/example",
                        Youtube = "https://youtube.com/example",
                        Instagram = "https://instagram.com/example",
                        X = "https://x.com/example",
                        Logo = "https://example.com/logo.png",
                       // TermsAndConditions = "Default Terms and Conditions",
                        IsRegistrationOpened = false,
                        MaxRegistrationPerSession = 2,
                        CurrentSessionId = null,
                        TermsAndConditions = @"
                        <p><strong>مقتطفات من القانون الداخلي للمركز:</strong></p>
                        <ul>
                          <li>يجب الالتزام بالقانون الداخلي لجامعة فرحات عباس وأي إخلال به يؤدي إلى الإقصاء المباشر من المركز.</li>
                          <li>لا يمكن بأي حال من الأحوال التحويل بين الأفواج بعد انتهاء الآجال المخصصة لذلك.</li>
                          <li>لا يمكن استرداد مبلغ التسجيل لأي سبب من الأسباب كما لا يمكن الرجاء التسجيل في دورة أخرى.</li>
                          <li>تؤدي الغيابات المتكررة إلى الإقصاء المباشر دون أي تعويض.</li>
                          <li>يتم الاعتماد عن المعدل لتحديد المستوى إلى تصنيف المعني في المستوى الأدنى ولا يعاد الامتحان إلا إذا تم تبرير معقول.</li>
                          <li>يكون اختيار الأفواج حسب الحصص المتوفرة والتي تكون موزعة.</li>
                          <li>لا يمكن تغيير الفوج عند نفس الأستاذ ونفس المستوى فقط.</li>
                          <li>يتم التكوين بشهادة مستوى للطالب الناجح وشهادة مشاركة للطالب الراسب.</li>
                        </ul>

                         <br/>
                        <p><strong>Extraits du règlement intérieur du centre :</strong></p>
                        <ul>
                          <li>Le respect du règlement intérieur de l’université Ferhat Abbas est obligatoire ; toute infraction entraîne l’exclusion immédiate du centre.</li>
                          <li>Aucun transfert entre groupes n’est autorisé après l’expiration des délais prévus à cet effet.</li>
                          <li>Aucun remboursement des frais d’inscription n’est possible pour quelque raison que ce soit ; aucune nouvelle inscription à une autre session n’est acceptée.</li>
                          <li>Les absences répétées entraînent l’exclusion immédiate sans compensation.</li>
                          <li>Le classement de l’étudiant dans un niveau inférieur est basé sur la moyenne, et aucun examen de rattrapage ne sera organisé sauf en cas de justification valable.</li>
                          <li>Le choix des groupes se fait selon les places disponibles et leur répartition.</li>
                          <li>Il est interdit de changer de groupe avec le même enseignant et au même niveau.</li>
                          <li>Une attestation de niveau est délivrée à l’étudiant ayant réussi, et une attestation de participation à l’étudiant ajourné.</li>
                        </ul>

                        ",

                    };
                    await ceilappdb.CreateAppSetting(appSetting);
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", ex.Message);
            }
            sessions = await ceilappService.GetSessions();
        }

        protected async System.Threading.Tasks.Task SaveButtonClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            try
            {
                await ceilappdb.UpdateAppSetting(appSetting.Id, appSetting);
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Application settings saved successfuly" });
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error",Detail=ex.Message });
            }
            

        }
    }
}