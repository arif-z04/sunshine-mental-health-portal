# Tables: Structured Relational Grids

A **table** is a collection of related data entries organized in rows and columns.

Sunshine contains **16 relational tables**:
1. `users`: Credentials, roles, and identity.
2. `patients`: Medical profile, emergency phone, blood group.
3. `doctors`: BMDC license, degrees, fee, payment policy.
4. `specializations`: Medical disciplines.
5. `doctor_specializations`: Junction table (many-to-many).
6. `doctor_working_hours`: Weekly recurring clinic intervals.
7. `appointments`: Consultations and clinical progress notes.
8. `payments`: bKash/Nagad transactions in ৳ BDT.
9. `subscription_plans`: Mindcare pricing packages.
10. `subscriptions`: Active patient digital passes.
11. `resource_categories`: Educational topics.
12. `resources`: Articles and CBT workbooks.
13. `audit_logs`: Immutable security audit trail.
14. `notifications`: In-app system alerts.
15. `session_reviews`: Post-session star ratings.
16. `emergency_contacts`: National hotlines (Kaan Pete Roi, 999).
