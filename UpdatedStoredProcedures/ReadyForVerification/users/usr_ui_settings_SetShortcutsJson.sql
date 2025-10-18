-- =============================================
-- Procedure: usr_ui_settings_SetShortcutsJson
-- Domain: users
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_ui_settings_SetShortcutsJson`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_SetShortcutsJson`(

    IN p_UserId VARCHAR(255),

    IN p_ShortcutsJson JSON,

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_RowCount INT DEFAULT 0;

    

    -- Exit handler for any SQL exception

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

        ROLLBACK;

    END;

    

    START TRANSACTION;

    

    -- Validate required parameters

    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'UserId is required';

        ROLLBACK;

    ELSEIF p_ShortcutsJson IS NULL THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'ShortcutsJson is required';

        ROLLBACK;

    ELSE

        -- Update shortcuts JSON

        UPDATE usr_ui_settings

        SET ShortcutsJson = p_ShortcutsJson

        WHERE UserId = p_UserId;

        

        -- Check if update was successful

        SET v_RowCount = ROW_COUNT();

        

        IF v_RowCount > 0 THEN

            SET p_Status = 1;  -- Success

            SET p_ErrorMsg = CONCAT('Shortcuts JSON saved for user "', p_UserId, '"');

            COMMIT;

        ELSE

            SET p_Status = -4;  -- Not found

            SET p_ErrorMsg = CONCAT('User "', p_UserId, '" not found in settings');

            ROLLBACK;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of usr_ui_settings_SetShortcutsJson
-- =============================================
