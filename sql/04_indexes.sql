-- =============================================================================
-- SCRIPT 04: INDEXES & PERFORMANCE OPTIMIZATION
-- Project: Sunshine Mental Health Counseling System (Bangladesh)
-- Description: Creates indexes for foreign keys, frequent queries, search,
--              and critical concurrency constraints (partial unique index).
-- =============================================================================

\c sunshine_db sunshine_user

-- 1. Identity Indexes
CREATE UNIQUE INDEX IF NOT EXISTS "RoleNameIndex" ON roles ("NormalizedName") WHERE "NormalizedName" IS NOT NULL;
CREATE UNIQUE INDEX IF NOT EXISTS "IX_users_Email" ON users ("Email");
CREATE INDEX IF NOT EXISTS "EmailIndex" ON users ("NormalizedEmail");
CREATE UNIQUE INDEX IF NOT EXISTS "UserNameIndex" ON users ("NormalizedUserName") WHERE "NormalizedUserName" IS NOT NULL;

-- 2. Doctor & Schedule Indexes
CREATE INDEX IF NOT EXISTS "IX_doctors_UserId" ON doctors ("UserId");
CREATE INDEX IF NOT EXISTS "IX_doctors_Specialization" ON doctors ("Specialization");
CREATE INDEX IF NOT EXISTS "IX_doctors_IsAvailable" ON doctors ("IsAvailable");
CREATE INDEX IF NOT EXISTS "IX_doctor_schedules_DoctorId" ON doctor_schedules ("DoctorId");
CREATE INDEX IF NOT EXISTS "IX_doctor_schedules_DayOfWeek" ON doctor_schedules ("DayOfWeek");

-- 3. Patient Indexes
CREATE INDEX IF NOT EXISTS "IX_patients_UserId" ON patients ("UserId");

-- 4. Appointment Indexes
-- CRITICAL: Concurrency safety - partial unique index prevents double booking active slots
CREATE UNIQUE INDEX IF NOT EXISTS "IX_appointments_DoctorId_AppointmentDate_StartTime"
ON appointments ("DoctorId", "AppointmentDate", "StartTime")
WHERE "Status" != 'CANCELLED';

CREATE INDEX IF NOT EXISTS "IX_appointments_PatientId" ON appointments ("PatientId");
CREATE INDEX IF NOT EXISTS "IX_appointments_DoctorId" ON appointments ("DoctorId");
CREATE INDEX IF NOT EXISTS "IX_appointments_AppointmentDate" ON appointments ("AppointmentDate");
CREATE INDEX IF NOT EXISTS "IX_appointments_Status" ON appointments ("Status");

-- 5. Payments Indexes
CREATE UNIQUE INDEX IF NOT EXISTS "IX_payments_TransactionId" ON payments ("TransactionId") WHERE "TransactionId" IS NOT NULL;
CREATE INDEX IF NOT EXISTS "IX_payments_UserId" ON payments ("UserId");
CREATE INDEX IF NOT EXISTS "IX_payments_AppointmentId" ON payments ("AppointmentId");
CREATE INDEX IF NOT EXISTS "IX_payments_SubscriptionId" ON payments ("SubscriptionId");
CREATE INDEX IF NOT EXISTS "IX_payments_Status" ON payments ("Status");

-- 6. Subscriptions Indexes
CREATE INDEX IF NOT EXISTS "IX_subscriptions_UserId" ON subscriptions ("UserId");
CREATE INDEX IF NOT EXISTS "IX_subscriptions_PlanId" ON subscriptions ("PlanId");
CREATE INDEX IF NOT EXISTS "IX_subscriptions_PaymentId" ON subscriptions ("PaymentId");
CREATE INDEX IF NOT EXISTS "IX_subscriptions_Status" ON subscriptions ("Status");

-- 7. Resources & Categories Indexes
CREATE INDEX IF NOT EXISTS "IX_resources_CategoryId" ON resources ("CategoryId");
CREATE INDEX IF NOT EXISTS "IX_resources_IsPremium" ON resources ("IsPremium");
CREATE INDEX IF NOT EXISTS "IX_resources_ResourceType" ON resources ("ResourceType");
CREATE INDEX IF NOT EXISTS "IX_resource_access_UserId" ON resource_access ("UserId");
CREATE INDEX IF NOT EXISTS "IX_resource_access_ResourceId" ON resource_access ("ResourceId");

-- 8. Notifications Indexes
CREATE INDEX IF NOT EXISTS "IX_notifications_UserId" ON notifications ("UserId");
CREATE INDEX IF NOT EXISTS "IX_notifications_IsRead" ON notifications ("IsRead");

-- 9. Audit Logs Indexes
CREATE INDEX IF NOT EXISTS "IX_audit_logs_ActorId" ON audit_logs ("ActorId");
CREATE INDEX IF NOT EXISTS "IX_audit_logs_Action" ON audit_logs ("Action");
CREATE INDEX IF NOT EXISTS "IX_audit_logs_CreatedAt" ON audit_logs ("CreatedAt");

\echo 'Database performance and unique indexes created successfully!';
