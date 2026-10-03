#!/bin/bash
set -e

BASE_URL="http://localhost:5000"
echo "================================================================="
echo "   SUNSHINE MENTAL HEALTH PORTAL — COMPREHENSIVE E2E VERIFICATION"
echo "================================================================="

# Helper functions
assert_http() {
  local status="$1"
  local expected="$2"
  local desc="$3"
  if [ "$status" -eq "$expected" ]; then
    echo "  [PASS] $desc (HTTP $status)"
  else
    echo "  [FAIL] $desc (Got HTTP $status, Expected $expected)"
    exit 1
  fi
}

echo ""
echo "--- 1. Testing Web Pages & Material UI Static Assets ---"
CODE=$(curl -s -L -o /dev/null -w "%{http_code}" "$BASE_URL/")
assert_http "$CODE" 200 "Homepage serves successfully"

CODE=$(curl -s -L -o /dev/null -w "%{http_code}" "$BASE_URL/patient")
assert_http "$CODE" 200 "Patient Care Portal serves successfully"

CODE=$(curl -s -L -o /dev/null -w "%{http_code}" "$BASE_URL/doctor")
assert_http "$CODE" 200 "Doctor Practice Portal serves successfully"

CODE=$(curl -s -L -o /dev/null -w "%{http_code}" "$BASE_URL/admin")
assert_http "$CODE" 200 "Admin Operations Console serves successfully"

CODE=$(curl -s -L -o /dev/null -w "%{http_code}" "$BASE_URL/admin/login")
assert_http "$CODE" 200 "Isolated Admin Login serves successfully"

echo ""
echo "--- 2. Patient Registration, Profile & Authentication ---"
RANDOM_ID=$(date +%s)
TEST_PAT_EMAIL="patient_${RANDOM_ID}@example.com"
REG_RES=$(curl -s -X POST "$BASE_URL/api/auth/register/patient" \
  -H "Content-Type: application/json" \
  -d "{
    \"email\": \"$TEST_PAT_EMAIL\",
    \"password\": \"Password123!\",
    \"fullName\": \"Farzana Haque\",
    \"phone\": \"+8801819001122\",
    \"emergencyContact\": \"Karim Haque (+8801711002233)\"
  }")
PAT_TOKEN=$(echo "$REG_RES" | grep -o '"token":"[^"]*' | cut -d'"' -f4)
if [ -z "$PAT_TOKEN" ]; then
  echo "  [FAIL] Patient registration failed: $REG_RES"
  exit 1
fi
echo "  [PASS] Patient registered and JWT token received"

# Fetch profile
PROF_RES=$(curl -s "$BASE_URL/api/patient/profile" -H "Authorization: Bearer $PAT_TOKEN")
if echo "$PROF_RES" | grep -q "Farzana Haque"; then
  echo "  [PASS] Patient profile fetched successfully"
else
  echo "  [FAIL] Patient profile fetch unexpected output: $PROF_RES"
  exit 1
fi

# Update profile
UPD_RES=$(curl -s -X PUT "$BASE_URL/api/patient/profile" \
  -H "Authorization: Bearer $PAT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Farzana Haque Chowdhury",
    "phone": "+8801819001122",
    "dateOfBirth": "1998-05-14",
    "gender": "Female",
    "emergencyContact": "Karim Haque (+8801711002233)",
    "medicalHistoryNotes": "Mild stress and anxiety prior to graduate thesis defense."
  }')
if echo "$UPD_RES" | grep -q "Farzana Haque Chowdhury"; then
  echo "  [PASS] Patient profile updated successfully"
else
  echo "  [FAIL] Patient profile update failed: $UPD_RES"
  exit 1
fi

echo ""
echo "--- 3. Specialist Directory & Slot Discovery ---"
DOCS_RES=$(curl -s "$BASE_URL/api/patient/doctors")
if echo "$DOCS_RES" | grep -q "Dr. Tanvir Ahmed"; then
  echo "  [PASS] Specialists directory lists Bangladesh certified doctors"
