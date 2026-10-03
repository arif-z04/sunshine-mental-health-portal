# Authentication Architecture Overview

Sunshine implements a dual-mode, secure authentication architecture.

---

## 1. Dual Authentication: Cookies + Bearer JWT

1. **HTTP-only Cookies (`sunshine_token`)**:
   - Automatically sent by web browsers on every page request.
   - Protected with `HttpOnly` and `SameSite=Lax` flags, preventing malicious cross-site scripts (XSS) from reading the authentication token.
2. **Bearer JWT (`Authorization: Bearer <token>`)**:
   - Returned in JSON login responses for programmatic API consumption (mobile clients, curl integration scripts, automated test harnesses).

---

## 2. Cryptographic Password Hashing (PBKDF2)

Passwords are **never stored in plaintext**. Sunshine hashes passwords using the industry-standard **PBKDF2 (Password-Based Key Derivation Function 2)** algorithm with `HMAC-SHA256`:
* **Salt**: A unique 128-bit cryptographically secure random salt generated via `RandomNumberGenerator.GetBytes(16)`.
* **Iterations**: 100,000 iterations to withstand brute-force GPU attacks.
* **Storage Format**: `salt_hex:hash_hex`.
