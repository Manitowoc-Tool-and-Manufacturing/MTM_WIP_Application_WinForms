-- =============================================
-- Procedure: md_part_ids_Get_All
-- Domain: master-data
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `md_part_ids_Get_All`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_part_ids_Get_All`(

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

    

    -- Get all parts

    SELECT * FROM `md_part_ids`

    ORDER BY `PartID`;

    

    -- Check if data was returned

    SELECT FOUND_ROWS() INTO v_Count;

    

    IF v_Count > 0 THEN

        SET p_Status = 1;  -- Success with data

        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' part(s)');

    ELSE

        SET p_Status = 0;  -- Success but no data

        SET p_ErrorMsg = 'No parts found';

    END IF;

    

    COMMIT;

END
//

DELIMITER ;

-- =============================================
-- End of md_part_ids_Get_All
-- =============================================
