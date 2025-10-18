-- =============================================
-- Procedure: md_operation_numbers_Add_Operation
-- Domain: master-data
-- Extracted: 2025-10-17 20:49:20
-- Source: mtm_wip_application on localhost:3306
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `md_operation_numbers_Add_Operation`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `md_operation_numbers_Add_Operation`(

    IN p_Operation VARCHAR(100),

    IN p_IssuedBy VARCHAR(100),

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

    IF p_Operation IS NULL OR TRIM(p_Operation) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'Operation is required';

        ROLLBACK;

    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN

        SET p_Status = -2;

        SET p_ErrorMsg = 'IssuedBy is required';

        ROLLBACK;

    ELSE

        -- Insert new operation

        INSERT INTO `md_operation_numbers` (`Operation`, `IssuedBy`)

        VALUES (p_Operation, p_IssuedBy);

        

        -- Check if insert was successful

        SET v_RowCount = ROW_COUNT();

        

        IF v_RowCount > 0 THEN

            SET p_Status = 1;  -- Success

            SET p_ErrorMsg = CONCAT('Operation "', p_Operation, '" added successfully');

            COMMIT;

        ELSE

            SET p_Status = -3;  -- Business logic error

            SET p_ErrorMsg = 'Failed to add operation';

            ROLLBACK;

        END IF;

    END IF;

END
//

DELIMITER ;

-- =============================================
-- End of md_operation_numbers_Add_Operation
-- =============================================
