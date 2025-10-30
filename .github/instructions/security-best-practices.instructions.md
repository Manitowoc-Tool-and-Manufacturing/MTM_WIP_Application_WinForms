---
description: 'Security best practices for MTM application development'
applyTo: '**/*.cs,**/*.json'
---

<!-- Based on patterns from: https://github.com/github/awesome-copilot -->

# Security Best Practices

## Overview

This file defines security standards and best practices for the MTM WIP Application to protect against common vulnerabilities and ensure secure manufacturing operations.

## Relevant MCP Tools

- `check_security` – Primary static analysis pass; run after security-sensitive updates to flag SQL injection, hardcoded credentials, and other issues listed below.
- `analyze_stored_procedures` – Validate stored procedure signatures, error handling, and parameter naming when modifying database code referenced in this guide.
- `audit_database_cleanup` – Ensure temporary or test data created during security validation is removed from MySQL instances.
- `validate_error_handling` – Confirm Service_ErrorHandler usage replaces insecure MessageBox flows and that exception handling patterns remain consistent.
- `verify_ignore_files` – Double-check that secrets, logs, and generated artifacts remain excluded from source control when adding new security-related assets.

## Core Principles

### Defense in Depth
- Multiple layers of security controls
- Never rely on a single security measure
- Validate at every boundary
- Fail securely

### Least Privilege
- Grant minimum necessary permissions
- Use role-based access where applicable
- Separate credentials by environment
- Regular permission audits

### Secure by Default
- Security enabled out of the box
- Opt-in for less secure options
- Secure defaults in configuration
- Document security implications

## Input Validation

### Validate All User Input
- Never trust user input
- Validate data type, format, length, and range
- Use allowlists, not denylists
- Reject invalid input, don't sanitize

### Validation Pattern
```
public ServiceResult ValidatePartNumber(string partNumber)
{
    if (string.IsNullOrWhiteSpace(partNumber))
        return ServiceResult.Failure("Part number is required");

    if (partNumber.Length > 50)
        return ServiceResult.Failure("Part number exceeds maximum length");

    if (!Regex.IsMatch(partNumber, @"^[A-Z0-9-]+$"))
        return ServiceResult.Failure("Part number contains invalid characters");

    return ServiceResult.Success();
}
```

### WinForms UI Validation
- Use `ErrorProvider` or inline messaging to surface validation issues before database calls.
- Disable primary action buttons until required fields are valid.
- Keep validation logic in helpers or services so both UI and background workflows enforce rules consistently.
- Treat client-side validation as a UX aid; revalidate on the server boundary for security.

## SQL Injection Prevention

### Always Use Parameterized Queries
- Never concatenate user input into SQL strings
- Route every call through stored procedures
- The stored procedure helpers handle parameter binding safely
- Validate parameters before database calls

### Parameterized Query Example
```
// ✅ SECURE: Stored procedure invocation through helper
var parameters = new Dictionary<string, object>
{
  ["LocationCode"] = locationCode,
  ["IncludeInactive"] = includeInactive
};

var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatus(
  connectionString,
  "usp_GetInventoryByLocation",
  parameters,
  useAsync: true);

if (!result.IsSuccess)
{
  // Surface user-friendly message without exposing sensitive details
  return ServiceResult.Failure(result.StatusMessage);
}

// ❌ INSECURE: String concatenation exposes injection vector
var unsafeSql = $"SELECT * FROM Inventory WHERE LocationCode = '{locationCode}'"; // DON'T DO THIS
```

### Stored Procedure Security
- Use stored procedures for all database operations
- Pass parameters via Dictionary<string, object>
- Helper_Database_StoredProcedure handles parameter binding securely
- Never build dynamic SQL from user input inside stored procedures

## Authentication and Authorization

### User Session Management
- Session timeout after 60 minutes (configurable)
- Store minimal session data
- Invalidate sessions on logout
- No sensitive data in session storage

### Credential Management
- Never hardcode credentials
- Store passwords hashed (if implementing authentication)
- Use secure password requirements
- Implement account lockout after failed attempts

## Connection String Security

### Secure Storage
- Keep connection strings in configuration helpers (e.g., `Helper_Database_Variables`) or environment-specific config files
- Never commit connection strings with production credentials
- Use environment variables or secret stores (Key Vault, user secrets) for production
- Separate credentials for dev/test/prod

