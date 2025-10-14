namespace MTM_Inventory_Application.Models;

/// <summary>
/// Defines the severity levels for database errors in the MTM Inventory Application.
/// Used to classify database operation failures and determine appropriate handling.
/// </summary>
public enum DatabaseErrorSeverity
{
    /// <summary>
    /// Warning - Unexpected but handled situation, operation may have degraded functionality.
    /// Examples: Slow query performance, non-critical validation warnings, deprecated feature usage.
    /// </summary>
    Warning = 0,
    
    /// <summary>
    /// Error - Operation failed but system integrity maintained, user can retry or use alternative approach.
    /// Examples: Failed insert due to business rules, connection timeout, query returned no results unexpectedly.
    /// </summary>
    Error = 1,
    
    /// <summary>
    /// Critical - Data integrity risk or major system functionality compromised, immediate attention required.
    /// Examples: Transaction rollback failure, data corruption detected, foreign key constraint violated, deadlock.
    /// </summary>
    Critical = 2
}