else
  echo "  [FAIL] Specialists directory missing expected doctors"
  exit 1
fi

# Fetch single doctor
DOC1_RES=$(curl -s "$BASE_URL/api/patient/doctors/1")
if echo "$DOC1_RES" | grep -q "BSMMU"; then
  echo "  [PASS] Single doctor profile fetched (Doctor #1)"
else
  echo "  [FAIL] Doctor #1 profile fetch failed"
  exit 1
fi

DAYS_AHEAD=$((10 + (RANDOM % 20)))
TARGET_DATE=$(date -d "+$DAYS_AHEAD days" +%Y-%m-%d)
DOW=$(date -d "$TARGET_DATE" +%u)
if [ "$DOW" -eq 5 ]; then
  TARGET_DATE=$(date -d "$TARGET_DATE + 1 day" +%Y-%m-%d)
fi
SLOTS_RES=$(curl -s "$BASE_URL/api/patient/doctors/1/slots?date=$TARGET_DATE")
if echo "$SLOTS_RES" | grep -q "startTime"; then
  echo "  [PASS] Slot generator returned working consultation intervals for $TARGET_DATE"
else
  echo "  [FAIL] Slot generation returned empty or invalid response: $SLOTS_RES"
  exit 1
fi

echo ""
echo "--- 4. Appointment Booking, Advance Hold & MFS Payment Simulation ---"
BOOK_RES=$(curl -s -X POST "$BASE_URL/api/patient/appointments/book" \
  -H "Authorization: Bearer $PAT_TOKEN" \
  -H "Content-Type: application/json" \
  -d "{
    \"doctorId\": 1,
    \"appointmentDate\": \"$TARGET_DATE\",
    \"startTime\": \"16:00:00\",
    \"endTime\": \"16:45:00\",
    \"reason\": \"Academic anxiety consultation\"
  }")
APT_ID=$(echo "$BOOK_RES" | grep -o '"id":[0-9]*' | head -1 | cut -d':' -f2)
if [ -n "$APT_ID" ] && echo "$BOOK_RES" | grep -q "PENDING"; then
  echo "  [PASS] Appointment #$APT_ID booked with PENDING status (Advance policy 15-min countdown)"
else
  echo "  [FAIL] Appointment booking failed: $BOOK_RES"
  exit 1
fi

# Process bKash payment
PAY_RES=$(curl -s -X POST "$BASE_URL/api/patient/payments/process" \
  -H "Authorization: Bearer $PAT_TOKEN" \
  -H "Content-Type: application/json" \
  -d "{
    \"paymentType\": \"APPOINTMENT\",
    \"appointmentId\": $APT_ID,
    \"paymentMethod\": \"BKASH\",
    \"accountNumberOrCard\": \"01700000000\",
    \"accountHolderName\": \"Farzana Haque\",
    \"otpOrPin\": \"12345\"
  }")
if echo "$PAY_RES" | grep -q '"success":true'; then
  TRX_ID=$(echo "$PAY_RES" | grep -o '"transactionId":"[^"]*' | head -1 | cut -d'"' -f4)
  echo "  [PASS] bKash sandbox payment successful (TrxID: $TRX_ID)"
else
  echo "  [FAIL] bKash payment simulation failed: $PAY_RES"
  exit 1
fi

# Verify appointment details
APT_DETAIL=$(curl -s "$BASE_URL/api/patient/appointments/$APT_ID" -H "Authorization: Bearer $PAT_TOKEN")
if echo "$APT_DETAIL" | grep -q "CONFIRMED"; then
  echo "  [PASS] Appointment status transitioned to CONFIRMED upon payment"
else
  echo "  [FAIL] Appointment did not transition to CONFIRMED: $APT_DETAIL"
  exit 1
fi

