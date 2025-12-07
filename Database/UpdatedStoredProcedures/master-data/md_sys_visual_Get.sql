USE mtm_wip_application_winforms;
DROP PROCEDURE IF EXISTS md_sys_visual_Get;

DELIMITER //

CREATE PROCEDURE md_sys_visual_Get(
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

    SELECT id, json_shift_data, json_user_fullnames, last_updated
    FROM sys_visual
    ORDER BY id DESC
    LIMIT 1;

    SET p_Status = 1;
    SET p_ErrorMsg = NULL;
END //

DELIMITER ;
