DELIMITER $$

DROP PROCEDURE IF EXISTS `md_analytics_GetAllUsers`$$

CREATE PROCEDURE `md_analytics_GetAllUsers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
    END;

    SELECT User FROM usr_users ORDER BY User;

    SET p_Status = 0;
    SET p_ErrorMsg = NULL;
END$$

DELIMITER ;