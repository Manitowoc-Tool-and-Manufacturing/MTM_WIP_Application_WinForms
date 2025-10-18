-- =============================================
-- Procedure: usr_ui_settings_Get
-- Domain: users
-- Extracted: 2025-10-17 20:49:21
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_ui_settings_Get`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_Get`(

    IN p_UserId VARCHAR(64),

    OUT p_Status INT,

    OUT p_ErrorMsg VARCHAR(500)

)
BEGIN

    DECLARE v_Count INT DEFAULT 0;

    

    -- Exit handler for any SQL exception

    DECLARE EXIT HANDLER FOR SQLEXCEPTION

    BEGIN

        GET DIAGNOSTICS CONDITION 1

            p_ErrorMsg = MESSAGE_TEXT;

        SET p_Status = -1;

        ROLLBACK;

    END;

    

    START TRANSACTION;

    

    -- Validate parameter

    IF p_UserId IS NULL OR TRIM(p_UserId) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'UserId is required';

        ROLLBACK;

    ELSE

        -- Get UI settings for user

        SELECT SettingsJson

        FROM usr_ui_settings

        WHERE UserId = p_UserId;

        

        -- Check if data was returned

        SELECT FOUND_ROWS() INTO v_Count;

        

        IF v_Count > 0 THEN

            SET p_Status = 1;  -- Success with data

            SET p_ErrorMsg = CONCAT('Retrieved settings for user "', p_UserId, '"');

        ELSE

            SET p_Status = 0;  -- Success but no data

            SET p_ErrorMsg = CONCAT('No settings found for user "', p_UserId, '"');

        END IF;

        

        COMMIT;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of usr_ui_settings_Get
-- =============================================
