# Omarchy Linux Troubleshooting Guide

Solutions to common system-level issues on Omarchy / Arch Linux:

---

## 1. "error: failed to init transaction (unable to lock database)"
* **Cause**: Another `pacman` or `yay` process was interrupted or is still running in the background.
* **Fix**: Ensure no other updates are running, then remove the lock file:
  ```bash
  sudo rm /var/lib/pacman/db.lck
  ```

---

## 2. "command not found: dotnet"
* **Cause**: The .NET SDK package is not installed or not in your shell's PATH.
* **Fix**:
  ```bash
  sudo pacman -S dotnet-sdk
  ```

---

## 3. "sudo: a password is required"
* **Cause**: Linux requires your local user password to execute commands with root administrative privileges. Type your personal user password (characters will not show on screen for security) and press `Enter`.
