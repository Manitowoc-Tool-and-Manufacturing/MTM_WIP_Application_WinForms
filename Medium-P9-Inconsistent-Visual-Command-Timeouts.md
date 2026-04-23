# Medium P9: Inconsistent Visual Command Timeouts

## Summary

Most Visual queries rely on default command timeout behavior, while only some analytics methods set explicit values.

## Why This Needs Fixing

- Timeout behavior is inconsistent across the Visual service.
- Long-running queries are harder to reason about operationally.
- Combined with missing cancellation, this makes abandoned work more expensive.

## Evidence

- Explicit timeout examples: `Services/Visual/Service_VisualDatabase.cs:1787`, `:2107`
- Most other command paths do not set `CommandTimeout`.

## Risk

- Unpredictable runtime behavior between methods.
- Longer-than-expected server resource retention under slow queries.

## Recommended Fix

1. Standardize command timeout policy across the Visual service.
2. Use method-specific overrides only where justified and documented.
3. Pair timeout policy with cancellation support.

## Acceptance Criteria

- Every Visual query path has an intentional timeout policy.
- Timeout values are consistent and documented.
- Slow-query handling is predictable across the service.
