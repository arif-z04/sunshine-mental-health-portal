# Sunshine Mental Health Portal — Role-Based Access Control (RBAC)

## 1. Access Control Matrix

The platform strictly differentiates capabilities between Patients, Counselors, Administrators, and Public Visitors:

| Feature / Operation | Public Visitor | Patient | Doctor / Clinician | Admin |
| :--- | :---: | :---: | :---: | :---: |
| Browse Counselors & Fees (BDT) | ✓ | ✓ | ✓ | ✓ |
| View Public CBT Articles | ✓ | ✓ | ✓ | ✓ |
| Book Telehealth Appointment | ✗ | ✓ | ✗ | ✗ |
| View & Cancel Own Appointments | ✗ | ✓ | ✓ | ✓ |
| Pay Session Fee (bKash/Nagad) | ✗ | ✓ | ✗ | ✗ |
| Purchase CBT Membership Pass | ✗ | ✓ | ✗ | ✗ |
| Read Premium Paywalled Books | ✗ | ✓ (Active Sub) | ✓ | ✓ |
| Manage Clinical Weekly Hours | ✗ | ✗ | ✓ | ✓ |
| Edit Consultation Policy | ✗ | ✗ | ✓ | ✗ |
| Update Clinical Notes | ✗ | ✗ | ✓ | ✗ |
| BMDC Doctor Verification | ✗ | ✗ | ✗ | ✓ |
| Activate/Suspend User Accounts | ✗ | ✗ | ✗ | ✓ |
| Modify Subscription Plans | ✗ | ✗ | ✗ | ✓ |
| Issue bKash/Nagad Refunds | ✗ | ✗ | ✗ | ✓ |
| Inspect Operational Audit Logs | ✗ | ✗ | ✗ | ✓ |

---

## 2. Policy Enforcement & Route Protection

Authorization is enforced at both controller and endpoint levels:
1. `[Authorize(Roles = AppRoles.Patient)]` — Reserved for registered patients.
2. `[Authorize(Roles = AppRoles.Doctor)]` — Reserved for clinicians; cross-doctor modification is blocked by checking `Doctor.UserId == currentUserId`.
3. `[Authorize(Roles = AppRoles.Admin)]` — Enforces administrator privileges.
4. Dedicated `/api/admin/login` Route: Explicitly checks `user.Role == AppRoles.Admin` and returns `403 Forbidden` if a non-admin attempts login.
