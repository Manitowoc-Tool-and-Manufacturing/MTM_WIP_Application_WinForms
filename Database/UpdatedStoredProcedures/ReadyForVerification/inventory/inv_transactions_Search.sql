-- =============================================
-- Procedure: inv_transactions_Search
-- Domain: inventory
-- Created: 2025-10-17
-- Purpose: Advanced transaction search with 17 filter parameters, pagination, and sorting
-- =============================================

DELIMITER //

DROP PROCEDURE IF EXISTS `inv_transactions_Search`//

CREATE DEFINER=`root`@`localhost` PROCEDURE `inv_transactions_Search`(
    IN p_UserName VARCHAR(100),
    IN p_IsAdmin BOOLEAN,
    IN p_PartID VARCHAR(300),
    IN p_BatchNumber VARCHAR(300),
    IN p_FromLocation VARCHAR(100),
    IN p_ToLocation VARCHAR(100),
    IN p_Operation VARCHAR(100),
    IN p_TransactionType VARCHAR(50),
    IN p_Quantity INT,
    IN p_Notes VARCHAR(1000),
    IN p_ItemType VARCHAR(100),
    IN p_FromDate DATETIME,
    IN p_ToDate DATETIME,
    IN p_SortColumn VARCHAR(100),
    IN p_SortDescending BOOLEAN,
    IN p_Page INT,
    IN p_PageSize INT,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE v_Offset INT DEFAULT 0;
    DECLARE v_OrderBy VARCHAR(200) DEFAULT 'ORDER BY ReceiveDate DESC';
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    -- Validate pagination parameters
    IF p_Page < 1 THEN SET p_Page = 1; END IF;
    IF p_PageSize < 1 OR p_PageSize > 1000 THEN SET p_PageSize = 20; END IF;
    SET v_Offset = (p_Page - 1) * p_PageSize;
    
    -- Build ORDER BY clause
    IF p_SortColumn IS NOT NULL AND TRIM(p_SortColumn) != '' THEN
        SET v_OrderBy = CONCAT('ORDER BY ', p_SortColumn);
        IF p_SortDescending THEN
            SET v_OrderBy = CONCAT(v_OrderBy, ' DESC');
        ELSE
            SET v_OrderBy = CONCAT(v_OrderBy, ' ASC');
        END IF;
    END IF;
    
    -- Build and execute search query with filters
    SELECT 
        ID,
        TransactionType,
        BatchNumber,
        PartID,
        FromLocation,
        ToLocation,
        Operation,
        Quantity,
        Notes,
        User,
        ItemType,
        ReceiveDate
    FROM inv_transaction
    WHERE 1=1
        -- User filter (admin sees all, regular user sees only their transactions)
        AND (p_IsAdmin = TRUE OR User = p_UserName)
        -- PartID filter (empty string means no filter)
        AND (TRIM(COALESCE(p_PartID, '')) = '' OR PartID LIKE CONCAT('%', p_PartID, '%'))
        -- BatchNumber filter
        AND (TRIM(COALESCE(p_BatchNumber, '')) = '' OR BatchNumber LIKE CONCAT('%', p_BatchNumber, '%'))
        -- FromLocation filter
        AND (TRIM(COALESCE(p_FromLocation, '')) = '' OR FromLocation = p_FromLocation)
        -- ToLocation filter
        AND (TRIM(COALESCE(p_ToLocation, '')) = '' OR ToLocation = p_ToLocation)
        -- Operation filter
        AND (TRIM(COALESCE(p_Operation, '')) = '' OR Operation = p_Operation)
        -- TransactionType filter
        AND (TRIM(COALESCE(p_TransactionType, '')) = '' OR TransactionType = p_TransactionType)
        -- Quantity filter (NULL means no filter)
        AND (p_Quantity IS NULL OR Quantity = p_Quantity)
        -- Notes filter
        AND (TRIM(COALESCE(p_Notes, '')) = '' OR Notes LIKE CONCAT('%', p_Notes, '%'))
        -- ItemType filter
        AND (TRIM(COALESCE(p_ItemType, '')) = '' OR ItemType = p_ItemType)
        -- Date range filter
        AND (p_FromDate IS NULL OR ReceiveDate >= p_FromDate)
        AND (p_ToDate IS NULL OR ReceiveDate <= p_ToDate)
    ORDER BY 
        CASE WHEN p_SortColumn = 'ReceiveDate' AND p_SortDescending THEN ReceiveDate END DESC,
        CASE WHEN p_SortColumn = 'ReceiveDate' AND NOT p_SortDescending THEN ReceiveDate END ASC,
        CASE WHEN p_SortColumn = 'PartID' AND p_SortDescending THEN PartID END DESC,
        CASE WHEN p_SortColumn = 'PartID' AND NOT p_SortDescending THEN PartID END ASC,
        CASE WHEN p_SortColumn = 'Quantity' AND p_SortDescending THEN Quantity END DESC,
        CASE WHEN p_SortColumn = 'Quantity' AND NOT p_SortDescending THEN Quantity END ASC,
        CASE WHEN p_SortColumn = 'User' AND p_SortDescending THEN User END DESC,
        CASE WHEN p_SortColumn = 'User' AND NOT p_SortDescending THEN User END ASC,
        ReceiveDate DESC
    LIMIT v_Offset, p_PageSize;
    
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Found ', v_Count, ' transaction(s) matching criteria');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No transactions found matching search criteria';
    END IF;
END
//

DELIMITER ;

-- =============================================
-- End of inv_transactions_Search
-- =============================================
