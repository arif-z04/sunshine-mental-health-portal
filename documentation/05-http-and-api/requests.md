# Anatomy of an HTTP Request

When a user clicks "Book Appointment", the browser crafts an HTTP Request:

```http
POST /api/patient/appointments HTTP/1.1
Host: localhost:5000
Content-Type: application/json
Authorization: Bearer eyJhbGciOiJIUz...

{
  "doctorId": 1,
  "appointmentDate": "2026-10-22",
  "startTime": "16:00:00"
}
```
1. **Request Line**: `POST /api/patient/appointments HTTP/1.1`
2. **Headers**: `Host`, `Content-Type`, `Authorization`.
3. **Empty Line**: Separates headers from payload.
4. **Body**: The JSON payload with doctor ID and selected time.
