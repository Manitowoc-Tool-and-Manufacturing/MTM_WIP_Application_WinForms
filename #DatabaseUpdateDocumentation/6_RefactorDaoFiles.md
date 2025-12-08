# Refactor DAO Files

When refactoring Data Access Object (DAO) files:

1.  **Pattern Adherence**: Verify the class follows the project's standard DAO pattern, utilizing the centralized stored procedure helper.
2.  **Stored Procedure Usage**: Ensure no inline SQL is present. All data access must be performed via stored procedures.
3.  **Return Types**: Refactor methods to return the standardized result wrapper generic type used throughout the application.
4.  **Async/Await**: Convert any synchronous database calls to their asynchronous equivalents.
5.  **Parameter Handling**: Ensure parameters are passed as dictionaries or objects that the helper can map to stored procedure parameters.
