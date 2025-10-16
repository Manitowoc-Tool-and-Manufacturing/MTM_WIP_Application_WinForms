DELIMITER //
DROP PROCEDURE IF EXISTS `md_locations_Delete_ByLocation`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Delete_ByLocation`(
    IN p_Location VARCHAR(100),
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE v_RowCount INT DEFAULT 0;
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    IF p_Location IS NULL OR TRIM(p_Location) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'Location is required';
        ROLLBACK;
    ELSE
        DELETE FROM `md_locations`
        WHERE `Location` = p_Location;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" deleted successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Location "', p_Location, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END
//
DELIMITER ;
