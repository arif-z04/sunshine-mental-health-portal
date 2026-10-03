# Comprehensive Feature Catalog

This document details every major feature implemented in the Sunshine Mental Health Portal.

---

## 1. Specialist Discovery & Dynamic BST Slot Scheduling
* **Certified Directory**: Filter doctors by specialization (Clinical Psychology, Adult Psychiatry, Family & Child Counseling).
* **Dynamic Slot Engine**: Calculates available 45-minute consultation slots between 16:00 and 20:30 Asia/Dhaka time based on doctor working hours and existing confirmed appointments.
* **Advance Reservation Hold**: For doctors with `ADVANCE` payment policy, booking places the slot in `PENDING` status with an exact 15-minute countdown timer. If payment is not completed, background workers auto-cancel the slot.

## 2. Appointment Booking, Rescheduling & Concurrency Control
* **Double-Booking Strict Defense**:
  - Application layer: Serializable database transaction in `AppointmentService.cs`.
  - Database layer: Partial unique index on `appointments("DoctorId", "AppointmentDate", "StartTime") WHERE "Status" != 'CANCELLED'`.
* **Patient Self-Service Rescheduling**: Patients can reschedule pending or confirmed appointments to another available date/slot with automatic conflict checking.
* **Appointment Cancellation**: Patients or doctors can cancel sessions with automated status updating and clinical audit logging.

## 3. Mobile Financial Services (MFS) Simulation
* **Full BDT Checkout Flow**: Simulates bKash, Nagad, Rocket, and Cards.
* **Sandbox Validation**: Checks 11-digit Bangladesh phone numbers (`+8801[3-9]...`), generates realistic transaction IDs (`TRX-BKS-...`), and updates appointment payment status immediately to `SUCCESS` and appointment status to `CONFIRMED`.

## 4. Digital Resource Vault & Subscription Paywall
* **Categorized Resources**: Articles, CBT workbooks, sleep roadmaps, and mindfulness audio guides.
* **Tiered Subscription Plans**: Monthly (৳499), Quarterly (৳1,299), and Yearly (৳3,999) passes.
* **Granular Paywall Enforcement**: Free resources are publicly accessible; premium workbooks return HTTP 401/403 for non-subscribed users and render full download links for active subscribers.

## 5. Clinician Practice Management
* **Consultation Workspace**: Doctors can review upcoming appointments, inspect patient history, transition status (`CONFIRMED` -> `IN_PROGRESS` -> `COMPLETED`), and record private clinical notes.
* **Policy Management**: Toggle between `ADVANCE` and `POST_PAYMENT`.

## 6. Administrative Operations & Audit Console
* **Isolated Admin Entrypoint**: Dedicated login at `/admin/login` rejecting non-admin credentials with HTTP 403 Forbidden.
* **Live System Metrics**: Total revenue (৳ BDT), active appointments, active subscriptions, total clinicians, and registered patients.
* **Doctor Credential Verification**: Review BMDC certificates and approve or reject doctor practice requests.
* **Immutable Audit Trail**: Tracks all logins, profile updates, appointment status changes, and administrative actions with IP address and timestamps.
