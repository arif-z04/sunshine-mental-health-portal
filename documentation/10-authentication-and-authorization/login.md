# Login Flow & Credential Verification

In `AuthService.cs`:
1. Client submits email and password.
2. System retrieves user and password hash from PostgreSQL.
3. System verifies password with PBKDF2.
4. Generates signed JWT token and issues `sunshine_token` cookie.
