# Testing Philosophy & Test Pyramid

Testing is a core quality gate in the Sunshine engineering workflow.

---

## 1. The Test Pyramid in Sunshine

```text
               /\
              /  \     E2E Verification (test_e2e_full.sh)
             /────\
            /      \    Integration Tests (Payment, Auth, Bangladesh)
           /────────\
          /          \   Unit Tests (Slot generator, Phone regex, DTOs)
         /────────────\
```

1. **Unit & Integration Tests (`server/Sunshine.Tests`)**: 26 automated xUnit tests validating domain rules, BST slot generation, MFS payment processing, and RBAC.
2. **Database Integrity Tests (`sql/07_test_queries.sql`)**: Direct SQL queries asserting table joins, row counts, and BDT revenue sums.
3. **Bash E2E Multi-Portal Verification (`test_e2e_full.sh`)**: End-to-end multi-persona simulation running against a live instance.
