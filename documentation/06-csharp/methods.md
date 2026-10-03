# C# Methods: Reusable Logic

A **method** is a block of code with a name that performs a specific task and optionally returns a result.

---

```csharp
public decimal CalculateTotalWithVat(decimal consultationFee)
{
    decimal vatRate = 0.05m; // 5% VAT
    return consultationFee + (consultationFee * vatRate);
}
```
* `public`: Accessible from other classes.
* `decimal`: The return type.
* `CalculateTotalWithVat`: The method name.
* `(decimal consultationFee)`: Parameter accepted by the method.
