DROP PROCEDURE IF EXISTS `md_locations_Add_Location`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Add_Location`(
    IN p_Location VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
    IN p_Building VARCHAR(100),
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
    IF p_Location IS NULL OR TRIM(p_Location) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Location is required';
        ROLLBACK;
    ELSEIF p_IssuedBy IS NULL OR TRIM(p_IssuedBy) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'IssuedBy is required';
        ROLLBACK;
    ELSEIF p_Building IS NULL OR TRIM(p_Building) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Building is required';
        ROLLBACK;
    ELSE
        -- Insert new location
        INSERT INTO `md_locations` (`Location`, `Building`, `IssuedBy`)
        VALUES (p_Location, p_Building, p_IssuedBy);
        
        -- Check if insert was successful
        SET v_RowCount = ROW_COUNT();
        
        IF v_RowCount > 0 THEN
            SET p_Status = 1;  -- Success
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" added successfully');
            COMMIT;
        ELSE
            SET p_Status = -3;  -- Business logic error
            SET p_ErrorMsg = 'Failed to add location';
            ROLLBACK;
        END IF;
    END IF;
END$$
DELIMITER ;