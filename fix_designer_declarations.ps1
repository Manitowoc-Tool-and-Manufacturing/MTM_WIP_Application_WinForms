$files = @(
    "Controls\DeveloperTools\Control_LogViewer.Designer.cs",
    "Controls\DeveloperTools\Control_SystemHealth.Designer.cs",
    "Controls\DeveloperTools\Control_LogStatistics.Designer.cs",
    "Controls\DeveloperTools\Control_RecentErrors.Designer.cs",
    "Controls\DeveloperTools\Control_FeedbackManager.Designer.cs",
    "Controls\DeveloperTools\Control_SystemInfo.Designer.cs",
    "Forms\DeveloperTools\DeveloperToolsForm.Designer.cs"
)

foreach ($file in $files) {
    $path = Join-Path "c:\Users\johnk\source\repos\MTM_WIP_Application_WinForms" $file
    if (-not (Test-Path $path)) { continue }

    $content = Get-Content -Path $path -Raw

    # Fix declarations
    $content = $content -replace "private System\.Windows\.Forms\.", "private "
    $content = $content -replace "private System\.Drawing\.", "private "

    # Specific fix for Control_LogViewer
    if ($file -eq "Controls\DeveloperTools\Control_LogViewer.Designer.cs") {
        if ($content -notmatch "private TableLayoutPanel Control_LogViewer_TableLayout_Main;") {
            # Insert before the first TableLayoutPanel declaration or at the beginning of the region
            if ($content -match "private TableLayoutPanel Control_LogViewer_TableLayout_Header;") {
                $content = $content -replace "private TableLayoutPanel Control_LogViewer_TableLayout_Header;", "private TableLayoutPanel Control_LogViewer_TableLayout_Main;`r`n        private TableLayoutPanel Control_LogViewer_TableLayout_Header;"
            } else {
                # Fallback: add to end of class
                $content = $content -replace "\}\s*\}\s*$", "        private TableLayoutPanel Control_LogViewer_TableLayout_Main;`r`n    }`r`n}"
            }
        }
    }

    Set-Content -Path $path -Value $content
    Write-Host "Fixed declarations in $file"
}
