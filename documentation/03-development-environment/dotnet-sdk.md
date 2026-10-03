# .NET 10 SDK Setup & Verification

## 1. What is the .NET SDK?
The **.NET Software Development Kit (SDK)** contains the C# compiler (`csc`), the runtime execution engine, the dependency package manager (NuGet), and the command-line interface (`dotnet`).

## 2. Verification Commands
```bash
dotnet --version
# Output: 10.0.xxx

dotnet --info
```
`dotnet --info` displays:
* .NET SDK version and processor architecture (`x64`).
* Installed runtimes: `Microsoft.AspNetCore.App` and `Microsoft.NETCore.App`.
