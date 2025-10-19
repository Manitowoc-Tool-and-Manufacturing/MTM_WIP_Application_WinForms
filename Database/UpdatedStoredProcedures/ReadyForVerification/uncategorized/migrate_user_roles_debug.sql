DELIMITER //
DROP PROCEDURE IF EXISTS `migrate_user_roles_debug`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `migrate_user_roles_debug`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE v_old_username VARCHAR(100);
    DECLARE v_new_userid INT;
    DECLARE v_roleid INT;
    DECLARE v_rolename VARCHAR(50);
    DECLARE v_existing_roleid INT;
    DECLARE v_ProcessedCount INT DEFAULT 0;
    DECLARE v_ErrorCount INT DEFAULT 0;
    DECLARE user_cur CURSOR FOR
        SELECT User FROM `mtm database`.users;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    OPEN user_cur;
    read_loop: LOOP
        FETCH user_cur INTO v_old_username;
        IF done THEN
            LEAVE read_loop;
        END IF;
        INSERT INTO migration_debug_log (message) VALUES (CONCAT('Processing user: ', v_old_username));
        SET v_new_userid = NULL;
        SELECT ID INTO v_new_userid
        FROM mtm_wip_application.usr_users
        WHERE User = v_old_username
        LIMIT 1;
        IF v_new_userid IS NULL THEN
            INSERT INTO migration_debug_log (message) VALUES (CONCAT('User NOT found in new system: ', v_old_username));
            SET v_ErrorCount = v_ErrorCount + 1;
            ITERATE read_loop;
        END IF;
        IF EXISTS (SELECT 1 FROM `mtm database`.leads WHERE USER = v_old_username) THEN
            SET v_rolename = 'Admin';
        ELSEIF EXISTS (SELECT 1 FROM `mtm database`.readonly WHERE USER = v_old_username) THEN
            SET v_rolename = 'ReadOnly';
        ELSE
            SET v_rolename = 'User';
        END IF;
        INSERT INTO migration_debug_log (message) VALUES (CONCAT('Assigned role: ', v_rolename));
        SET v_roleid = NULL;
        SELECT ID INTO v_roleid
        FROM mtm_wip_application.sys_roles
        WHERE RoleName = v_rolename
        LIMIT 1;
        IF v_roleid IS NULL THEN
            INSERT INTO migration_debug_log (message) VALUES (CONCAT('Role NOT found: ', v_rolename));
            SET v_ErrorCount = v_ErrorCount + 1;
            ITERATE read_loop;
        END IF;
        SET v_existing_roleid = NULL;
        SELECT RoleID INTO v_existing_roleid
        FROM mtm_wip_application.sys_user_roles
        WHERE UserID = v_new_userid
        LIMIT 1;
        IF v_existing_roleid IS NOT NULL THEN
            IF v_existing_roleid = v_roleid THEN
                INSERT INTO migration_debug_log (message) VALUES (CONCAT('User-role already exists for: ', v_old_username, ' | Role: ', v_rolename));
            ELSE
                UPDATE mtm_wip_application.sys_user_roles
                SET RoleID = v_roleid, AssignedBy = '[ System Migration ]', AssignedAt = NOW()
                WHERE UserID = v_new_userid;
                INSERT INTO migration_debug_log (message) VALUES (CONCAT('User-role updated for: ', v_old_username, ' to role: ', v_rolename));
                SET v_ProcessedCount = v_ProcessedCount + 1;
            END IF;
        ELSE
            INSERT INTO mtm_wip_application.sys_user_roles
                (UserID, RoleID, AssignedBy, AssignedAt)
            VALUES
                (v_new_userid, v_roleid, '[ System Migration ]', NOW());
            INSERT INTO migration_debug_log (message) VALUES (CONCAT('Migrated user: ', v_old_username, ' with role: ', v_rolename));
            SET v_ProcessedCount = v_ProcessedCount + 1;
        END IF;
    END LOOP;
    CLOSE user_cur;
    IF v_ProcessedCount > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Processed ', v_ProcessedCount, ' user(s), ', v_ErrorCount, ' error(s)');
    ELSEIF v_ErrorCount > 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = CONCAT('No users processed, ', v_ErrorCount, ' error(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No users needed migration';
    END IF;
END
//
DELIMITER ;
