# Complete Database Schema (16 Tables)

The Sunshine database comprises 16 relational tables in the `public` schema.

---

## 1. Entity Groupings

1. **Identity & Core Profiles**:
   - `users`: Core account identity (email, password hash, role).
   - `patients`: Patient medical metadata, emergency contact, date of birth, blood group.
   - `doctors`: BMDC registration number, qualifications, bio, consultation fee, payment policy.
2. **Clinical Specialization & Scheduling**:
   - `specializations`: Medical disciplines (e.g. Child Psychology, Addiction Psychiatry).
   - `doctor_specializations`: Many-to-many relationship linking doctors to specializations.
   - `doctor_working_hours`: Weekly recurring availability (DayOfWeek, StartTime, EndTime, SlotDurationMinutes).
3. **Appointments & Clinical Ledger**:
   - `appointments`: Scheduled sessions (DoctorId, PatientId, Date, StartTime, EndTime, Status, Notes).
   - `session_reviews`: Post-session rating (1-5 stars) and feedback comments.
4. **Financial & Subscription Management**:
   - `payments`: Transaction records (bKash, Nagad, Rocket), amount in BDT, transaction IDs.
   - `subscription_plans`: Subscription tiers (Monthly, Quarterly, Annual) with pricing in BDT.
   - `subscriptions`: Active patient subscriptions tracking StartDate and EndDate.
5. **Therapeutic Digital Vault**:
   - `resource_categories`: Subject groupings (Anxiety, Depression, Sleep, Stress).
   - `resources`: Articles, CBT workbooks, audio guides with `is_premium` flag.
6. **Security & Crisis Management**:
   - `audit_logs`: Immutable security audit trail recording user and administrative actions.
   - `notifications`: In-app system alerts (appointment confirmations, payment notices).
   - `emergency_contacts`: National crisis hotlines (Kaan Pete Roi, 999, 16263, NIMH).
