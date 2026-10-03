# Your First Day: From Zero to Running Application

Follow these exact steps on your Omarchy Linux machine to see the application running with your own eyes.

---

## Step 1: Open Your Terminal
Press `Super + Enter` (or launch your terminal emulator like Alacritty, Kitty, or Foot).

## Step 2: Navigate to the Workspace
```bash
cd /home/noir/Desktop/Another-Sunshine-Project
```
Verify where you are by running:
```bash
pwd
```
*It should print `/home/noir/Desktop/Another-Sunshine-Project`.*

## Step 3: Check That PostgreSQL is Running
```bash
sudo systemctl status postgresql
```
*Look for `Active: active (running)`. If stopped, start it with `sudo systemctl start postgresql`.*

## Step 4: Run the Automated Verification Suite
Sunshine includes a master test script that runs automated checks across all features:
```bash
./test_e2e_full.sh
```
Watch the terminal print passing checks for authentication, bKash checkout, and scheduling.

## Step 5: Start the Web Server
```bash
cd server/Sunshine.App
dotnet run --urls "http://0.0.0.0:5000"
```

## Step 6: Open Your Browser
Navigate to:
* **Landing Page**: `http://localhost:5000/`
* **Patient Portal**: `http://localhost:5000/patient`
* **Doctor Portal**: `http://localhost:5000/doctor`
* **Admin Portal**: `http://localhost:5000/admin` (Isolated Login: `/admin/login`)

Log in using the demo account:
* Email: `admin@sunshine.org`
* Password: `Password123!`

Congratulations! You are officially running the Sunshine Mental Health Portal!
