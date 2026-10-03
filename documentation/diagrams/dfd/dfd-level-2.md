# Data Flow Diagram (DFD) Level 2 — Appointment Booking Engine

## Purpose
Detailed decomposition of Process 3.0 (Appointment Management).

## Diagram

```mermaid
flowchart TD
    Patient(("Patient"))
    
    subgraph BookingProcess ["3.0 Appointment Booking Process Decomposition"]
        P3_1["3.1 Validate Slot Availability & Working Hours"]
        P3_2["3.2 Serializable Concurrency Check"]
        P3_3["3.3 Evaluate Clinician Payment Policy"]
        P3_4["3.4 Create Pending Appointment & 15-Min Timer"]
        P3_5["3.5 Confirm Appointment on MFS Authorization"]
        P3_6["3.6 Auto-Cancel Expired Reservations"]
    end
    
    D2_Appointments[("D2: appointments")]
    D3_WorkingHours[("D3: doctor_working_hours")]
    D4_Payments[("D4: payments")]
    
    Patient -->|Slot Request (DoctorId, Date, Time)| P3_1
    P3_1 <--> D3_WorkingHours
    P3_1 -->|Valid Slot Request| P3_2
    
    P3_2 <-->|Serializable Check| D2_Appointments
    P3_2 -->|Slot Confirmed Free| P3_3
    
    P3_3 -->|Policy: ADVANCE| P3_4
    P3_4 -->|Store PENDING| D2_Appointments
    
    P3_4 -->|Timeout Triggered (15m)| P3_6
    P3_6 -->|UPDATE Status='CANCELLED'| D2_Appointments
    
    D4_Payments -->|Payment Received Event| P3_5
    P3_5 -->|UPDATE Status='CONFIRMED'| D2_Appointments
    P3_5 -->|Success Notification| Patient
```
