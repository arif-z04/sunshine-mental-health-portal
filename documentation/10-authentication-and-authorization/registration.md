# Patient Registration Flow

New users register via `POST /api/auth/register`:
1. Validates email and Bangladesh phone format.
2. Hashes password using PBKDF2 with 100,000 iterations.
3. Inserts records into `users` and `patients` tables.
4. Issues authentication token.
