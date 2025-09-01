using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using Radzen;

using Ceilapp.Models;

namespace Ceilapp
{
    public partial class SecurityService
    {
        public async Task RegisterWithRole(string userName, string password,string role)
        {
            var uri = new Uri("Account/RegisterWithRole", UriKind.Relative);

            var content = new FormUrlEncodedContent(new Dictionary<string, string> {
                { "userName", userName },
                { "password", password },
                { "role", role }
            });

            var response = await httpClient.PostAsync(uri, content);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();

                throw new ApplicationException(message);
            }
        }
    }
}