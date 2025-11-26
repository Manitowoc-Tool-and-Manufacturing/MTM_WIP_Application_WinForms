# Research: Refactor Shortcuts UI

## Decisions

### 1. UI Component Design
**Decision**: Create `Control_SettingsCollapsibleCard` inheriting from `ThemedUserControl`.
**Rationale**:
- Matches existing `Control_SettingsCategoryCard` style for consistency.
- Needs specific expand/collapse functionality not present in the base card.
- Will use a `TableLayoutPanel` or `FlowLayoutPanel` for the header (Title, Description, Icon, Toggle Button) and a separate container for the content.
- Animation can be achieved by toggling visibility or height, but simple visibility toggle is safer for WinForms performance.

### 2. QuickButton Handling
**Decision**: Explicitly register `QuickButton_01` through `QuickButton_10` in `Service_Shortcut` and `default_shortcuts.json`.
**Rationale**:
- Currently, `Control_QuickButtons` relies on implicit or hardcoded handling (or maybe none global?).
- Centralizing them in `Service_Shortcut` ensures they are reserved and user-configurable (if we allow remapping, but the requirement says "strictly reserve Alt+0 through Alt+9", implying they might be fixed or at least protected).
- The requirement "Enforce QuickButton exclusivity" means we must prevent *other* shortcuts from using these keys.

### 3. Data Persistence
**Decision**: Continue using `IDao_Shortcuts` and `Service_Shortcut`.
**Rationale**:
- Existing architecture is sound.
- `Service_Shortcut` already handles caching and user overrides.
- No need to change the database schema, just the data population (defaults).

### 4. Default Shortcuts
**Decision**: Update `default_shortcuts.json` to include all missing shortcuts found during audit.
**Rationale**:
- Ensures a complete baseline for all users.
- `Service_Shortcut` loads these if no user overrides exist.

## Alternatives Considered

### Alternative 1: Modify `Control_SettingsCategoryCard`
- **Idea**: Add collapse functionality to the existing card.
- **Rejected**: The existing card is designed for navigation links (`SubcategoryLink`). The new UI needs to host arbitrary controls (Shortcut rows). Mixing these concerns would make the control complex and brittle. Better to create a specialized `CollapsibleCard`.

### Alternative 2: Hardcode QuickButton Keys
- **Idea**: Keep QuickButton keys hardcoded in `MainForm` key preview.
- **Rejected**: Violates the "Centralized Shortcut Service" principle. All shortcuts should be known to the service to detect conflicts.

## Unknowns Resolved
- **`Control_SettingsCollapsibleCard`**: Will be a new control modeled on `Control_SettingsCategoryCard`.
- **`Service_Shortcut`**: Capable of handling the requirements with minor updates (registering new defaults).
- **`Control_QuickButtons`**: Needs update to register its shortcuts.
