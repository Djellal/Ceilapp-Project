using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using Ceilapp.Models.ceilapp;
using System.Globalization;
using System.Collections.Generic;

using System;
using System.Net.Mail;



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
        [HttpGet("/Document/FicheInsc")]
        public FileStreamResult Attestation([FromQuery] int inscid)
        {
            try
            {
                var inscription = context.dbContext.CourseRegistrations.Find(inscid);
                
                var fileBytes = documentService.GenererFicheInscription(inscription);
                var stream = new MemoryStream(fileBytes);
                return new FileStreamResult(stream, "application/pdf");
            }
            catch (Exception ex)
            {

                return null;
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
