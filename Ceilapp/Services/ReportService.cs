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
                     page.Background()
                    .Layers(layer =>
                    {
                        layer.PrimaryLayer();
                        
                        layer.Layer()
                            .AlignCenter()
                            .AlignMiddle()
                            .Image("wwwroot/images/fichinsc.png")
                            .FitArea()
                            ; // 10% opacity for watermark effect
                    });
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    
                    


                    page.Content()
                        .PaddingTop(4, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Item().Text($"FICHE D'INSCRIPTION").AlignCenter().FontSize(20).SemiBold() .FontColor(Colors.Blue.Darken2);
                            column.Item().Text($"Code:\t {registration.InscriptionCode}");
                            column.Item().Text($"Session:\t {registration.Session?.SessionName}");
                            column.Item().Text($"Nom:\t {registration.LastName} {registration.FirstName}");
                            column.Item().Text($"Date de naissance:\t {registration.BirthDate:dd/MM/yyyy}");
                            column.Item().Text($"Cours: \t{registration.Course?.Name}");
                            column.Item().Text($"Niveau: \t{registration.CourseLevel?.Name}");                            
                            column.Item().Text($"Total payé:\t {registration.PaidFeeValue:C}");
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Généré le {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            }).GeneratePdf();
        }
    }
}
