# REST API Quick Reference

Fast endpoint reference:

---

| Verb | Path | Auth | Roles | Description |
| :--- | :--- | :---: | :---: | :--- |
| `POST` | `/api/auth/register` | None | Public | Register patient account |
| `POST` | `/api/auth/login` | None | Public | User authentication |
| `POST` | `/api/auth/logout` | Cookie/JWT | Any | Invalidate session |
| `GET` | `/api/patient/doctors` | None | Public | List certified specialists |
| `GET` | `/api/patient/doctors/{id}/slots` | None | Public | Get 45-min BST slots |
| `POST` | `/api/patient/appointments` | JWT | `PATIENT` | Book consultation |
| `POST` | `/api/patient/appointments/{id}/reschedule` | JWT | `PATIENT` | Reschedule consultation |
| `PUT` | `/api/doctor/appointments/{id}/status` | JWT | `DOCTOR` | Record clinical notes |
| `POST` | `/api/admin/login` | None | `ADMIN` only | Isolated admin gateway |
| `GET` | `/api/admin/metrics` | JWT | `ADMIN` | Total BDT revenue & counts |
| `GET` | `/api/admin/audit-logs` | JWT | `ADMIN` | Inspect audit trail |
