-- =============================================================================
-- SCRIPT 05: CONSTRAINTS & FOREIGN KEYS
-- Project: Sunshine Mental Health Counseling System (Bangladesh)
-- Description: Applies foreign key relationships and domain CHECK constraints.
-- =============================================================================

\c sunshine_db sunshine_user

-- 1. FOREIGN KEYS

-- user_roles
DO $$ BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_user_roles_users_UserId') THEN
        ALTER TABLE user_roles ADD CONSTRAINT "FK_user_roles_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_user_roles_roles_RoleId') THEN
        ALTER TABLE user_roles ADD CONSTRAINT "FK_user_roles_roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES roles("Id") ON DELETE CASCADE;
    END IF;
END $$;

-- identity claims
DO $$ BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_role_claims_roles_RoleId') THEN
        ALTER TABLE role_claims ADD CONSTRAINT "FK_role_claims_roles_RoleId" FOREIGN KEY ("RoleId") REFERENCES roles("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_user_claims_users_UserId') THEN
        ALTER TABLE user_claims ADD CONSTRAINT "FK_user_claims_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_user_logins_users_UserId') THEN
        ALTER TABLE user_logins ADD CONSTRAINT "FK_user_logins_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_user_tokens_users_UserId') THEN
        ALTER TABLE user_tokens ADD CONSTRAINT "FK_user_tokens_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
END $$;

-- patients & doctors
DO $$ BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_patients_users_UserId') THEN
        ALTER TABLE patients ADD CONSTRAINT "FK_patients_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_doctors_users_UserId') THEN
        ALTER TABLE doctors ADD CONSTRAINT "FK_doctors_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_doctor_schedules_doctors_DoctorId') THEN
        ALTER TABLE doctor_schedules ADD CONSTRAINT "FK_doctor_schedules_doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES doctors("Id") ON DELETE CASCADE;
    END IF;
END $$;

-- appointments
DO $$ BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_appointments_patients_PatientId') THEN
        ALTER TABLE appointments ADD CONSTRAINT "FK_appointments_patients_PatientId" FOREIGN KEY ("PatientId") REFERENCES patients("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_appointments_doctors_DoctorId') THEN
        ALTER TABLE appointments ADD CONSTRAINT "FK_appointments_doctors_DoctorId" FOREIGN KEY ("DoctorId") REFERENCES doctors("Id") ON DELETE CASCADE;
    END IF;
END $$;

-- payments & subscriptions
DO $$ BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_subscriptions_users_UserId') THEN
        ALTER TABLE subscriptions ADD CONSTRAINT "FK_subscriptions_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_subscriptions_plans_PlanId') THEN
        ALTER TABLE subscriptions ADD CONSTRAINT "FK_subscriptions_plans_PlanId" FOREIGN KEY ("PlanId") REFERENCES subscription_plans("Id") ON DELETE RESTRICT;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_payments_users_UserId') THEN
        ALTER TABLE payments ADD CONSTRAINT "FK_payments_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_payments_appointments_AppointmentId') THEN
        ALTER TABLE payments ADD CONSTRAINT "FK_payments_appointments_AppointmentId" FOREIGN KEY ("AppointmentId") REFERENCES appointments("Id") ON DELETE SET NULL;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_payments_subscriptions_SubscriptionId') THEN
        ALTER TABLE payments ADD CONSTRAINT "FK_payments_subscriptions_SubscriptionId" FOREIGN KEY ("SubscriptionId") REFERENCES subscriptions("Id") ON DELETE SET NULL;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_subscriptions_payments_PaymentId') THEN
        ALTER TABLE subscriptions ADD CONSTRAINT "FK_subscriptions_payments_PaymentId" FOREIGN KEY ("PaymentId") REFERENCES payments("Id") ON DELETE SET NULL;
    END IF;
END $$;

-- resources & notifications
DO $$ BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_resources_categories_CategoryId') THEN
        ALTER TABLE resources ADD CONSTRAINT "FK_resources_categories_CategoryId" FOREIGN KEY ("CategoryId") REFERENCES resource_categories("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_resource_access_users_UserId') THEN
        ALTER TABLE resource_access ADD CONSTRAINT "FK_resource_access_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_resource_access_resources_ResourceId') THEN
        ALTER TABLE resource_access ADD CONSTRAINT "FK_resource_access_resources_ResourceId" FOREIGN KEY ("ResourceId") REFERENCES resources("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_notifications_users_UserId') THEN
        ALTER TABLE notifications ADD CONSTRAINT "FK_notifications_users_UserId" FOREIGN KEY ("UserId") REFERENCES users("Id") ON DELETE CASCADE;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'FK_audit_logs_users_ActorId') THEN
        ALTER TABLE audit_logs ADD CONSTRAINT "FK_audit_logs_users_ActorId" FOREIGN KEY ("ActorId") REFERENCES users("Id") ON DELETE SET NULL;
    END IF;
END $$;

-- 2. CHECK CONSTRAINTS

DO $$ BEGIN
    -- users role check
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_users_role') THEN
        ALTER TABLE users ADD CONSTRAINT chk_users_role CHECK ("Role" IN ('ADMIN', 'DOCTOR', 'PATIENT'));
    END IF;

    -- doctors checks
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_doctors_fee') THEN
        ALTER TABLE doctors ADD CONSTRAINT chk_doctors_fee CHECK ("ConsultationFee" >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_doctors_policy') THEN
        ALTER TABLE doctors ADD CONSTRAINT chk_doctors_policy CHECK ("PaymentPolicy" IN ('ADVANCE', 'POST_PAYMENT'));
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_doctors_exp') THEN
        ALTER TABLE doctors ADD CONSTRAINT chk_doctors_exp CHECK ("ExperienceYears" >= 0);
    END IF;

    -- doctor_schedules checks
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_schedules_day') THEN
        ALTER TABLE doctor_schedules ADD CONSTRAINT chk_schedules_day CHECK ("DayOfWeek" BETWEEN 0 AND 6);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_schedules_time') THEN
        ALTER TABLE doctor_schedules ADD CONSTRAINT chk_schedules_time CHECK ("StartTime" < "EndTime");
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_schedules_duration') THEN
        ALTER TABLE doctor_schedules ADD CONSTRAINT chk_schedules_duration CHECK ("SlotDurationMinutes" > 0);
    END IF;

    -- appointments checks
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_appointments_status') THEN
        ALTER TABLE appointments ADD CONSTRAINT chk_appointments_status CHECK ("Status" IN ('PENDING', 'CONFIRMED', 'COMPLETED', 'CANCELLED', 'NO_SHOW', 'REJECTED'));
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_appointments_time') THEN
        ALTER TABLE appointments ADD CONSTRAINT chk_appointments_time CHECK ("StartTime" < "EndTime");
    END IF;

    -- subscription_plans checks
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_plans_duration_type') THEN
        ALTER TABLE subscription_plans ADD CONSTRAINT chk_plans_duration_type CHECK ("DurationType" IN ('MONTHLY', 'QUARTERLY', 'YEARLY'));
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_plans_duration_days') THEN
        ALTER TABLE subscription_plans ADD CONSTRAINT chk_plans_duration_days CHECK ("DurationDays" > 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_plans_price') THEN
        ALTER TABLE subscription_plans ADD CONSTRAINT chk_plans_price CHECK ("Price" >= 0);
    END IF;

    -- subscriptions checks
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_subscriptions_status') THEN
        ALTER TABLE subscriptions ADD CONSTRAINT chk_subscriptions_status CHECK ("Status" IN ('ACTIVE', 'EXPIRED', 'CANCELLED'));
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_subscriptions_dates') THEN
        ALTER TABLE subscriptions ADD CONSTRAINT chk_subscriptions_dates CHECK ("StartDate" <= "EndDate");
    END IF;

    -- payments checks
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_payments_amount') THEN
        ALTER TABLE payments ADD CONSTRAINT chk_payments_amount CHECK ("Amount" >= 0);
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_payments_type') THEN
        ALTER TABLE payments ADD CONSTRAINT chk_payments_type CHECK ("PaymentType" IN ('APPOINTMENT', 'SUBSCRIPTION'));
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_payments_method') THEN
        ALTER TABLE payments ADD CONSTRAINT chk_payments_method CHECK ("PaymentMethod" IN ('BKASH', 'NAGAD', 'ROCKET', 'CARD'));
    END IF;
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_payments_status') THEN
        ALTER TABLE payments ADD CONSTRAINT chk_payments_status CHECK ("Status" IN ('PENDING', 'SUCCESS', 'FAILED', 'REFUNDED'));
    END IF;

    -- resources checks
    IF NOT EXISTS (SELECT 1 FROM pg_constraint WHERE conname = 'chk_resources_type') THEN
        ALTER TABLE resources ADD CONSTRAINT chk_resources_type CHECK ("ResourceType" IN ('BOOK', 'ARTICLE', 'AUDIO', 'VIDEO'));
    END IF;
END $$;

\echo 'Database foreign key relationships and check constraints applied successfully!';
