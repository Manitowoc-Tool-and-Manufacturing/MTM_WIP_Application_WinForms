DELIMITER //
DROP PROCEDURE IF EXISTS `maint_InsertMissingUserUiSettings`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_InsertMissingUserUiSettings`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    INSERT INTO `MTM_WIP_Application_Winforms`.usr_settings (UserId, SettingsJson, UpdatedAt)
    SELECT u.User, '{"Theme_Name": "Default"}', NOW()
    FROM `MTM_WIP_Application_Winforms`.usr_users u
    LEFT JOIN `MTM_WIP_Application_Winforms`.usr_settings s ON s.UserId = u.User
    WHERE s.UserId IS NULL;
    SET v_RowCount = ROW_COUNT();
    IF v_RowCount > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Inserted ', v_RowCount, ' missing user UI settings');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No missing user UI settings found';
    END IF;
END
//
DELIMITER ;
