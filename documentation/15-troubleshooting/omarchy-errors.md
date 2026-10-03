# Omarchy & Arch Linux System Diagnostics

---

## 1. Pacman Database Lock
* **Error**: `error: failed to init transaction (unable to lock database)`
* **Fix**: If no other pacman process is running:
  ```bash
  sudo rm /var/lib/pacman/db.lck
  ```

## 2. Service Failed to Start
* **Diagnose with journalctl**:
  ```bash
  journalctl -xeu postgresql.service
  ```
