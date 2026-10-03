# PostgreSQL Initialization on Omarchy

Follow these steps to initialize and start PostgreSQL on a fresh Omarchy system:

---

## Step 1: Initialize Database Storage Cluster (One-Time Only)
```bash
sudo -u postgres initdb -D /var/lib/postgres/data --locale=C.UTF-8 --encoding=UTF8
```

## Step 2: Start and Enable PostgreSQL Service
```bash
sudo systemctl enable --now postgresql
```

## Step 3: Verify with `psql`
```bash
sudo -u postgres psql -c "SELECT version();"
```
*Prints the running PostgreSQL 18 engine version.*
