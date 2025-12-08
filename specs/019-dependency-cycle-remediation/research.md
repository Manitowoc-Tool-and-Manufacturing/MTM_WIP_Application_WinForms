# Research: Dependency Cycle Remediation

## Decisions

### 1. Orchestrator Pattern for Startup
**Decision**: Transform `Service_OnStartup_AppLifecycle` into a pure Orchestrator.
**Rationale**: This centralizes the control flow, making the startup sequence linear and easy to reason about. It eliminates the need for services to call each other, breaking the cycles.
**Alternatives Considered**:
- **Event Bus**: Services publish events. Too complex for a simple startup sequence.
- **Mediator Pattern**: Similar to Orchestrator but often implies more dynamic interaction. Orchestrator is simpler and sufficient here.

### 2. Static Helper for Log Configuration
**Decision**: Create a static `Helper_LogPath` class.
**Rationale**: The application heavily uses static helpers. Introducing a singleton or DI service for just this one path helper might be overkill and inconsistent with the existing codebase style (though DI is preferred for new components, this is a low-level helper). A static helper with no dependencies is safe and breaks the cycle.
**Alternatives Considered**:
- **Injecting Configuration**: Passing configuration to `LoggingUtility`. Good, but `LoggingUtility` is static.
- **Environment Variables**: Too implicit.

### 3. Startup Result Model
**Decision**: Introduce `Model_StartupResult`.
**Rationale**: Standardizes the return value of startup services, allowing the Orchestrator to make decisions (retry, abort, ignore) without the services needing to know about the Orchestrator.

## Unknowns Resolved

- **Hidden Dependencies**: Analysis of `Service_OnStartup_AppLifecycle` shows it primarily calls other services. The main risk is `Service_OnStartup_Database` calling back into it for error reporting. This will be resolved by returning errors instead.
- **LoggingUtility Dependencies**: It depends on `Helper_Database_Variables` for pathing and `Service_OnStartup_AppDataCleaner` for cleanup. Both will be removed/refactored.
