<#
.SYNOPSIS
    Analyzes the entire codebase for Settings system usage and dependencies.

.DESCRIPTION
    Scans all C# files to identify:
    - Settings-related controls and forms
    - Database operations (stored procedures, tables)
    - Model classes used by Settings
    - Helper methods and services
    - Cross-references and dependencies

.PARAMETER OutputPath
    Path to output JSON results file

.EXAMPLE
    .\Analyze_Codebase_SettingsSystem.ps1 -OutputPath ".\settings-analysis.json"
#>

param(
    [string]$OutputPath = ".\settings-analysis.json"
)

$ErrorActionPreference = "Stop"

Write-Host "=== Settings System Codebase Analysis ===" -ForegroundColor Cyan
Write-Host "Starting comprehensive analysis..." -ForegroundColor Yellow

$analysis = @{
    Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    SettingsForms = @()
    SettingsControls = @()
    Models = @()
    DAOs = @()
    StoredProcedures = @()
    Helpers = @()
    Services = @()
    DatabaseTables = @()
    Dependencies = @()
    UISettings = @()
    UserPreferences = @()
    ThemeReferences = @()
    ShortcutReferences = @()
}

# 1. Analyze Settings Forms
Write-Host "`n[1/12] Analyzing Settings Forms..." -ForegroundColor Green
$settingsForms = Get-ChildItem -Path "Forms\Settings" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue
foreach ($form in $settingsForms) {
    $content = Get-Content $form.FullName -Raw
    $analysis.SettingsForms += @{
        Name = $form.Name
        Path = $form.FullName
        Lines = ($content -split "`n").Count
        HasDesigner = Test-Path ($form.FullName -replace '\.cs$', '.Designer.cs')
        Classes = [regex]::Matches($content, 'public (?:partial )?class (\w+)').Groups[1].Value
        Methods = [regex]::Matches($content, 'public \w+ (\w+)\(').Groups[1].Value
        UsesDAO = $content -match 'Dao_'
        UsesHelper = $content -match 'Helper_'
        UsesService = $content -match 'Service_'
        UsesModel = $content -match 'Model_'
    }
}

# 2. Analyze Settings Controls
Write-Host "[2/12] Analyzing Settings Controls..." -ForegroundColor Green
$settingsControls = Get-ChildItem -Path "Controls\SettingsForm" -Filter "*.cs" -Recurse -ErrorAction SilentlyContinue
foreach ($control in $settingsControls) {
    if ($control.Name -notmatch '\.Designer\.cs$' -and $control.Name -notmatch '\.resx$') {
        $content = Get-Content $control.FullName -Raw
        $analysis.SettingsControls += @{
            Name = $control.Name
            Path = $control.FullName
            Lines = ($content -split "`n").Count
            Category = if ($control.Name -match 'Add') { 'Add' }
                      elseif ($control.Name -match 'Edit') { 'Edit' }
                      elseif ($control.Name -match 'Remove') { 'Remove' }
                      elseif ($control.Name -match 'About|Database|Shortcuts|Theme|Developer') { 'Settings' }
                      else { 'Other' }
            Entity = [regex]::Match($control.Name, '_(\w+)\.cs$').Groups[1].Value
            StoredProcedureCalls = [regex]::Matches($content, '"((?:md|usr|sys|log|inv)_[^"]+)"').Groups[1].Value | Select-Object -Unique
            DaoCalls = [regex]::Matches($content, 'Dao_(\w+)\.').Groups[1].Value | Select-Object -Unique
            HelperCalls = [regex]::Matches($content, 'Helper_(\w+)\.').Groups[1].Value | Select-Object -Unique
        }
    }
}

# 3. Find all Models used by Settings
Write-Host "[3/12] Analyzing Model classes..." -ForegroundColor Green
$modelFiles = Get-ChildItem -Path "Models" -Filter "Model_*.cs" -ErrorAction SilentlyContinue
foreach ($model in $modelFiles) {
    $content = Get-Content $model.FullName -Raw
    $isSettingsRelated = $content -match 'User|Theme|Shortcut|UiColors|AppVariables|Settings'
    
    if ($isSettingsRelated) {
        $analysis.Models += @{
            Name = $model.Name
            Path = $model.FullName
            Properties = [regex]::Matches($content, 'public \w+\?? (\w+) \{').Groups[1].Value
            UsedBySettings = $true
        }
    }
}

# 4. Find all DAOs used by Settings
Write-Host "[4/12] Analyzing DAO classes..." -ForegroundColor Green
$daoFiles = Get-ChildItem -Path "Data" -Filter "Dao_*.cs" -ErrorAction SilentlyContinue
$settingsControlContent = ($settingsControls | ForEach-Object { Get-Content $_.FullName -Raw }) -join "`n"
$settingsFormContent = ($settingsForms | ForEach-Object { Get-Content $_.FullName -Raw }) -join "`n"

