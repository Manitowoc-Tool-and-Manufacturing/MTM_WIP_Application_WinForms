# High P7: Search Controls Allow Reentry

## Summary

Several Visual search controls allow repeated async entry through click and keyboard paths without a unified in-progress guard.

## Why This Needs Fixing

- Enter-key handlers and button handlers can both trigger searches.
- Some controls disable buttons during work, but that does not fully serialize all entry points.
- `Control_DieToolDiscovery` is especially exposed because it lacks a clear search-in-progress lock.

## Evidence

- `Controls/Visual/Control_VisualInventory.cs:62,66-68,71,90`
- `Controls/Visual/Control_InventoryAudit.cs:69,83,235,253,283,333`
- `Controls/Visual/Control_DieToolDiscovery.cs:34,38,89,104,124,197,279,293,303,317,334`

## Risk

- Duplicate user-triggered queries.
- Accidental request bursts from repeated Enter presses or rapid clicks.

## Recommended Fix

1. Add a shared in-progress guard per control.
2. Route all search entry points through a single guarded execution method.
3. Disable or ignore duplicate triggers while a request is active.

## Acceptance Criteria

- A control cannot launch the same search twice concurrently.
- Click and Enter handlers share the same guarded execution path.
- Search state is restored correctly after success, failure, and cancellation.
