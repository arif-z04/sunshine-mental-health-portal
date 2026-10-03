# Inspecting System Logs with `journalctl`

When a service crashes or refuses to start, do not panic. Use `journalctl` to inspect its log output.

---

## 1. The Key Command
```bash
journalctl -u postgresql -n 50 --no-pager
```
* `journalctl`: Systemd's unified logging tool.
* `-u postgresql`: Filter logs strictly for the unit named `postgresql`.
* `-n 50`: Display only the most recent 50 lines.
* `--no-pager`: Output directly into your terminal window rather than opening a scrolling viewer.

---

## 2. Following Logs Live (Real-Time Tailing)
```bash
journalctl -u postgresql -f
```
* `-f` (*Follow*): Keeps the terminal open and prints new database queries and connection events as they happen live. Press `Ctrl + C` to exit.
