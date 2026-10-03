#!/bin/bash
set -e

echo "=== Concurrency & Double Booking Verification ==="

# 1. Login Patient 1 (Anika)
ANIKA_RES=$(curl -s -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"anika@example.com","password":"Password123!"}')
ANIKA_TOKEN=$(echo $ANIKA_RES | grep -o '"token":"[^"]*' | cut -d'"' -f4)

# 2. Login Patient 2 (Rahat)
RAHAT_RES=$(curl -s -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"rahat@example.com","password":"Password123!"}')
RAHAT_TOKEN=$(echo $RAHAT_RES | grep -o '"token":"[^"]*' | cut -d'"' -f4)

# Target Date: 3 days from now (to avoid conflicts with earlier test runs)
TARGET_DATE=$(date -d "+3 days" +%Y-%m-%d || date -v+3d +%Y-%m-%d)
TARGET_SLOT="17:30:00"
TARGET_END="18:15:00"

echo "Targeting Date: $TARGET_DATE at $TARGET_SLOT with Dr. Tanvir (Doctor ID 1)"

echo ""
echo "Attempt 1: Patient 1 (Anika) booking slot $TARGET_SLOT..."
RES1=$(curl -s -X POST http://localhost:5000/api/patient/appointments/book \
  -H "Authorization: Bearer $ANIKA_TOKEN" \
  -H "Content-Type: application/json" \
  -d "{\"doctorId\":1,\"appointmentDate\":\"$TARGET_DATE\",\"startTime\":\"$TARGET_SLOT\",\"endTime\":\"$TARGET_END\",\"reason\":\"Stress management booking 1\"}")
echo "Response 1: $RES1"

echo ""
echo "Attempt 2: Patient 2 (Rahat) attempting to book the EXACT SAME slot $TARGET_SLOT..."
RES2=$(curl -s -X POST http://localhost:5000/api/patient/appointments/book \
  -H "Authorization: Bearer $RAHAT_TOKEN" \
  -H "Content-Type: application/json" \
  -d "{\"doctorId\":1,\"appointmentDate\":\"$TARGET_DATE\",\"startTime\":\"$TARGET_SLOT\",\"endTime\":\"$TARGET_END\",\"reason\":\"Conflicting booking attempt 2\"}")
echo "Response 2: $RES2"

# Verification
if echo "$RES2" | grep -qi "already been booked"; then
  echo ""
  echo ">>> SUCCESS: Double-booking was strictly rejected by database / concurrency check! <<<"
else
  echo ""
  echo ">>> FAILURE: Double-booking was not prevented! <<<"
  exit 1
fi
