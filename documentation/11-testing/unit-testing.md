# Unit Testing with xUnit

Unit tests isolate specific business rules and methods without requiring network calls or a running database.

---

## 1. Test Project: `server/Sunshine.Tests`

Built using `xUnit` and `Microsoft.EntityFrameworkCore.InMemory`.

### Execution Command:
```bash
dotnet test
```

### Output:
```text
Passed!  - Failed: 0, Passed: 26, Skipped: 0, Total: 26, Duration: 1 s - Sunshine.Tests.dll (net10.0)
```
