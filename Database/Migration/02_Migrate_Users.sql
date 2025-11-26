-- 02_Migrate_Users.sql
-- Migrates users from mtm_wip_application to mtm_wip_application_winforms

INSERT IGNORE INTO mtm_wip_application_winforms.usr_users (User, `Full Name`, Shift, VitsUser, Pin, LastShownVersion, HideChangeLog)
SELECT User, `Full Name`, Shift, VitsUser, Pin, LastShownVersion, HideChangeLog 
FROM mtm_wip_application.usr_users;

-- Note: Settings migration is complex and handled by application logic or separate script if needed.
-- This script focuses on the core user record.

INSERT IGNORE INTO mtm_wip_application_winforms.sys_roles (RoleName, Description, Permissions, IsSystem, CreatedBy, CreatedAt)
SELECT RoleName, Description, Permissions, IsSystem, CreatedBy, CreatedAt
FROM mtm_wip_application.sys_roles;

INSERT IGNORE INTO mtm_wip_application_winforms.sys_user_roles (UserID, RoleID, AssignedBy, AssignedAt)
SELECT UserID, RoleID, AssignedBy, AssignedAt
FROM mtm_wip_application.sys_user_roles;
