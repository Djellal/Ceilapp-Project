using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Ceilapp.Models;

namespace Ceilapp.Controllers
{
    public partial class AccountController
    {
        [HttpPost]
        public async Task<IActionResult> RegisterWithRole(string userName, string password, string role)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Invalid user name or password.");
            }

            var user = new ApplicationUser { UserName = userName, Email = userName, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, password);
            var result2 = await userManager.AddToRoleAsync(user, role);

            if (result.Succeeded && result2.Succeeded)
            {
                try
                {
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            var message = string.Join(", ", result.Errors.Select(error => error.Description));
            return BadRequest(message);
        }
    }
}
