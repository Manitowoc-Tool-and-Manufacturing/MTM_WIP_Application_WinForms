# Refactor Service Files

When refactoring Service files:

1.  **Direct Access Review**: Identify services that bypass DAOs for direct database access.
2.  **Keyword Quoting**: For services using inline SQL (e.g., Analytics, Migration), ensure all column names matching reserved keywords are enclosed in backticks.
3.  **Connection Management**: Verify that connections are obtained via the centralized configuration helper and are properly disposed of.
4.  **Infor Visual Separation**: Ensure that services interacting with the Infor Visual database use the appropriate SQL Server providers and do not mix logic with the MySQL WIP database.
