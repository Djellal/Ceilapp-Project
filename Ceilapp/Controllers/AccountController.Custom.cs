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
    //                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

    //                var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code }, protocol: Request.Scheme);

    //                var text = $@"Hi, <br /> <br />  
    //We received your registration request for Ceilapp. <br /> <br />  
    //To confirm your registration please click the following link: <a href=""{callbackUrl}"">confirm your registration</a> <br /> <br />  
    //If you didn't request this registration, you can safely ignore this email. Someone else might have typed your email address by mistake.";

    //                await SendEmailAsync(user.Email, "Confirm your registration", text);

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
