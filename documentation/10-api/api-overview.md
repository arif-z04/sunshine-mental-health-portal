# REST API Architecture Overview

The Sunshine platform provides a comprehensive suite of RESTful API endpoints.

---

## 1. General API Characteristics
* **Base URL**: `http://localhost:5000/api`
* **Transport**: HTTP/1.1 and HTTP/2 over TLS/HTTPS (or HTTP in local development).
* **Payload Encoding**: JSON (`Content-Type: application/json`).
* **Authentication**: Bearer JWT header or `sunshine_token` HTTP-only cookie.
* **Date Formats**: ISO 8601 (`YYYY-MM-DD` for dates, `HH:mm:ss` for times, `YYYY-MM-DDTHH:mm:ssZ` for timestamps).

---

## 2. API Functional Groupings
1. **`/api/auth`**: Account registration, credential login, logout, identity inspection.
2. **`/api/patient`**: Doctor discovery, slot availability, booking, rescheduling, cancellations, patient profiles.
3. **`/api/doctor`**: Doctor profiles, appointments list, session completion, clinical notes, working hours.
4. **`/api/admin`**: Dedicated admin login, financial metrics, doctor verification, user management, audit logs.
5. **`/api/payment`**: MFS payment simulation (bKash/Nagad/Rocket).
6. **`/api/subscription`**: Plan catalog, subscription creation, active status inspection.
7. **`/api/resource`**: Educational resource categories and items.
