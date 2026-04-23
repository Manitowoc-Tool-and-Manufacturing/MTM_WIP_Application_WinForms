# Critical P3: Unbounded Infor Distinct Lookups

## Summary

Several autocomplete sources query large Visual tables using unbounded `SELECT DISTINCT ... ORDER BY ...` patterns.

## Why This Needs Fixing

- Work order, PO, CO, and user suggestion sources are backed by `INVENTORY_TRANS`.
- There is no row cap, no date bound, no cache, and no debounce at the SQL source level.
- These queries are attached to interactive suggestion workflows.

## Evidence

- `Services/Visual/Service_VisualDatabase.cs:1167`
- `Services/Visual/Service_VisualDatabase.cs:1175`
- `Services/Visual/Service_VisualDatabase.cs:1183`
- `Services/Visual/Service_VisualDatabase.cs:1191`
- `Services/Visual/Service_VisualDatabase.cs:1328-1333`

## Risk

- Large index scans or full-history reads.
- Excessive read load during ordinary UI interaction.
- Slow autocomplete encourages user retries, increasing pressure further.

## Recommended Fix

1. Replace these with bounded or precomputed lookup sources.
2. Add source-specific limits or recency filters where acceptable.
3. Consider dedicated lookup queries or persisted lookup tables for heavy suggestion sets.

## Acceptance Criteria

- Suggestion queries no longer scan the full historical transaction space on each request.
- Lookup latency remains stable under repeated UI usage.
- Heavy suggestion sources are bounded, cached, or precomputed.
