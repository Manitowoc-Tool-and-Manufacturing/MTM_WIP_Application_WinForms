# Research & Architecture Decisions: Infor Visual Dashboard

**Feature**: Infor Visual Dashboard (014-visual-dashboard)
**Date**: 2025-11-26

## 1. Database Connectivity Strategy

### Decision
Use `System.Data.SqlClient` (or `Microsoft.Data.SqlClient` if compatible with .NET 8 patterns in this repo) to connect to the Infor Visual SQL Server.

### Rationale
- The application currently uses `MySql.Data` for its own data.
- Infor Visual is on SQL Server.
- A separate connection stack is required.
- **Constraint**: The connection must be Read-Only.
- **Security**: Credentials (`VisualUsername`, `VisualPassword`) are stored in the application database (`usr_users` table) as part of the user's profile, not hardcoded or in local configuration files.

### Alternatives Considered
- **ODBC**: Slower, requires machine configuration. Rejected.
- **Linked Server (MySQL -> SQL Server)**: Too complex to maintain, security risks. Rejected.
- **Web API Middleware**: Would be cleaner architecture, but out of scope for this WinForms application. Direct connection chosen for simplicity and speed.

## 2. SQL Management Strategy

### Decision
Use **Embedded Resources** (`.sql` files) for all Infor Visual queries.

### Rationale
- **Constitution Violation**: The Constitution mandates Stored Procedures.
- **Exception Justification**: We cannot create Stored Procedures in the vendor's ERP database (Infor Visual). We must run raw SQL.
- **Mitigation**: To adhere to the *spirit* of the constitution (no inline strings), we will store SQL in `.sql` files embedded in the assembly and load them at runtime. This prevents "magic strings" in C# code and allows for easier editing.

## 3. UI Architecture

### Decision
Create a new `InforVisualDashboard` form inheriting from `ThemedForm`.
Refactor "Empty State" logic into `Control_EmptyState`.

### Rationale
- **Consistency**: Must inherit `ThemedForm` to participate in the theming system.
- **Modernization**: The spec calls for a "modern" look. We will achieve this via custom painting or flat styles within the `ThemedForm` constraints.
- **Reuse**: The "Nothing Found" image/logic is currently duplicated or tightly coupled in `Control_AdvancedRemove`. Extracting this to `Control_EmptyState` (UserControl) allows reuse in the new dashboard and cleans up existing code.

## 4. Data Layer Pattern

### Decision
Implement `Service_VisualDatabase` instead of a standard DAO.

### Rationale
- Standard DAOs in this project are static and tightly coupled to `Helper_Database_StoredProcedure` (MySQL).
- This feature requires SQL Server and instance-based connections (using specific user credentials).
- A Service pattern (`Service_VisualDatabase`) fits better than a DAO pattern here, as it manages the connection lifecycle and credential injection.
- **Return Type**: It MUST still return `Model_Dao_Result<T>` to maintain compatibility with the rest of the application's error handling and flow control.

## 5. Dependency Management

### Decision
Add `System.Data.SqlClient` package.

### Rationale
- Required for SQL Server connectivity.
- Confirmed by Spec Q&A.

## 6. AI Integration Strategy

### Decision
Create `[Category].instruction.md` and `[Category].prompt.md` pairs.

### Rationale
- Ensures that future modifications to the SQL queries are handled by AI with strict context.
- Acts as documentation for the table schemas which we cannot control.
