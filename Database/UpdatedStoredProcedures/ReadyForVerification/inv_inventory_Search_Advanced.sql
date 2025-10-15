-- ============================================================================
-- Procedure: inv_inventory_Search_Advanced
-- Description: Advanced search for inventory with multiple optional filters
-- Parameters:
--   IN p_PartID VARCHAR(300)      - Part ID filter (optional)
--   IN p_Operation VARCHAR(300)   - Operation filter (optional)
--   IN p_Location VARCHAR(300)    - Location filter (optional)
--   IN p_QtyMin DECIMAL(10,2)     - Minimum quantity filter (optional)
--   IN p_QtyMax DECIMAL(10,2)     - Maximum quantity filter (optional)
--   IN p_Notes TEXT               - Notes filter (optional)
--   IN p_User VARCHAR(300)        - User filter (optional)
--   IN p_FilterByDate BOOLEAN     - Whether to filter by date range
--   IN p_DateFrom DATETIME        - Start date for date range (optional)
--   IN p_DateTo DATETIME          - End date for date range (optional)
--   OUT p_Status INT              - Status code (0=success with data, 1=success no data, -1=error)
--   OUT p_ErrorMsg VARCHAR(500)   - Error message if any
-- Returns: DataTable with matching inventory records
-- ============================================================================

DROP PROCEDURE IF EXISTS `inv_inventory_Search_Advanced`;

DELIMITER $$

CREATE PROCEDURE `inv_inventory_Search_Advanced`(
    IN p_PartID VARCHAR(300),
    IN p_Operation VARCHAR(300),
    IN p_Location VARCHAR(300),
    IN p_QtyMin DECIMAL(10,2),
    IN p_QtyMax DECIMAL(10,2),
    IN p_Notes TEXT,
    IN p_User VARCHAR(300),
    IN p_FilterByDate BOOLEAN,
    IN p_DateFrom DATETIME,
    IN p_DateTo DATETIME,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    
    -- Error handling
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        SET p_Status = -1;
        SET p_ErrorMsg = 'Database error occurred while searching inventory';
        ROLLBACK;
    END;

    -- Build and execute dynamic search query
    SELECT 
        ID,
        PartID,
        Location,
        Operation,
        Quantity,
        ItemType,
        ReceiveDate,
        LastUpdated,
        User,
        BatchNumber,
        Notes
    FROM inv_inventory
    WHERE 
        (p_PartID IS NULL OR p_PartID = '' OR PartID LIKE CONCAT('%', p_PartID, '%'))
        AND (p_Operation IS NULL OR p_Operation = '' OR Operation LIKE CONCAT('%', p_Operation, '%'))
        AND (p_Location IS NULL OR p_Location = '' OR Location LIKE CONCAT('%', p_Location, '%'))
        AND (p_QtyMin IS NULL OR Quantity >= p_QtyMin)
        AND (p_QtyMax IS NULL OR Quantity <= p_QtyMax)
        AND (p_Notes IS NULL OR p_Notes = '' OR Notes LIKE CONCAT('%', p_Notes, '%'))
        AND (p_User IS NULL OR p_User = '' OR User LIKE CONCAT('%', p_User, '%'))
        AND (
            p_FilterByDate = FALSE 
            OR (
                (p_DateFrom IS NULL OR LastUpdated >= p_DateFrom)
                AND (p_DateTo IS NULL OR LastUpdated <= p_DateTo)
            )
        )
    ORDER BY LastUpdated DESC;
    
    -- Get row count
    SELECT FOUND_ROWS() INTO v_Count;
    
    IF v_Count > 0 THEN
        SET p_Status = 0;
        SET p_ErrorMsg = NULL;
    ELSE
        SET p_Status = 1;
        SET p_ErrorMsg = 'No inventory records found matching search criteria';
    END IF;
    
    COMMIT;
END$$

DELIMITER ;
