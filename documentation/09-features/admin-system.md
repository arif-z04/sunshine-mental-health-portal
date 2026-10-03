# Admin Operations Console & Audit Trail

The administrative control center ([`/admin`](file:///home/noir/Desktop/Another-Sunshine-Project/server/Sunshine.App/wwwroot/admin/index.html)).

---

## 1. Administrative Capabilities
* **Isolated Security Gateway**: Non-admins attempting to authenticate at `/api/admin/login` receive an immediate `HTTP 403 Forbidden`.
* **Live System Metrics**: Queries PostgreSQL for total BDT revenue, active appointment tallies, and user registrations.
* **Doctor Credential Verification**: Review BMDC certificates; approve or reject doctor practice applications.
* **User Management**: Toggle user `IsActive` status to suspend accounts violating clinical guidelines.
* **Security Audit Trail**: Inspect chronological, immutable logs of all operational actions.
