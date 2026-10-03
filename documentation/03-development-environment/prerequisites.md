# Development Prerequisites & Installation Matrix

Before developing Sunshine, verify your system software against this verified version matrix:

---

| Software | Minimum Version | Verified in Repo | Installation Command (Omarchy) |
| :--- | :--- | :--- | :--- |
| **.NET SDK** | 10.0.100 | `10.0.100-preview` | `sudo pacman -S dotnet-sdk` |
| **PostgreSQL** | 16.0+ | `18.0` | `sudo pacman -S postgresql` |
| **Git** | 2.30+ | `2.40+` | `sudo pacman -S git` |
| **cURL** | 7.80+ | `8.x` | `sudo pacman -S curl` |
| **jq** | 1.6+ | `1.7` | `sudo pacman -S jq` |

---

## One-Line Installation for Omarchy Linux
```bash
sudo pacman -S git postgresql dotnet-sdk curl jq ripgrep fd
```
