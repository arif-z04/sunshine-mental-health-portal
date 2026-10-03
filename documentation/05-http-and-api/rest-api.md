# What Makes an API "RESTful"?

**REST** stands for **Representational State Transfer**.

---

## Core Rules of REST:
1. **Resource-Oriented URLs**: URLs represent nouns, not verbs:
   - ✅ `/api/patient/appointments` (RESTful)
   - ❌ `/api/patient/bookAppointmentNow` (Non-RESTful RPC)
2. **Standard HTTP Verbs**:
   - `GET /api/patient/appointments` -> Read appointments.
   - `POST /api/patient/appointments` -> Create an appointment.
3. **Stateless**: The server does not store client session state in RAM between requests. Every request carries all tokens and information necessary to fulfill it.
