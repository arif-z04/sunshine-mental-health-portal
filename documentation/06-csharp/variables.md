# C# Variables & Data Types

In C#, every variable must have a declared data type:

---

```csharp
// Integer: Whole numbers
int doctorId = 1;

// Decimal: High-precision numbers (Always use for BDT money!)
decimal consultationFeeBdt = 1500.00m;

// String: Text inside double quotes
string doctorName = "Dr. Tanvir Ahmed, MBBS, MD";

// Boolean: true or false
bool isBmdcVerified = true;

// DateOnly & TimeOnly (.NET 6+)
DateOnly appointmentDate = new DateOnly(2026, 10, 22);
TimeOnly startTime = new TimeOnly(16, 0, 0); // 16:00 BST
```
*Notice: Monetary amounts in C# always use `decimal` with an `m` suffix (`1500.00m`), never `float` or `double`, to prevent rounding errors in financial transactions.*
