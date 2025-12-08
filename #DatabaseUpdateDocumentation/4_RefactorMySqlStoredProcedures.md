# Refactor MySQL Stored Procedures

When refactoring MySQL stored procedures for this project, follow these guidelines:

1.  **Reserved Keywords**: Scan the procedure body for any words that have become reserved keywords in target MySQL versions (e.g., 8.0). Enclose all such identifiers (column names, aliases, variables) in backticks.
2.  **Implicit Sorting**: Identify any `GROUP BY` clauses that rely on implicit sorting. Add an explicit `ORDER BY` clause to ensure deterministic results.
3.  **Deprecated Features**: Replace deprecated functions or patterns (such as `SQL_CALC_FOUND_ROWS`) with modern equivalents (e.g., separate `COUNT(*)` queries).
4.  **Character Sets**: Confirm that character set and collation definitions are explicit and compatible with `utf8mb4`.
5.  **Standard Output**: Ensure the procedure maintains the standard output parameters for status and error messages used by the application's data layer.
