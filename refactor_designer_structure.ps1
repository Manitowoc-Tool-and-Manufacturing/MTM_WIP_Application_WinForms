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
    if (-not (Test-Path $path)) { 
        Write-Host "File not found: $path"
        continue 
    }

    $content = Get-Content -Path $path -Raw

    # 1. Add Usings if missing
    if ($content -notmatch "using System.Windows.Forms;") {
        # Insert after namespace declaration or at top if no namespace (though these files have namespace)
        # Actually, standard is at the very top.
        $content = "using System.Drawing;`r`nusing System.Windows.Forms;`r`n`r`n" + $content
    }

    # 2. Replace Fully Qualified Names
    # Be careful with System.Drawing.Color vs Color (ambiguous if not careful, but usually fine in WinForms)
    $content = $content -replace "new System\.Windows\.Forms\.", "new "
    $content = $content -replace "System\.Windows\.Forms\.DockStyle", "DockStyle"
    $content = $content -replace "System\.Windows\.Forms\.AnchorStyles", "AnchorStyles"
    $content = $content -replace "System\.Windows\.Forms\.Orientation", "Orientation"
    $content = $content -replace "System\.Windows\.Forms\.DataGridView", "DataGridView"
    $content = $content -replace "System\.Windows\.Forms\.BorderStyle", "BorderStyle"
    $content = $content -replace "System\.Windows\.Forms\.ScrollBars", "ScrollBars"
    $content = $content -replace "System\.Windows\.Forms\.SizeType", "SizeType"
    $content = $content -replace "System\.Windows\.Forms\.RowStyle", "RowStyle"
    $content = $content -replace "System\.Windows\.Forms\.ColumnStyle", "ColumnStyle"
    $content = $content -replace "System\.Windows\.Forms\.Padding", "Padding"
    $content = $content -replace "System\.Windows\.Forms\.CheckState", "CheckState"
    $content = $content -replace "System\.Windows\.Forms\.ComboBoxStyle", "ComboBoxStyle"
    $content = $content -replace "System\.Windows\.Forms\.DateTimePickerFormat", "DateTimePickerFormat"
    $content = $content -replace "System\.Windows\.Forms\.DataGridViewSelectionMode", "DataGridViewSelectionMode"
    $content = $content -replace "System\.Windows\.Forms\.DataGridViewColumnHeadersHeightSizeMode", "DataGridViewColumnHeadersHeightSizeMode"
    $content = $content -replace "System\.Windows\.Forms\.DataGridViewAutoSizeColumnMode", "DataGridViewAutoSizeColumnMode"
    $content = $content -replace "System\.Windows\.Forms\.AutoScaleMode", "AutoScaleMode"
    $content = $content -replace "System\.Drawing\.Point", "Point"
    $content = $content -replace "System\.Drawing\.Size", "Size"
    $content = $content -replace "System\.Drawing\.Color", "Color"
    $content = $content -replace "System\.Drawing\.Font", "Font"
    $content = $content -replace "System\.Drawing\.FontStyle", "FontStyle"
    $content = $content -replace "System\.Drawing\.GraphicsUnit", "GraphicsUnit"
    $content = $content -replace "System\.Drawing\.ContentAlignment", "ContentAlignment"
    $content = $content -replace "System\.Drawing\.SizeF", "SizeF"

    # 3. Add Regions (Fields)
    if ($content -notmatch "#region Fields") {
        $content = $content -replace "private System.ComponentModel.IContainer components = null;", "#region Fields`r`n`r`n        private System.ComponentModel.IContainer components = null;`r`n`r`n        #endregion"
    }

    # 4. Add Regions (Methods/Dispose)
    if ($content -notmatch "#region Methods") {
        # Regex to match the Dispose method block
        $disposePattern = "(protected override void Dispose\(bool disposing\)[\s\S]*?base\.Dispose\(disposing\);\s*\})"
        $content = [regex]::Replace($content, $disposePattern, "#region Methods`r`n`r`n        `$1`r`n        #endregion")
    }

    Set-Content -Path $path -Value $content
    Write-Host "Processed $file"
}
