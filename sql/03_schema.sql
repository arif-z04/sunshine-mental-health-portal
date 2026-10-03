-- =============================================================================
-- SCRIPT 03: DATABASE SCHEMA DEFINITION
-- Project: Sunshine Mental Health Counseling System (Bangladesh)
-- Description: Defines normalized PostgreSQL tables for users, identity,
--              patients, doctors, schedules, appointments, subscriptions,
--              payments (bKash/Nagad), resources, notifications, and audit logs.
-- =============================================================================

\c sunshine_db sunshine_user

-- 1. ROLES TABLE (ASP.NET Core Identity ApplicationRole)
CREATE TABLE IF NOT EXISTS roles (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(256) NULL,
    "NormalizedName" VARCHAR(256) NULL,
    "ConcurrencyStamp" TEXT NULL
);

-- 2. USERS TABLE (ASP.NET Core Identity ApplicationUser)
CREATE TABLE IF NOT EXISTS users (
    "Id" SERIAL PRIMARY KEY,
    "FullName" VARCHAR(150) NOT NULL,
    "Role" VARCHAR(20) NOT NULL DEFAULT 'PATIENT',
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UserName" VARCHAR(256) NULL,
    "NormalizedUserName" VARCHAR(256) NULL,
    "Email" VARCHAR(256) NOT NULL,
    "NormalizedEmail" VARCHAR(256) NULL,
    "EmailConfirmed" BOOLEAN NOT NULL DEFAULT FALSE,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" VARCHAR(50) NULL,
    "PhoneNumberConfirmed" BOOLEAN NOT NULL DEFAULT FALSE,
    "TwoFactorEnabled" BOOLEAN NOT NULL DEFAULT FALSE,
    "LockoutEnd" TIMESTAMPTZ NULL,
    "LockoutEnabled" BOOLEAN NOT NULL DEFAULT FALSE,
    "AccessFailedCount" INT NOT NULL DEFAULT 0
);

-- 3. USER ROLES JUNCTION TABLE
CREATE TABLE IF NOT EXISTS user_roles (
    "UserId" INT NOT NULL,
    "RoleId" INT NOT NULL,
    PRIMARY KEY ("UserId", "RoleId")
);

-- 4. IDENTITY CLAIMS, LOGINS & TOKENS
CREATE TABLE IF NOT EXISTS role_claims (
    "Id" SERIAL PRIMARY KEY,
    "RoleId" INT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL
);

CREATE TABLE IF NOT EXISTS user_claims (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL
);

CREATE TABLE IF NOT EXISTS user_logins (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" INT NOT NULL,
    PRIMARY KEY ("LoginProvider", "ProviderKey")
);

CREATE TABLE IF NOT EXISTS user_tokens (
    "UserId" INT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    PRIMARY KEY ("UserId", "LoginProvider", "Name")
);

