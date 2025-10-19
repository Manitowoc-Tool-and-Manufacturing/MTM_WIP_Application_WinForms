-- =============================================
-- Procedure: usr_ui_settings_Delete_ByUserId
-- Domain: users
-- Created: 2025-10-17
-- Purpose: Deletes all UI settings for a user
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `usr_ui_settings_Delete_ByUserId`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `usr_ui_settings_Delete_ByUserId`(
    IN p_UserId INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowsAffected INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    
    START TRANSACTION;
    
    -- Validate input
    IF p_UserId IS NULL OR p_UserId <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid UserId is required';
        ROLLBACK;
    ELSE
        -- Delete all UI settings for user
        DELETE FROM usr_ui_settings
        WHERE UserId = p_UserId;
        
        SET v_RowsAffected = ROW_COUNT();
        
        IF v_RowsAffected > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Deleted ', v_RowsAffected, ' UI setting(s) for user ', p_UserId);
            COMMIT;
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('No UI settings found for user ', p_UserId);
            COMMIT;
        END IF;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of usr_ui_settings_Delete_ByUserId
-- =============================================
