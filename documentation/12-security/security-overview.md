# Security Architecture Overview

Healthcare tele-counseling demands the highest standards of data security and confidentiality.

---

## 1. Key Security Measures in Sunshine
1. **PBKDF2 Password Hashing**: 100,000 iterations, unique 128-bit salt per user.
2. **Dual-Token Authentication**: HTTP-only SameSite cookies and cryptographically signed JWTs.
3. **Role-Based Access Control**: Strict segregation between Patients, Doctors, and Administrators.
4. **IDOR Mitigation**: Server-side verification that authenticated callers own the requested clinical records.
5. **Database-Level Protection**: Parameterized queries via EF Core preventing SQL Injection; partial unique indexes preventing concurrency collisions.
6. **Immutable Security Audit Trail**: Capturing administrative, clinical, and financial operations.
