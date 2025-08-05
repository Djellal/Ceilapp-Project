using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ceilapp.Models.ceilapp;
using System.Threading.Tasks;

namespace Ceilapp
{
    public class ReportService
    {
        public byte[] GenererFicheInscription(CourseRegistration registration)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            
            return Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    
                    page.Header()
                        .Text("FICHE D'INSCRIPTION")
                        .FontSize(20)
                        .SemiBold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content()
                        .Column(column =>
                        {
                            column.Item().Text($"Code: {registration.InscriptionCode}");
                            column.Item().Text($"Nom: {registration.LastName} {registration.FirstName}");
                            column.Item().Text($"Date de naissance: {registration.BirthDate:dd/MM/yyyy}");
                            column.Item().Text($"Cours: {registration.Course?.Name}");
                            column.Item().Text($"Niveau: {registration.CourseLevel?.Name}");
                            column.Item().Text($"Session: {registration.Session?.SessionName}");
                            column.Item().Text($"Total payé: {registration.PaidFeeValue:C}");
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Généré le {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            }).GeneratePdf();
        }
    }
}
