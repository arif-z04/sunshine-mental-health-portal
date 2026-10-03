# Request Validation & Defensive Programming

Defensive programming guarantees that malformed input is rejected before touching the database.

---

## 1. Data Annotations on DTOs

In `server/Sunshine.App/DTOs/DTOs.cs`:

```csharp
public record RegisterRequest(
    [Required, EmailAddress] string Email,
    [Required, MinLength(8)] string Password,
    [Required, MaxLength(100)] string FullName,
    [RegularExpression(@"^\+?8801[3-9]\d{8}$", ErrorMessage = "Invalid Bangladesh phone number.")]
    string? PhoneNumber,
    string Role
);
```

* `[Required]`: Disallows null or empty strings.
* `[EmailAddress]`: Enforces valid RFC email structure.
* `[RegularExpression]`: Validates Bangladesh mobile operators (Grameenphone, Robi, Banglalink, Teletalk).
