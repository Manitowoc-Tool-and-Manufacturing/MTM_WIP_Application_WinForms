# Refactor Designer Files

When refactoring Windows Forms Designer files:

1.  **Control Inheritance**: Ensure custom controls inherit from the application's themed base classes rather than standard .NET forms or controls.
2.  **Naming Conventions**: Verify that control names follow the project's specific naming schema (e.g., `FormName_ControlType_Description`).
3.  **Event Wiring**: Ensure event handlers are wired up correctly and no orphaned events exist.
