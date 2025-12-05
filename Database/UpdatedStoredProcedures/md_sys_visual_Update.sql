USE mtm_wip_application_winforms;
DROP PROCEDURE IF EXISTS md_sys_visual_Update;

DELIMITER //

CREATE PROCEDURE md_sys_visual_Update(
    IN p_JsonShifts JSON,
    IN p_JsonNames JSON,
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 @sqlstate = RETURNED_SQLSTATE, @errno = MYSQL_ERRNO, @text = MESSAGE_TEXT;
        SET p_Status = -1;
        SET p_ErrorMsg = CONCAT('Error ', @errno, ' (', @sqlstate, '): ', @text);
    END;

    -- Check if a record exists
    IF (SELECT COUNT(*) FROM sys_visual) > 0 THEN
        UPDATE sys_visual
        SET json_shift_data = p_JsonShifts,
            json_user_fullnames = p_JsonNames,
            last_updated = NOW();
    ELSE
        INSERT INTO sys_visual (json_shift_data, json_user_fullnames, last_updated)
        VALUES (p_JsonShifts, p_JsonNames, NOW());
    END IF;

    SET p_Status = 1;
    SET p_ErrorMsg = NULL;
END //

DELIMITER ;
