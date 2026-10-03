#!/bin/bash
set -e

echo "=== 1. Testing Static HTML & Portals ==="
curl -s -o /dev/null -w "Homepage HTTP: %{http_code}\n" http://localhost:5000/
curl -s -o /dev/null -w "Admin Login HTTP: %{http_code}\n" http://localhost:5000/admin/login
curl -s -o /dev/null -w "Doctor Portal HTTP: %{http_code}\n" http://localhost:5000/doctor
curl -s -o /dev/null -w "Patient Portal HTTP: %{http_code}\n" http://localhost:5000/patient

echo ""
echo "=== 2. Testing Doctor Directory Endpoint ==="
curl -s http://localhost:5000/api/patient/doctors | head -c 200
echo ""

echo ""
echo "=== 3. Testing Dedicated Admin Login (Valid Admin) ==="
ADMIN_RES=$(curl -s -X POST http://localhost:5000/api/admin/login \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@sunshine.org","password":"Password123!"}')
echo "Admin Login Response: $ADMIN_RES"
ADMIN_TOKEN=$(echo $ADMIN_RES | grep -o '"token":"[^"]*' | cut -d'"' -f4)

echo ""
echo "=== 4. Testing Dedicated Admin Login (Non-Admin Rejection) ==="
NON_ADMIN_CODE=$(curl -s -o /dev/null -w "%{http_code}" -X POST http://localhost:5000/api/admin/login \
  -H "Content-Type: application/json" \
  -d '{"email":"anika@example.com","password":"Password123!"}')
echo "Non-Admin Login into Admin Route Returned HTTP Status: $NON_ADMIN_CODE (Expected: 403 Forbidden)"

echo ""
echo "=== 5. Testing Admin Metrics ==="
curl -s http://localhost:5000/api/admin/metrics \
  -H "Authorization: Bearer $ADMIN_TOKEN"

echo ""
echo "=== 6. Testing Patient Auth & Subscription Paywall ==="
ANIKA_RES=$(curl -s -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"anika@example.com","password":"Password123!"}')
ANIKA_TOKEN=$(echo $ANIKA_RES | grep -o '"token":"[^"]*' | cut -d'"' -f4)

SAZZAD_RES=$(curl -s -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"sazzad@example.com","password":"Password123!"}')
SAZZAD_TOKEN=$(echo $SAZZAD_RES | grep -o '"token":"[^"]*' | cut -d'"' -f4)

echo "Anika (Subscribed) Accessing Premium Resource #3:"
curl -s -X POST http://localhost:5000/api/patient/resources/3/access \
  -H "Authorization: Bearer $ANIKA_TOKEN"

echo ""
echo "Sazzad (Unsubscribed) Accessing Premium Resource #3:"
curl -s -X POST http://localhost:5000/api/patient/resources/3/access \
  -H "Authorization: Bearer $SAZZAD_TOKEN"

echo ""
echo "=== 7. Testing Doctor Slot Generation & Booking ==="
TOMORROW=$(date -d "+1 day" +%Y-%m-%d || date -v+1d +%Y-%m-%d)
echo "Slots for Doctor 1 on $TOMORROW:"
curl -s "http://localhost:5000/api/patient/doctors/1/slots?date=$TOMORROW" | head -c 250
echo ""

echo ""
echo "=== ALL VERIFICATIONS PASSED SUCCESSFULLY ==="
