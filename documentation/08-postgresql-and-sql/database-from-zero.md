# Databases from Absolute Zero

## 1. What is Data?
Data is information—such as a patient's phone number, an appointment time, or a doctor's BMDC license.

## 2. Why Not Store Data in Text Files?
Imagine saving appointments in a single `appointments.txt` file:
* **Concurrency Crashes**: If two patients try to book at the exact same second, one write will overwrite or corrupt the other!
* **Slow Searching**: If you have 500,000 appointments, finding a specific doctor's schedule requires scanning through the entire file line by line.
* **No Integrity Checks**: Nothing stops someone from accidentally typing `"FREE"` into a field that requires a monetary number.

A **Relational Database Management System (RDBMS)** solves all these problems with blazing speed, ACID transactions, and strict data type enforcement.
