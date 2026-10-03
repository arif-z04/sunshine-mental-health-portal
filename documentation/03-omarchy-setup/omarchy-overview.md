# Omarchy Linux Overview

**Omarchy** is an Arch Linux-based distribution designed for software developers, engineers, and power users.

---

## 1. What makes Omarchy / Arch Unique?

1. **Rolling Release Model**: Unlike fixed-release distributions (e.g. Ubuntu 22.04 or Debian 12) which freeze software versions for years, Omarchy is rolling. When a new version of the Linux kernel, PostgreSQL, or development libraries is released, it is packaged and delivered to your machine within days.
2. **Pacman Package Manager**: Arch uses `pacman`, one of the fastest, cleanest binary package managers in the Linux world.
3. **Arch User Repository (AUR)**: The AUR provides community-maintained build scripts for virtually every piece of software on Earth.
4. **Minimalism & Control**: The system only runs software you explicitly enable. No hidden background services or bloatware.

---

## 2. Core Administration Tools in Omarchy

* **`pacman`**: Installs official packages from Arch repositories.
* **`yay`**: Automated AUR helper for community packages.
* **`systemctl`**: Controls system daemons (starts PostgreSQL, enables services on boot).
* **`journalctl`**: Queries the systemd logging daemon to inspect service output and diagnose crashes.
