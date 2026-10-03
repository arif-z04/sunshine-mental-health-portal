# System Overview

The **Sunshine Mental Health Portal** is an enterprise-grade telehealth platform designed specifically to meet the healthcare, cultural, and logistical realities of mental wellness in Bangladesh.

---

## 1. Core Purpose & Mission

1. **Overcoming Stigma**: Mental health counseling is private and dignified. Patients can book sessions under their chosen display names, review clinician credentials, and attend secure consultations.
2. **Bangladesh Healthcare Ecosystem Integration**:
   - Clinicians are verified with their **BMDC (Bangladesh Medical and Dental Council)** registration numbers.
   - Clinical specialists are categorized by educational institutes (BSMMU, Dhaka University, National Institute of Mental Health - NIMH).
   - Emergency banner prominently links to **কান পেতে রই (Kaan Pete Roi)**, **999**, and **16263**.
3. **Frictionless Local Payments**:
   - Native integration with **bKash (বিকাশ)**, **Nagad (নগদ)**, and **Rocket (রকেট)** sandbox simulation in Bangladeshi Taka (৳ BDT).
   - Clear policy management: Clinicians can choose **ADVANCE** payment (with a strict 15-minute reservation hold) or **POST_PAYMENT** after consultation.
4. **Therapeutic Resource Library**:
   - Bilingual psychoeducational articles and cognitive-behavioral therapy (CBT) workbooks tailored for local stressors (exam stress, university entrance exams, corporate burnout, adolescent family conflicts).

---

## 2. Multi-Portal Architecture

Rather than forcing all personas into a confusing monolithic interface, Sunshine separates users into dedicated, role-tailored portals:

```text
                               +-----------------------------+
                               |     Public Landing Page     |
                               |             (/)             |
                               +-----------------------------+
                                     /         |         \
                                    /          |          \
                                   v           v           v
+------------------------+ +------------------------+ +------------------------+
|      Patient Portal    | |      Doctor Portal     | |       Admin Portal     |
|       (/patient)       | |       (/doctor)        | |    (/admin, /admin/login)|
+------------------------+ +------------------------+ +------------------------+
| - Specialist Directory | | - BMDC Verification    | | - Metrics & BDT Revenue|
| - Booking & Reschedule | | - Consultation Ledger  | | - Doctor Approval      |
| - MFS Checkout (৳)     | | - Clinical Progress    | | - User Management      |
| - Resource Vault       | | - Advance/Post Policy  | | - Immutable Audit Log  |
| - Profile Management   | | - BST Working Hours    | | - Subscription Plans   |
+------------------------+ +------------------------+ +------------------------+
```
