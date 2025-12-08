# Startup Service Contracts

Since the application uses static services, these "contracts" define the expected public method signatures for the refactored services.

## Service_OnStartup_Database

```csharp
public static class Service_OnStartup_Database
{
    /// <summary>
    /// Validates database connectivity.
    /// </summary>
    /// <returns>A Model_StartupResult indicating success or failure.</returns>
    public static Model_StartupResult ValidateConnectivity();

    /// <summary>
    /// Initializes the parameter cache.
    /// </summary>
    /// <returns>A Model_StartupResult indicating success or failure.</returns>
    public static Model_StartupResult InitializeParameterCache();
}
```

## Service_OnStartup_User

```csharp
public static class Service_OnStartup_User
{
    /// <summary>
    /// Validates the current user.
    /// </summary>
    /// <returns>A Model_StartupResult indicating success or failure.</returns>
    public static Model_StartupResult ValidateUser();
}
```

## Service_OnStartup_AppLifecycle (Orchestrator)

```csharp
public static class Service_OnStartup_AppLifecycle
{
    /// <summary>
    /// Runs the application startup sequence.
    /// </summary>
    public static void RunApplication();
}
```
