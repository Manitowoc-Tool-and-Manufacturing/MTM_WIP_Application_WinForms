# Clarification Questions: Startup Process

## Question 1: User Access Failure Fallback

**Context**:
Currently, if `Dao_System.System_UserAccessTypeAsync` fails (e.g., DB error or timeout), the code has a fallback that grants **Developer** access in some paths (to prevent lockup) or **Normal** access in others.

**Question**:
What should be the strict behavior if User Permissions cannot be verified during startup?

**Options**:
A. **Exit Application** (Safest): Show error and close. User cannot use app without verified permissions.
B. **ReadOnly Mode**: Load app but disable all editing features.
C. **Normal User Mode**: Default to standard permissions (Current behavior in some paths).
D. **Developer Mode**: Default to full access (Current fallback in `Dao_System` - **Security Risk**).

B

## Question 2: Database Timeout Duration

**Context**:
The startup process waits for database connectivity.

**Question**:
How long should the splash screen wait for a database connection before timing out?

**Options**:
A. **10 Seconds** (Quick fail)
B. **30 Seconds** (Standard)
C. **60 Seconds** (Patient)

**Recommendation**:
Option A (10s) for initial check, maybe retry once.
