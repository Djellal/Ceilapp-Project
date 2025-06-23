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
                        LinkredIn = "https://linkedin.com/in/example",
                        Youtube = "https://youtube.com/example",
                        Instagram = "https://instagram.com/example",
                        X = "https://x.com/example",
                        Logo = "https://example.com/logo.png",
                        TermsAndConditions = "Default Terms and Conditions",
                        IsRegistrationOpened = false,
                        MaxRegistrationPerSession = 2,
                        CurrentSessionId = null

                    };
                    await ceilappdb.CreateAppSetting(appSetting);
                }
            }
            catch (Exception ex)
            {
                NotificationService.Notify(NotificationSeverity.Error, "Error", ex.Message);
            }
        }

        protected async System.Threading.Tasks.Task SaveButtonClick(Microsoft.AspNetCore.Components.Web.MouseEventArgs args)
        {
            try
            {
                await ceilappdb.UpdateAppSetting(appSetting.Id, appSetting);
            }
            catch (Exception ex)
            {
                NotificationService.Notify(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error",Detail=ex.Message });
            }
            

        }
    }
}