# Decision Tree: Diagnosing Common Errors

```text
The Portal Won't Load
         │
         ├── Is PostgreSQL running?
         │       ├── No  ──> sudo systemctl start postgresql
         │       └── Yes ──> Continue
         │
         ├── Is the backend running on port 5000?
         │       ├── No  ──> cd server/Sunshine.App && dotnet run
         │       └── Yes ──> Continue
         │
         └── Does the browser show a CORS error?
                 ├── Yes ──> Ensure requesting http://localhost:5000 directly
                 └── No  ──> Check DevTools Console
```
