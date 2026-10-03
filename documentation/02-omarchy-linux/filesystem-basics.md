# Linux Filesystem Basics

Understanding how files and directories are organized in Linux.

---

## 1. The Root Directory (`/`)
In Windows, storage drives are labeled with drive letters (`C:\`, `D:\`).
In Linux, everything exists under a single unified tree starting at the **root directory**, represented by a single forward slash: `/`.

```text
/                      (Root directory)
├── bin                (Core system executable programs)
├── etc                (System-wide configuration files)
├── home               (User home directories)
│   └── noir           (Your personal files: /home/noir/)
│       └── Desktop
├── var                (Variable data: logs, PostgreSQL database cluster)
│   ├── log
│   └── lib/postgres/data
└── tmp                (Temporary files cleared on reboot)
```
