# Seed Data & Sequence Synchronization

`sql/06_seed_data.sql` populates realistic Bangladesh telehealth demo data:
* Clinicians from BSMMU, Dhaka University, and NIMH.
* Emergency crisis hotlines (Kaan Pete Roi `+8801779554391`).
* Dynamic sequence synchronization resetting all `SERIAL` sequence counters using `pg_get_serial_sequence`.
