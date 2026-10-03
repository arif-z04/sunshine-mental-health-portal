# Structured Error Responses

When an operation fails, the API returns a structured, predictable error body.

---

## 1. Standard Error Schema

```json
{
  "success": false,
  "statusCode": 400,
  "message": "This time slot has already been booked. Please select a different time.",
  "timestamp": "2026-10-03T17:01:46.4339958Z"
}
```

---

## 2. Validation Failure Schema (ASP.NET Core Default)

If required parameters fail model binding:
```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "PhoneNumber": [
      "Invalid Bangladesh phone number."
    ]
  }
}
```
