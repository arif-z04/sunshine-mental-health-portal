# Production Database Administration & Backups

Best practices for running PostgreSQL in production.

---

## 1. Automated Backups with `pg_dump`
Create a daily cron job script:
```bash
#!/bin/bash
BACKUP_DIR="/var/backups/sunshine"
mkdir -p "$BACKUP_DIR"
DATE=$(date +%Y%m%d_%H%M%S)
PGPASSWORD='SecurePassword' pg_dump -h localhost -U sunshine_user -F c sunshine_db > "$BACKUP_DIR/sunshine_$DATE.dump"
# Keep only last 14 days of backups
find "$BACKUP_DIR" -type f -mtime +14 -delete
```

## 2. Restoring a Backup with `pg_restore`
```bash
pg_restore -U sunshine_user -d sunshine_db -c /var/backups/sunshine/sunshine_20261022.dump
```
