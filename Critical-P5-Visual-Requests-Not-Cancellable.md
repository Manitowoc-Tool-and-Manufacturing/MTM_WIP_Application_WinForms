# Critical P5: Visual Requests Not Cancellable

## Summary

The Visual service contract and its call sites do not support cancellation.

## Why This Needs Fixing

- Closing a form or changing filters does not cancel the in-flight SQL work.
- Users can stack replacement queries on top of work they no longer need.
- This wastes server resources during slow or abandoned operations.

## Evidence

- `Services/Visual/IService_VisualDatabase.cs`
- `Forms/Visual/Form_InforVisualDashboard.cs:73,166,231,287`
- `Forms/Visual/Form_PODetails.cs:46,60,139`
- `Controls/Visual/Control_VisualInventory.cs:71,90`
- `Controls/Visual/Control_InventoryAudit.cs:235,253,283,333`
- `Controls/Visual/Control_DieToolDiscovery.cs:124,197,334`

## Risk

- SQL continues work after the UI no longer needs it.
- Operators generate more load by retrying while older requests are still running.

## Recommended Fix

1. Add `CancellationToken` parameters to `IService_VisualDatabase` methods.
2. Propagate tokens from forms and controls.
3. Cancel active tokens on form close, navigation change, or superseding request.

## Acceptance Criteria

- Forms and controls can cancel in-flight Visual requests.
- Superseded filter changes do not leave obsolete queries running.
- Closing a Visual form cancels its pending work cleanly.
