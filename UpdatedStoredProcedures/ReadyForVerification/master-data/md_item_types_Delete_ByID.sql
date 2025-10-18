-- =============================================
-- Procedure: md_item_types_Delete_ByID
-- Domain: master-data
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `md_item_types_Delete_ByID`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Delete_ByID`(

    IN p_ID INT,

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

    

    -- Validate required parameter

    IF p_ID IS NULL OR p_ID <= 0 THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Valid ID is required';

        ROLLBACK;

    ELSE

        -- Delete item type

        DELETE FROM `md_item_types`

        WHERE `ID` = p_ID;

        

        -- Check if delete was successful

        SET v_RowCount = ROW_COUNT();

        

        IF v_RowCount > 0 THEN

            SET p_Status = 1;  -- Success

            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' deleted successfully');

            COMMIT;

        ELSE

            SET p_Status = -4;  -- Not found

            SET p_ErrorMsg = CONCAT('Item type ID ', p_ID, ' not found');

            ROLLBACK;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of md_item_types_Delete_ByID
-- =============================================
