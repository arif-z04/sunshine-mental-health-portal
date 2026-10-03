# Request & Response Payload Examples

Concrete examples of JSON payloads used across Sunshine.

---

## 1. Booking an Appointment

**Request**: `POST /api/patient/appointments`
```json
{
  "doctorId": 1,
  "appointmentDate": "2026-10-22",
  "startTime": "16:00:00"
}
```

**Response**: `HTTP 200 OK`
```json
{
  "id": 8,
  "doctorId": 1,
  "doctorName": "Dr. Tanvir Ahmed, MBBS, MD",
  "patientId": 1,
  "patientName": "Farzana Haque",
  "appointmentDate": "2026-10-22",
  "startTime": "16:00:00",
  "endTime": "16:45:00",
  "status": "PENDING",
  "consultationFee": 1500.00,
  "paymentPolicy": "ADVANCE"
}
```

---

## 2. Simulating MFS Payment (bKash)

**Request**: `POST /api/payment/simulate`
```json
{
  "paymentType": "APPOINTMENT",
  "appointmentId": 8,
  "paymentMethod": "BKASH",
  "phoneNumber": "01700000000",
  "pin": "12345"
}
```

**Response**: `HTTP 200 OK`
```json
{
  "success": true,
  "message": "Payment of ৳1,500 BDT authorized successfully via BKASH.",
  "transactionId": "TRX-BKS-6D29EE42",
  "payment": {
    "id": 16,
    "amount": 1500.00,
    "currency": "BDT",
    "status": "SUCCESS"
  }
}
```
