<#
.SYNOPSIS
    Extracts all settings entities and their configurations from the codebase

.DESCRIPTION
    Scans Settings controls to identify:
    - Entity names (User, Part, Location, Operation, ItemType)
    - CRUD operations (Add/Edit/Remove/View)
    - Database tables and stored procedures used
    - UI patterns and dependencies

.EXAMPLE
    .\Extract_Settings_Entities.ps1
#>

$ErrorActionPreference = "Continue"

Write-Host "=== Settings Entity Extraction ===" -ForegroundColor Cyan

# Extract entities from control names
$controls = Get-ChildItem -Path "Controls\SettingsForm" -Filter "Control_*.cs" | Where-Object { $_.Name -notmatch '\.Designer\.cs$' }

$entities = @{}

foreach ($control in $controls) {
    $name = $control.BaseName
    
    # Parse control name pattern: Control_{Operation}_{Entity}
    if ($name -match 'Control_(Add|Edit|Remove)_(\w+)') {
        $operation = $matches[1]
        $entity = $matches[2]
        
        if (-not $entities.ContainsKey($entity)) {
            $entities[$entity] = @{
                Name = $entity
                Operations = @{}
                Controls = @()
            }
        }
        
        $entities[$entity].Operations[$operation] = $control.Name
        $entities[$entity].Controls += $control.Name
    }
    elseif ($name -match 'Control_(\w+)') {
        $setting = $matches[1]
        if ($setting -in @('Database', 'Theme', 'Shortcuts', 'About', 'Developer_ParameterPrefixMaintenance')) {
            $entities[$setting] = @{
                Name = $setting
                Type = "Setting"
                Controls = @($control.Name)
            }
        }
    }
}

Write-Host "`nEntities Found:" -ForegroundColor Green
$entities.Keys | Sort-Object | ForEach-Object {
    $entity = $entities[$_]
    Write-Host "  - $($entity.Name)" -ForegroundColor Yellow
    if ($entity.Operations) {
        Write-Host "    Operations: $($entity.Operations.Keys -join ', ')" -ForegroundColor Gray
    }
    if ($entity.Type -eq "Setting") {
        Write-Host "    Type: Setting Panel" -ForegroundColor Gray
    }
}

# Output JSON
$output = @{
    Timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    Entities = $entities
    Summary = @{
        TotalEntities = $entities.Keys.Count
        CRUDEntities = ($entities.Values | Where-Object { $_.Operations }).Count
        SettingPanels = ($entities.Values | Where-Object { $_.Type -eq "Setting" }).Count
    }
}

$output | ConvertTo-Json -Depth 10 | Out-File "settings-entities.json" -Encoding UTF8

Write-Host "`n✓ Complete! Output: settings-entities.json" -ForegroundColor Green
Write-Host "Total Entities: $($output.Summary.TotalEntities)"
Write-Host "CRUD Entities: $($output.Summary.CRUDEntities)"
Write-Host "Setting Panels: $($output.Summary.SettingPanels)"
