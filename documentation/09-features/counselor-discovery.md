# Specialist Discovery & Filtering

Helping patients find the right specialist quickly and confidently.

---

## 1. Discovery Flow
1. Patient navigates to `/patient`.
2. Frontend calls `GET /api/patient/doctors`.
3. System returns doctors with their BMDC number, institutional affiliation (BSMMU, DU, NIMH), consultation fee, and areas of expertise.
4. Patient clicks "View Profile & Book" to inspect available dates.