foreach ($dao in $daoFiles) {
    $daoName = $dao.BaseName
    $content = Get-Content $dao.FullName -Raw
    $usedBySettings = ($settingsControlContent -match $daoName) -or ($settingsFormContent -match $daoName)
    
    if ($usedBySettings) {
        $analysis.DAOs += @{
            Name = $dao.Name
            Path = $dao.FullName
            Entity = $daoName -replace '^Dao_', ''
            Methods = [regex]::Matches($content, 'public static \w+ (\w+)\(').Groups[1].Value
            StoredProcedures = [regex]::Matches($content, '"((?:md|usr|sys)_[^"]+)"').Groups[1].Value | Select-Object -Unique
        }
    }
}

# 5. Identify Stored Procedures
Write-Host "[5/12] Identifying Stored Procedures..." -ForegroundColor Green
$allSpCalls = @()
$allSpCalls += $analysis.SettingsControls | ForEach-Object { $_.StoredProcedureCalls }
$allSpCalls += $analysis.DAOs | ForEach-Object { $_.StoredProcedures }
$allSpCalls = $allSpCalls | Where-Object { $_ } | Select-Object -Unique

foreach ($sp in $allSpCalls) {
    # Find SP file in UpdatedStoredProcedures
    $spFile = Get-ChildItem -Path "Database\UpdatedStoredProcedures" -Filter "$sp.sql" -Recurse -ErrorAction SilentlyContinue | Select-Object -First 1
    
    $analysis.StoredProcedures += @{
        Name = $sp
        Category = ($sp -split '_')[0]
        Exists = $spFile -ne $null
        Path = if ($spFile) { $spFile.FullName } else { "NOT FOUND" }
    }
}

# 6. Analyze Helper usage
Write-Host "[6/12] Analyzing Helper classes..." -ForegroundColor Green
$helperFiles = Get-ChildItem -Path "Helpers" -Filter "Helper_*.cs" -ErrorAction SilentlyContinue
$allHelperCalls = $analysis.SettingsControls | ForEach-Object { $_.HelperCalls } | Where-Object { $_ } | Select-Object -Unique

foreach ($helperCall in $allHelperCalls) {
    $helperFile = $helperFiles | Where-Object { $_.BaseName -eq "Helper_$helperCall" }
    if ($helperFile) {
        $content = Get-Content $helperFile.FullName -Raw
        $analysis.Helpers += @{
            Name = "Helper_$helperCall"
            Path = $helperFile.FullName
            UsedBySettings = $true
            Methods = [regex]::Matches($content, 'public static \w+ (\w+)\(').Groups[1].Value
        }
    }
}

# 7. Analyze Services
Write-Host "[7/12] Analyzing Service classes..." -ForegroundColor Green
$serviceFiles = Get-ChildItem -Path "Services" -Filter "Service_*.cs" -ErrorAction SilentlyContinue
foreach ($service in $serviceFiles) {
    $serviceName = $service.BaseName
    $usedBySettings = ($settingsControlContent -match $serviceName) -or ($settingsFormContent -match $serviceName)
    
    if ($usedBySettings) {
        $content = Get-Content $service.FullName -Raw
        $analysis.Services += @{
            Name = $service.Name
            Path = $service.FullName
            UsedBySettings = $true
        }
    }
}

# 8. Identify Database Tables
Write-Host "[8/12] Identifying Database Tables..." -ForegroundColor Green
$spContent = ""
foreach ($sp in $analysis.StoredProcedures) {
    if ($sp.Exists -and $sp.Path -ne "NOT FOUND") {
        $spContent += Get-Content $sp.Path -Raw -ErrorAction SilentlyContinue
    }
}

if ($spContent) {
    $tableMatches = [regex]::Matches($spContent, '(?:FROM|JOIN|INTO|UPDATE)\s+([a-z_]+)')
    if ($tableMatches.Count -gt 0) {
        $tables = $tableMatches | ForEach-Object { $_.Groups[1].Value } | 
            Where-Object { $_ -match '^(md|usr|sys|log)_' } | 
            Select-Object -Unique

        foreach ($table in $tables) {
            $analysis.DatabaseTables += @{
                Name = $table
                Category = ($table -split '_')[0]
                UsedBySettings = $true
            }
        }
    }
}

# 9. Find UI Settings patterns
Write-Host "[9/12] Analyzing UI Settings patterns..." -ForegroundColor Green
$allContentParts = @()
foreach ($item in ($settingsControls + $settingsForms)) {
    if ($item.FullName -and (Test-Path $item.FullName)) {
        $allContentParts += Get-Content $item.FullName -Raw -ErrorAction SilentlyContinue
    }
}
$allContent = $allContentParts -join "`n"
$uiSettings = @{
    ThemeUsage = $allContent -match 'Theme|UserUiColors|ApplyTheme'
    ShortcutUsage = $allContent -match 'Shortcut|KeyDown|ProcessCmdKey'
    QuickButtonUsage = $allContent -match 'QuickButton'
    UserPreferencesUsage = $allContent -match 'UserPreference|usr_ui_settings'
    DatabaseConnectionUsage = $allContent -match 'Database|Connection|Helper_Database'
}

$analysis.UISettings = $uiSettings

