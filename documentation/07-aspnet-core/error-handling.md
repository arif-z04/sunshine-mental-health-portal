# Structured Error Responses

Consistent error handling ensures frontends can render helpful user feedback:

---

```json
{
  "success": false,
  "statusCode": 400,
  "message": "This time slot has already been booked. Please select a different time.",
  "timestamp": "2026-10-03T17:01:46Z"
}
```
* The client inspects `message` and displays a user-friendly toast notification.
