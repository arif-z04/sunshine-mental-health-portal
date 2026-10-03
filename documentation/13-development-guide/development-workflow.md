# Daily Developer Workflow on Omarchy

A practical step-by-step routine for local engineering.

---

## 1. Daily Development Routine

```text
1. Open Terminal
2. Verify PostgreSQL status (systemctl status postgresql)
3. Pull latest changes (git pull)
4. Build solution (dotnet build Sunshine.slnx)
5. Run automated tests (dotnet test)
6. Launch local server (dotnet run --urls "http://0.0.0.0:5000")
7. Code feature / Fix bug
8. Run test_e2e_full.sh to verify zero regressions
9. Commit changes
```
