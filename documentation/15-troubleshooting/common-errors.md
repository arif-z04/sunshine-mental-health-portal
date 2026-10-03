# Common Errors & Quick Fixes

Fast reference for frequent developer errors.

---

## 1. Port 5000 Already in Use
* **Error**: `System.IO.IOException: Failed to bind to address http://0.0.0.0:5000: address already in use.`
* **Fix**:
  ```bash
  # Find PID using port 5000
  lsof -i :5000
  # Terminate previous instance
  kill -9 <PID>
  ```

---

## 2. Database Connection Refused
* **Error**: `Npgsql.NpgsqlException: Connection refused`
* **Fix**:
  ```bash
  sudo systemctl start postgresql
  ```
