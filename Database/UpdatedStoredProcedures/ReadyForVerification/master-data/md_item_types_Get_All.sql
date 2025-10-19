DELIMITER //
DROP PROCEDURE IF EXISTS `md_item_types_Get_All`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_item_types_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    -- Transaction management removed: Works within caller's transaction context (tests use transactions)`r`n    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;
    SELECT * FROM `md_item_types`
    ORDER BY `ItemType`;
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' item type(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No item types found';
    END IF;
END
//
DELIMITER ;
