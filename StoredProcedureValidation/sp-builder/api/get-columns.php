<?php
/**
 * Get Columns API Endpoint
 * 
 * Returns column details for a specified table
 */

require_once 'config.php';

// Validate required parameter
validateRequiredParams(['table'], 'GET');

$tableName = $_GET['table'];

try {
    $conn = getDbConnection();
    
    // Query for column information
    $stmt = $conn->prepare("
        SELECT 
            COLUMN_NAME,
            COLUMN_DEFAULT,
            IS_NULLABLE,
            DATA_TYPE,
            CHARACTER_MAXIMUM_LENGTH,
            NUMERIC_PRECISION,
            NUMERIC_SCALE,
            COLUMN_TYPE,
            COLUMN_KEY,
            EXTRA,
            COLUMN_COMMENT
        FROM information_schema.COLUMNS
        WHERE TABLE_SCHEMA = ? AND TABLE_NAME = ?
        ORDER BY ORDINAL_POSITION
    ");
    
    $stmt->bind_param('ss', DB_NAME, $tableName);
    $stmt->execute();
    $result = $stmt->get_result();
    
    $columns = [];
    while ($row = $result->fetch_assoc()) {
        $columns[] = [
            'name' => $row['COLUMN_NAME'],
            'type' => $row['DATA_TYPE'],
            'columnType' => $row['COLUMN_TYPE'],
            'length' => $row['CHARACTER_MAXIMUM_LENGTH'],
            'precision' => $row['NUMERIC_PRECISION'],
            'scale' => $row['NUMERIC_SCALE'],
            'nullable' => $row['IS_NULLABLE'] === 'YES',
            'default' => $row['COLUMN_DEFAULT'],
            'key' => $row['COLUMN_KEY'],
            'extra' => $row['EXTRA'],
            'autoIncrement' => strpos($row['EXTRA'], 'auto_increment') !== false,
            'isPrimaryKey' => $row['COLUMN_KEY'] === 'PRI',
            'isForeignKey' => $row['COLUMN_KEY'] === 'MUL',
            'comment' => $row['COLUMN_COMMENT']
        ];
    }
    
    $stmt->close();
    $conn->close();
    
    if (empty($columns)) {
        sendErrorResponse(
            'validation',
            "Table '$tableName' not found or has no columns",
            "No columns returned for table: $tableName",
            404
        );
    }
    
    sendSuccessResponse([
        'table' => $tableName,
        'columns' => $columns,
        'count' => count($columns)
    ]);
    
} catch (Exception $e) {
    sendErrorResponse(
        'database',
        'Failed to retrieve columns',
        $e->getMessage()
    );
}
?>