### Connection String Pattern
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=MTM_WIP_Application_Winforms;User=root;Password=root;SslMode=none;AllowPublicKeyRetrieval=true"
  }
}
```

### Production Considerations
- Use encrypted connections (SSL/TLS) in production
- Rotate credentials regularly
- Use service accounts with minimum privileges
- Monitor connection string access

## Logging Security

### Never Log Sensitive Data
- Don't log passwords or credentials
- Don't log full connection strings
- Don't log personal identifying information unnecessarily
- Don't log credit card numbers, SSN, etc.

### Secure Logging Pattern
```
// ✅ SAFE: Log action without sensitive data
LoggingUtility.LogApplicationInfo($"User {userId} logged in successfully");

// ❌ UNSAFE: Logging password
LoggingUtility.LogApplicationInfo($"User {userId} logged in with password {password}"); // DON'T DO THIS
```

### Log Sanitization
- Sanitize parameters before logging
- Redact sensitive fields
- Use structured logging carefully
- Review log output for sensitive data

## Error Handling Security

### Don't Expose Internal Details
- Generic error messages to users
- Detailed errors only in logs
- No stack traces to end users
- No database schema information in errors

### Error Message Pattern
```
// ✅ User-facing message
"An error occurred while saving inventory. Please try again."

// ✅ Log message (internal only)
LoggingUtility.LogApplicationError(ex);
```

### Exception Handling
- Catch specific exceptions
- Log full exception details securely
- Return user-friendly error messages
- Don't reveal sensitive information in exceptions

## Configuration Security

### Secure Configuration Management
- Separate configuration by environment
- Use secure storage for secrets (Azure Key Vault, user secrets)
- Never commit secrets to source control
- Use .gitignore for sensitive files

### Configuration File Security
```json
{
  // ✅ OK for development
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=mtm_test;User=root;Password=root"
  },

  // ❌ Never commit production credentials
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=mtm_prod;User=sa;Password=RealProdPassword123"
  }
}
```

## Dependency Security

### Package Management
- Keep NuGet packages up-to-date
- Review package dependencies regularly
- Use only trusted package sources
- Check for known vulnerabilities

### Dependency Scanning
- Use tools like `dotnet list package --vulnerable`
- Subscribe to security advisories
- Update packages promptly when vulnerabilities discovered
- Review release notes before updating

## Cross-Platform Security

### File System Security
- Use Path.Combine for cross-platform paths
- Validate file paths to prevent directory traversal
- Check file permissions before access
- Handle case-sensitive file systems

### Platform-Specific Security
- Test security controls on all platforms
- Handle platform-specific vulnerabilities
- Use platform-appropriate secure storage

## Data Protection

### Sensitive Data Handling
- Encrypt sensitive data at rest
- Use secure channels for data in transit
- Minimize sensitive data storage
- Securely dispose of sensitive data

### Memory Security
- Clear sensitive data from memory after use
- Use SecureString for passwords if needed
- Avoid string manipulation of sensitive data
- Implement proper disposal patterns

## Manufacturing Security

### Transaction Integrity
- Validate all transaction operations
- Ensure transaction atomicity
- Log all manufacturing transactions for audit
- Prevent unauthorized transactions

### Location and Operation Validation
- Validate location codes against allowed values
- Validate operations against ValidOperations configuration
- Ensure user authorization for operations
- Audit trail for all manufacturing operations

## Security Testing

### Security Test Scenarios
- Test with malicious input
- Test SQL injection attempts
- Test authorization boundaries
- Test error handling with sensitive data

### Security Review Checklist
- [ ] All user input validated
- [ ] No SQL injection vulnerabilities
- [ ] No hardcoded credentials
- [ ] No sensitive data in logs
- [ ] Error messages don't reveal internals
- [ ] Connection strings secured
- [ ] Dependencies up-to-date
- [ ] Authentication/authorization implemented correctly

## Incident Response

### Security Incident Handling
- Log security-relevant events
- Monitor for suspicious activity
- Have incident response plan
- Document security incidents

### Vulnerability Disclosure
- Responsible disclosure process
- Security contact information
- Patch management process
- Security advisories to users
