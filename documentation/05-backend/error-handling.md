# Error Handling & Exception Management

Reliable applications catch errors gracefully and present informative error messages.

---

## 1. Custom Exception Filter / Error Responses

When an error occurs, Sunshine returns a structured error object:

```json
{
  "success": false,
  "statusCode": 400,
  "message": "This time slot has already been booked. Please select a different time.",
  "timestamp": "2026-10-03T17:01:46.4339958Z"
}
```

* Never expose internal C# exception stack traces or raw database connection strings to the public client in production.
