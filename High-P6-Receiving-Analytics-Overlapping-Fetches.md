# High P6: Receiving Analytics Overlapping Fetches

## Summary

`Control_ReceivingAnalytics` can start overlapping fetches through startup, checkbox, and filter events without single-flight protection.

## Why This Needs Fixing

- Multiple rapid filter changes can issue overlapping schedule queries.
- Older slower responses can overwrite newer UI state.
- This creates repeated expensive reads against the receiving schedule query path.

## Evidence

- `Controls/Visual/Control_ReceivingAnalytics.cs:101`
- `Controls/Visual/Control_ReceivingAnalytics.cs:135,138,139`
- `Controls/Visual/Control_ReceivingAnalytics.cs:142-155`
- `Controls/Visual/Control_ReceivingAnalytics.cs:158-161`
- `Controls/Visual/Control_ReceivingAnalytics.cs:247-296`

## Risk

- Duplicate schedule queries.
- Stale data winning the race to paint the UI.
- User retry behavior increases pressure further.

## Recommended Fix

1. Add single-flight behavior with a semaphore or request version token.
2. Ignore stale responses if a newer fetch has started.
3. Cancel or debounce filter-driven fetches where practical.

## Acceptance Criteria

- Only the newest fetch can update the UI.
- Rapid filter changes do not issue uncontrolled overlapping queries.
- Initial fire-and-forget load cannot race incorrectly against later user changes.
