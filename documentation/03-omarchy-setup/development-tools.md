# Essential Developer CLI Utilities

To be productive on Omarchy Linux, the following CLI utilities are recommended:

---

## 1. Recommended Utilities

```bash
sudo pacman -S jq ripgrep fd fzf htop
```

* **`jq`**: Lightweight command-line JSON processor. Perfect for formatting API responses from `curl`:
  ```bash
  curl -s http://localhost:5000/api/patient/doctors | jq .
  ```
* **`ripgrep` (`rg`)**: Blazing fast search utility across project files:
  ```bash
  rg "AppointmentStatuses" server/Sunshine.App
  ```
* **`fd`**: Fast alternative to `find`:
  ```bash
  fd "Entities.cs"
  ```
* **`htop`**: Interactive process monitor to inspect CPU and RAM consumption.
