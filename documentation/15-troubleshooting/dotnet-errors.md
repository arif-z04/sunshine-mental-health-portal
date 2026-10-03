# .NET & C# Compilation Errors

---

## 1. SDK Not Found
* **Fix**: Check `dotnet --version`. Install via `sudo pacman -S dotnet-sdk`.

## 2. NuGet Restore Failures
* **Fix**: Clear local cache and restore:
  ```bash
  dotnet nuget locals all --clear
  dotnet restore Sunshine.slnx
  ```
