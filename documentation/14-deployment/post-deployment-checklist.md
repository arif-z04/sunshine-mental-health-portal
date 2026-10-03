# Post-Deployment Verification Checklist

Verify before opening the portal to real patients.

---

* [ ] PostgreSQL is running and protected behind local sockets.
* [ ] Database user is not superuser `postgres`.
* [ ] `sunshine.service` starts automatically on server reboot (`systemctl is-enabled sunshine`).
* [ ] HTTPS redirects HTTP traffic cleanly.
* [ ] All demo seed passwords changed in production.
* [ ] Automated database backup cron job verified with a test restore.
* [ ] Crisis hotlines banner numbers verified (`+8801779554391`).
