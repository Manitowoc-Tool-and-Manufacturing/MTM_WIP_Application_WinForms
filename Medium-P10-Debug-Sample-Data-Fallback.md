# Medium P10: Debug Sample Data Fallback Masks Connectivity Failures

## Summary

Debug-only fallback to sample data can hide real connection and query failures during development and testing.

## Why This Needs Fixing

- Teams can underestimate production failure behavior.
- Sample data may make Visual paths appear healthy when they are not.
- This distorts validation of performance and resilience fixes.

## Evidence

- `Services/Visual/Service_VisualDatabase.cs:44,72`
- Additional sample-data branches exist throughout the service, including around `:605`, `:633`, `:693`, `:719`.

## Risk

- False confidence in connectivity.
- Missed reproduction of real outage or latency issues.

## Recommended Fix

1. Make sample-data fallback opt-in and clearly surfaced in the UI/logs.
2. Prevent silent fallback when validating production-like environments.
3. Document which scenarios permit sample mode.

## Acceptance Criteria

- Developers can clearly tell when sample mode is active.
- Production-like testing does not silently mask Visual failures.
- Sample-data behavior is deliberate rather than automatic.
