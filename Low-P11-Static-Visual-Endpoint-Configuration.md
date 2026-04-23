# Low P11: Static Visual Endpoint Configuration

## Summary

Visual server and database names come from app settings, while credentials are loaded dynamically.

## Why This Needs Fixing

- Endpoint changes require config deployment rather than runtime or per-user control.
- Operational flexibility is lower than the rest of the connection model suggests.

## Evidence

- `App.config`
- `Services/Visual/Service_VisualDatabase.cs:20-27`
- `Forms/MainForm/Classes/MainFormUserSettingsHelper.cs:25-35`

## Risk

- Configuration rigidity.
- Potential mismatch between user expectations and actual runtime behavior.

## Recommended Fix

1. Decide explicitly whether Visual endpoint settings should be static or runtime-configurable.
2. If runtime-configurable, load and validate server/database settings through the same settings path as credentials.
3. If static by design, document that clearly.

## Acceptance Criteria

- The intended configuration model is explicit and consistent.
- Endpoint behavior matches what the settings UX implies.
- Operational changes can be made through the intended path without ambiguity.