echo ""
echo "--- 5. Appointment Rescheduling & Conflict Safety ---"
RESCHED_RES=$(curl -s -X POST "$BASE_URL/api/patient/appointments/$APT_ID/reschedule" \
  -H "Authorization: Bearer $PAT_TOKEN" \
  -H "Content-Type: application/json" \
  -d "{
    \"newDate\": \"$TARGET_DATE\",
    \"newStartTime\": \"16:45:00\",
    \"newEndTime\": \"17:30:00\",
    \"reason\": \"Moved session 45 minutes later\"
  }")
if echo "$RESCHED_RES" | grep -q "16:45:00"; then
  echo "  [PASS] Appointment rescheduled to 16:45:00 successfully"
else
  echo "  [FAIL] Appointment rescheduling failed: $RESCHED_RES"
  exit 1
fi

echo ""
echo "--- 6. Double-Booking Strict Prevention Test ---"
# Anika attempts to book the exact same slot that Farzana rescheduled to
ANIKA_RES=$(curl -s -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"anika@example.com","password":"Password123!"}')
ANIKA_TOKEN=$(echo "$ANIKA_RES" | grep -o '"token":"[^"]*' | cut -d'"' -f4)

CONFLICT_RES=$(curl -s -X POST "$BASE_URL/api/patient/appointments/book" \
  -H "Authorization: Bearer $ANIKA_TOKEN" \
  -H "Content-Type: application/json" \
  -d "{
    \"doctorId\": 1,
    \"appointmentDate\": \"$TARGET_DATE\",
    \"startTime\": \"16:45:00\",
    \"endTime\": \"17:30:00\",
    \"reason\": \"Conflicting booking attempt\"
  }")
if echo "$CONFLICT_RES" | grep -qi "already been booked"; then
  echo "  [PASS] Concurrency double-booking strictly rejected by database / service rule"
else
  echo "  [FAIL] Double booking was NOT rejected: $CONFLICT_RES"
  exit 1
fi

echo ""
echo "--- 7. Resource Vault & Subscription Paywall ---"
FREE_RES=$(curl -s -X POST "$BASE_URL/api/patient/resources/1/access" -H "Authorization: Bearer $PAT_TOKEN")
if echo "$FREE_RES" | grep -q "contentUrl"; then
  echo "  [PASS] Free open resource accessed successfully"
else
  echo "  [FAIL] Free resource access failed: $FREE_RES"
  exit 1
fi

PREMIUM_RES_STATUS=$(curl -s -o /dev/null -w "%{http_code}" -X POST "$BASE_URL/api/patient/resources/3/access" -H "Authorization: Bearer $PAT_TOKEN")
if [ "$PREMIUM_RES_STATUS" -eq 401 ] || [ "$PREMIUM_RES_STATUS" -eq 403 ]; then
  echo "  [PASS] Premium resource #3 locked (HTTP $PREMIUM_RES_STATUS) for unsubscribed patient"
else
  echo "  [FAIL] Expected 401/403 for locked premium resource, got $PREMIUM_RES_STATUS"
  exit 1
fi

# Subscribe Farzana to Monthly Mindcare (Plan 1)
SUB_PAY_RES=$(curl -s -X POST "$BASE_URL/api/patient/payments/process" \
  -H "Authorization: Bearer $PAT_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "paymentType": "SUBSCRIPTION",
    "planId": 1,
    "paymentMethod": "BKASH",
    "accountNumberOrCard": "01700000000",
    "accountHolderName": "Farzana Haque",
    "otpOrPin": "12345"
  }')
if echo "$SUB_PAY_RES" | grep -q '"success":true'; then
  echo "  [PASS] Farzana subscribed to Monthly Pass via bKash"
else
  echo "  [FAIL] Subscription payment failed: $SUB_PAY_RES"
  exit 1
fi

