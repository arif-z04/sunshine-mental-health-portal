# Comprehensive Master E2E Suite (`test_e2e_full.sh`)

`test_e2e_full.sh` is Sunshine's master end-to-end verification script.

---

## 1. What Does It Test?

The script simulates a complete clinical lifecycle across all three user personas:
1. **Portal Assets**: Serves public landing, patient, doctor, and admin portals with HTTP 200.
2. **Patient Onboarding**: Registers a new patient (`Farzana Haque`), updates profile details, and queries doctors.
3. **Appointment Booking & Advance Hold**: Books Dr. Tanvir for 16:00; checks 15-min hold.
4. **MFS bKash Checkout**: Simulates ৳1,500 payment, checks transaction code, verifies status becomes `CONFIRMED`.
5. **Rescheduling**: Moves appointment to 16:45; verifies conflict safety.
6. **Double-Booking Rejection**: Proves duplicate booking of 16:45 is strictly rejected.
7. **Resource Paywall**: Accesses free resource; asserts premium resource is blocked (401); buys subscription via bKash; verifies premium resource is unlocked (200).
8. **Doctor Desk**: Dr. Tanvir logs in, inspects patient appointment, and marks it `COMPLETED` with clinical notes.
9. **Admin Console**: Non-admin rejected (403); Admin logs in, verifies live BDT revenue metrics, and inspects audit trail.

### Execution Command:
```bash
./test_e2e_full.sh
```
