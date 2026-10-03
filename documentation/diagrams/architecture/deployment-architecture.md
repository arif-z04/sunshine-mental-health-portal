# Production Deployment Architecture Diagram

## Purpose
Shows how Sunshine is hosted on an Omarchy/Arch Linux production server.

## Diagram

```mermaid
flowchart TD
    InternetClient["Internet Client (Browser / Mobile)"] -->|HTTPS 443 (SSL/TLS)| Nginx["Nginx Reverse Proxy"]
    
    subgraph Host ["Linux Production Host (Omarchy / Arch Linux)"]
        Nginx -->|Reverse Proxy HTTP 5000| Kestrel["Kestrel Web Server (sunshine.service)"]
        
        subgraph SystemdUnit ["Systemd Service Daemon"]
            Kestrel --> App["Sunshine.App (.NET 10 Runtime)"]
        end
        
        App -->|Unix Socket / TCP 5432| Postgres["PostgreSQL 18 Service (postgresql.service)"]
        Postgres --> DBStorage[("Database Storage: /var/lib/postgres/data")]
        
        Cron["Cron Daemon"] -->|Daily pg_dump| Backup[("/var/backups/sunshine/*.dump")]
    end
```
