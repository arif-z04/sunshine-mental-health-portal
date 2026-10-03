# Entity Models (`Entities.cs`)

Entities represent database tables in C# memory.

---

In `server/Sunshine.App/Models/Entities.cs`:
```csharp
[Table("payments")]
public class Payment
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "BDT";
    public string PaymentMethod { get; set; } = "BKASH";
    public string TransactionId { get; set; } = string.Empty;
    public string Status { get; set; } = "SUCCESS";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
```
