using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sunshine.App.Models;

namespace Sunshine.App.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        // 1. Seed Roles
        string[] roles = { AppRoles.Admin, AppRoles.Doctor, AppRoles.Patient };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new ApplicationRole(role));
            }
        }

        // Default password for all seed accounts
        const string defaultPassword = "Password123!";

        // 2. Seed Super Admin
        var adminEmail = "admin@sunshine.org";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Prof. Dr. Farzana Rahman (Chief Director)",
                PhoneNumber = "+880 1711-000001",
                Role = AppRoles.Admin,
                IsActive = true,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
                UpdatedAt = DateTime.UtcNow
            };
            await userManager.CreateAsync(adminUser, defaultPassword);
            await userManager.AddToRoleAsync(adminUser, AppRoles.Admin);
        }

        // 3. Seed Bangladeshi Doctors
        var doctorsData = new[]
        {
            new
            {
                Email = "dr.tanvir@sunshine.org",
                Name = "Dr. Tanvir Ahmed, MBBS, MD",
                Phone = "+880 1712-000101",
                Specialization = "Adult Psychiatry & Clinical Depression",
                Bio = "Assistant Professor of Psychiatry at BSMMU (PG Hospital). Expert in mood disorders, chronic depression, and stress management with 14+ years of clinical experience in Dhaka.",
                Fee = 1500.00m,
                Policy = PaymentPolicies.Advance,
                Exp = 14,
                Qualification = "MBBS (DMC), MD (Psychiatry, BSMMU), BMDC Reg: A-54892"
            },
            new
            {
                Email = "dr.nusrat@sunshine.org",
                Name = "Nusrat Jahan, MS (DU)",
                Phone = "+880 1819-000102",
                Specialization = "Cognitive Behavioral Therapy (CBT) & Exam Anxiety",
                Bio = "Senior Clinical Psychologist (DU). Specializes in cognitive restructuring for university/BCS exam pressure, OCD, panic disorder, and young adult psychological well-being.",
                Fee = 1200.00m,
                Policy = PaymentPolicies.Advance,
                Exp = 10,
                Qualification = "B.Sc (Hons), M.S. in Clinical Psychology (University of Dhaka)"
            },
            new
            {
                Email = "dr.rafiq@sunshine.org",
                Name = "Dr. K. M. Rafiqul Islam, FCPS",
                Phone = "+880 1914-000103",
                Specialization = "Family, Marital & Adolescent Counseling",
                Bio = "Consultant Psychiatrist at National Institute of Mental Health (NIMH), Dhaka. Compassionate counselor for couples communication, family transitions, and adolescent mental health.",
                Fee = 1800.00m,
                Policy = PaymentPolicies.PostPayment,
                Exp = 16,
                Qualification = "MBBS, FCPS (Psychiatry, NIMH), MACP (USA), BMDC Reg: A-41209"
            }
        };

        foreach (var doc in doctorsData)
        {
            var user = await userManager.FindByEmailAsync(doc.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = doc.Email,
                    Email = doc.Email,
                    FullName = doc.Name,
                    PhoneNumber = doc.Phone,
                    Role = AppRoles.Doctor,
                    IsActive = true,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-50),
                    UpdatedAt = DateTime.UtcNow
                };
                await userManager.CreateAsync(user, defaultPassword);
                await userManager.AddToRoleAsync(user, AppRoles.Doctor);

                var doctor = new Doctor
                {
                    UserId = user.Id,
                    Specialization = doc.Specialization,
                    Bio = doc.Bio,
                    ConsultationFee = doc.Fee,
                    PaymentPolicy = doc.Policy,
                    IsAvailable = true,
                    ExperienceYears = doc.Exp,
                    Qualification = doc.Qualification,
                    CreatedAt = DateTime.UtcNow.AddDays(-50),
                    UpdatedAt = DateTime.UtcNow
                };
                db.Doctors.Add(doctor);
                await db.SaveChangesAsync();

                // Seed Sunday to Thursday schedules (Bangladesh standard clinical working days) + Saturday
                for (int day = 0; day <= 6; day++)
                {
                    if (day == 5) continue; // Friday weekend in Bangladesh

                    db.DoctorSchedules.Add(new DoctorSchedule
                    {
                        DoctorId = doctor.Id,
                        DayOfWeek = day,
                        StartTime = new TimeOnly(16, 0), // 4:00 PM evening clinic
                        EndTime = new TimeOnly(21, 0),   // 9:00 PM
                        SlotDurationMinutes = 45,
                        IsActive = true
                    });
                }
                await db.SaveChangesAsync();
            }
        }

        // 4. Seed Bangladeshi Patients
        var patientsData = new[]
        {
            new { Email = "anika@example.com", Name = "Anika Tabassum", Phone = "+880 1711-223344", Contact = "Farhan Tabassum (+880 1711-998877)", Notes = "Workplace burnout & corporate stress in Gulshan, Dhaka." },
            new { Email = "rahat@example.com", Name = "Rahat Chowdhury", Phone = "+880 1819-334455", Contact = "Nasrin Chowdhury (+880 1819-887766)", Notes = "BCS & competitive examination anxiety." },
            new { Email = "sazzad@example.com", Name = "Sazzad Hossain", Phone = "+880 1912-445566", Contact = "Fatema Hossain (+880 1912-776655)", Notes = "General emotional check-in & mindfulness practice." }
        };

        foreach (var p in patientsData)
        {
            var user = await userManager.FindByEmailAsync(p.Email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = p.Email,
                    Email = p.Email,
                    FullName = p.Name,
                    PhoneNumber = p.Phone,
                    Role = AppRoles.Patient,
                    IsActive = true,
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    UpdatedAt = DateTime.UtcNow
                };
                await userManager.CreateAsync(user, defaultPassword);
                await userManager.AddToRoleAsync(user, AppRoles.Patient);

                var patient = new Patient
                {
                    UserId = user.Id,
                    DateOfBirth = new DateOnly(1996, 6, 20),
                    Gender = "Female",
                    EmergencyContact = p.Contact,
                    MedicalHistoryNotes = p.Notes,
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    UpdatedAt = DateTime.UtcNow
                };
                db.Patients.Add(patient);
                await db.SaveChangesAsync();
            }
        }

        // 5. Seed Subscription Plans in BDT (৳)
        if (!await db.SubscriptionPlans.AnyAsync())
        {
            db.SubscriptionPlans.AddRange(
                new SubscriptionPlan
                {
                    Name = "Monthly Mindcare (মাসিক মাইন্ডকেয়ার)",
                    DurationType = "MONTHLY",
                    DurationDays = 30,
                    Price = 499.00m,
                    Description = "Unlimited 24/7 access to all therapeutic e-books, sleep roadmaps, and mindfulness audio recordings in Bengali and English.",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-60)
                },
                new SubscriptionPlan
                {
                    Name = "Quarterly Wellness Pass (ত্রৈমাসিক ওয়েলনেস পাস)",
                    DurationType = "QUARTERLY",
                    DurationDays = 90,
                    Price = 1299.00m,
                    Description = "3 months of full digital library access plus newly published clinical worksheets (Save 15%).",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-60)
                },
                new SubscriptionPlan
                {
                    Name = "Annual Holistic Pass (বার্ষিক হলিস্টিক পাস)",
                    DurationType = "YEARLY",
                    DurationDays = 365,
                    Price = 3999.00m,
                    Description = "Full 1-year unlimited access to our entire mental wellness digital vault, audio masterclasses, and exam stress protocols (Save 35%).",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-60)
                }
            );
            await db.SaveChangesAsync();
        }

        // 6. Seed Resource Categories & Resources for Bangladesh
        if (!await db.ResourceCategories.AnyAsync())
        {
            var catAnxiety = new ResourceCategory { Name = "Anxiety & Exam Stress (উদ্বেগ ও পরীক্ষার চাপ)", Slug = "anxiety-stress", Description = "Cognitive restructuring, breathing protocols, and university/BCS stress tools." };
            var catDepression = new ResourceCategory { Name = "Depression & Low Mood (হতাশা ও আত্মবিশ্বাস)", Slug = "depression-mood", Description = "Behavioral activation roadmaps and mood restoration habits." };
            var catMindfulness = new ResourceCategory { Name = "Mindfulness & Meditation (মননশীল মেডিটেশন)", Slug = "mindfulness-meditation", Description = "Somatic body scan audio and grounding exercises in Bengali and English." };
            var catSleep = new ResourceCategory { Name = "Sleep & Urban Health (ঘুম ও স্বাস্থ্যবিধি)", Slug = "sleep-health", Description = "Circadian rhythm protocols and evening unwind checklists for urban life in Bangladesh." };
            var catRelationships = new ResourceCategory { Name = "Relationships & Family (পারিবারিক ও দাম্পত্য সম্পর্ক)", Slug = "relationships-family", Description = "Nonviolent communication guides and family boundary frameworks." };

            db.ResourceCategories.AddRange(catAnxiety, catDepression, catMindfulness, catSleep, catRelationships);
            await db.SaveChangesAsync();

            db.Resources.AddRange(
                new Resource
                {
                    CategoryId = catAnxiety.Id,
                    Title = "Understanding the Anatomy of Anxiety & Panic Triggers",
                    Author = "Nusrat Jahan, MS (DU)",
                    Description = "A clinical handbook explaining autonomic fight-or-flight triggers and practical 5-minute de-escalation methods for students and professionals in Bangladesh.",
                    ResourceType = "ARTICLE",
                    ContentUrl = "https://resources.sunshine.org/articles/anatomy-of-anxiety-bd.pdf",
                    IsPremium = false,
                    ThumbnailUrl = "https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500",
                    CreatedAt = DateTime.UtcNow.AddDays(-40)
                },
                new Resource
                {
                    CategoryId = catMindfulness.Id,
                    Title = "5-Minute Morning Grounding Meditation (বাংলা ও ইংরেজি গাইড)",
                    Author = "Prof. Dr. Farzana Rahman",
                    Description = "Quick audio exercise using the 5-4-3-2-1 sensory technique to center thoughts before starting a busy day.",
                    ResourceType = "AUDIO",
                    ContentUrl = "https://resources.sunshine.org/audio/morning-grounding-bd.mp3",
                    IsPremium = false,
                    ThumbnailUrl = "https://images.unsplash.com/photo-1518241353330-0f7941c2d9b5?w=500",
                    CreatedAt = DateTime.UtcNow.AddDays(-35)
                },
                new Resource
                {
                    CategoryId = catAnxiety.Id,
                    Title = "Mastering Cognitive Restructuring: Complete CBT Workbook (বাংলা সংস্করণ)",
                    Author = "Nusrat Jahan, MS (DU)",
                    Description = "An 80-page interactive workbook with thought records, cognitive distortion breakdowns, and graded exposure exercises tailored for Bangladeshi students and working professionals.",
                    ResourceType = "BOOK",
                    ContentUrl = "https://resources.sunshine.org/books/cbt-mastery-workbook-bd.pdf",
                    IsPremium = true,
                    ThumbnailUrl = "https://images.unsplash.com/photo-1544717305-2782549b5136?w=500",
                    CreatedAt = DateTime.UtcNow.AddDays(-30)
                },
                new Resource
                {
                    CategoryId = catDepression.Id,
                    Title = "Behavioral Activation Roadmap for Overcoming Low Mood",
                    Author = "Dr. Tanvir Ahmed, MBBS, MD (BSMMU)",
                    Description = "Step-by-step clinical roadmap with daily routine restoration sheets, energy recovery metrics, and mood tracking.",
                    ResourceType = "BOOK",
                    ContentUrl = "https://resources.sunshine.org/books/behavioral-activation-roadmap.pdf",
                    IsPremium = true,
                    ThumbnailUrl = "https://images.unsplash.com/photo-1499209974431-9dddcece7f88?w=500",
                    CreatedAt = DateTime.UtcNow.AddDays(-25)
                },
                new Resource
                {
                    CategoryId = catMindfulness.Id,
                    Title = "Deep Somatic Body Scan for Stress Release (45 min)",
                    Author = "Dr. K. M. Rafiqul Islam, FCPS",
                    Description = "High-fidelity guided meditation audio designed to release chronic tension and somatic stress after long working days in Dhaka.",
                    ResourceType = "AUDIO",
                    ContentUrl = "https://resources.sunshine.org/audio/deep-somatic-body-scan.mp3",
                    IsPremium = true,
                    ThumbnailUrl = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=500",
                    CreatedAt = DateTime.UtcNow.AddDays(-20)
                }
            );
            await db.SaveChangesAsync();
        }

        // 7. Seed Active Subscription for Anika via bKash
        var anika = await userManager.FindByEmailAsync("anika@example.com");
        var annualPlan = await db.SubscriptionPlans.FirstOrDefaultAsync(p => p.DurationType == "YEARLY");
        if (anika != null && annualPlan != null && !await db.Subscriptions.AnyAsync(s => s.UserId == anika.Id))
        {
            var payment = new Payment
            {
                UserId = anika.Id,
                Amount = annualPlan.Price,
                Currency = "BDT",
                PaymentType = PaymentTypes.Subscription,
                PaymentMethod = PaymentMethods.Bkash,
                TransactionId = "TRX-BKS-9A82FC10",
                Status = PaymentStatuses.Success,
                PaymentDate = DateTime.UtcNow.AddDays(-15),
                CreatedAt = DateTime.UtcNow.AddDays(-15)
            };
            db.Payments.Add(payment);
            await db.SaveChangesAsync();

            var sub = new Subscription
            {
                UserId = anika.Id,
                PlanId = annualPlan.Id,
                StartDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-15)),
                EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(350)),
                Status = SubscriptionStatuses.Active,
                PaymentId = payment.Id,
                CreatedAt = DateTime.UtcNow.AddDays(-15),
                UpdatedAt = DateTime.UtcNow
            };
            db.Subscriptions.Add(sub);
            await db.SaveChangesAsync();

            payment.SubscriptionId = sub.Id;
            await db.SaveChangesAsync();
        }

        // 8. Seed Sample Appointment for Anika with Dr. Tanvir
        var docTanvir = await db.Doctors.Include(d => d.User).FirstOrDefaultAsync(d => d.User.Email == "dr.tanvir@sunshine.org");
        var patAnika = await db.Patients.Include(p => p.User).FirstOrDefaultAsync(p => p.User.Email == "anika@example.com");
        if (docTanvir != null && patAnika != null && !await db.Appointments.AnyAsync())
        {
            var aptDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2));
            var apt = new Appointment
            {
                PatientId = patAnika.Id,
                DoctorId = docTanvir.Id,
                AppointmentDate = aptDate,
                StartTime = new TimeOnly(16, 0),
                EndTime = new TimeOnly(16, 45),
                Status = AppointmentStatuses.Confirmed,
                Reason = "Follow-up counseling session on corporate burnout and panic triggers.",
                Notes = "Patient is responding well to behavioral activation. Recommended Chapter 2 of CBT workbook.",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
                UpdatedAt = DateTime.UtcNow
            };
            db.Appointments.Add(apt);
            await db.SaveChangesAsync();

            db.Payments.Add(new Payment
            {
                UserId = patAnika.UserId,
                Amount = docTanvir.ConsultationFee,
                Currency = "BDT",
                PaymentType = PaymentTypes.Appointment,
                AppointmentId = apt.Id,
                PaymentMethod = PaymentMethods.Bkash,
                TransactionId = "TRX-BKS-3B77EE91",
                Status = PaymentStatuses.Success,
                PaymentDate = DateTime.UtcNow.AddDays(-2),
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            });

            db.Notifications.Add(new Notification
            {
                UserId = patAnika.UserId,
                Title = "Session Confirmed",
                Message = $"Your consultation with {docTanvir.User.FullName} for {aptDate:yyyy-MM-dd} at 04:00 PM is confirmed. Helpline support available via 16263 / 999.",
                Type = "APPOINTMENT",
                IsRead = false,
                CreatedAt = DateTime.UtcNow.AddDays(-2)
            });

            await db.SaveChangesAsync();
        }
    }
}
