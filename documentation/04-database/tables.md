# Detailed Table Specifications

Below is the column-level reference for the key tables defined in `sql/03_schema.sql`.

---

## 1. Table: `users`
* `Id` (SERIAL PRIMARY KEY): Unique account ID.
* `Email` (VARCHAR(150) NOT NULL UNIQUE): User login email.
* `PasswordHash` (VARCHAR(255) NOT NULL): Salted PBKDF2 hash.
* `FullName` (VARCHAR(100) NOT NULL): Display name.
* `PhoneNumber` (VARCHAR(20)): Contact phone (+8801...).
* `Role` (VARCHAR(20) NOT NULL): Role constraint (`PATIENT`, `DOCTOR`, `ADMIN`).
* `IsActive` (BOOLEAN DEFAULT TRUE): Account active flag.
* `CreatedAt` (TIMESTAMPTZ DEFAULT NOW()): Registration timestamp.

---

## 2. Table: `doctors`
* `Id` (SERIAL PRIMARY KEY): Unique doctor ID.
* `UserId` (INT NOT NULL UNIQUE REFERENCES users(Id) ON DELETE CASCADE): Linked user account.
* `BmdcNumber` (VARCHAR(50) NOT NULL UNIQUE): Bangladesh Medical & Dental Council license.
* `Qualifications` (VARCHAR(255) NOT NULL): Degree listing (MBBS, MD, FCPS).
* `Bio` (TEXT): Clinical experience summary.
* `ConsultationFee` (NUMERIC(10,2) NOT NULL): Fee in BDT (৳).
* `PaymentPolicy` (VARCHAR(20) DEFAULT 'ADVANCE'): Policy constraint (`ADVANCE`, `POST_PAYMENT`).
* `IsVerified` (BOOLEAN DEFAULT FALSE): BMDC approval status managed by Admin.

---

## 3. Table: `appointments`
* `Id` (SERIAL PRIMARY KEY): Unique appointment ID.
* `DoctorId` (INT NOT NULL REFERENCES doctors(Id)): Clinician.
* `PatientId` (INT NOT NULL REFERENCES patients(Id)): Patient.
* `AppointmentDate` (DATE NOT NULL): Date of session (BST).
* `StartTime` (TIME NOT NULL): Slot start time (e.g. 16:00:00).
* `EndTime` (TIME NOT NULL): Slot end time (e.g. 16:45:00).
* `Status` (VARCHAR(20) NOT NULL DEFAULT 'PENDING'): Status constraint (`PENDING`, `CONFIRMED`, `IN_PROGRESS`, `COMPLETED`, `CANCELLED`, `REJECTED`).
* `Notes` (TEXT): Clinical summary recorded by doctor.
* `CreatedAt` (TIMESTAMPTZ DEFAULT NOW()): Booking timestamp.

---

## 4. Table: `payments`
* `Id` (SERIAL PRIMARY KEY): Unique payment ID.
* `UserId` (INT NOT NULL REFERENCES users(Id)): Paying user.
* `Amount` (NUMERIC(10,2) NOT NULL): BDT amount.
* `Currency` (VARCHAR(3) DEFAULT 'BDT'): Currency code (BDT).
* `PaymentType` (VARCHAR(20) NOT NULL): (`APPOINTMENT`, `SUBSCRIPTION`).
* `AppointmentId` (INT REFERENCES appointments(Id)): Optional linked appointment.
* `SubscriptionId` (INT REFERENCES subscriptions(Id)): Optional linked subscription.
* `PaymentMethod` (VARCHAR(20) NOT NULL): (`BKASH`, `NAGAD`, `ROCKET`, `CARD`).
* `TransactionId` (VARCHAR(100) NOT NULL UNIQUE): MFS transaction code (e.g. `TRX-BKS-6D29EE42`).
* `Status` (VARCHAR(20) NOT NULL DEFAULT 'SUCCESS'): (`PENDING`, `SUCCESS`, `FAILED`).
