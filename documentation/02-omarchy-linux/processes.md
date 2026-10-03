# Managing Linux Processes

Every running program on your computer is assigned a unique numeric **Process ID (PID)**.

---

## 1. Finding a Running Process
To see if Sunshine is running:
```bash
ps aux | grep Sunshine
```
* `ps aux`: Lists every active process running on the operating system.
* `|`: The "pipe" character; passes the output of `ps` into the next command.
* `grep Sunshine`: Filters lines containing the word "Sunshine".

---

## 2. Stopping a Stuck Process (`kill`)
If a server program is frozen in the background, you can terminate it by its PID:
```bash
kill 12345        # Politely asks PID 12345 to shut down (SIGTERM)
kill -9 12345     # Forces immediate termination (SIGKILL)
```
