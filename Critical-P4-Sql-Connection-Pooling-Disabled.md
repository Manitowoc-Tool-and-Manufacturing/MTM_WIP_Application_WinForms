# Critical P4: SQL Connection Pooling Disabled

## Summary

The Visual SQL connection string explicitly disables pooling.

## Why This Needs Fixing

- Every request pays full connection open/login cost.
- This amplifies the damage from repeated autocomplete and overlapping searches.
- It turns normal UI churn into expensive connection churn.

## Evidence

- `Services/Visual/Service_VisualDatabase.cs:1450-1462`

## Risk

- More TCP/session churn.
- Higher auth and open overhead.
- Worse server behavior under repeated short-lived request bursts.

## Recommended Fix

1. Re-enable SQL Server pooling unless there is a proven production reason not to.
2. Document the original reason if pooling must remain disabled.
3. Validate behavior under repeated Visual queries after the change.

## Acceptance Criteria

- Visual requests reuse pooled connections by default.
- No regression is introduced in connection lifecycle behavior.
- Connection churn drops measurably during repeated query workflows.
