# User Login & Credential Verification

Authenticating existing users into the platform.

---

## 1. Login Request (`POST /api/auth/login`)

```json
{
  "email": "anika@example.com",
  "password": "Password123!"
}
```

---

## 2. Verification Process in `AuthService.cs`

1. Look up user by email in PostgreSQL:
   ```csharp
   var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
   ```
2. Check if account is active (`user.IsActive == true`). If deactivated, return HTTP 403.
3. Extract stored salt and hash from `user.PasswordHash`.
4. Hash the incoming password using the identical salt and 100,000 iterations.
5. Perform a constant-time comparison (`CryptographicOperations.FixedTimeEquals`) to prevent timing side-channel attacks.
6. Generate JWT containing claims: `sub` (UserId), `email`, `role`, and `name`.
