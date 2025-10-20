# Developer Tools Documentation

**Purpose**: This directory contains documentation, examples, and resources for the MTM WIP Application Developer Tools Suite.

## Directory Structure

- **Examples/** - Code samples and templates for using developer tools
- **Screenshots/** - UI screenshots for user guides and documentation
- **Workflows/** - Workflow diagrams and process documentation

## Developer Tools Suite

The Developer Tools Suite provides advanced debugging, schema inspection, and code generation capabilities for developers working on the MTM WIP Application.

### Available Tools

1. **Debug Dashboard** - Real-time stored procedure execution tracing
2. **Parameter Prefix Maintenance** - CRUD interface for parameter prefix overrides
3. **Schema Inspector** - Database schema browser with column/parameter details
4. **Procedure Call Hierarchy** - Visualize stored procedure dependencies
5. **Code Generator** - Generate DAO method boilerplate from stored procedures

### Access Requirements

All developer tools require:
- **Admin role** (assigned via sys_user_roles)
- **Developer role** (assigned via sys_user_roles)

Access through: Settings → Developer

### Documentation Resources

- [Quickstart Guide](../../specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/quickstart.md)
- [Feature Specification](../../specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/spec.md)
- [Implementation Plan](../../specs/002-003-database-layer-complete/002-003-001-developer-tools-suite/plan.md)

### Grant Developer Role

To enable developer tools for a user:

```sql
-- Execute Grant-Developer-Role.sql script
source Database/Scripts/Grant-Developer-Role.sql

-- Or manually:
INSERT IGNORE INTO sys_roles (RoleName, Description, IsSystem, CreatedBy, CreatedAt)
VALUES ('Developer', 'Developer access with debugging and schema tools', 1, '[ System ]', NOW());

INSERT IGNORE INTO sys_user_roles (UserId, RoleId, AssignedBy, AssignedAt)
SELECT u.ID, r.ID, 'SYSTEM', NOW()
FROM usr_users u
CROSS JOIN sys_roles r
WHERE u.User = 'YOUR_USERNAME' AND r.RoleName = 'Developer';
```

---

Last updated: 2025-10-18
