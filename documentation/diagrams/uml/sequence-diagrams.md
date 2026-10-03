# Sequence Diagrams: Appointment Booking & Payment

## Purpose
Step-by-step sequence diagrams showing appointment booking and bKash sandbox payment.

## 1. Appointment Booking Sequence

```mermaid
sequenceDiagram
    autonumber
    actor Patient
    participant Browser as Patient Portal (JS)
    participant Controller as PatientController
    participant Service as AppointmentService
    participant DB as PostgreSQL (sunshine_db)

    Patient->>Browser: Selects Doctor #1 and Slot 16:00:00
    Browser->>Controller: POST /api/patient/appointments
    Controller->>Service: BookAppointmentAsync(patientId, request)
    Service->>DB: BEGIN TRANSACTION ISOLATION LEVEL SERIALIZABLE
    Service->>DB: Check for existing active slot conflict
    alt Slot is already booked
        DB-->>Service: Conflict detected
        Service-->>Controller: Throw InvalidOperationException
        Controller-->>Browser: HTTP 400 Bad Request
        Browser-->>Patient: Display "Slot already booked" toast
    else Slot is free
        Service->>DB: INSERT INTO appointments (Status='PENDING')
        Service->>DB: COMMIT
        DB-->>Service: Appointment #8 Created
        Service-->>Controller: Return AppointmentDto
        Controller-->>Browser: HTTP 200 OK
        Browser-->>Patient: Open bKash MFS Checkout Modal (15-min countdown)
    end
```

## 2. bKash MFS Payment Sequence

```mermaid
sequenceDiagram
    autonumber
    actor Patient
    participant Modal as bKash Modal UI
    participant PayCtrl as PaymentController
    participant PayService as PaymentService
    participant DB as PostgreSQL (sunshine_db)

    Patient->>Modal: Inputs 01700000000 & PIN 12345
    Modal->>PayCtrl: POST /api/payment/simulate
    PayCtrl->>PayService: ProcessPaymentAsync(request)
    PayService->>DB: INSERT INTO payments (Amount=1500, Status='SUCCESS')
    PayService->>DB: UPDATE appointments SET Status='CONFIRMED' WHERE Id=8
    PayService->>DB: INSERT INTO audit_logs (Action='PAYMENT_SUCCESS')
    DB-->>PayService: Success
    PayService-->>PayCtrl: Return PaymentResultDto (TRX-BKS-6D29EE42)
    PayCtrl-->>Modal: HTTP 200 OK
    Modal-->>Patient: Display "Appointment Confirmed" Green Badge
```
