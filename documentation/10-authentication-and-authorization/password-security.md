# Cryptographic Password Hashing (PBKDF2)

Sunshine never stores passwords in plain text. It uses **PBKDF2 with HMAC-SHA256**:
* 128-bit unique random salt per user.
* 100,000 hashing rounds to defeat brute-force GPU cracking.
