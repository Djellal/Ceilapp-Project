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
            var uri = new Uri($"{navigationManager.BaseUri}Account/RegisterWithRole");

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
       

        public async Task<IEnumerable<ApplicationUser>> GetUsers(string roleFilter = null)
        {
            var uri = new Uri(baseUri, $"ApplicationUsers");
            
            if (!string.IsNullOrEmpty(roleFilter))
            {
                uri = new Uri(baseUri, $"ApplicationUsers?$filter=Roles/any(r: r/Name eq '{roleFilter}')&$expand=Roles");
            }
            else
            {
                uri = new Uri(baseUri, $"ApplicationUsers?$expand=Roles");
            }

            uri = uri.GetODataUri();
            var response = await httpClient.GetAsync(uri);
            var result = await response.ReadAsync<ODataServiceResult<ApplicationUser>>();
            return result.Value;
        }


    }
}