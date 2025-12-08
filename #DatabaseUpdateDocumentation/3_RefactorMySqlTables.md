# Refactor MySQL Tables

When refactoring MySQL table definitions:

1.  **Column Naming**: Check all column names against the reserved keyword list for the target MySQL version. Rename or quote (backtick) any conflicting names in the definition.
2.  **Data Types**: Review data types for compatibility and efficiency. Ensure `JSON` types are handled correctly if used.
3.  **Storage Engine**: Confirm the storage engine is explicitly defined (usually InnoDB).
4.  **Encoding**: Verify the table default character set and collation are set to `utf8mb4`.
