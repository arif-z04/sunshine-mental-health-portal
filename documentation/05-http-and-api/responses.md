# Anatomy of an HTTP Response

The server processes the request and replies with an HTTP Response:

```http
HTTP/1.1 200 OK
Content-Type: application/json; charset=utf-8
Date: Sat, 03 Oct 2026 17:00:00 GMT

{
  "id": 8,
  "status": "PENDING",
  "consultationFee": 1500.00,
  "paymentPolicy": "ADVANCE"
}
```
1. **Status Line**: `HTTP/1.1 200 OK` (Indicates success).
2. **Headers**: Tells browser the format is UTF-8 JSON.
3. **Body**: The created appointment object.
