# Sunshine Mental Health Portal — API Reference

All API endpoints are hosted at `/api/` and return standardized JSON payloads. Protected routes require an `Authorization: Bearer <JWT>` header.

---

## 1. Authentication Endpoints (`/api/auth`)

### `POST /api/auth/login`
- **Description**: Authenticates users (Patients, Doctors, Admins) and issues a signed JWT token.
- **Request Body**:
  ```json
  {
    "email": "anika@example.com",
    "password": "Password123!"
  }
  ```
- **Response** (`200 OK`):
  ```json
  {
    "success": true,
    "message": "Login successful.",
    "token": "eyJhbGciOiJIUzI1Ni...",
    "user": {
      "id": 5,
      "email": "anika@example.com",
      "fullName": "Anika Tabassum",
      "phone": "+8801711223344",
      "role": "PATIENT",
      "isActive": true,
      "patientId": 1,
      "doctorId": null,
      "createdAt": "2026-09-03T16:31:18Z"
    }
  }
  ```

### `POST /api/auth/register/patient`
- **Description**: Self-service registration for new patients with optional Bangladesh medical history.
- **Request Body**:
  ```json
  {
    "email": "newpatient@example.com",
    "password": "Password123!",
    "fullName": "Nasrin Akter",
    "phone": "+8801711998877",
    "dateOfBirth": "1998-04-12",
    "gender": "Female",
    "emergencyContact": "Kamal Hossain (+8801819223344)"
  }
  ```

### `GET /api/auth/me`
- **Description**: Retrieves current authenticated profile.
- **Header**: `Authorization: Bearer <TOKEN>`

---

## 2. Patient Endpoints (`/api/patient`)

### `GET /api/patient/doctors`
- **Description**: Public directory search for verified clinicians in Bangladesh.
- **Query Params**: `specialization`, `paymentPolicy` (`ADVANCE` | `POST_PAYMENT`), `availableOnly` (`true` | `false`).

### `GET /api/patient/doctors/{id}/slots?date=YYYY-MM-DD`
- **Description**: Computes active 45-minute consultation slots based on the doctor's recurring weekly working hours, marking slots as `isBooked: true` if an active booking exists.

### `POST /api/patient/appointments/book`
- **Description**: Reserves a clinical slot. Enforces concurrency safety and sets a 15-minute countdown for `ADVANCE` payment policy doctors.
- **Request Body**:
  ```json
  {
    "doctorId": 1,
    "appointmentDate": "2026-10-06",
    "startTime": "16:00:00",
    "endTime": "16:45:00",
    "reason": "Exam stress and burnout"
  }
  ```

### `GET /api/patient/appointments`
- **Description**: Lists upcoming and past appointments for the authenticated patient.

### `POST /api/patient/appointments/{id}/cancel`
- **Description**: Cancels an appointment, freeing the slot for other patients.

### `POST /api/patient/payments/process`
- **Description**: Dispatches checkout via bKash, Nagad, Rocket, or Card.
- **Request Body**:
  ```json
  {
    "paymentType": "APPOINTMENT",
    "appointmentId": 2,
    "paymentMethod": "BKASH",
    "accountNumberOrCard": "01711223344",
    "accountHolderName": "Anika Tabassum",
    "otpOrPin": "12345"
  }
  ```

### `GET /api/patient/resources`
- **Description**: Lists free and premium self-help materials with paywall status (`hasAccess: true/false`).

### `POST /api/patient/resources/{id}/access`
- **Description**: Validates active subscription before granting access to premium content.

---

## 3. Doctor Endpoints (`/api/doctor`)

### `GET /api/doctor/profile` & `PUT /api/doctor/profile`
- **Description**: View and update counselor qualifications, bio, and consultation fee in BDT.

### `PATCH /api/doctor/policy`
- **Description**: 1-click toggle between `ADVANCE` and `POST_PAYMENT` policies.

### `GET /api/doctor/schedules` & `POST /api/doctor/schedules`
- **Description**: View and configure recurring weekly working hours (e.g. Saturday-Thursday 16:00-21:00 BST).

### `GET /api/doctor/appointments`
- **Description**: Clinical desk for reviewing scheduled patients.

### `PATCH /api/doctor/appointments/{id}/status`
- **Description**: Update session status (`CONFIRMED`, `COMPLETED`, `CANCELLED`, `NO_SHOW`) and record clinical notes.

---

## 4. Admin Endpoints (`/api/admin`)

### `POST /api/admin/login`
- **Description**: Dedicated isolated admin login strictly rejecting non-admin credentials with HTTP 403.

### `GET /api/admin/metrics`
- **Description**: High-level platform KPIs: total revenue in BDT, active appointments, active subscriptions, doctors, and patients.

### `GET /api/admin/users` & `PATCH /api/admin/users/{id}/status`
- **Description**: List user accounts with search/filter, and toggle `IsActive` state with audit logging.

### `PATCH /api/admin/doctors/{id}/verify`
- **Description**: Approve or suspend counselor BMDC registration status.

### `GET /api/admin/plans`, `POST /api/admin/plans`, `PUT /api/admin/plans/{id}`
- **Description**: CRUD operations on BDT membership tier passes.

### `GET /api/admin/audit-logs`
- **Description**: Retrieve platform operational audit trail.
