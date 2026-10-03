# Relational Integrity & Foreign Keys

Relational databases preserve data integrity through strict foreign key relationships.

---

## 1. One-to-One Relationships

* **`users` -> `patients`**:
  Each patient record references exactly one user account (`patients.UserId -> users.Id`). Deleting the user cascades to remove the patient profile.
* **`users` -> `doctors`**:
  Each doctor record references exactly one user account (`doctors.UserId -> users.Id`).

---

## 2. One-to-Many Relationships

* **`doctors` -> `appointments`**:
  A single doctor conducts multiple appointments across time.
* **`patients` -> `appointments`**:
  A single patient books multiple sessions.
* **`doctors` -> `doctor_working_hours`**:
  A single doctor defines working hours for Saturday through Thursday.
* **`subscription_plans` -> `subscriptions`**:
  A plan (e.g. Monthly Pass) is purchased by many subscribers.
* **`resource_categories` -> `resources`**:
  A category (e.g. Anxiety & Stress) organizes multiple articles and workbooks.

---

## 3. Many-to-Many Relationships

* **`doctors` <-> `specializations`**:
  A psychiatrist may specialize in both *Adult ADHD* and *Anxiety Disorders*. This is resolved using the junction table **`doctor_specializations`** (`DoctorId`, `SpecializationId`).
