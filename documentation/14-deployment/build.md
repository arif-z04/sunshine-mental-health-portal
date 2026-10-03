# Building & Publishing the Application

Compiling optimized release binaries.

---

## 1. Publish Command
```bash
dotnet publish server/Sunshine.App/Sunshine.App.csproj   -c Release   -o /var/www/sunshine   --runtime linux-x64   --self-contained false
```
* `-c Release`: Enables compiler optimizations.
* `-o /var/www/sunshine`: Publishes compiled DLLs and `wwwroot` client files to the destination directory.
* `--self-contained false`: Uses the system-installed .NET 10 runtime.
