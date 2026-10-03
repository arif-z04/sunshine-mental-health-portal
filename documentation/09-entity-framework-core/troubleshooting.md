# EF Core Troubleshooting Guide

* **InvalidOperationException: Sequence contains no elements**: Use `.FirstOrDefaultAsync()` instead of `.FirstAsync()`.
* **Duplicate Key Violation**: Re-synchronize PostgreSQL auto-increment sequence counters.
