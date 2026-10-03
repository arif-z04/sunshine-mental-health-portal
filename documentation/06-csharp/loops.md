# Loops & Iteration in C#

Repeating actions over collections:

---

```csharp
List<string> hotlines = new List<string> { "999", "16263", "+8801779554391" };

foreach (var hotline in hotlines)
{
    Console.WriteLine($"Emergency Hotline: {hotline}");
}
```
