# High P8: No Visual Disconnect Recovery

## Summary

The application has MySQL recovery and monitoring behavior, but nothing equivalent for the Visual SQL path.

## Why This Needs Fixing

- Visual outages currently fail only at request time.
- There is no backoff, circuit breaker, or operator guidance specific to Visual SQL degradation.
- Users are likely to retry manually, which increases pressure during outages.

## Evidence

- `Services/Database/Service_ConnectionRecoveryManager.cs`
- `Helpers/Helper_Database_ConnectionMonitor.cs`
- No Visual-specific equivalent found during the audit.

## Risk

- Human retry storms during partial or full Visual outages.
- Repeated failed request pressure on an already degraded dependency.

## Recommended Fix

1. Add a Visual-specific availability/recovery strategy.
2. Introduce backoff or circuit-breaker behavior for repeated failures.
3. Surface a clear Visual outage state in the UI so users stop retrying blindly.

## Acceptance Criteria

- Repeated Visual failures transition the UI into a protected degraded mode.
- The app limits retry frequency during outages.
- Users see clear status messaging for Visual-specific failures.
