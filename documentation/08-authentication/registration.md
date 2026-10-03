# User Registration Flow

How new patients register accounts in Sunshine.

---

## 1. Registration Sequence

```text
User fills registration form
         │
         ▼
POST /api/auth/register
         │
         ▼
Validate Email & BD Phone format
         │
         ▼
Check if Email already exists
         │
         ▼
Generate 128-bit salt & PBKDF2 Hash
         │
         ▼
Insert into `users` table
         │
         ▼
If Role is 'PATIENT', insert into `patients` table
         │
         ▼
Generate JWT token & Set sunshine_token Cookie
         │
         ▼
Return HTTP 200 with UserDto and Token
```
