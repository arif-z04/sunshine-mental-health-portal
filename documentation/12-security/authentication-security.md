# Authentication Security & Credential Protection

Protecting user credentials and sessions.

---

## 1. Preventing Credential Stuffing & Brute Force
* Passwords require minimum 8 characters and mixed-character strength.
* Constant-time comparison (`FixedTimeEquals`) during login prevents timing attacks.
* Inactive accounts (`IsActive = false`) are rejected immediately, allowing administrators to lock compromised accounts.

---

## 2. JWT Signature Verification
* Signed using HMAC-SHA256 with a 256-bit secret key stored in configuration.
* Tokens enforce expiration (`exp` claim) after 7 days.
