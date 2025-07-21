using System;
using System.Collections.Generic;
using System.Linq;
using Ceilapp.Models.ceilapp;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ceilapp.Data.Seeders
{
    public static class AlgerianLocationSeeder
    {
        public static void SeedAlgerianStates(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ceilappContext>();
            
            // Check if states already exist
            if (!context.States.Any())
            {
                var states = new List<State>
                {
                    new State { Id = "01", Name = "Adrar", NameAr = "أدرار" },
                    new State { Id = "02", Name = "Chlef", NameAr = "الشلف" },
                    new State { Id = "03", Name = "Laghouat", NameAr = "الأغواط" },
                    new State { Id = "04", Name = "Oum El Bouaghi", NameAr = "أم البواقي" },
                    new State { Id = "05", Name = "Batna", NameAr = "باتنة" },
                    new State { Id = "06", Name = "Béjaïa", NameAr = "بجاية" },
                    new State { Id = "07", Name = "Biskra", NameAr = "بسكرة" },
                    new State { Id = "08", Name = "Béchar", NameAr = "بشار" },
                    new State { Id = "09", Name = "Blida", NameAr = "البليدة" },
                    new State { Id = "10", Name = "Bouira", NameAr = "البويرة" },
                    new State { Id = "11", Name = "Tamanrasset", NameAr = "تمنراست" },
                    new State { Id = "12", Name = "Tébessa", NameAr = "تبسة" },
                    new State { Id = "13", Name = "Tlemcen", NameAr = "تلمسان" },
                    new State { Id = "14", Name = "Tiaret", NameAr = "تيارت" },
                    new State { Id = "15", Name = "Tizi Ouzou", NameAr = "تيزي وزو" },
                    new State { Id = "16", Name = "Alger", NameAr = "الجزائر" },
                    new State { Id = "17", Name = "Djelfa", NameAr = "الجلفة" },
                    new State { Id = "18", Name = "Jijel", NameAr = "جيجل" },
                    new State { Id = "19", Name = "Sétif", NameAr = "سطيف" },
                    new State { Id = "20", Name = "Saïda", NameAr = "سعيدة" },
                    new State { Id = "21", Name = "Skikda", NameAr = "سكيكدة" },
                    new State { Id = "22", Name = "Sidi Bel Abbès", NameAr = "سيدي بلعباس" },
                    new State { Id = "23", Name = "Annaba", NameAr = "عنابة" },
                    new State { Id = "24", Name = "Guelma", NameAr = "قالمة" },
                    new State { Id = "25", Name = "Constantine", NameAr = "قسنطينة" },
                    new State { Id = "26", Name = "Médéa", NameAr = "المدية" },
                    new State { Id = "27", Name = "Mostaganem", NameAr = "مستغانم" },
                    new State { Id = "28", Name = "M'Sila", NameAr = "المسيلة" },
                    new State { Id = "29", Name = "Mascara", NameAr = "معسكر" },
                    new State { Id = "30", Name = "Ouargla", NameAr = "ورقلة" },
                    new State { Id = "31", Name = "Oran", NameAr = "وهران" },
                    new State { Id = "32", Name = "El Bayadh", NameAr = "البيض" },
                    new State { Id = "33", Name = "Illizi", NameAr = "إليزي" },
                    new State { Id = "34", Name = "Bordj Bou Arréridj", NameAr = "برج بوعريريج" },
                    new State { Id = "35", Name = "Boumerdès", NameAr = "بومرداس" },
                    new State { Id = "36", Name = "El Tarf", NameAr = "الطارف" },
                    new State { Id = "37", Name = "Tindouf", NameAr = "تندوف" },
                    new State { Id = "38", Name = "Tissemsilt", NameAr = "تيسمسيلت" },
                    new State { Id = "39", Name = "El Oued", NameAr = "الوادي" },
                    new State { Id = "40", Name = "Khenchela", NameAr = "خنشلة" },
                    new State { Id = "41", Name = "Souk Ahras", NameAr = "سوق أهراس" },
                    new State { Id = "42", Name = "Tipaza", NameAr = "تيبازة" },
                    new State { Id = "43", Name = "Mila", NameAr = "ميلة" },
                    new State { Id = "44", Name = "Aïn Defla", NameAr = "عين الدفلى" },
                    new State { Id = "45", Name = "Naâma", NameAr = "النعامة" },
                    new State { Id = "46", Name = "Aïn Témouchent", NameAr = "عين تموشنت" },
                    new State { Id = "47", Name = "Ghardaïa", NameAr = "غرداية" },
                    new State { Id = "48", Name = "Relizane", NameAr = "غليزان" },
                    new State { Id = "49", Name = "Timimoun", NameAr = "تيميمون" },
                    new State { Id = "50", Name = "Bordj Badji Mokhtar", NameAr = "برج باجي مختار" },
                    new State { Id = "51", Name = "Ouled Djellal", NameAr = "أولاد جلال" },
                    new State { Id = "52", Name = "Béni Abbès", NameAr = "بني عباس" },
                    new State { Id = "53", Name = "In Salah", NameAr = "عين صالح" },
                    new State { Id = "54", Name = "In Guezzam", NameAr = "عين قزام" },
                    new State { Id = "55", Name = "Touggourt", NameAr = "تقرت" },
                    new State { Id = "56", Name = "Djanet", NameAr = "جانت" },
                    new State { Id = "57", Name = "El M'Ghair", NameAr = "المغير" },
                    new State { Id = "58", Name = "El Meniaa", NameAr = "المنيعة" }
                };
                
                context.States.AddRange(states);
                context.SaveChanges();
                
                Console.WriteLine("Algerian states seeded successfully.");
            }
            else
            {
                Console.WriteLine("Algerian states already exist in the database.");
            }
        }

        public static void SeedAlgerianMunicipalities(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ceilappContext>();
            
            // Check if municipalities already exist
            if (!context.Municipalities.Any())
            {
                // Make sure states exist first
                if (!context.States.Any())
                {
                    Console.WriteLine("Cannot seed municipalities: States don't exist yet.");
                    return;
                }
                
                var municipalities = new List<Municipality>
                {
                    // Adrar (01)
                    new Municipality { Name = "Adrar", NameAr = "أدرار", StateId = "01" },
                    new Municipality { Name = "Tamest", NameAr = "تامست", StateId = "01" },
                    new Municipality { Name = "Reggane", NameAr = "رقان", StateId = "01" },
                    new Municipality { Name = "In Zghmir", NameAr = "إن زغمير", StateId = "01" },
                    new Municipality { Name = "Tit", NameAr = "تيت", StateId = "01" },
                    new Municipality { Name = "Tsabit", NameAr = "تسابيت", StateId = "01" },
                    new Municipality { Name = "Zaouiet Kounta", NameAr = "زاوية كنتة", StateId = "01" },
                    new Municipality { Name = "Aoulef", NameAr = "أولف", StateId = "01" },
                    new Municipality { Name = "Tamekten", NameAr = "تامقتن", StateId = "01" },
                    new Municipality { Name = "Tamantit", NameAr = "تمنطيط", StateId = "01" },
                    
                    // Chlef (02)
                    new Municipality { Name = "Chlef", NameAr = "الشلف", StateId = "02" },
                    new Municipality { Name = "Ténès", NameAr = "تنس", StateId = "02" },
                    new Municipality { Name = "Bénairia", NameAr = "بني عيرية", StateId = "02" },
                    new Municipality { Name = "El Karimia", NameAr = "الكريمية", StateId = "02" },
                    new Municipality { Name = "Tadjena", NameAr = "تاجنة", StateId = "02" },
                    new Municipality { Name = "Taougrite", NameAr = "تاوقريت", StateId = "02" },
                    
                    // Laghouat (03)
                    new Municipality { Name = "Laghouat", NameAr = "الأغواط", StateId = "03" },
                    new Municipality { Name = "Ksar El Hirane", NameAr = "قصر الحيران", StateId = "03" },
                    new Municipality { Name = "Benacer Benchohra", NameAr = "بن ناصر بن شهرة", StateId = "03" },
                    new Municipality { Name = "Sidi Makhlouf", NameAr = "سيدي مخلوف", StateId = "03" },
                    new Municipality { Name = "Hassi Delaa", NameAr = "حاسي الدلاعة", StateId = "03" },
                    
                    // Oum El Bouaghi (04)
                    new Municipality { Name = "Oum El Bouaghi", NameAr = "أم البواقي", StateId = "04" },
                    new Municipality { Name = "Aïn Beïda", NameAr = "عين البيضاء", StateId = "04" },
                    new Municipality { Name = "Aïn M'lila", NameAr = "عين مليلة", StateId = "04" },
                    new Municipality { Name = "Behir Chergui", NameAr = "بحير الشرقي", StateId = "04" },
                    
                    // Batna (05)
                    new Municipality { Name = "Batna", NameAr = "باتنة", StateId = "05" },
                    new Municipality { Name = "Tazoult", NameAr = "تازولت", StateId = "05" },
                    new Municipality { Name = "N'Gaous", NameAr = "نقاوس", StateId = "05" },
                    new Municipality { Name = "Arris", NameAr = "أريس", StateId = "05" },
                    new Municipality { Name = "Theniet El Abed", NameAr = "ثنية العابد", StateId = "05" },
                    
                    // Béjaïa (06)
                    new Municipality { Name = "Béjaïa", NameAr = "بجاية", StateId = "06" },
                    new Municipality { Name = "Akbou", NameAr = "أقبو", StateId = "06" },
                    new Municipality { Name = "Seddouk", NameAr = "صدوق", StateId = "06" },
                    new Municipality { Name = "Tichy", NameAr = "تيشي", StateId = "06" },
                    new Municipality { Name = "Chemini", NameAr = "شميني", StateId = "06" },
                    
                    // Biskra (07)
                    new Municipality { Name = "Biskra", NameAr = "بسكرة", StateId = "07" },
                    new Municipality { Name = "Sidi Okba", NameAr = "سيدي عقبة", StateId = "07" },
                    new Municipality { Name = "Tolga", NameAr = "طولقة", StateId = "07" },
                    new Municipality { Name = "Ouled Djellal", NameAr = "أولاد جلال", StateId = "07" },
                    new Municipality { Name = "Zeribet El Oued", NameAr = "زريبة الوادي", StateId = "07" },
                    
                    // Alger (16)
                    new Municipality { Name = "Alger Centre", NameAr = "الجزائر الوسطى", StateId = "16" },
                    new Municipality { Name = "Bab El Oued", NameAr = "باب الوادي", StateId = "16" },
                    new Municipality { Name = "Hussein Dey", NameAr = "حسين داي", StateId = "16" },
                    new Municipality { Name = "El Harrach", NameAr = "الحراش", StateId = "16" },
                    new Municipality { Name = "Bab Ezzouar", NameAr = "باب الزوار", StateId = "16" },
                    new Municipality { Name = "Dar El Beïda", NameAr = "الدار البيضاء", StateId = "16" },
                    new Municipality { Name = "Zeralda", NameAr = "زرالدة", StateId = "16" },
                    new Municipality { Name = "Birtouta", NameAr = "بئر توتة", StateId = "16" },
                    new Municipality { Name = "Rouiba", NameAr = "الرويبة", StateId = "16" },
                    new Municipality { Name = "Reghaïa", NameAr = "رغاية", StateId = "16" },
                    
                    // Sétif (19)
                    new Municipality { Name = "Sétif", NameAr = "سطيف", StateId = "19" },
                    new Municipality { Name = "Ain El Kebira", NameAr = "عين الكبيرة", StateId = "19" },
                    new Municipality { Name = "Bougaa", NameAr = "بوقاعة", StateId = "19" },
                    new Municipality { Name = "El Eulma", NameAr = "العلمة", StateId = "19" },
                    new Municipality { Name = "Ain Oulmene", NameAr = "عين ولمان", StateId = "19" },
                    new Municipality { Name = "Djemila", NameAr = "جميلة", StateId = "19" },
                    new Municipality { Name = "Ain Arnat", NameAr = "عين أرنات", StateId = "19" },
                    new Municipality { Name = "Ain Azel", NameAr = "عين أزال", StateId = "19" },
                    new Municipality { Name = "Guenzet", NameAr = "قنزات", StateId = "19" },
                    new Municipality { Name = "Beni Aziz", NameAr = "بني عزيز", StateId = "19" },
                    
                    // Oran (31)
                    new Municipality { Name = "Oran", NameAr = "وهران", StateId = "31" },
                    new Municipality { Name = "Bir El Djir", NameAr = "بئر الجير", StateId = "31" },
                    new Municipality { Name = "Es Senia", NameAr = "السانية", StateId = "31" },
                    new Municipality { Name = "Arzew", NameAr = "أرزيو", StateId = "31" },
                    new Municipality { Name = "Bethioua", NameAr = "بطيوة", StateId = "31" },
                    new Municipality { Name = "Aïn El Turk", NameAr = "عين الترك", StateId = "31" },
                    
                    // Constantine (25)
                    new Municipality { Name = "Constantine", NameAr = "قسنطينة", StateId = "25" },
                    new Municipality { Name = "Hamma Bouziane", NameAr = "حامة بوزيان", StateId = "25" },
                    new Municipality { Name = "El Khroub", NameAr = "الخروب", StateId = "25" },
                    new Municipality { Name = "Aïn Smara", NameAr = "عين سمارة", StateId = "25" },
                    new Municipality { Name = "Didouche Mourad", NameAr = "ديدوش مراد", StateId = "25" },
                    
                    // Annaba (23)
                    new Municipality { Name = "Annaba", NameAr = "عنابة", StateId = "23" },
                    new Municipality { Name = "El Bouni", NameAr = "البوني", StateId = "23" },
                    new Municipality { Name = "El Hadjar", NameAr = "الحجار", StateId = "23" },
                    new Municipality { Name = "Sidi Amar", NameAr = "سيدي عمار", StateId = "23" },
                    new Municipality { Name = "Berrahal", NameAr = "برحال", StateId = "23" }
                };
                
                context.Municipalities.AddRange(municipalities);
                context.SaveChanges();
                
                Console.WriteLine("Algerian municipalities seeded successfully.");
            }
            else
            {
                Console.WriteLine("Algerian municipalities already exist in the database.");
            }
        }

        // Method to call both seeders in the correct order
        public static void SeedAlgerianLocations(WebApplication app)
        {
            SeedAlgerianStates(app);
            SeedAlgerianMunicipalities(app);
        }
    }
}