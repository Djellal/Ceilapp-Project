using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ceilapp.Models.ceilapp;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ceilapp
{
    public class ReportService
    {
        private ceilappService ceilappService;

        public ReportService(ceilappService ceilappService)
        {
            this.ceilappService = ceilappService;
        }
        public byte[] GenererFicheInscription(int registrationId)
        {
            try
            {
                var registration = ceilappService.dbContext.CourseRegistrations
                        .Include(cr => cr.Session)
                        .Include(cr => cr.Course)
                        .Include(cr => cr.CourseLevel)
                        .Include(cr => cr.Profession)
                        .Include(cr => cr.Municipality)
                        .Include(cr => cr.State)
                        .FirstOrDefault(cr => cr.Id == registrationId);
                if (registration == null)
                {
                    return null; // or throw an exception if you prefer
                }

                QuestPDF.Settings.License = LicenseType.Community;

                return Document.Create(document =>
                {
                    document.Page(page =>
                    {
                        page.Background()
                   .Layers(layer =>
                   {
                       layer.PrimaryLayer();

                       layer.Layer()
                           .AlignCenter()
                           .AlignMiddle()
                           .Image("wwwroot/images/fichinsc.png")
                           .FitArea();
                   });
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);




                        page.Content()
                            .PaddingTop(4, Unit.Centimetre)

                            .Column(column =>
                            {
                                column.Spacing(10); // <-- Add this line to increase inter-line space (10 points)
                                column.Item().Text($"FICHE D'INSCRIPTION").AlignCenter().FontSize(20).SemiBold().FontColor(Colors.Blue.Darken2);
                                column.Item().Text($"Session:\t {registration.Session?.SessionName}");
                                column.Item().Text($"N° d'inscription:\t {registration.InscriptionCode}");

                                column.Item().Text($"Nom:\t {registration.LastName} {registration.FirstName}");
                                column.Item().Text($"Date de naissance:\t {registration.BirthDate:dd/MM/yyyy} à {registration.Municipality?.Name} - {registration.State?.Name}");
                                column.Item().LineHorizontal(1);
                                column.Item().Text($"Cours: \t{registration.Course?.Name}");
                                column.Item().Text($"Niveau: \t{registration.CourseLevel?.Name}");
                                column.Item().Text($"Prfession:\t {registration.Profession?.Name} , (Droits d'inscriptions :{registration.Profession?.FeeValue:N2})");
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text($"Généré le {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });
                }).GeneratePdf();
            }
            catch (Exception ex)
            {
#if DEBUG   
                throw ex;
#endif
                return null; // or handle the exception as needed
            }
        }
    }
}
