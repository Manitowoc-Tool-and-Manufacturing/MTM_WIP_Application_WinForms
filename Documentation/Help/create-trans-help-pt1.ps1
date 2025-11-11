Write-Host 'Creating comprehensive Transaction History help file...'

$htmlContent = @'
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset=\"UTF-8\">
    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">
    <title>Transaction History - MTM Inventory Application Help</title>
    <link rel=\"stylesheet\" href=\"css/help-styles.css\">
</head>
<body>
    <div class=\"help-container\">
        <div class=\"help-header\">
            <h1>📊 Transaction History</h1>
            <p>Complete guide to searching, viewing, and analyzing transaction records in MTM WIP Application</p>
        </div>
        
        <div class=\"help-nav\">
            <nav aria-label=\"breadcrumb\">
                <ol class=\"breadcrumb\">
                    <li class=\"breadcrumb-item\"><a href=\"index.html\">Help Home</a></li>
                    <li class=\"breadcrumb-item active\">Transaction History</li>
                </ol>
            </nav>
            <input type=\"text\" class=\"search-box\" placeholder=\"Search transaction help...\" id=\"searchBox\">
        </div>
        
        <div class=\"help-content\">
            <div class=\"alert alert-info\">
                <strong>Quick Start:</strong> Access transaction history from the main menu, use search filters to find specific transactions, and click on any row to view detailed information in the side panel.
            </div>
'@

$htmlContent | Out-File -FilePath 'transaction-history-new.html' -Encoding UTF8 -NoNewline
Write-Host 'Part 1 written successfully'
