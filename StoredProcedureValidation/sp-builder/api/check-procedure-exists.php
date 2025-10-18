<?php
/**
 * Check Procedure Exists API Endpoint
 * 
 * Checks if a stored procedure exists in the database
 */

require_once 'config.php';

// Validate required parameter
validateRequiredParams(['procedure'], 'GET');

$procedureName = $_GET['procedure'];

try {
    $conn = getDbConnection();
    
    // Query information_schema.ROUTINES for procedure
    $dbName = DB_NAME; // Must use variable for bind_param reference
    $stmt = $conn->prepare("
        SELECT 
            ROUTINE_NAME,
            ROUTINE_TYPE,
            DTD_IDENTIFIER,
            CREATED,
            LAST_ALTERED,
            ROUTINE_COMMENT
        FROM information_schema.ROUTINES
        WHERE ROUTINE_SCHEMA = ? 
          AND ROUTINE_NAME = ?
          AND ROUTINE_TYPE = 'PROCEDURE'
    ");
    
    $stmt->bind_param('ss', $dbName, $procedureName);
    $stmt->execute();
    $result = $stmt->get_result();
    
    if ($row = $result->fetch_assoc()) {
        // Procedure exists
        $stmt->close();
        $conn->close();
        
        sendSuccessResponse([
            'exists' => true,
            'procedure' => [
                'name' => $row['ROUTINE_NAME'],
                'type' => $row['ROUTINE_TYPE'],
                'returnType' => $row['DTD_IDENTIFIER'],
                'created' => $row['CREATED'],
                'modified' => $row['LAST_ALTERED'],
                'comment' => $row['ROUTINE_COMMENT']
            ]
        ]);
    } else {
        // Procedure does not exist
        $stmt->close();
        $conn->close();
        
        sendSuccessResponse([
            'exists' => false,
            'procedure' => null
        ]);
    }
    
} catch (Exception $e) {
    sendErrorResponse(
        'database',
        'Failed to check procedure existence',
        $e->getMessage()
    );
}
?>
