# Refactor Code Behind Files

When refactoring Form or Control code-behind files:

1.  **Separation of Concerns**: Remove any direct database connection or query logic. Delegate data retrieval to Services or DAOs.
2.  **Error Handling**: Replace generic message boxes with the application's centralized error handling service.
3.  **Threading**: Ensure data loading operations are performed asynchronously to prevent UI blocking.
4.  **Resource Management**: Verify that any disposable resources are properly cleaned up.
