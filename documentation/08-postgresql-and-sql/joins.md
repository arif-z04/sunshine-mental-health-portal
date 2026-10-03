# SQL JOINs: Connecting the Dots

A **`JOIN`** query combines rows from two or more tables based on a related column between them:

```sql
SELECT a."Id", d."Qualifications", u."FullName" AS "DoctorName", p_u."FullName" AS "PatientName"
FROM appointments a
JOIN doctors d ON a."DoctorId" = d."Id"
JOIN users u ON d."UserId" = u."Id"
JOIN patients p ON a."PatientId" = p."Id"
JOIN users p_u ON p."UserId" = p_u."Id";
```
