# Refactor MySQL Databases

When refactoring the database schema definition:

1.  **Isolation**: Ensure the script targets only the WIP application database. Do not include commands that could affect the Infor Visual database.
2.  **Idempotency**: Structure creation scripts to handle existing objects gracefully (e.g., using `DROP IF EXISTS` for clean builds or `IF NOT EXISTS` for incremental updates).
3.  **Defaults**: Define default character sets and collations at the database level to ensure consistency for new tables.
