# Inheritance & Polymorphism

Sharing properties and behaviors between classes:

---

```csharp
// Base class
public class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

// Derived class inherits Id and CreatedAt
public class Notification : BaseEntity
{
    public int UserId { get; set; }
    public string Message { get; set; } = string.Empty;
}
```
