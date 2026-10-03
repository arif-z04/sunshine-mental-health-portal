# Database Quick Reference Sheet

Quick cheat sheet of tables and key columns:

---

* **`users`**: `Id`, `Email`, `PasswordHash`, `FullName`, `PhoneNumber`, `Role`, `IsActive`, `CreatedAt`.
* **`doctors`**: `Id`, `UserId`, `BmdcNumber`, `Qualifications`, `Bio`, `ConsultationFee`, `PaymentPolicy`, `IsVerified`.
* **`patients`**: `Id`, `UserId`, `DateOfBirth`, `BloodGroup`, `EmergencyContactName`, `EmergencyContactPhone`.
* **`appointments`**: `Id`, `DoctorId`, `PatientId`, `AppointmentDate`, `StartTime`, `EndTime`, `Status`, `Notes`.
* **`payments`**: `Id`, `UserId`, `Amount`, `Currency`, `PaymentType`, `AppointmentId`, `SubscriptionId`, `PaymentMethod`, `TransactionId`, `Status`.
* **`subscriptions`**: `Id`, `UserId`, `PlanId`, `StartDate`, `EndDate`, `Status`.
* **`resources`**: `Id`, `CategoryId`, `Title`, `Author`, `Description`, `ResourceType`, `ContentUrl`, `IsPremium`.
* **`audit_logs`**: `Id`, `ActorId`, `ActorEmail`, `Action`, `TargetType`, `TargetId`, `IpAddress`, `Details`, `CreatedAt`.
