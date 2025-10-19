-- =============================================
-- Procedure: md_locations_Exists_ByLocation
-- Domain: master-data
-- Created: 2025-10-17
-- Purpose: Checks if location exists in md_locations table
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `md_locations_Exists_ByLocation`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Exists_ByLocation`(
    IN p_Location VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Exists INT DEFAULT 0;
    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    
    -- Validate input
    IF p_Location IS NULL OR TRIM(p_Location) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Location is required';
    ELSE
        -- Check if location exists
        SELECT COUNT(*) INTO v_Exists
        FROM md_locations
        WHERE Location = p_Location;
        
        IF v_Exists > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" exists');
        ELSE
            SET p_Status = 0;
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" does not exist');
        END IF;
        
        -- Return existence value for ExecuteScalarAsync
        SELECT v_Exists;
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of md_locations_Exists_ByLocation
-- =============================================
