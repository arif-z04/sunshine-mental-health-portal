# Complete API Endpoint Catalog

---

## 1. Authentication Endpoints (`AuthController.cs`)
* `POST /api/auth/register`: Register new patient account.
* `POST /api/auth/login`: Authenticate existing user.
* `POST /api/auth/logout`: Invalidate session and clear auth cookies.
* `GET /api/auth/me`: Get profile of currently authenticated user.

## 2. Patient Endpoints (`PatientController.cs`)
* `GET /api/patient/doctors`: List certified specialist doctors.
* `GET /api/patient/doctors/{id}`: Get single doctor details.
* `GET /api/patient/doctors/{id}/slots?date=YYYY-MM-DD`: Get calculated 45-min slots.
* `POST /api/patient/appointments`: Book an appointment slot.
* `GET /api/patient/appointments`: List appointments for current patient.
* `GET /api/patient/appointments/{id}`: Inspect specific appointment.
* `POST /api/patient/appointments/{id}/reschedule`: Move appointment to a new date/time.
* `POST /api/patient/appointments/{id}/cancel`: Cancel an appointment.
* `GET /api/patient/profile`: Fetch patient medical profile.
* `PUT /api/patient/profile`: Update medical profile and phone number.

## 3. Doctor Endpoints (`DoctorController.cs`)
* `GET /api/doctor/profile`: Retrieve doctor professional profile.
* `PUT /api/doctor/profile`: Update bio, fee, and payment policy.
* `GET /api/doctor/appointments`: List consultations for current doctor.
* `PUT /api/doctor/appointments/{id}/status`: Update appointment status and clinical notes.
* `GET /api/doctor/working-hours`: Retrieve recurring working hours.
* `PUT /api/doctor/working-hours`: Update weekly clinical schedule.

## 4. Admin Endpoints (`AdminController.cs`)
* `POST /api/admin/login`: Dedicated admin login (rejects non-admin HTTP 403).
* `GET /api/admin/metrics`: Fetch live BDT revenue, appointment and subscriber counts.
* `GET /api/admin/users`: List system accounts with role/status filters.
* `PUT /api/admin/users/{id}/toggle-active`: Suspend or activate a user account.
* `GET /api/admin/doctors/pending`: List unverified doctor applications.
* `POST /api/admin/doctors/{id}/verify`: Approve or reject BMDC credentials.
* `GET /api/admin/audit-logs`: Inspect immutable chronological security audit trail.

## 5. Payment & Subscription Endpoints
* `POST /api/payment/simulate`: Authorize simulated bKash/Nagad payment.
* `GET /api/subscription/plans`: List active subscription packages.
* `POST /api/subscription/subscribe`: Activate digital vault pass via bKash.
