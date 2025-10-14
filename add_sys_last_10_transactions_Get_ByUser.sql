-- ================================================================================
-- MTM INVENTORY APPLICATION - Quick Button Data Retrieval Procedure
-- ================================================================================
-- File: add_sys_last_10_transactions_Get_ByUser.sql
-- Purpose: Create the sys_last_10_transactions_Get_ByUser stored procedure
-- Usage: Run this script against your mtm_wip_application database
-- MySQL Version: 5.7.24+ (MAMP Compatible)
-- ================================================================================

-- Switch to your database
USE mtm_wip_application;

-- Drop existing procedure if it exists
DROP PROCEDURE IF EXISTS sys_last_10_transactions_Get_ByUser;

-- Create the stored procedure with status reporting
DELIMITER $$
CREATE PROCEDURE sys_last_10_transactions_Get_ByUser(
    IN p_User VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(255)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Error handler for SQL exceptions
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Database error occurred while retrieving quick buttons for user: ', p_User);
    END;
    
    -- Count the number of quick buttons for the user
    SELECT COUNT(*) INTO v_Count FROM sys_last_10_transactions WHERE User = p_User;
    
    -- Return the quick button data ordered by position
    SELECT 
        Position,
        User,
        PartID AS p_PartID,          -- Aliased to match C# code expectations
        Operation AS p_Operation,    -- Aliased to match C# code expectations
        Quantity,
        ReceiveDate
    FROM sys_last_10_transactions 
    WHERE User = p_User 
    ORDER BY Position;
    
    -- Set success status
    SET p_Status = 0;
    SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' quick buttons for user: ', p_User);
END $$
DELIMITER ;

-- ================================================================================
-- VERIFICATION QUERIES
-- ================================================================================

-- Verify the procedure was created successfully
SELECT 
    ROUTINE_NAME,
    ROUTINE_TYPE,
    DEFINER,
    CREATED,
    LAST_ALTERED
FROM information_schema.ROUTINES 
WHERE ROUTINE_SCHEMA = 'mtm_wip_application' 
  AND ROUTINE_NAME = 'sys_last_10_transactions_Get_ByUser';

-- Test the procedure with a sample user (optional)
-- CALL sys_last_10_transactions_Get_ByUser('YOUR_USERNAME', @status, @msg);
-- SELECT @status AS Status, @msg AS Message;

-- ================================================================================
-- END OF SCRIPT
-- ================================================================================
