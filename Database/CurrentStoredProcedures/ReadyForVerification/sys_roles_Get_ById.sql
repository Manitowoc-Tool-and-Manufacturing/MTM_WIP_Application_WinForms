DROP PROCEDURE IF EXISTS `sys_roles_Get_ById`;

DELIMITER $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `sys_roles_Get_ById`(
    IN p_ID INT,
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
    IF p_ID IS NULL OR p_ID <= 0 THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Valid role ID is required';
        ROLLBACK;
    ELSE
        -- Get role by ID
        SELECT * FROM sys_roles WHERE ID = p_ID;
        
        -- Check if data was returned
        SELECT FOUND_ROWS() INTO v_Count;
        
        IF v_Count > 0 THEN
            SET p_Status = 1;  -- Success with data
            SET p_ErrorMsg = CONCAT('Retrieved role ID ', p_ID);
        ELSE
            SET p_Status = 0;  -- Success but no data
            SET p_ErrorMsg = CONCAT('Role ID ', p_ID, ' not found');
        END IF;
        
        COMMIT;
    END IF;
END$$
DELIMITER ;