# Now access premium resource #3
UNLOCKED_STATUS=$(curl -s -o /dev/null -w "%{http_code}" -X POST "$BASE_URL/api/patient/resources/3/access" -H "Authorization: Bearer $PAT_TOKEN")
if [ "$UNLOCKED_STATUS" -eq 200 ]; then
  echo "  [PASS] Premium resource #3 unlocked with HTTP 200 after active subscription"
else
  echo "  [FAIL] Expected 200 for unlocked resource, got $UNLOCKED_STATUS"
  exit 1
fi

echo ""
echo "--- 8. Clinician Portal Operations (Dr. Tanvir) ---"
DOC_LOGIN_RES=$(curl -s -X POST "$BASE_URL/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"dr.tanvir@sunshine.org","password":"Password123!"}')
DOC_TOKEN=$(echo "$DOC_LOGIN_RES" | grep -o '"token":"[^"]*' | cut -d'"' -f4)

DOC_APTS=$(curl -s "$BASE_URL/api/doctor/appointments" -H "Authorization: Bearer $DOC_TOKEN")
if echo "$DOC_APTS" | grep -q "Farzana Haque"; then
  echo "  [PASS] Clinician desk displays patient Farzana Haque's appointment"
else
  echo "  [FAIL] Clinician desk missing scheduled appointment"
  exit 1
fi

# Update appointment status & clinical notes
DOC_UPD_RES=$(curl -s -X PATCH "$BASE_URL/api/doctor/appointments/$APT_ID/status" \
  -H "Authorization: Bearer $DOC_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "status": "COMPLETED",
    "notes": "Patient demonstrated good receptiveness to breathing techniques."
  }')
if echo "$DOC_UPD_RES" | grep -q "COMPLETED"; then
  echo "  [PASS] Clinician updated appointment to COMPLETED with clinical notes"
else
  echo "  [FAIL] Clinician status update failed: $DOC_UPD_RES"
  exit 1
fi

echo ""
echo "--- 9. Admin Operations Console & Security Checks ---"
# Non-admin rejection at /api/admin/login
FORBIDDEN_CODE=$(curl -s -o /dev/null -w "%{http_code}" -X POST "$BASE_URL/api/admin/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"anika@example.com","password":"Password123!"}')
assert_http "$FORBIDDEN_CODE" 403 "Non-admin rejected from /api/admin/login"

# Valid Admin login
ADMIN_LOGIN_RES=$(curl -s -X POST "$BASE_URL/api/admin/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@sunshine.org","password":"Password123!"}')
ADMIN_TOKEN=$(echo "$ADMIN_LOGIN_RES" | grep -o '"token":"[^"]*' | cut -d'"' -f4)
if [ -n "$ADMIN_TOKEN" ]; then
  echo "  [PASS] Administrator authenticated successfully into Admin Console"
else
  echo "  [FAIL] Admin login failed: $ADMIN_LOGIN_RES"
  exit 1
fi

# Admin Metrics
METRICS_RES=$(curl -s "$BASE_URL/api/admin/metrics" -H "Authorization: Bearer $ADMIN_TOKEN")
if echo "$METRICS_RES" | grep -q "totalRevenue"; then
  echo "  [PASS] Admin metrics retrieved (Live BDT revenue & appointment counts)"
else
  echo "  [FAIL] Admin metrics query failed: $METRICS_RES"
  exit 1
fi

# Admin Audit Trail
AUDIT_RES=$(curl -s "$BASE_URL/api/admin/audit-logs" -H "Authorization: Bearer $ADMIN_TOKEN")
if echo "$AUDIT_RES" | grep -q "ADMIN_LOGIN"; then
  echo "  [PASS] Immutable security audit trail logs admin logins and operational events"
else
  echo "  [FAIL] Audit trail empty or failed: $AUDIT_RES"
  exit 1
fi

echo ""
echo "================================================================="
echo "   >>> ALL COMPREHENSIVE E2E VERIFICATIONS PASSED (100%) <<<"
echo "================================================================="
