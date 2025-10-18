## Manual Test Runner

This helper enables interactive execution of the manual Stored Procedure Builder tests (TEST-001 through TEST-004).

### Files
- `manual-test-definitions.json` – Structured metadata for each manual test, including prerequisites and step-by-step actions.
- `ManualTestRunner.ps1` – PowerShell script that walks through a selected test, prompts for outcomes, and records a timestamped report under `reports/`.

### Usage
```powershell
cd specs/004-interactive-mysql-5/tests/test-runner

# List available tests
pwsh ./ManualTestRunner.ps1 -List

# Run a specific test by id
pwsh ./ManualTestRunner.ps1 -TestId TEST-003

# Run interactively by selecting from a list
pwsh ./ManualTestRunner.ps1
```

During execution you will:
- Confirm prerequisites are satisfied.
- Review each step's actions and expected results.
- Enter actual observations and mark the step as PASS, FAIL, or SKIP.
- Provide optional notes when a failure occurs.

Reports are saved as JSON with the filename format `yyyyMMdd-HHmmss-<TEST-ID>.json` inside `reports/`. These files capture per-step outcomes and make it easier to share evidence or import into other tooling.
