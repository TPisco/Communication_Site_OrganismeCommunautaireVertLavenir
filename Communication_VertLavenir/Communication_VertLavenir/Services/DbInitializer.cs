using Communication_VertLavenir.Data;
using Communication_VertLavenir.Models;
using Microsoft.EntityFrameworkCore;

namespace Communication_VertLavenir.Services
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var db = services.GetRequiredService<AppDbContext>();
            var hasher = services.GetRequiredService<IPasswordHasherService>();

            await db.Database.MigrateAsync();

            await SeedSettingsAsync(db);
            await SeedMetricsAsync(db);
            await SeedValuesAsync(db);
            await SeedActionsAsync(db);
            await SeedUsersAsync(db, hasher);
            await SeedEventsAsync(db);
            await SeedRegistrationsAndDonationsAsync(db);
            await SeedContactAsync(db);

            await db.SaveChangesAsync();
        }

        private static async Task SeedSettingsAsync(AppDbContext db)
        {
            if (await db.SiteSettings.AnyAsync()) return;

            db.SiteSettings.AddRange(
                new SiteSetting { Key = SiteSettingKeys.MissionFr, Value = "Depuis 8 ans, Vert l'avenir œuvre dans la région de la Montérégie pour sensibiliser la population à la protection de l'environnement. Nous croyons qu'ensemble, nous pouvons créer un avenir plus vert et durable." },
                new SiteSetting { Key = SiteSettingKeys.MissionEn, Value = "For 8 years, Vert l'avenir has worked in the Montérégie region to raise awareness about environmental protection. We believe that together we can create a greener and more sustainable future." },
                new SiteSetting { Key = SiteSettingKeys.MissionEs, Value = "Desde hace 8 años, Vert l'avenir trabaja en la región de Montérégie para sensibilizar a la población sobre la protección del medio ambiente. Creemos que juntos podemos crear un futuro más verde y sostenible." },
                new SiteSetting { Key = SiteSettingKeys.ContactEmail, Value = "contact@vertlavenir.org" },
                new SiteSetting { Key = SiteSettingKeys.ContactPhone, Value = "+1 (514) 555-0123" },
                new SiteSetting { Key = SiteSettingKeys.ContactAddress, Value = "945 Ch. de Chambly, Longueuil, QC J4H 3M6" },
                new SiteSetting { Key = SiteSettingKeys.OpeningHoursFr, Value = "Lundi - Vendredi: 9h00 - 17h00\nSamedi: 10h00 - 14h00\nDimanche: Fermé" },
                new SiteSetting { Key = SiteSettingKeys.OpeningHoursEn, Value = "Monday - Friday: 9:00 - 17:00\nSaturday: 10:00 - 14:00\nSunday: Closed" },
                new SiteSetting { Key = SiteSettingKeys.OpeningHoursEs, Value = "Lunes - Viernes: 9:00 - 17:00\nSábado: 10:00 - 14:00\nDomingo: Cerrado" },
                new SiteSetting { Key = SiteSettingKeys.SocialFacebook, Value = "https://facebook.com" },
                new SiteSetting { Key = SiteSettingKeys.SocialInstagram, Value = "https://instagram.com" },
                new SiteSetting { Key = SiteSettingKeys.SocialLinkedIn, Value = "https://linkedin.com" }
            );
            await db.SaveChangesAsync();
        }

        private static async Task SeedMetricsAsync(AppDbContext db)
        {
            if (await db.ImpactMetrics.AnyAsync()) return;

            db.ImpactMetrics.AddRange(
                new ImpactMetric { Key = "volunteers", LabelFr = "Bénévoles actifs", LabelEn = "Active volunteers", LabelEs = "Voluntarios activos", Value = "15 000+", Icon = "bi-people", DisplayOrder = 1 },
                new ImpactMetric { Key = "events", LabelFr = "Évènements réalisés", LabelEn = "Events held", LabelEs = "Eventos realizados", Value = "2 500", Icon = "bi-calendar-event", DisplayOrder = 2 },
                new ImpactMetric { Key = "waste", LabelFr = "Tonnes de déchets ramassés", LabelEn = "Tons of waste collected", LabelEs = "Toneladas de residuos recogidos", Value = "50T", Icon = "bi-recycle", DisplayOrder = 3 },
                new ImpactMetric { Key = "years", LabelFr = "Années d'action", LabelEn = "Years of action", LabelEs = "Años de acción", Value = "12", Icon = "bi-award", DisplayOrder = 4 }
            );
            await db.SaveChangesAsync();
        }

        private static async Task SeedValuesAsync(AppDbContext db)
        {
            if (await db.OrgValues.AnyAsync()) return;

            db.OrgValues.AddRange(
                new OrgValue { TitleFr = "Écoresponsable", TitleEn = "Eco-responsible", TitleEs = "Ecorresponsable", DescriptionFr = "Nous plaçons la protection de l'environnement au cœur de chacune de nos décisions et actions.", DescriptionEn = "We place environmental protection at the heart of every decision and action.", DescriptionEs = "Situamos la protección del medio ambiente en el centro de cada decisión y acción.", DisplayOrder = 1 },
                new OrgValue { TitleFr = "Activiste", TitleEn = "Activist", TitleEs = "Activista", DescriptionFr = "Nous nous engageons activement pour défendre la planète et faire entendre la voix de la communauté.", DescriptionEn = "We actively engage to defend the planet and make the community's voice heard.", DescriptionEs = "Nos comprometemos activamente para defender el planeta y dar voz a la comunidad.", DisplayOrder = 2 },
                new OrgValue { TitleFr = "Engagé", TitleEn = "Committed", TitleEs = "Comprometidos", DescriptionFr = "Nous croyons en l'action concrète et durable au service de notre communauté locale.", DescriptionEn = "We believe in concrete and sustainable action serving our local community.", DescriptionEs = "Creemos en la acción concreta y sostenible al servicio de nuestra comunidad local.", DisplayOrder = 3 }
            );
            await db.SaveChangesAsync();
        }

        private static async Task SeedActionsAsync(AppDbContext db)
        {
            if (await db.KeyActions.AnyAsync()) return;

            db.KeyActions.AddRange(
                new KeyAction { TitleFr = "Reforestation", TitleEn = "Reforestation", TitleEs = "Reforestación", DescriptionFr = "Plantation d'arbres et restauration des espaces verts pour lutter contre le changement climatique.", DescriptionEn = "Tree planting and restoration of green spaces to fight climate change.", DescriptionEs = "Plantación de árboles y restauración de espacios verdes para combatir el cambio climático.", Icon = "bi-tree", DisplayOrder = 1 },
                new KeyAction { TitleFr = "Recyclage", TitleEn = "Recycling", TitleEs = "Reciclaje", DescriptionFr = "Programmes de collecte et sensibilisation au recyclage dans les communautés locales.", DescriptionEn = "Collection programs and recycling awareness in local communities.", DescriptionEs = "Programas de recolección y concienciación sobre el reciclaje en las comunidades locales.", Icon = "bi-recycle", DisplayOrder = 2 },
                new KeyAction { TitleFr = "Éducation", TitleEn = "Education", TitleEs = "Educación", DescriptionFr = "Ateliers et formations pour sensibiliser à l'importance de la protection environnementale.", DescriptionEn = "Workshops and training to raise awareness of environmental protection.", DescriptionEs = "Talleres y formaciones para concienciar sobre la importancia de la protección ambiental.", Icon = "bi-mortarboard", DisplayOrder = 3 },
                new KeyAction { TitleFr = "Biodiversité", TitleEn = "Biodiversity", TitleEs = "Biodiversidad", DescriptionFr = "Actions concrètes en faveur de la préservation des écosystèmes locaux et de la faune.", DescriptionEn = "Concrete actions to preserve local ecosystems and wildlife.", DescriptionEs = "Acciones concretas para preservar los ecosistemas locales y la fauna.", Icon = "bi-flower1", DisplayOrder = 4 }
            );
            await db.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(AppDbContext db, IPasswordHasherService hasher)
        {
            if (await db.Users.AnyAsync()) return;

            db.Users.AddRange(
                new ApplicationUser
                {
                    FirstName = "Super", LastName = "Admin", Email = "admin@vertlavenir.org",
                    Role = UserRole.Admin, Phone = "+1 (514) 555-0000",
                    PasswordHash = hasher.Hash("Admin123!"), CreatedAt = DateTime.UtcNow.AddYears(-2)
                },
                new ApplicationUser
                {
                    FirstName = "Staff", LastName = "Membre", Email = "staff@vertlavenir.org",
                    Role = UserRole.Staff, Phone = "+1 (514) 555-0011",
                    PasswordHash = hasher.Hash("Staff123!"), CreatedAt = DateTime.UtcNow.AddMonths(-10)
                },
                new ApplicationUser
                {
                    FirstName = "Alice", LastName = "Dupont", Email = "alice@example.com",
                    Role = UserRole.Member, Phone = "+1 (450) 555-0002",
                    PasswordHash = hasher.Hash("Member123!"), CreatedAt = DateTime.UtcNow.AddMonths(-6)
                },
                new ApplicationUser
                {
                    FirstName = "Bob", LastName = "Martin", Email = "bob@example.com",
                    Role = UserRole.Member, Phone = "+1 (450) 555-0003",
                    PasswordHash = hasher.Hash("Member123!"), CreatedAt = DateTime.UtcNow.AddMonths(-3)
                },
                new ApplicationUser
                {
                    FirstName = "Claire", LastName = "Leroy", Email = "claire@example.com",
                    Role = UserRole.Member, Phone = "+1 (450) 555-0004",
                    PasswordHash = hasher.Hash("Member123!"), CreatedAt = DateTime.UtcNow.AddMonths(-1)
                }
            );
            await db.SaveChangesAsync();
        }

        private static async Task SeedEventsAsync(AppDbContext db)
        {
            if (await db.Events.AnyAsync()) return;

            var today = DateTime.Today;

            db.Events.AddRange(
                new Event
                {
                    TitleFr = "Jardinage urbain", TitleEn = "Urban gardening", TitleEs = "Jardinería urbana",
                    DescriptionFr = "Apprenez à cultiver vos propres légumes en milieu urbain et découvrez les techniques de compostage.",
                    DescriptionEn = "Learn to grow your own vegetables in the city and discover composting techniques.",
                    DescriptionEs = "Aprenda a cultivar sus propias verduras en la ciudad y descubra técnicas de compostaje.",
                    StartDateTime = today.AddDays(10).AddHours(9), EndDateTime = today.AddDays(10).AddHours(12),
                    Location = "Parc Central, Longueuil", Capacity = 15, Category = EventCategory.Jardinage
                },
                new Event
                {
                    TitleFr = "Collecte de déchets - Rivière", TitleEn = "Waste collection - River", TitleEs = "Recogida de residuos - Río",
                    DescriptionFr = "Corvée de nettoyage des berges pour préserver notre écosystème aquatique.",
                    DescriptionEn = "Riverbank cleanup to preserve our aquatic ecosystem.",
                    DescriptionEs = "Limpieza de las orillas para preservar nuestro ecosistema acuático.",
                    StartDateTime = today.AddDays(17).AddHours(10), EndDateTime = today.AddDays(17).AddHours(14),
                    Location = "Rivière Richelieu, Beloeil", Capacity = 30, Category = EventCategory.Collecte
                },
                new Event
                {
                    TitleFr = "Atelier énergie solaire", TitleEn = "Solar energy workshop", TitleEs = "Taller de energía solar",
                    DescriptionFr = "Découvrez comment installer des panneaux solaires et réduire votre empreinte carbone.",
                    DescriptionEn = "Discover how to install solar panels and reduce your carbon footprint.",
                    DescriptionEs = "Descubra cómo instalar paneles solares y reducir su huella de carbono.",
                    StartDateTime = today.AddDays(24).AddHours(18), EndDateTime = today.AddDays(24).AddHours(20),
                    Location = "Centre communautaire, Brossard", Capacity = 20, Category = EventCategory.Atelier
                },
                new Event
                {
                    TitleFr = "Conférence climat", TitleEn = "Climate conference", TitleEs = "Conferencia sobre el clima",
                    DescriptionFr = "Comprendre les enjeux climatiques actuels et les actions concrètes à entreprendre.",
                    DescriptionEn = "Understand current climate issues and concrete actions to take.",
                    DescriptionEs = "Comprender los desafíos climáticos actuales y las acciones concretas a emprender.",
                    StartDateTime = today.AddDays(32).AddHours(19), EndDateTime = today.AddDays(32).AddHours(21),
                    Location = "Bibliothèque municipale, Saint-Bruno", Capacity = 100, Category = EventCategory.Conference
                },
                new Event
                {
                    TitleFr = "Fabrication de produits ménagers", TitleEn = "Household products workshop", TitleEs = "Taller de productos de limpieza",
                    DescriptionFr = "Créez vos propres produits ménagers écologiques et économiques.",
                    DescriptionEn = "Create your own ecological and economical household products.",
                    DescriptionEs = "Cree sus propios productos de limpieza ecológicos y económicos.",
                    StartDateTime = today.AddDays(40).AddHours(13), EndDateTime = today.AddDays(40).AddHours(16),
                    Location = "Maison de la culture, Chambly", Capacity = 12, Category = EventCategory.Atelier
                },
                new Event
                {
                    TitleFr = "Plantation d'arbres", TitleEn = "Tree planting", TitleEs = "Plantación de árboles",
                    DescriptionFr = "Participez à la reforestation de notre région et combattez les îlots de chaleur.",
                    DescriptionEn = "Take part in reforesting our region and fight urban heat islands.",
                    DescriptionEs = "Participe en la reforestación de nuestra región y combata las islas de calor.",
                    StartDateTime = today.AddDays(48).AddHours(9), EndDateTime = today.AddDays(48).AddHours(15),
                    Location = "Parc Maternel, Varennes", Capacity = 40, Category = EventCategory.Plantation
                },
                new Event
                {
                    TitleFr = "Journée de recyclage", TitleEn = "Recycling day", TitleEs = "Día de reciclaje",
                    DescriptionFr = "Service à domicile pour récupérer vos appareils électroniques.",
                    DescriptionEn = "Home service to collect your electronic devices.",
                    DescriptionEs = "Servicio a domicilio para recoger sus aparatos electrónicos.",
                    StartDateTime = today.AddDays(-20).AddHours(9), EndDateTime = today.AddDays(-20).AddHours(16),
                    Location = "Longueuil", Capacity = 50, Category = EventCategory.Collecte
                }
            );
            await db.SaveChangesAsync();
        }

        private static async Task SeedRegistrationsAndDonationsAsync(AppDbContext db)
        {
            if (!await db.EventRegistrations.AnyAsync())
            {
                var members = await db.Users.Where(u => u.Role == UserRole.Member).ToListAsync();
                var events = await db.Events.OrderBy(e => e.StartDateTime).ToListAsync();

                if (members.Count > 0 && events.Count > 0)
                {
                    foreach (var ev in events.Take(4))
                    {
                        foreach (var m in members)
                        {
                            db.EventRegistrations.Add(new EventRegistration
                            {
                                EventId = ev.Id,
                                UserId = m.Id,
                                Name = m.FullName,
                                Email = m.Email,
                                Phone = m.Phone,
                                Status = RegistrationStatus.Confirmed,
                                RegistrationDate = DateTime.UtcNow.AddDays(-5)
                            });
                        }
                    }
                    await db.SaveChangesAsync();
                }
            }

            if (!await db.Donations.AnyAsync())
            {
                var donor = await db.Users.FirstOrDefaultAsync(u => u.Role == UserRole.Member);
                var rnd = new Random(42);
                decimal[] amounts = { 25m, 50m, 100m, 250m };
                PaymentMethod[] methods = { PaymentMethod.CarteCredit, PaymentMethod.PayPal, PaymentMethod.Cheque };

                for (int i = 0; i < 12; i++)
                {
                    db.Donations.Add(new Donation
                    {
                        Amount = amounts[rnd.Next(amounts.Length)],
                        Currency = "CAD",
                        FirstName = donor?.FirstName ?? "Anonyme",
                        LastName = donor?.LastName ?? "Donateur",
                        Email = donor?.Email ?? "anonyme@example.com",
                        PaymentMethod = methods[rnd.Next(methods.Length)],
                        Status = DonationStatus.Completed,
                        UserId = donor?.Id,
                        CreatedAt = DateTime.UtcNow.AddDays(-rnd.Next(1, 300))
                    });
                }
                await db.SaveChangesAsync();
            }
        }

        private static async Task SeedContactAsync(AppDbContext db)
        {
            if (await db.ContactMessages.AnyAsync()) return;

            db.ContactMessages.Add(new ContactMessage
            {
                Name = "Jean Tremblay",
                Email = "jean.tremblay@example.com",
                Phone = "+1 (450) 555-9999",
                Subject = "Bénévolat",
                Message = "Bonjour, j'aimerais devenir bénévole pour vos activités de plantation d'arbres.",
                Status = ContactStatus.New
            });
            await db.SaveChangesAsync();
        }
    }
}
