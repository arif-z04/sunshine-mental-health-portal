# Frontend Pages & Portal Structure

Sunshine is divided into four dedicated interfaces:

---

1. **Public Website (`/`)**:
   - Hero section with calming imagery.
   - Bangladesh crisis hotlines banner.
   - "How It Works" 3-step guide (Select Specialist -> Choose Slot -> Confirm & Consult).
   - Specialist highlights & Patient testimonials.
   - Interactive FAQ accordion.

2. **Patient Care Portal (`/patient`)**:
   - Tab 1: **Specialists Directory** (filter by specialization, view profiles).
   - Tab 2: **My Appointments** (upcoming & historical sessions, Reschedule & Cancel actions).
   - Tab 3: **Resource Vault** (psychoeducation library, CBT workbooks, subscription passes).
   - Tab 4: **My Profile** (personal medical details, phone number, emergency contacts).

3. **Doctor Practice Portal (`/doctor`)**:
   - Verified BMDC status badge.
   - Consultation Ledger (manage appointments, update progress to `COMPLETED`).
   - Private clinical consultation notes capture.
   - Payment policy toggle (`ADVANCE` vs. `POST_PAYMENT`).

4. **Admin Operations Console (`/admin` and `/admin/login`)**:
   - Isolated admin login entrypoint rejecting non-administrators.
   - Live metrics: Total Revenue (৳ BDT), Active Appointments, Subscriptions, Doctors, Patients.
   - Doctor verification approval queue.
   - Immutable security audit log viewer.
