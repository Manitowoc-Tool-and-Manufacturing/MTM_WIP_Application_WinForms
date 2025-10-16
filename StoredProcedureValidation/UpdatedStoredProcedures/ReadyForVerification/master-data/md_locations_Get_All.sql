DELIMITER //
DROP PROCEDURE IF EXISTS `md_locations_Get_All`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Get_All`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_Count INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    SELECT * FROM `md_locations`
    ORDER BY `Location`;
    SELECT FOUND_ROWS() INTO v_Count;
    IF v_Count > 0 THEN
        SET p_Status = 1;
        SET p_ErrorMsg = CONCAT('Retrieved ', v_Count, ' location(s)');
    ELSE
        SET p_Status = 0;
        SET p_ErrorMsg = 'No locations found';
    END IF;
    COMMIT;
END
//
DELIMITER ;
