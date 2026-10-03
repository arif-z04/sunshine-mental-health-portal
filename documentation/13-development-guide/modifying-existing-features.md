# How to Safely Modify Existing Features

A checklist for modifying existing functionality without introducing regressions.

---

1. **Locate the Feature Entrypoint**: Inspect `wwwroot/` to see which JS function calls the API.
2. **Inspect the Controller & Service**: Check DTO definitions and business rule checks.
3. **Run Existing Tests**: Execute `dotnet test` to establish a passing baseline.
4. **Apply Changes Incrementally**: Keep edits focused and concise.
5. **Re-run Tests & Verification Scripts**: Run `dotnet test` and `./test_e2e_full.sh`.
