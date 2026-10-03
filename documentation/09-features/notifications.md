# System Notifications & Alerts

Keeping patients and clinicians informed of critical events.

---

## 1. Notification Triggers
* **Appointment Booked**: Alerts doctor of a new pending or confirmed session.
* **Payment Received**: Informs patient with transaction reference (`TRX-BKS-...`).
* **Appointment Rescheduled**: Notifies doctor of the new date and time.
* **BMDC Verification Approved**: Alerts doctor that their clinical profile is now live.

Notifications are stored in the PostgreSQL `notifications` table and displayed in user portal headers.
