DELIMITER //
DROP PROCEDURE IF EXISTS `maint_reload_part_ids_and_operation_numbers`//
CREATE DEFINER=`root`@`localhost` PROCEDURE `maint_reload_part_ids_and_operation_numbers`(
    OUT p_Status INT,
    OUT p_ErrorMsg VARCHAR(500)
)
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1
            p_ErrorMsg = MESSAGE_TEXT;
        SET p_Status = -1;
        ROLLBACK;
    END;
    START TRANSACTION;
    TRUNCATE TABLE mtm_wip_application.md_part_ids;
    INSERT INTO mtm_wip_application.md_part_ids (
        `PartID`,
        `Operations`,
        `Customer`,
        `Description`,
        `IssuedBy`,
        `ItemType`
    )
    SELECT
        seqs.ID AS `PartID`,
        CAST(seqs.sequence_array AS JSON) AS `Operations`,
        '' AS `Customer`,
        descs.Description AS `Description`,
        '[ System ]' AS `IssuedBy`,
        'WIP' AS `ItemType`
    FROM (
        SELECT
            ID,
            CONCAT('[', GROUP_CONCAT(DISTINCT SEQUENCE_NO ORDER BY SEQUENCE_NO), ']') AS sequence_array
        FROM
            `mtm database`.part_requirement
        GROUP BY
            ID
    ) seqs
    LEFT JOIN (
        SELECT
            ID,
            MIN(Description) AS Description
        FROM
            `mtm database`.part_requirement
        WHERE
            Description IS NOT NULL AND Description <> ''
        GROUP BY
            ID
    ) descs
    ON seqs.ID = descs.ID;
    COMMIT;
    START TRANSACTION;
    INSERT IGNORE INTO mtm_wip_application.md_operation_numbers (`Operation`)
    SELECT DISTINCT op_num
    FROM (
        SELECT
            TRIM(BOTH '"' FROM
                JSON_UNQUOTE(
                    JSON_EXTRACT(mpi.Operations, CONCAT('$[', n.n, ']'))
                )
            ) AS op_num
        FROM
            mtm_wip_application.md_part_ids mpi
        JOIN (
            SELECT 0 AS n UNION ALL SELECT 1 UNION ALL SELECT 2 UNION ALL SELECT 3 UNION ALL
            SELECT 4 UNION ALL SELECT 5 UNION ALL SELECT 6 UNION ALL SELECT 7 UNION ALL
            SELECT 8 UNION ALL SELECT 9 UNION ALL SELECT 10 UNION ALL SELECT 11 UNION ALL
            SELECT 12 UNION ALL SELECT 13 UNION ALL SELECT 14 UNION ALL SELECT 15
        ) n
        ON n.n < JSON_LENGTH(mpi.Operations)
        WHERE mpi.Operations IS NOT NULL
    ) AS all_ops
    WHERE op_num IS NOT NULL AND op_num <> '';
    COMMIT;
    SET p_Status = 1;
    SET p_ErrorMsg = 'Part IDs and operation numbers reloaded successfully';
END
//
DELIMITER ;
