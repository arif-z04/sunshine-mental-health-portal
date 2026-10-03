# Automated API Verification Scripts

Three targeted bash scripts test live server behavior:

---

1. **`./test_verification.sh`**:
   - Smoke tests static routes (`/`, `/admin/login`, `/doctor`, `/patient`).
   - Verifies specialist directory and dynamic slot generation.
   - Tests valid Admin login and asserts `403 Forbidden` for non-admin attempts.
   - Verifies digital vault paywall gating.

2. **`./test_bangladesh.sh`**:
   - Asserts subscription plans are priced in BDT (`৳`).
   - Tests specialist directory in Bangladesh.
   - Simulates patient booking and authorizes a ৳1,500 payment via bKash.

3. **`./test_double_booking.sh`**:
   - Verifies concurrency defense by launching simultaneous booking requests for the identical slot.
