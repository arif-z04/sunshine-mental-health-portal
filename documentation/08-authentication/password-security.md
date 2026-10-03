# Password Security & Cryptographic Standards

Technical breakdown of password storage and validation.

---

## 1. Why Plaintext or MD5/SHA1 is Strictly Prohibited

* Plaintext passwords leak immediately if database backups are compromised.
* Fast hash functions (MD5, SHA-1, SHA-256) are vulnerable to rainbow table lookups and brute-force GPU cracking (billions of hashes per second).

---

## 2. Implementation in `AuthService.cs`

```csharp
public string HashPassword(string password)
{
    byte[] salt = RandomNumberGenerator.GetBytes(16); // 128-bit secure salt
    byte[] hash = KeyDerivation.Pbkdf2(
        password: password,
        salt: salt,
        prf: KeyDerivationPrf.HMACSHA256,
        iterationCount: 100000,
        numBytesRequested: 32 // 256-bit derived key
    );
    return $"{Convert.ToHexString(salt)}:{Convert.ToHexString(hash)}";
}
```
