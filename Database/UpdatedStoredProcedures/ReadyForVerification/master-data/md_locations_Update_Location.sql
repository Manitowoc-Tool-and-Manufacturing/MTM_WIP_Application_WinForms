DELIMITER //
DROP PROCEDURE IF EXISTS `md_locations_Update_Location`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `md_locations_Update_Location`(
    IN p_OldLocation VARCHAR(100),
    IN p_Location VARCHAR(100),
    IN p_IssuedBy VARCHAR(100),
    IN p_Building VARCHAR(100),
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
    IF p_OldLocation IS NULL OR TRIM(p_OldLocation) = '' THEN
        SET p_Status = -2;
        SET p_ErrorMsg = 'OldLocation is required';
        ROLLBACK;
    ELSEIF p_Location IS NULL OR TRIM(p_Location) = '' THEN
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
        UPDATE `md_locations`
        SET `Location` = p_Location,
            `Building` = p_Building,
            `IssuedBy` = p_IssuedBy
        WHERE `Location` = p_OldLocation;
        SET v_RowCount = ROW_COUNT();
        IF v_RowCount > 0 THEN
            SET p_Status = 1;
            SET p_ErrorMsg = CONCAT('Location "', p_OldLocation, '" updated successfully');
            COMMIT;
        ELSE
            SET p_Status = -4;
            SET p_ErrorMsg = CONCAT('Location "', p_OldLocation, '" not found');
            ROLLBACK;
        END IF;
    END IF;
END
//
DELIMITER ;
