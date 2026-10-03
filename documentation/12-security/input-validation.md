# Input Validation & Sanitization

Defending against malformed or malicious client payloads.

---

## 1. Server-Side Validation Rules
* **Email**: Validated with RFC-compliant regex.
* **Phone**: Validated against Bangladesh telecommunication prefix (`^\+?8801[3-9]\d{8}$`).
* **Monetary Values**: Validated with `Range(1, 100000)` and database check constraint `Amount > 0`.
* **String Lengths**: Bio and notes fields capped to prevent denial-of-service memory exhaustion.
