# Decision Making: `if` Statements & Switches

Executing code conditionally based on business rules:

---

```csharp
if (user.Role == "ADMIN")
{
    Console.WriteLine("Grant access to financial revenue metrics.");
}
else if (user.Role == "DOCTOR")
{
    Console.WriteLine("Grant access to clinical consultation ledger.");
}
else
{
    Console.WriteLine("Standard patient access.");
}
```
