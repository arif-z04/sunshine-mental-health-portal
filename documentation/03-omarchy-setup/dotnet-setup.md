# .NET 10 SDK Setup on Omarchy

The Sunshine backend is built on **C# and ASP.NET Core 10**.

---

## 1. Installing .NET SDK on Omarchy

Arch Linux provides official .NET packages:

```bash
sudo pacman -S dotnet-sdk dotnet-runtime aspnet-runtime
```

Verify the installation:
```bash
dotnet --version
# Expected: 10.0.xxx (or latest installed SDK)
```

Inspect the installed SDKs and runtimes:
```bash
dotnet --info
```
This command outputs:
* .NET SDK Version and Architecture (`x64` or `arm64`).
* Host runtime environment.
* Available .NET Runtimes (`Microsoft.AspNetCore.App`, `Microsoft.NETCore.App`).

---

## 2. Useful Global .NET Tools

Optionally install the Entity Framework Core CLI tool if you wish to run migrations via the command line:

```bash
dotnet tool install --global dotnet-ef
```
Ensure your PATH includes `~/.dotnet/tools` by adding this to your `~/.bashrc` or `~/.zshrc`:
```bash
export PATH="$PATH:$HOME/.dotnet/tools"
```
