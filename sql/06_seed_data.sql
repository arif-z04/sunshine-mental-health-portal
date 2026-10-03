-- =============================================================================
-- SCRIPT 06: REALISTIC BANGLADESH SEED DATA
-- Project: Sunshine Mental Health Counseling System (Bangladesh)
-- Description: Seeds roles, Bangladeshi clinicians, patients, schedules,
--              BDT plans, CBT resources, demo appointments, bKash payments,
--              notifications, and initial audit trail.
-- =============================================================================

\c sunshine_db sunshine_user

-- 1. Insert Identity Roles
INSERT INTO roles ("Id", "Name", "NormalizedName", "ConcurrencyStamp")
VALUES
  (1, 'ADMIN', 'ADMIN', gen_random_uuid()::text),
  (2, 'DOCTOR', 'DOCTOR', gen_random_uuid()::text),
  (3, 'PATIENT', 'PATIENT', gen_random_uuid()::text)
ON CONFLICT ("Id") DO NOTHING;

-- 2. Insert Users
-- Standard password: 'Password123!'
-- ASP.NET Core Identity PBKDF2 hash: AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==
INSERT INTO users (
    "Id", "FullName", "Role", "IsActive", "UserName", "NormalizedUserName",
    "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash",
    "SecurityStamp", "ConcurrencyStamp", "PhoneNumber", "PhoneNumberConfirmed",
    "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount", "CreatedAt", "UpdatedAt"
)
VALUES
  -- Super Admin
  (1, 'Prof. Dr. Farzana Rahman', 'ADMIN', TRUE, 'admin@sunshine.org', 'ADMIN@SUNSHINE.ORG',
   'admin@sunshine.org', 'ADMIN@SUNSHINE.ORG', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801711000001', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Doctor 1: Dr. Tanvir Ahmed (BSMMU)
  (2, 'Dr. Tanvir Ahmed, MBBS, MD', 'DOCTOR', TRUE, 'dr.tanvir@sunshine.org', 'DR.TANVIR@SUNSHINE.ORG',
   'dr.tanvir@sunshine.org', 'DR.TANVIR@SUNSHINE.ORG', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801712000101', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Doctor 2: Nusrat Jahan (DU CBT)
  (3, 'Nusrat Jahan, MS (DU)', 'DOCTOR', TRUE, 'dr.nusrat@sunshine.org', 'DR.NUSRAT@SUNSHINE.ORG',
   'dr.nusrat@sunshine.org', 'DR.NUSRAT@SUNSHINE.ORG', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801819000102', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Doctor 3: Dr. K. M. Rafiqul Islam (NIMH)
  (4, 'Dr. K. M. Rafiqul Islam, FCPS', 'DOCTOR', TRUE, 'dr.rafiq@sunshine.org', 'DR.RAFIQ@SUNSHINE.ORG',
   'dr.rafiq@sunshine.org', 'DR.RAFIQ@SUNSHINE.ORG', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801914000103', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Doctor 4: Dr. Mahmud Hasan (Sylhet)
  (8, 'Dr. Mahmud Hasan, MBBS, MPhil', 'DOCTOR', TRUE, 'dr.mahmud@sunshine.org', 'DR.MAHMUD@SUNSHINE.ORG',
   'dr.mahmud@sunshine.org', 'DR.MAHMUD@SUNSHINE.ORG', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801715000104', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Doctor 5: Dr. Tahmina Akter (Chittagong)
  (9, 'Dr. Tahmina Akter, MS, CMC', 'DOCTOR', TRUE, 'dr.tahmina@sunshine.org', 'DR.TAHMINA@SUNSHINE.ORG',
   'dr.tahmina@sunshine.org', 'DR.TAHMINA@SUNSHINE.ORG', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801817000105', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Patient 1: Anika Tabassum (Subscribed)
  (5, 'Anika Tabassum', 'PATIENT', TRUE, 'anika@example.com', 'ANIKA@EXAMPLE.COM',
   'anika@example.com', 'ANIKA@EXAMPLE.COM', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801711223344', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Patient 2: Rahat Chowdhury
  (6, 'Rahat Chowdhury', 'PATIENT', TRUE, 'rahat@example.com', 'RAHAT@EXAMPLE.COM',
   'rahat@example.com', 'RAHAT@EXAMPLE.COM', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801819334455', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  -- Patient 3: Sazzad Hossain
  (7, 'Sazzad Hossain', 'PATIENT', TRUE, 'sazzad@example.com', 'SAZZAD@EXAMPLE.COM',
   'sazzad@example.com', 'SAZZAD@EXAMPLE.COM', TRUE, 'AQAAAAEAAYagAAAAEBI0VniavN7wESIzRFVmd4gzc2wfb8/OBfW6OCzcMQ+oeP9WRC9KlVaoYnvCuaWBpQ==',
   gen_random_uuid()::text, gen_random_uuid()::text, '+8801912445566', TRUE, FALSE, FALSE, 0, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

-- Align sequences
SELECT setval(pg_get_serial_sequence('users', 'Id'), (SELECT MAX("Id") FROM users));
SELECT setval(pg_get_serial_sequence('roles', 'Id'), (SELECT MAX("Id") FROM roles));

-- 3. Assign User Roles
INSERT INTO user_roles ("UserId", "RoleId")
VALUES
  (1, 1), -- Admin
  (2, 2), -- Dr. Tanvir
  (3, 2), -- Nusrat Jahan
  (4, 2), -- Dr. Rafiqul
  (8, 2), -- Dr. Mahmud
  (9, 2), -- Dr. Tahmina
  (5, 3), -- Anika
  (6, 3), -- Rahat
  (7, 3)  -- Sazzad
ON CONFLICT DO NOTHING;

-- 4. Insert Doctors
INSERT INTO doctors ("Id", "UserId", "Specialization", "Bio", "ConsultationFee", "PaymentPolicy", "IsAvailable", "ExperienceYears", "Qualification", "CreatedAt", "UpdatedAt")
VALUES
  (1, 2, 'Adult Psychiatry & Clinical Depression',
   'Assistant Professor of Psychiatry at BSMMU (PG Hospital). Expert in mood stabilization, panic disorder, and stress recovery with 14+ years of clinical experience in Dhaka.',
   1500.00, 'ADVANCE', TRUE, 14, 'MBBS (DMC), MD (Psychiatry, BSMMU), BMDC Reg: A-54892', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  (2, 3, 'Cognitive Behavioral Therapy (CBT) & Exam Anxiety',
   'Senior Clinical Psychologist (DU). Specializes in cognitive restructuring for university/BCS exam pressure, OCD, and young adult psychological well-being.',
   1200.00, 'ADVANCE', TRUE, 10, 'B.Sc (Hons), M.S. in Clinical Psychology (University of Dhaka)', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  (3, 4, 'Family, Marital & Adolescent Counseling',
   'Consultant Psychiatrist at National Institute of Mental Health (NIMH), Dhaka. Compassionate counselor for couples communication, family transitions, and adolescent mental health.',
   1800.00, 'POST_PAYMENT', TRUE, 16, 'MBBS, FCPS (Psychiatry, NIMH), MACP (USA), BMDC Reg: A-41209', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  (4, 8, 'Addiction Recovery & Stress Management',
   'Consultant Psychiatrist at Sylhet MAG Osmani Medical College. Specializes in neurobiology of stress, holistic rehabilitation, and emotional trauma recovery.',
   1000.00, 'ADVANCE', TRUE, 8, 'MBBS (SOMC), MPhil (Psychiatry), BMDC Reg: A-62314', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),

  (5, 9, 'Child & Adolescent Behavioral Therapy',
   'Clinical Psychologist at Chittagong Medical College Hospital. Dedicated to adolescent anxiety, neurodivergence support, and parental coaching.',
   1400.00, 'ADVANCE', TRUE, 11, 'B.Sc, M.S. in Psychology (CU), Trained in Play Therapy', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

SELECT setval(pg_get_serial_sequence('doctors', 'Id'), (SELECT MAX("Id") FROM doctors));

-- 5. Insert Doctor Working Schedules (Sat-Thu evening clinics in BST)
INSERT INTO doctor_schedules ("DoctorId", "DayOfWeek", "StartTime", "EndTime", "SlotDurationMinutes", "IsActive")
SELECT d."Id", s.day, '16:00'::time, '21:00'::time, 45, TRUE
FROM doctors d
CROSS JOIN (VALUES (6), (0), (1), (2), (3), (4)) AS s(day)
ON CONFLICT DO NOTHING;

-- 6. Insert Patients
INSERT INTO patients ("Id", "UserId", "DateOfBirth", "Gender", "EmergencyContact", "MedicalHistoryNotes", "CreatedAt", "UpdatedAt")
VALUES
  (1, 5, '1996-06-20', 'Female', 'Farhan Tabassum (+8801711998877)', 'Workplace burnout & corporate stress in Gulshan, Dhaka.', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
  (2, 6, '1998-09-12', 'Male', 'Nasrin Chowdhury (+8801819887766)', 'BCS & competitive examination anxiety.', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP),
  (3, 7, '1994-03-05', 'Male', 'Fatema Hossain (+8801912776655)', 'General emotional check-in & mindfulness practice.', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

SELECT setval(pg_get_serial_sequence('patients', 'Id'), (SELECT MAX("Id") FROM patients));

-- 7. Insert Subscription Plans (in BDT ৳)
INSERT INTO subscription_plans ("Id", "Name", "DurationType", "DurationDays", "Price", "Description", "IsActive", "CreatedAt")
VALUES
  (1, 'Monthly Mindcare (মাসিক মাইন্ডকেয়ার)', 'MONTHLY', 30, 499.00,
   'Unlimited 24/7 access to all therapeutic e-books, sleep roadmaps, and mindfulness audio recordings in Bengali and English.', TRUE, CURRENT_TIMESTAMP),
  (2, 'Quarterly Wellness Pass (ত্রৈমাসিক ওয়েলনেস পাস)', 'QUARTERLY', 90, 1299.00,
   '3 months of full digital library access plus newly published clinical worksheets (Save 15%).', TRUE, CURRENT_TIMESTAMP),
  (3, 'Annual Holistic Pass (বার্ষিক হলিস্টিক পাস)', 'YEARLY', 365, 3999.00,
   'Full 1-year unlimited access to our entire mental wellness digital vault, audio masterclasses, and exam stress protocols (Save 35%).', TRUE, CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

SELECT setval(pg_get_serial_sequence('subscription_plans', 'Id'), (SELECT MAX("Id") FROM subscription_plans));

-- 8. Insert Resource Categories
INSERT INTO resource_categories ("Id", "Name", "Slug", "Description", "CreatedAt")
VALUES
  (1, 'Anxiety & Exam Stress (উদ্বেগ ও পরীক্ষার চাপ)', 'anxiety-stress', 'Cognitive restructuring, breathing protocols, and university/BCS stress tools.', CURRENT_TIMESTAMP),
  (2, 'Depression & Low Mood (হতাশা ও আত্মবিশ্বাস)', 'depression-mood', 'Behavioral activation roadmaps and mood restoration habits.', CURRENT_TIMESTAMP),
  (3, 'Mindfulness & Meditation (মননশীল মেডিটেশন)', 'mindfulness-meditation', 'Somatic body scan audio and grounding exercises in Bengali and English.', CURRENT_TIMESTAMP),
  (4, 'Sleep & Urban Health (ঘুম ও স্বাস্থ্যবিধি)', 'sleep-health', 'Circadian rhythm protocols and evening unwind checklists for urban life in Bangladesh.', CURRENT_TIMESTAMP),
  (5, 'Relationships & Family (পারিবারিক ও দাম্পত্য সম্পর্ক)', 'relationships-family', 'Nonviolent communication guides and family boundary frameworks.', CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

SELECT setval(pg_get_serial_sequence('resource_categories', 'Id'), (SELECT MAX("Id") FROM resource_categories));

-- 9. Insert Resources
INSERT INTO resources ("Id", "CategoryId", "Title", "Author", "Description", "ResourceType", "ContentUrl", "IsPremium", "ThumbnailUrl", "CreatedAt")
VALUES
  (1, 1, 'Understanding the Anatomy of Anxiety & Panic Triggers', 'Nusrat Jahan, MS (DU)',
   'A clinical handbook explaining autonomic fight-or-flight triggers and practical 5-minute de-escalation methods for students and professionals in Bangladesh.',
   'ARTICLE', 'https://resources.sunshine.org/articles/anatomy-of-anxiety-bd.pdf', FALSE,
   'https://images.unsplash.com/photo-1506126613408-eca07ce68773?w=500', CURRENT_TIMESTAMP),

  (2, 3, '5-Minute Morning Grounding Meditation (বাংলা ও ইংরেজি গাইড)', 'Prof. Dr. Farzana Rahman',
   'Quick audio exercise using the 5-4-3-2-1 sensory technique to center thoughts before starting a busy day.',
   'AUDIO', 'https://resources.sunshine.org/audio/morning-grounding-bd.mp3', FALSE,
   'https://images.unsplash.com/photo-1518241353330-0f7941c2d9b5?w=500', CURRENT_TIMESTAMP),

  (3, 1, 'Mastering Cognitive Restructuring: Complete CBT Workbook (বাংলা সংস্করণ)', 'Nusrat Jahan, MS (DU)',
   'An 80-page interactive workbook with thought records, cognitive distortion breakdowns, and graded exposure exercises tailored for Bangladeshi students and working professionals.',
   'BOOK', 'https://resources.sunshine.org/books/cbt-mastery-workbook-bd.pdf', TRUE,
   'https://images.unsplash.com/photo-1544717305-2782549b5136?w=500', CURRENT_TIMESTAMP),

  (4, 2, 'Behavioral Activation Roadmap for Overcoming Low Mood', 'Dr. Tanvir Ahmed, MBBS, MD (BSMMU)',
   'Step-by-step clinical roadmap with daily routine restoration sheets, energy recovery metrics, and mood tracking.',
   'BOOK', 'https://resources.sunshine.org/books/behavioral-activation-roadmap.pdf', TRUE,
   'https://images.unsplash.com/photo-1499209974431-9dddcece7f88?w=500', CURRENT_TIMESTAMP),

  (5, 3, 'Deep Somatic Body Scan for Stress Release (45 min)', 'Dr. K. M. Rafiqul Islam, FCPS',
   'High-fidelity guided meditation audio designed to release chronic tension and somatic stress after long working days in Dhaka.',
   'AUDIO', 'https://resources.sunshine.org/audio/deep-somatic-body-scan.mp3', TRUE,
   'https://images.unsplash.com/photo-1507525428034-b723cf961d3e?w=500', CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

SELECT setval(pg_get_serial_sequence('resources', 'Id'), (SELECT MAX("Id") FROM resources));

-- 10. Insert Sample Active Subscription & bKash Payment for Anika Tabassum
INSERT INTO payments ("Id", "UserId", "Amount", "Currency", "PaymentType", "PaymentMethod", "TransactionId", "Status", "PaymentDate", "CreatedAt")
VALUES
  (1, 5, 3999.00, 'BDT', 'SUBSCRIPTION', 'BKASH', 'TRX-BKS-9A82FC10', 'SUCCESS', CURRENT_TIMESTAMP - INTERVAL '15 days', CURRENT_TIMESTAMP - INTERVAL '15 days')
ON CONFLICT ("Id") DO NOTHING;

INSERT INTO subscriptions ("Id", "UserId", "PlanId", "StartDate", "EndDate", "Status", "PaymentId", "CreatedAt", "UpdatedAt")
VALUES
  (1, 5, 3, CURRENT_DATE - 15, CURRENT_DATE + 350, 'ACTIVE', 1, CURRENT_TIMESTAMP - INTERVAL '15 days', CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

UPDATE payments SET "SubscriptionId" = 1 WHERE "Id" = 1;

-- 11. Insert Sample Confirmed Appointment with Dr. Tanvir
INSERT INTO appointments ("Id", "PatientId", "DoctorId", "AppointmentDate", "StartTime", "EndTime", "Status", "Reason", "Notes", "CreatedAt", "UpdatedAt")
VALUES
  (1, 1, 1, CURRENT_DATE + 2, '16:00', '16:45', 'CONFIRMED',
   'Follow-up counseling session on corporate burnout and panic triggers.',
   'Patient is responding well to behavioral activation. Recommended Chapter 2 of CBT workbook.',
   CURRENT_TIMESTAMP - INTERVAL '2 days', CURRENT_TIMESTAMP)
ON CONFLICT ("Id") DO NOTHING;

SELECT setval(pg_get_serial_sequence('appointments', 'Id'), (SELECT MAX("Id") FROM appointments));

INSERT INTO payments ("Id", "UserId", "Amount", "Currency", "PaymentType", "AppointmentId", "PaymentMethod", "TransactionId", "Status", "PaymentDate", "CreatedAt")
VALUES
  (2, 5, 1500.00, 'BDT', 'APPOINTMENT', 1, 'BKASH', 'TRX-BKS-3B77EE91', 'SUCCESS', CURRENT_TIMESTAMP - INTERVAL '2 days', CURRENT_TIMESTAMP - INTERVAL '2 days')
ON CONFLICT ("Id") DO NOTHING;

SELECT setval(pg_get_serial_sequence('payments', 'Id'), (SELECT MAX("Id") FROM payments));

-- 12. Insert Notifications
INSERT INTO notifications ("UserId", "Title", "Message", "Type", "IsRead", "CreatedAt")
VALUES
  (5, 'Session Confirmed', 'Your consultation with Dr. Tanvir Ahmed for 04:00 PM BST is confirmed. Helpline support available via 16263 / 999.', 'APPOINTMENT', FALSE, CURRENT_TIMESTAMP - INTERVAL '2 days'),
  (2, 'New Patient Booking', 'Patient Anika Tabassum booked a 45-minute consultation session.', 'APPOINTMENT', TRUE, CURRENT_TIMESTAMP - INTERVAL '2 days');

-- 13. Insert Initial Audit Logs
INSERT INTO audit_logs ("ActorId", "ActorEmail", "Action", "TargetType", "TargetId", "Details", "IpAddress", "CreatedAt")
VALUES
  (1, 'admin@sunshine.org', 'SYSTEM_INIT', 'PLATFORM', '1', 'Initial Bangladesh production-ready database seed completed.', '127.0.0.1', CURRENT_TIMESTAMP - INTERVAL '30 days'),
  (1, 'admin@sunshine.org', 'DOCTOR_VERIFIED', 'DOCTOR', '1', 'Verified BMDC credentials for Dr. Tanvir Ahmed (A-54892).', '127.0.0.1', CURRENT_TIMESTAMP - INTERVAL '20 days'),
  (1, 'admin@sunshine.org', 'DOCTOR_VERIFIED', 'DOCTOR', '3', 'Verified BMDC credentials for Dr. K. M. Rafiqul Islam (A-41209).', '127.0.0.1', CURRENT_TIMESTAMP - INTERVAL '20 days');

-- 14. Synchronize all table serial sequences
DO $$
DECLARE
    r RECORD;
BEGIN
    FOR r IN (
        SELECT table_name, column_name, pg_get_serial_sequence(table_name, column_name) as seq
        FROM information_schema.columns 
        WHERE table_schema = 'public' AND column_default LIKE 'nextval%'
    ) LOOP
        IF r.seq IS NOT NULL THEN
            EXECUTE format('SELECT setval(%L, COALESCE((SELECT MAX(%I) FROM %I), 0) + 1, false)', r.seq, r.column_name, r.table_name);
        END IF;
    END LOOP;
END $$;

\echo 'Realistic Bangladesh seed data inserted and sequences synchronized successfully!';
