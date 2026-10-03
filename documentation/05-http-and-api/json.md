# JSON Data Serialization

How C# objects become JSON strings and vice-versa.

---

## 1. Serialization (C# -> JSON)
When `PatientController` returns an object:
```csharp
return Ok(new DoctorDto(1, "Dr. Tanvir", 1500.00m));
```
ASP.NET Core serializes it into text:
`{"id":1,"fullName":"Dr. Tanvir","fee":1500.00}`

## 2. Deserialization (JSON -> C#)
When the browser sends JSON:
`{"doctorId":1,"appointmentDate":"2026-10-22"}`
ASP.NET Core automatically parses the string and instantiates a typed C# `BookAppointmentRequest` record.
