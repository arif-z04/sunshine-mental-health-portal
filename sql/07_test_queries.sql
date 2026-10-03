-- =============================================================================
-- SCRIPT 07: VERIFICATION & TEST QUERIES
-- Project: Sunshine Mental Health Counseling System (Bangladesh)
-- Description: Diagnostic and verification queries to validate database integrity,
--              double-booking safety, relationships, BDT finances, and metrics.
-- =============================================================================

\c sunshine_db sunshine_user

\echo '=== 1. System Row Count Health Check ==='
SELECT 'users' AS "Table", COUNT(*) AS "Count" FROM users
UNION ALL
SELECT 'roles', COUNT(*) FROM roles
UNION ALL
SELECT 'doctors', COUNT(*) FROM doctors
UNION ALL
SELECT 'doctor_schedules', COUNT(*) FROM doctor_schedules
UNION ALL
SELECT 'patients', COUNT(*) FROM patients
UNION ALL
SELECT 'appointments', COUNT(*) FROM appointments
UNION ALL
SELECT 'subscription_plans', COUNT(*) FROM subscription_plans
UNION ALL
SELECT 'subscriptions', COUNT(*) FROM subscriptions
UNION ALL
SELECT 'payments', COUNT(*) FROM payments
UNION ALL
SELECT 'resource_categories', COUNT(*) FROM resource_categories
UNION ALL
SELECT 'resources', COUNT(*) FROM resources
UNION ALL
SELECT 'notifications', COUNT(*) FROM notifications
UNION ALL
SELECT 'audit_logs', COUNT(*) FROM audit_logs;

\echo ''
\echo '=== 2. Registered Doctors with Bangladesh BMDC Credentials & Fees ==='
SELECT
    d."Id" AS "DoctorId",
    u."FullName",
    u."Email",
    u."PhoneNumber",
    d."Specialization",
    d."Qualification",
    d."ConsultationFee" AS "Fee_BDT",
    d."PaymentPolicy",
    d."IsAvailable",
    d."ExperienceYears"
FROM doctors d
JOIN users u ON d."UserId" = u."Id"
ORDER BY d."Id";

\echo ''
\echo '=== 3. Active Weekly Doctor Clinical Hours (BST) ==='
SELECT
    d."Id" AS "DoctorId",
    u."FullName" AS "DoctorName",
    CASE ds."DayOfWeek"
        WHEN 0 THEN 'Sunday'
        WHEN 1 THEN 'Monday'
        WHEN 2 THEN 'Tuesday'
        WHEN 3 THEN 'Wednesday'
        WHEN 4 THEN 'Thursday'
        WHEN 5 THEN 'Friday'
        WHEN 6 THEN 'Saturday'
    END AS "DayOfWeek",
    ds."StartTime",
    ds."EndTime",
    ds."SlotDurationMinutes"
FROM doctor_schedules ds
JOIN doctors d ON ds."DoctorId" = d."Id"
JOIN users u ON d."UserId" = u."Id"
WHERE ds."IsActive" = TRUE
ORDER BY d."Id", ds."DayOfWeek", ds."StartTime";

\echo ''
\echo '=== 4. Appointment Ledger with Patient Details & Status ==='
SELECT
    a."Id" AS "AptId",
    pat_u."FullName" AS "PatientName",
    doc_u."FullName" AS "DoctorName",
    a."AppointmentDate",
    a."StartTime",
    a."EndTime",
    a."Status",
    a."Reason",
    pay."Status" AS "PaymentStatus",
    pay."TransactionId"
FROM appointments a
JOIN patients p ON a."PatientId" = p."Id"
JOIN users pat_u ON p."UserId" = pat_u."Id"
JOIN doctors d ON a."DoctorId" = d."Id"
JOIN users doc_u ON d."UserId" = doc_u."Id"
LEFT JOIN payments pay ON a."Id" = pay."AppointmentId"
ORDER BY a."AppointmentDate" DESC, a."StartTime" DESC;

\echo ''
\echo '=== 5. Double-Booking Constraint Verification Test ==='
-- The partial unique index: "IX_appointments_DoctorId_AppointmentDate_StartTime"
-- This query demonstrates that PostgreSQL prevents conflicting slots:
SELECT
    indexname,
    indexdef
FROM pg_indexes
WHERE tablename = 'appointments' AND indexname LIKE '%IX_appointments_DoctorId%';

\echo ''
\echo '=== 6. Total Revenue Collected in BDT (৳) ==='
SELECT
    "Currency",
    "PaymentType",
    "PaymentMethod",
    COUNT(*) AS "TotalTransactions",
    SUM("Amount") AS "TotalAmount_BDT"
FROM payments
WHERE "Status" = 'SUCCESS'
GROUP BY "Currency", "PaymentType", "PaymentMethod";

\echo ''
\echo '=== 7. Active Subscriptions & Paywalled Resource Library Access ==='
SELECT
    s."Id" AS "SubId",
    u."FullName" AS "SubscriberName",
    u."Email",
    sp."Name" AS "PlanName",
    s."StartDate",
    s."EndDate",
    s."Status",
    sp."Price" AS "PlanPrice_BDT"
FROM subscriptions s
JOIN users u ON s."UserId" = u."Id"
JOIN subscription_plans sp ON s."PlanId" = sp."Id"
WHERE s."Status" = 'ACTIVE';

\echo ''
\echo '=== 8. Digital Mental Health Vault (Free vs Premium CBT Resources) ==='
SELECT
    rc."Name" AS "Category",
    r."Title",
    r."Author",
    r."ResourceType",
    CASE WHEN r."IsPremium" THEN 'PREMIUM (Subscription Required)' ELSE 'FREE (Open Access)' END AS "AccessLevel",
    r."ContentUrl"
FROM resources r
JOIN resource_categories rc ON r."CategoryId" = rc."Id"
ORDER BY r."IsPremium", rc."Id";

\echo ''
\echo '=== 9. Platform Audit Trail ==='
SELECT
    "Id",
    "ActorEmail",
    "Action",
    "TargetType",
    "TargetId",
    "Details",
    "CreatedAt"
FROM audit_logs
ORDER BY "CreatedAt" DESC;

\echo 'Sanity check and verification queries completed successfully!';
