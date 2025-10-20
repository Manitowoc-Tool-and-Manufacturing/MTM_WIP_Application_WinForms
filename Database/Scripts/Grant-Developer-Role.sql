-- Grant Developer Role Script
-- Purpose: Grant JOHNK user Developer privileges for accessing developer tools
-- Created: 2025-10-18
-- Database: mtm_wip_application_winforms_test

USE mtm_wip_application_winforms_test;

-- Step 1: Create Developer role if it doesn't exist
INSERT IGNORE INTO sys_roles (RoleName, Description, IsSystem, CreatedBy, CreatedAt)
VALUES ('Developer', 'Developer access with debugging and schema tools', 1, '[ System ]', NOW());

-- Step 2: Get the Developer role ID
SET @DeveloperRoleId = (SELECT ID FROM sys_roles WHERE RoleName = 'Developer');
SET @JohnkUserId = (SELECT ID FROM usr_users WHERE User = 'JOHNK');

-- Step 3: Assign Developer role to JOHNK (if not already assigned)
INSERT IGNORE INTO sys_user_roles (UserId, RoleId, AssignedBy, AssignedAt)
VALUES (@JohnkUserId, @DeveloperRoleId, 'SYSTEM', NOW());

-- Step 4: Verify the assignment
SELECT 
    u.User AS UserName,
    GROUP_CONCAT(r.RoleName ORDER BY r.RoleName SEPARATOR ', ') AS Roles,
    CASE 
        WHEN GROUP_CONCAT(r.RoleName) LIKE '%Admin%' AND GROUP_CONCAT(r.RoleName) LIKE '%Developer%' 
        THEN 'Full Developer Access'
        WHEN GROUP_CONCAT(r.RoleName) LIKE '%Developer%' 
        THEN 'Developer Access'
        WHEN GROUP_CONCAT(r.RoleName) LIKE '%Admin%' 
        THEN 'Admin Access'
        ELSE 'Standard Access'
    END AS AccessLevel
FROM usr_users u
LEFT JOIN sys_user_roles ur ON u.ID = ur.UserId
LEFT JOIN sys_roles r ON ur.RoleId = r.ID
WHERE u.User = 'JOHNK'
GROUP BY u.User;

-- Expected output: JOHNK | Admin, Developer, User | Full Developer Access
