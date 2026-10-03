# Model Validation & Data Annotations

Defending endpoints against bad data:

---

```csharp
public record RegisterRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    [Required] string FullName,
    [RegularExpression(@"^\+?8801[3-9]\d{8}$", ErrorMessage = "Invalid Bangladesh phone number.")]
    string? PhoneNumber
);
```
If a client sends an empty password or an invalid phone number, ASP.NET Core returns `HTTP 400 Bad Request` automatically before your service code even runs!
