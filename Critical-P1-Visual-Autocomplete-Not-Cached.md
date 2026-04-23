# Critical P1: Visual Autocomplete Not Cached

## Summary

Methods named like cached providers are not actually cached. Each request resolves the Visual service and performs a live SQL query.

## Why This Needs Fixing

- Repeated autocomplete usage creates unnecessary SQL traffic.
- The current naming hides the real cost and encourages reuse in more controls.
- This increases connection churn and repeated reads against Visual SQL.

## Evidence

- `Helpers/Helper_SuggestionTextBox.cs:90-92`
- `Helpers/Helper_SuggestionTextBox.cs:232-298`
- `Services/Visual/Service_VisualDatabase.cs:1165-1328`

## Risk

- Repeated list loads for part, user, location, warehouse, work order, PO, CO, FGT, and MMC/MMF sources.
- Higher SQL login/open overhead because Visual connections are short-lived and pooling is disabled.

## Recommended Fix

1. Add a shared in-memory cache for Visual suggestion datasets.
2. Give each dataset a TTL and explicit invalidation path.
3. Rename methods so behavior matches the name if true caching is not introduced immediately.

## Acceptance Criteria

- Repeated calls for the same suggestion source do not hit SQL again within the cache TTL.
- Cache invalidation can be triggered explicitly after relevant refresh operations.
- Method names accurately reflect cached behavior.
