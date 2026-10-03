# Debugging Workflow & Diagnostic Techniques

Step-by-step methodology for diagnosing bugs.

---

## 1. The Investigation Funnel

When an error occurs:
```text
Browser Console (F12 -> Console)
         │  (Check for JS errors or unhandled promises)
         ▼
Network Tab (F12 -> Network)
         │  (Inspect failing HTTP request & JSON response body)
         ▼
ASP.NET Core Server Output
         │  (Inspect terminal logs or journalctl for C# exceptions)
         ▼
PostgreSQL Database
         │  (Query affected tables in psql to inspect persistent state)
```
