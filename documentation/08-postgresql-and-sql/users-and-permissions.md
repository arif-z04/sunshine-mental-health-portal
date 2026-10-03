# Users & Permissions: `sunshine_user`

Sunshine connects using a dedicated database user:
* **Username**: `sunshine_user`
* **Password**: `SunshinePass123!`
* **Database**: `sunshine_db`

Never use the `postgres` superuser for your application connections. Restricting permissions prevents catastrophic drops or unauthorized access if a vulnerability is exploited.
