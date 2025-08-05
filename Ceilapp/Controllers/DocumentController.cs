using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using Ceilapp.Models.ceilapp;
using System.Globalization;
using System.Collections.Generic;

using System;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;



namespace Ceilapp.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly ReportService documentService;        
        private readonly ceilappService context;
        public DocumentController(IWebHostEnvironment environment, ceilappService context, ReportService documentService)
        {
            this.context = context;
            this.environment = environment;
            this.documentService = documentService;
           

        }
       

        [Authorize]
        [HttpGet("/Document/FicheInsc")]
        public IActionResult FicheInsc([FromQuery] int inscid)
        {
            try
            {
                var inscription = context.dbContext.CourseRegistrations.Include(cr=>cr.Session).Include(cr => cr.Course).Include(cr => cr.CourseLevel).FirstOrDefault(cr => cr.Id == inscid);
                if (inscription == null)
                {
                    return NotFound();
                }

                // Check if user is in STUDENT role and trying to access their own registration
                //if (User.IsInRole(Constants.STUDENT) && inscription.UserId != User.Identity.Name)
                //{
                //    return Forbid(); // Return forbidden if student tries to access another student's registration
                //}
                
                var fileBytes = documentService.GenererFicheInscription(inscription);
                var stream = new MemoryStream(fileBytes);
                return File(stream, "application/pdf");
            }
            catch (Exception ex)
            {
                // Log the exception details here if needed
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
       

        // [HttpGet("/Document/AttestationsList(ids='{ids}')")]
        // public IActionResult AttestationsList(string ids = null)
        // {
        //     try
        //     {

        //         return documentService.AttestationsList(ids); ;
        //     }
        //     catch (Exception ex)
        //     {

        //         return null;
        //     }
            
        // }

    }
}
