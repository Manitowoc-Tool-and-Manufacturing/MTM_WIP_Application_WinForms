<#
.SYNOPSIS
    Compiles comprehensive Settings system research data for documentation

.DESCRIPTION
    Systematically gathers:
    - All Settings controls and their purposes
    - Database tables and stored procedures
    - Models and DAOs used
    - UI patterns and event flows
    - Integration points with other systems

.EXAMPLE
    .\Research_Settings_Comprehensive.ps1
#>

$ErrorActionPreference = "Continue"

Write-Host "=== Comprehensive Settings System Research ===" -ForegroundColor Cyan
Write-Host "Phase 1: Entity Discovery" -ForegroundColor Yellow

# 1. Catalog all Settings controls
$settingsControls = @{
    CRUD = @{}
    Settings = @{}
}

Get-ChildItem "Controls\SettingsForm" -Filter "Control_*.cs" | Where-Object { $_.Name -notmatch '\.Designer\.cs$' } | ForEach-Object {
    $name = $_.BaseName
    
    if ($name -match 'Control_(Add|Edit|Remove)_(\w+)') {
        $op = $matches[1]
        $entity = $matches[2]
        
        if (-not $settingsControls.CRUD.ContainsKey($entity)) {
            $settingsControls.CRUD[$entity] = @{
                Entity = $entity
                Add = $null
                Edit = $null
                Remove = $null
            }
        }
        
        $settingsControls.CRUD[$entity][$op] = $_.Name
    }
    elseif ($name -match 'Control_(\w+)') {
        $settingsControls.Settings[$matches[1]] = $_.Name
    }
}

Write-Host "`nPhase 2: Database Schema Analysis" -ForegroundColor Yellow

# 2. Find database tables from stored procedures
$spFiles = Get-ChildItem "Database\UpdatedStoredProcedures" -Filter "*.sql" -Recurse -ErrorAction SilentlyContinue
$dbTables = @{}

foreach ($sp in $spFiles) {
    $content = Get-Content $sp.FullName -Raw -ErrorAction SilentlyContinue
    if ($content) {
        # Extract table references
        $tableMatches = [regex]::Matches($content, '(?:FROM|JOIN|INTO|UPDATE|INSERT INTO)\s+`?([a-z_]+)`?')
        foreach ($match in $tableMatches) {
            $table = $match.Groups[1].Value
            if ($table -match '^(md|usr|sys)_') {
                if (-not $dbTables.ContainsKey($table)) {
                    $dbTables[$table] = @{
                        Name = $table
                        UsedBy = @()
                    }
                }
                $dbTables[$table].UsedBy += $sp.BaseName
            }
        }
    }
}

Write-Host "`nPhase 3: DAO Method Analysis" -ForegroundColor Yellow

# 3. Analyze DAO methods
$daoMethods = @{}
$daoFile = "Data\Dao_User.cs"

if (Test-Path $daoFile) {
    $content = Get-Content $daoFile -Raw
    $methodMatches = [regex]::Matches($content, 'internal static async Task(?:<[^>]+>)? (\w+)\(')
    
    foreach ($match in $methodMatches) {
        $method = $match.Groups[1].Value
        $daoMethods[$method] = @{
            Name = $method
            IsGetter = $method -match '^Get'
            IsSetter = $method -match '^Set'
        }
    }
}

Write-Host "`nPhase 4: Settings Categories" -ForegroundColor Yellow

# 4. Categorize settings
$settingsCategories = @{
    UserManagement = @('User')
    MasterData = @('PartID', 'Location', 'Operation', 'ItemType')
    AppearanceAndUI = @('Theme', 'Shortcuts')
    SystemConfiguration = @('Database', 'Developer_ParameterPrefixMaintenance')
    Information = @('About')
}

Write-Host "`nPhase 5: Integration Points" -ForegroundColor Yellow

# 5. Find integration with other systems
$integrations = @{
    ThemeSystem = @()
    ShortcutSystem = @()
    ErrorHandling = @()
    ProgressReporting = @()
}

$allFiles = Get-ChildItem -Path "Controls\SettingsForm","Forms\Settings" -Filter "*.cs" -Recurse | Where-Object { $_.Name -notmatch '\.Designer\.cs$' }
foreach ($file in $allFiles) {
    $content = Get-Content $file.FullName -Raw -ErrorAction SilentlyContinue
    if ($content) {
        if ($content -match 'Core_Themes|UserUiColors|ApplyTheme') {
            $integrations.ThemeSystem += $file.Name
        }
        if ($content -match 'Shortcut|KeyDown|ProcessCmdKey') {
            $integrations.ShortcutSystem += $file.Name
        }
        if ($content -match 'Service_ErrorHandler|LoggingUtility') {
            $integrations.ErrorHandling += $file.Name
        }
        if ($content -match 'Helper_StoredProcedureProgress|ToolStripProgressBar') {
            $integrations.ProgressReporting += $file.Name
        }
    }
}

# Generate Report
Write-Host "`n=============== RESEARCH SUMMARY ===============" -ForegroundColor Cyan

Write-Host "`nCRUD Entities: $($settingsControls.CRUD.Keys.Count)" -ForegroundColor Green
$settingsControls.CRUD.Keys | Sort-Object | ForEach-Object {
    $entity = $settingsControls.CRUD[$_]
    Write-Host "  $_ : " -NoNewline -ForegroundColor Yellow
    $ops = @()
    if ($entity.Add) { $ops += "Add" }
    if ($entity.Edit) { $ops += "Edit" }
    if ($entity.Remove) { $ops += "Remove" }
    Write-Host ($ops -join ", ") -ForegroundColor Gray
}

Write-Host "`nSettings Panels: $($settingsControls.Settings.Keys.Count)" -ForegroundColor Green
$settingsControls.Settings.Keys | Sort-Object | ForEach-Object {
    Write-Host "  - $_" -ForegroundColor Yellow
}

Write-Host "`nDatabase Tables: $($dbTables.Keys.Count)" -ForegroundColor Green
$dbTables.Keys | Sort-Object | ForEach-Object {
    Write-Host "  - $_" -ForegroundColor Yellow
}

Write-Host "`nDAO User Methods: $($daoMethods.Keys.Count)" -ForegroundColor Green
Write-Host "  Getters: $(($daoMethods.Values | Where-Object { $_.IsGetter }).Count)"
Write-Host "  Setters: $(($daoMethods.Values | Where-Object { $_.IsSetter }).Count)"

Write-Host "`nIntegrations:" -ForegroundColor Green
Write-Host "  Theme System: $($integrations.ThemeSystem.Count) files"
Write-Host "  Shortcut System: $($integrations.ShortcutSystem.Count) files"
Write-Host "  Error Handling: $($integrations.ErrorHandling.Count) files"
Write-Host "  Progress Reporting: $($integrations.ProgressReporting.Count) files"

# Save to JSON
$output = @{
    Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Controls = $settingsControls
    DatabaseTables = $dbTables
    DAOMethods = $daoMethods
    Categories = $settingsCategories
    Integrations = $integrations
}

$output | ConvertTo-Json -Depth 10 | Out-File "settings-research.json" -Encoding UTF8

Write-Host "`n✓ Research complete! Saved to: settings-research.json" -ForegroundColor Green