# 10. Find User Preferences patterns
Write-Host "[10/12] Analyzing User Preferences..." -ForegroundColor Green
$themeMatches = if ($allContent) { [regex]::Matches($allContent, '(Theme\w+)') } else { @() }
$layoutMatches = if ($allContent) { [regex]::Matches($allContent, '(Layout\w+|Panel\w+)') } else { @() }
$displayMatches = if ($allContent) { [regex]::Matches($allContent, '(Show\w+|Hide\w+|Display\w+)') } else { @() }

$analysis.UserPreferences = @{
    ThemeSelection = if ($themeMatches.Count -gt 0) { $themeMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
    LayoutPreferences = if ($layoutMatches.Count -gt 0) { $layoutMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
    DisplayPreferences = if ($displayMatches.Count -gt 0) { $displayMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
}

# 11. Analyze Theme References
Write-Host "[11/12] Analyzing Theme system integration..." -ForegroundColor Green
$coreThemesMatches = if ($allContent) { [regex]::Matches($allContent, 'Core_Themes\.(\w+)') } else { @() }
$userUiColorsMatches = if ($allContent) { [regex]::Matches($allContent, 'UserUiColors\.(\w+)') } else { @() }
$themeLoadingMatches = if ($allContent) { [regex]::Matches($allContent, '(LoadTheme|ApplyTheme|GetTheme)\w*') } else { @() }

$analysis.ThemeReferences = @{
    CoreThemesCalls = if ($coreThemesMatches.Count -gt 0) { $coreThemesMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
    UserUiColorsCalls = if ($userUiColorsMatches.Count -gt 0) { $userUiColorsMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
    ThemeLoadingCalls = if ($themeLoadingMatches.Count -gt 0) { $themeLoadingMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
}

# 12. Analyze Shortcut References
Write-Host "[12/12] Analyzing Shortcut system integration..." -ForegroundColor Green
$shortcutMatches = if ($allContent) { [regex]::Matches($allContent, '(Shortcut_\w+)') } else { @() }
$helperShortcutMatches = if ($allContent) { [regex]::Matches($allContent, 'Helper_UI_Shortcuts\.(\w+)') } else { @() }
$keyEventMatches = if ($allContent) { [regex]::Matches($allContent, '(KeyDown|KeyPress|KeyUp|ProcessCmdKey)') } else { @() }

$analysis.ShortcutReferences = @{
    ShortcutCalls = if ($shortcutMatches.Count -gt 0) { $shortcutMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
    HelperShortcutCalls = if ($helperShortcutMatches.Count -gt 0) { $helperShortcutMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
    KeyEventHandlers = if ($keyEventMatches.Count -gt 0) { $keyEventMatches | ForEach-Object { $_.Groups[1].Value } | Select-Object -Unique } else { @() }
}

# Generate Summary Statistics
Write-Host "`n=== Analysis Summary ===" -ForegroundColor Cyan
Write-Host "Settings Forms: $($analysis.SettingsForms.Count)" -ForegroundColor Yellow
Write-Host "Settings Controls: $($analysis.SettingsControls.Count)" -ForegroundColor Yellow
Write-Host "  - Add Controls: $(($analysis.SettingsControls | Where-Object { $_.Category -eq 'Add' }).Count)"
Write-Host "  - Edit Controls: $(($analysis.SettingsControls | Where-Object { $_.Category -eq 'Edit' }).Count)"
Write-Host "  - Remove Controls: $(($analysis.SettingsControls | Where-Object { $_.Category -eq 'Remove' }).Count)"
Write-Host "  - Settings Controls: $(($analysis.SettingsControls | Where-Object { $_.Category -eq 'Settings' }).Count)"
Write-Host "Models: $($analysis.Models.Count)" -ForegroundColor Yellow
Write-Host "DAOs: $($analysis.DAOs.Count)" -ForegroundColor Yellow
Write-Host "Stored Procedures: $($analysis.StoredProcedures.Count)" -ForegroundColor Yellow
Write-Host "  - Found: $(($analysis.StoredProcedures | Where-Object { $_.Exists }).Count)"
Write-Host "  - Missing: $(($analysis.StoredProcedures | Where-Object { -not $_.Exists }).Count)"
Write-Host "Helpers: $($analysis.Helpers.Count)" -ForegroundColor Yellow
Write-Host "Services: $($analysis.Services.Count)" -ForegroundColor Yellow
Write-Host "Database Tables: $($analysis.DatabaseTables.Count)" -ForegroundColor Yellow

# Output to JSON
$jsonOutput = $analysis | ConvertTo-Json -Depth 10
$jsonOutput | Out-File -FilePath $OutputPath -Encoding UTF8

Write-Host "`n✓ Analysis complete! Results saved to: $OutputPath" -ForegroundColor Green
Write-Host "`nKey Findings:" -ForegroundColor Cyan
Write-Host "- Total Settings-related files: $(($analysis.SettingsForms + $analysis.SettingsControls).Count)"
Write-Host "- Missing stored procedures: $(($analysis.StoredProcedures | Where-Object { -not $_.Exists }).Count)"
Write-Host "- Theme integration points: $($analysis.ThemeReferences.CoreThemesCalls.Count)"
Write-Host "- Shortcut integration points: $($analysis.ShortcutReferences.ShortcutCalls.Count)"

return $analysis
