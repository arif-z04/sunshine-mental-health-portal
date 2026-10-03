# Network Ports & `lsof`

A **port** is a numbered communication channel allowing multiple network programs to share one IP address.

---

## 1. Sunshine's Port Map
* **Port 5000**: Sunshine Kestrel Web Server (`http://localhost:5000`).
* **Port 5432**: PostgreSQL Database Engine.
* **Port 80 / 443**: Standard HTTP / HTTPS ports used by web browsers.

---

## 2. Diagnosing "Port Already in Use"
If Sunshine crashes with:
`System.IO.IOException: Failed to bind to address http://0.0.0.0:5000: address already in use.`

Run this command to discover which program is occupying port 5000:
```bash
lsof -i :5000
```
* `lsof`: List Open Files.
* `-i :5000`: Find network connections listening on port 5000.
* Output shows the program name and its PID, allowing you to terminate it with `kill <PID>`.
