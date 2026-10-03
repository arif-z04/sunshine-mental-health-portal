# System Prerequisites

Before running or developing the Sunshine Mental Health Portal, ensure your development system satisfies the following hardware and software requirements.

---

## 1. Hardware Requirements

* **Processor (CPU)**: 64-bit x86_64 or ARM64 processor (2 cores minimum, 4 cores recommended).
* **Memory (RAM)**: 4 GB RAM minimum (8 GB recommended for running .NET compilation, PostgreSQL, and browser simultaneously).
* **Storage**: 5 GB of free disk space for the .NET SDK, PostgreSQL database files, node/tools, and git repository.
* **Network**: Active internet connection to install packages and restore dependencies.

---

## 2. Operating System

The primary supported and verified operating system for this documentation is **Omarchy Linux** (or any standard **Arch Linux** installation).
* Kernel: Linux 6.x or newer.
* Init System: `systemd`.
* Package Manager: `pacman` and AUR helpers (such as `yay`).

*(Note: While the application can run on Ubuntu, Debian, macOS, or Windows via .NET Core cross-platform support, all commands in this documentation are tailored for Omarchy Linux).*

---

## 3. Required Software & Version Matrix

| Software Component | Minimum Version | Verified Version in Repo | Purpose |
| :--- | :--- | :--- | :--- |
| **.NET SDK** | 10.0.100 | `10.0.100-preview.1` or newer | Compiles and executes C# ASP.NET Core application |
| **PostgreSQL** | 16.0 | `18.0` | Primary relational database server |
| **Git** | 2.30+ | `2.40+` | Version control system |
| **cURL / Bash** | 7.80+ / Bash 5.0+ | Modern system versions | Executes automated testing & verification suites |
| **Modern Browser** | Any modern engine | Google Chrome, Chromium, Firefox | Client-side web portal interaction |

---

## 4. Verification Commands

Run these terminal commands to verify whether your workstation is prepared:

```bash
# 1. Verify .NET SDK
dotnet --version
# Expected: 10.0.xxx

# 2. Verify PostgreSQL client
psql --version
# Expected: psql (PostgreSQL) 18.x or 16.x

# 3. Verify Git
git --version
# Expected: git version 2.x.x

# 4. Verify cURL
curl --version
# Expected: curl 8.x.x
```

If any of these commands output `command not found`, proceed to [Chapter 03: Omarchy Setup](../03-omarchy-setup/omarchy-overview.md) for step-by-step installation instructions.
