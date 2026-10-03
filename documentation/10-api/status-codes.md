# HTTP Status Codes Dictionary

Sunshine adheres strictly to standard HTTP status codes:

---

| Status Code | Meaning | When Used in Sunshine |
| :--- | :--- | :--- |
| **`200 OK`** | Success | Request succeeded and returned requested data. |
| **`201 Created`** | Created | Resource successfully created (e.g. registration). |
| **`400 Bad Request`** | Validation Error | Missing fields, invalid phone regex, slot already booked. |
| **`401 Unauthorized`** | Authentication Required | Missing or expired token; unsubscribed user accessing paywall. |
| **`403 Forbidden`** | Access Denied | Authenticated user lacks required role (e.g. non-admin accessing admin routes). |
| **`404 Not Found`** | Not Found | Doctor, appointment, or resource does not exist. |
| **`409 Conflict`** | State Conflict | Database unique constraint violation (concurrent booking collision). |
| **`500 Internal Error`** | Server Error | Unhandled server exception (logged for diagnostics). |