-- 5. PATIENTS TABLE (1-to-1 extension of users)
CREATE TABLE IF NOT EXISTS patients (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL UNIQUE,
    "DateOfBirth" DATE NULL,
    "Gender" VARCHAR(20) NULL,
    "EmergencyContact" VARCHAR(100) NULL,
    "MedicalHistoryNotes" TEXT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 6. DOCTORS TABLE (1-to-1 extension of users)
CREATE TABLE IF NOT EXISTS doctors (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL UNIQUE,
    "Specialization" VARCHAR(150) NOT NULL,
    "Bio" TEXT NULL,
    "ConsultationFee" NUMERIC(10,2) NOT NULL DEFAULT 1000.00,
    "PaymentPolicy" VARCHAR(20) NOT NULL DEFAULT 'ADVANCE',
    "IsAvailable" BOOLEAN NOT NULL DEFAULT TRUE,
    "ExperienceYears" INT NOT NULL DEFAULT 0,
    "Qualification" VARCHAR(255) NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 7. DOCTOR SCHEDULES TABLE (Recurring weekly clinical hours)
CREATE TABLE IF NOT EXISTS doctor_schedules (
    "Id" SERIAL PRIMARY KEY,
    "DoctorId" INT NOT NULL,
    "DayOfWeek" INT NOT NULL, -- 0=Sunday, 1=Monday ... 6=Saturday
    "StartTime" TIME NOT NULL,
    "EndTime" TIME NOT NULL,
    "SlotDurationMinutes" INT NOT NULL DEFAULT 45,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE
);

-- 8. APPOINTMENTS TABLE
CREATE TABLE IF NOT EXISTS appointments (
    "Id" SERIAL PRIMARY KEY,
    "PatientId" INT NOT NULL,
    "DoctorId" INT NOT NULL,
    "AppointmentDate" DATE NOT NULL,
    "StartTime" TIME NOT NULL,
    "EndTime" TIME NOT NULL,
    "Status" VARCHAR(20) NOT NULL DEFAULT 'PENDING',
    "Reason" TEXT NULL,
    "Notes" TEXT NULL,
    "BookingExpiresAt" TIMESTAMPTZ NULL, -- 15-minute advance payment countdown window
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 9. SUBSCRIPTION PLANS TABLE (in BDT ৳)
CREATE TABLE IF NOT EXISTS subscription_plans (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "DurationType" VARCHAR(20) NOT NULL,
    "DurationDays" INT NOT NULL,
    "Price" NUMERIC(10,2) NOT NULL,
    "Description" TEXT NULL,
    "IsActive" BOOLEAN NOT NULL DEFAULT TRUE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 10. PAYMENTS TABLE (bKash, Nagad, Rocket, Cards)
CREATE TABLE IF NOT EXISTS payments (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "Amount" NUMERIC(10,2) NOT NULL,
    "Currency" VARCHAR(10) NOT NULL DEFAULT 'BDT',
    "PaymentType" VARCHAR(20) NOT NULL,
    "AppointmentId" INT NULL,
    "SubscriptionId" INT NULL,
    "PaymentMethod" VARCHAR(50) NOT NULL DEFAULT 'BKASH',
    "TransactionId" VARCHAR(100) NULL,
    "Status" VARCHAR(20) NOT NULL DEFAULT 'PENDING',
    "PaymentDate" TIMESTAMPTZ NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 11. SUBSCRIPTIONS TABLE
CREATE TABLE IF NOT EXISTS subscriptions (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "PlanId" INT NOT NULL,
    "StartDate" DATE NOT NULL,
    "EndDate" DATE NOT NULL,
    "Status" VARCHAR(20) NOT NULL DEFAULT 'ACTIVE',
    "PaymentId" INT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP,
    "UpdatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 12. RESOURCE CATEGORIES TABLE
CREATE TABLE IF NOT EXISTS resource_categories (
    "Id" SERIAL PRIMARY KEY,
    "Name" VARCHAR(100) NOT NULL,
    "Slug" VARCHAR(100) NOT NULL UNIQUE,
    "Description" TEXT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 13. RESOURCES TABLE
CREATE TABLE IF NOT EXISTS resources (
    "Id" SERIAL PRIMARY KEY,
    "CategoryId" INT NOT NULL,
    "Title" VARCHAR(255) NOT NULL,
    "Author" VARCHAR(150) NOT NULL,
    "Description" TEXT NULL,
    "ResourceType" VARCHAR(20) NOT NULL DEFAULT 'BOOK',
    "ContentUrl" TEXT NOT NULL,
    "IsPremium" BOOLEAN NOT NULL DEFAULT FALSE,
    "ThumbnailUrl" TEXT NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 14. RESOURCE ACCESS LOG TABLE
CREATE TABLE IF NOT EXISTS resource_access (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "ResourceId" INT NOT NULL,
    "AccessedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 15. NOTIFICATIONS TABLE
CREATE TABLE IF NOT EXISTS notifications (
    "Id" SERIAL PRIMARY KEY,
    "UserId" INT NOT NULL,
    "Title" VARCHAR(200) NOT NULL,
    "Message" TEXT NOT NULL,
    "Type" VARCHAR(20) NOT NULL DEFAULT 'SYSTEM',
    "IsRead" BOOLEAN NOT NULL DEFAULT FALSE,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- 16. AUDIT LOGS TABLE
CREATE TABLE IF NOT EXISTS audit_logs (
    "Id" SERIAL PRIMARY KEY,
    "ActorId" INT NULL,
    "ActorEmail" VARCHAR(256) NULL,
    "Action" VARCHAR(100) NOT NULL,
    "TargetType" VARCHAR(100) NULL,
    "TargetId" VARCHAR(100) NULL,
    "Details" TEXT NULL,
    "IpAddress" VARCHAR(50) NULL,
    "CreatedAt" TIMESTAMPTZ NOT NULL DEFAULT CURRENT_TIMESTAMP
);

\echo 'Database schema tables defined successfully!';
