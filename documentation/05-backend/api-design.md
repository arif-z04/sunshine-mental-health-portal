# REST API Design Conventions

Sunshine follows RESTful API design standards.

---

## 1. URI Naming Rules
* Use plural nouns for resources: `/api/patient/doctors`, `/api/resource/items`.
* Use HTTP verbs to specify intent:
  - `GET`: Retrieve data without side effects.
  - `POST`: Create a new resource or trigger an action (e.g. `/api/auth/login`).
  - `PUT`: Update an existing resource (e.g. `PUT /api/patient/profile`).
  - `DELETE`: Remove a resource.

---

## 2. Standard JSON Response Format

All responses use camelCase JSON properties:
```json
{
  "id": 1,
  "appointmentDate": "2026-10-22",
  "startTime": "16:00:00",
  "status": "CONFIRMED",
  "feeBdt": 1500.00
}
```
