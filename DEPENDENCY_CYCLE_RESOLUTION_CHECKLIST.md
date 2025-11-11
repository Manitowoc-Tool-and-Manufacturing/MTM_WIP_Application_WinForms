# Dependency Cycle Resolution Checklist

This checklist provides a systematic approach to resolving the 6 dependency cycles identified in the codebase. Tasks are organized by priority based on impact analysis.

## Phase 1: Analysis & Preparation

- [ ] Examine `LoggingUtility` class to identify all outgoing dependencies
- [ ] Examine `Service_OnStartup_AppDataCleaner` to understand its logging usage
- [ ] Examine `Helper_Database_Variables` to understand its logging usage
- [ ] Examine `Core_Themes` class to identify theme-related dependencies
- [ ] Examine `ColumnOrderDialog` to understand its theme usage
- [ ] Examine `BaseIntegrationTest` and `ProcedureDiagnosticScope` circular dependency
- [ ] Document current usage patterns for each component in cycles
- [ ] Generate performance improvement report (MD/Mermaid format) showing:
  - [ ] Current dependency cycle metrics (6 cycles, complexity 3.0, hotspots)
  - [ ] Expected metrics after each phase completion
  - [ ] Mermaid dependency graphs showing before/after architecture
  - [ ] Impact analysis: compile time, maintainability score, testability improvements
  - [ ] Risk assessment for each refactoring phase

## Phase 2: LoggingUtility Refactoring (Highest Priority - Breaks 3 Cycles)

**Target Cycles**: Cycle 1, Cycle 2, Cycle 5

- [ ] Extract logging interface (`ILoggingService`) to break direct dependencies
- [ ] Implement dependency injection pattern for `LoggingUtility`
- [ ] Refactor `Service_OnStartup_AppDataCleaner` to use `ILoggingService` interface
- [ ] Refactor `Helper_Database_Variables` to use `ILoggingService` interface
- [ ] Remove circular references where `LoggingUtility` depends on classes that depend on it
- [ ] Consider implementing async logging to reduce coupling
- [ ] Add unit tests for refactored logging interactions
- [ ] Verify Cycle 1, 2, and 5 are resolved

## Phase 3: Core_Themes & ColumnOrderDialog Refactoring (Breaks 2 Cycles Each)

**Target Cycles**: Cycle 3, Cycle 4

- [ ] Extract theme interface (`IThemeProvider`) from `Core_Themes`
- [ ] Identify why `ColumnOrderDialog` creates dependency back to `Core_Themes`
- [ ] Refactor `ColumnOrderDialog` to receive theme data via constructor/properties
- [ ] Consider implementing Observer pattern for theme changes
- [ ] Move theme application logic out of dialog controls
- [ ] Add unit tests for theme application without circular dependencies
- [ ] Verify Cycle 3 and 4 are resolved

## Phase 4: Helper_Database_Variables Refactoring (Breaks 2 Cycles)

**Target Cycles**: Cycle 2, Cycle 5 (overlaps with Phase 2)

- [ ] Review why `Helper_Database_Variables` needs `LoggingUtility`
- [ ] Apply interface-based dependency injection for logging (completed in Phase 2)
- [ ] Extract database configuration into separate immutable configuration class
- [ ] Remove any static dependencies that create circular references
- [ ] Add unit tests for database variable handling
- [ ] Verify no remaining cycles involving this component

## Phase 5: Test Infrastructure Refactoring (Breaks 1 Cycle)

**Target Cycle**: Cycle 6

- [ ] Examine `BaseIntegrationTest` and `ProcedureDiagnosticScope` relationship
- [ ] Extract diagnostic interface to break test dependency cycle
- [ ] Refactor `ProcedureDiagnosticScope` to not depend on `BaseIntegrationTest`
- [ ] Consider moving diagnostic scope to a shared test utilities namespace
- [ ] Add unit tests for diagnostic scope without base test dependencies
- [ ] Verify Cycle 6 is resolved

## Phase 6: Service_OnStartup_AppDataCleaner Refactoring (Breaks 1 Cycle)

**Target Cycle**: Cycle 1 (overlaps with Phase 2)

- [ ] Review `Service_OnStartup_AppDataCleaner` dependencies on `LoggingUtility`
- [ ] Apply logging interface refactoring from Phase 2 (already completed)
- [ ] Verify no other circular dependencies exist
- [ ] Add unit tests for startup service
- [ ] Verify Cycle 1 fully resolved

## Phase 7: Architectural Improvements

- [ ] Implement Dependency Injection container for the application
- [ ] Define clear architectural layers (UI → Services → Data → Core)
- [ ] Enforce dependency direction rules (layers can only depend downward)
- [ ] Create architecture decision records (ADRs) for refactoring choices
- [ ] Update project documentation with new dependency guidelines
- [ ] Add code comments explaining dependency injection patterns

## Phase 8: Validation & Testing

- [ ] Re-run dependency cycle analysis tool to verify all 6 cycles are broken
- [ ] Confirm cycle count is 0 in new analysis report
- [ ] Run all unit tests to ensure no regressions
- [ ] Run all integration tests to ensure functionality preserved
- [ ] Perform manual testing of affected features:
  - [ ] Logging functionality across all modules
  - [ ] Theme application in dialogs
  - [ ] Database variable access
  - [ ] Startup services
  - [ ] Test infrastructure
- [ ] Update class dependency graph documentation
- [ ] Verify no new cycles were introduced during refactoring

## Phase 9: Code Quality & Documentation

- [ ] Add XML documentation to new interfaces and refactored classes
- [ ] Update architecture documentation (BROWNFIELD_ARCHITECTURE.md)
- [ ] Create developer guide for avoiding future circular dependencies
- [ ] Add static analysis rules to detect circular dependencies in CI/CD
- [ ] Document the refactoring decisions in release notes
- [ ] Add code review checklist item for dependency validation

## Optional Enhancement Tasks

- [ ] Consider implementing Event Aggregator pattern for cross-cutting concerns
- [ ] Evaluate using Mediator pattern for complex inter-component communication
- [ ] Review and consolidate logging strategies across the application
- [ ] Consider extracting theme management into a dedicated service layer
- [ ] Evaluate applying SOLID principles more rigorously across codebase

---

## Summary of Impact

| Phase | Cycles Broken | Components Affected |
|-------|---------------|---------------------|
| Phase 2 | 3 | LoggingUtility, Service_OnStartup_AppDataCleaner, Helper_Database_Variables |
| Phase 3 | 2 | Core_Themes, ColumnOrderDialog |
| Phase 4 | 0 (overlap) | Helper_Database_Variables |
| Phase 5 | 1 | BaseIntegrationTest, ProcedureDiagnosticScope |
| Phase 6 | 0 (overlap) | Service_OnStartup_AppDataCleaner |
| **Total** | **6** | **7 unique components** |

## Notes

- Phases 2 and 4 overlap because `Helper_Database_Variables` participates in cycles already addressed by logging refactoring
- Phases 2 and 6 overlap because `Service_OnStartup_AppDataCleaner` is resolved through logging interface extraction
- Prioritization follows the "Suggested Break Points" from the analysis report
- Each phase should be completed and validated before proceeding to the next
- Consider creating feature branches for each phase to allow incremental code review
