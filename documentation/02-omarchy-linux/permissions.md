# Linux Permissions & `sudo` Explained

Linux is built from the ground up as a secure multi-user operating system.

---

## 1. The Three Permission Types
Every file and folder on Linux has permissions for three categories of users:
1. **Owner (u)**: The specific user who owns the file.
2. **Group (g)**: Other members of the file's assigned user group.
3. **Others (o)**: Everyone else on the system.

Permissions are represented by three letters:
* **`r` (Read)**: Permission to view the file's contents.
* **`w` (Write)**: Permission to edit, overwrite, or delete the file.
* **`x` (Execute)**: Permission to run the file as a program or script.

Example output from `ls -l`:
`-rwxr-xr--  1 noir noir  12074 Oct  3 23:01 test_e2e_full.sh`
* The owner (`noir`) can read, write, and execute (`rwx`).
* The group (`noir`) can read and execute (`r-x`).
* Others can only read (`r--`).

---

## 2. Making a Script Executable (`chmod +x`)
If you create a new bash script `test.sh` and try to run it, Linux will block it by default. To make it runnable:
```bash
chmod +x test.sh
```
* `chmod`: Change Mode (change file permissions).
* `+x`: Add the execute permission flag.
