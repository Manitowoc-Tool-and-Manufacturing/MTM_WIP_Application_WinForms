<#
.SYNOPSIS
    Validates theme system usage according to theme-system.instructions.md rules.

.DESCRIPTION
    Scans C# files to ensure theme methods are called in appropriate locations only.
    Checks for violations of the DO/DON'T rules from .github/instructions/theme-system.instructions.md

.PARAMETER FilePath
    Path to a specific C# file to validate. If not provided, validates MainForm.cs and all UserControls.

.PARAMETER Strict
    If set, treats warnings as errors and returns non-zero exit code.

.PARAMETER ExportPath
    Path where validation results will be exported. Defaults to '.\theme-validation-results.txt'.
    Results include all violations, warnings, and summary statistics in a readable text format.

.EXAMPLE
    .\Scripts\Validate-ThemeSystemUsage.ps1
    Validates MainForm and all UserControls

.EXAMPLE
    .\Scripts\Validate-ThemeSystemUsage.ps1 -FilePath "Forms\ViewLogs\ViewApplicationLogsForm.cs"
    Validates a specific file

.EXAMPLE
    .\Scripts\Validate-ThemeSystemUsage.ps1 -Strict
    Validates with strict mode (warnings become errors)

.EXAMPLE
    .\Scripts\Validate-ThemeSystemUsage.ps1 -ExportPath "C:\Reports\theme-validation.txt"
    Validates and exports results to custom path
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [string]$FilePath,
    
    [Parameter(Mandatory = $false)]
    [switch]$Strict,
    
    [Parameter(Mandatory = $false)]
    [string]$ExportPath = ".\theme-validation-results.txt"
)

#region Configuration

# Theme method patterns to search for
$themeMethodPatterns = @{
    ApplyTheme                    = 'Core_Themes\.ApplyTheme\('
    ApplyDpiScaling               = 'Core_Themes\.ApplyDpiScaling\('
    ApplyRuntimeLayoutAdjustments = 'Core_Themes\.ApplyRuntimeLayoutAdjustments\('
    ApplyFocusHighlighting        = 'Core_Themes\.ApplyFocusHighlighting\('
    HandleDpiChanged              = 'Core_Themes\.HandleDpiChanged\('
    ApplyThemeToDataGridView      = 'Core_Themes\.ApplyThemeToDataGridView\('
}

# Allowed contexts for theme method calls
$allowedContexts = @{
    Constructor         = '\b\w+\s*\(\s*\)\s*\{?'
    FormLoadEvent       = '(private|protected|public)\s+(async\s+)?void\s+\w+_Load\s*\('
    FormShownEvent      = 'Shown\s*\+='
    DpiChangedEvent     = '(OnDpiChanged|DpiChanged)\s*\('
    SettingsMenuClick   = 'Settings_Click\s*\('
    InitializeMethod    = '(private|protected|public)\s+void\s+Initialize\w*\s*\('
}

# Event handlers that should NEVER contain theme calls
$forbiddenEventHandlers = @(
    '_Click\s*\(',
    '_TextChanged\s*\(',
    '_SelectedIndexChanged\s*\(',
    '_KeyDown\s*\(',
    '_KeyPress\s*\(',
    '_MouseClick\s*\(',
    '_MouseMove\s*\(',
    '_Tick\s*\(',
    '_ValueChanged\s*\(',
    '_CheckedChanged\s*\('
)

#endregion

#region Helper Functions

function Write-ValidationHeader {
    param([string]$Message)
    Write-Host "`n================================================" -ForegroundColor Cyan
    Write-Host $Message -ForegroundColor Cyan
    Write-Host "================================================`n" -ForegroundColor Cyan
}

function Write-ValidationResult {
    param(
        [string]$Level,  # SUCCESS, WARNING, ERROR
        [string]$Message,
        [string]$File,
        [int]$LineNumber = 0,
        [string]$Context = "",
        [System.Collections.ArrayList]$ExportBuffer
    )
    
    $color = switch ($Level) {
        "SUCCESS" { "Green" }
        "WARNING" { "Yellow" }
        "ERROR" { "Red" }
        default { "White" }
    }
    
    $icon = switch ($Level) {
        "SUCCESS" { "✅" }
        "WARNING" { "⚠️ " }
        "ERROR" { "❌" }
        default { "ℹ️ " }
    }
    
    if ($LineNumber -gt 0) {
        $fileOutput = "[$Level] ${File}:${LineNumber}`n  Message: $Message"
        
        Write-Host "$icon [$Level] " -ForegroundColor $color -NoNewline
        Write-Host "$File" -ForegroundColor White -NoNewline
        Write-Host ":$LineNumber" -ForegroundColor Gray -NoNewline
        Write-Host " - $Message" -ForegroundColor $color
        
        if ($Context) {
            Write-Host "    Context: $Context" -ForegroundColor DarkGray
            $fileOutput += "`n  Context: $Context"
        }
        
        if ($null -ne $ExportBuffer) {
            $null = $ExportBuffer.Add($fileOutput)
        }
    }
    else {
        Write-Host "$icon [$Level] $Message" -ForegroundColor $color
        if ($null -ne $ExportBuffer) {
            $null = $ExportBuffer.Add("[$Level] $Message")
        }
    }
}

function Test-IsUserControl {
    param([string]$FileContent)
    return $FileContent -match ':\s*UserControl\s*\{'
}

function Test-IsForm {
    param([string]$FileContent)
    return $FileContent -match ':\s*Form\s*\{'
}

function Get-MethodContext {
    param(
        [string[]]$Lines,
        [int]$LineIndex
    )
    
    # Search backwards to find the method signature
    $contextLines = @()
    $braceCount = 0
    $foundMethodSignature = $false
    $methodName = "Unknown"
    $isConstructor = $false
    $isEventHandler = $false
    
    # First pass: find the method signature and check if we're in a constructor
    for ($i = $LineIndex; $i -ge 0 -and $i -gt ($LineIndex - 100); $i--) {
        $line = $Lines[$i]
        $trimmedLine = $line.Trim()
        
        # Skip comments and empty lines
        if ($trimmedLine -match '^\s*//' -or $trimmedLine -match '^\s*\*' -or [string]::IsNullOrWhiteSpace($trimmedLine)) {
            continue
        }
        
        $contextLines += $line
        
        # Count braces to understand scope depth
        $openBraces = ($line -split '\{').Count - 1
        $closeBraces = ($line -split '\}').Count - 1
        $braceCount += $openBraces - $closeBraces
        
        # Check for constructor patterns
        # Pattern 1: public Control_Name() or public Control_Name(params)
        if ($line -match '(public|internal)\s+(\w+)\s*\(') {
            $possibleConstructorName = $matches[2]
            # Check if method name matches typical constructor patterns (Control_, Form name, etc.)
            if ($possibleConstructorName -match '^(Control_|Form|.*Form)' -or 
                $possibleConstructorName -match '^[A-Z][a-zA-Z0-9_]*$') {
                $methodName = $possibleConstructorName
                $isConstructor = $true
                $foundMethodSignature = $true
                break
            }
        }
        
        # Check for InitializeComponent call (strong indicator we're in a constructor)
        if ($line -match 'InitializeComponent\s*\(\s*\)' -and $braceCount -le 1) {
            # We're definitely in a constructor
            # Keep searching backwards for the actual constructor signature
            for ($j = $i; $j -ge 0 -and $j -gt ($i - 20); $j--) {
                $constructorLine = $Lines[$j]
                if ($constructorLine -match '(public|internal)\s+(\w+)\s*\(') {
                    $methodName = $matches[2]
                    $isConstructor = $true
                    $foundMethodSignature = $true
                    break
                }
            }
            if ($foundMethodSignature) { break }
        }
        
        # Regular method signature patterns
        if ($line -match '(private|protected|public|internal)\s+(async\s+)?(void|Task|bool|int|string|\w+)\s+(\w+)\s*\(') {
            $methodName = $matches[4]
            $isEventHandler = $methodName -match '_(Click|Load|Changed|KeyDown|KeyPress|Mouse|Tick|Shown|Resize|Paint|Enter|Leave)$'
            $foundMethodSignature = $true
            break
        }
        
        # Stop if we hit a class declaration (went too far)
        if ($line -match '(public|internal|private)\s+(partial\s+)?class\s+\w+') {
            break
        }
        
        # Stop if we've left the method scope (more closing braces than opening)
        if ($braceCount -lt -1) {
            break
        }
    }
    
    # If we still haven't found a method signature, check if we're still within a reasonable scope
    if (-not $foundMethodSignature) {
        # Look for region markers or field declarations that indicate we're in class scope
        for ($i = $LineIndex; $i -ge 0 -and $i -gt ($LineIndex - 30); $i--) {
            $line = $Lines[$i]
            if ($line -match '#region\s+(Constructors|Initialization)') {
                $isConstructor = $true
                $methodName = "Constructor (in region)"
                break
            }
        }
    }
    
    return @{
        MethodName    = $methodName
        MethodContext = ($contextLines[0..([Math]::Min(5, $contextLines.Count - 1))] -join "`n")
        IsConstructor = $isConstructor
        IsEventHandler = $isEventHandler
    }
}

#endregion

#region Validation Functions

function Test-ThemeMethodUsage {
    param(
        [string]$FilePath,
        [string]$FileContent,
        [string[]]$Lines
    )
    
    $violations = @()
    $warnings = @()
    $successes = @()
    
    $isUserControl = Test-IsUserControl -FileContent $FileContent
    $isForm = Test-IsForm -FileContent $FileContent
    $fileName = Split-Path $FilePath -Leaf
    
    # Check for theme method calls
    foreach ($methodName in $themeMethodPatterns.Keys) {
        $pattern = $themeMethodPatterns[$methodName]
        
        for ($i = 0; $i -lt $Lines.Count; $i++) {
            $line = $Lines[$i]
            $lineNumber = $i + 1
            
            if ($line -match $pattern -and $line -notmatch '^\s*//' -and $line -notmatch '^\s*\*') {
                # Found a theme method call - validate context
                $context = Get-MethodContext -Lines $Lines -LineIndex $i
                $methodContext = $context.MethodName
                
                # Rule 1: ApplyTheme() should NOT be in UserControls
                if ($methodName -eq "ApplyTheme" -and $isUserControl) {
                    $violations += @{
                        File       = $FilePath
                        Line       = $lineNumber
                        Method     = $methodName
                        Message    = "ApplyTheme() called in UserControl. Theme colors cascade from parent form."
                        Context    = $methodContext
                        Severity   = "ERROR"
                        Rule       = "UserControls should NOT call ApplyTheme()"
                    }
                    continue
                }
                
                # Rule 2: ApplyTheme() should be in Form Load event for Forms
                if ($methodName -eq "ApplyTheme" -and $isForm) {
                    if ($context.MethodName -notmatch '(_Load|Shown|InitializeTheme|ApplyTheme|OnDpiChanged|Settings)') {
                        $warnings += @{
                            File       = $FilePath
                            Line       = $lineNumber
                            Method     = $methodName
                            Message    = "ApplyTheme() called outside recommended contexts (Load, Shown, DpiChanged, Settings)"
                            Context    = $methodContext
                            Severity   = "WARNING"
                            Rule       = "ApplyTheme() should be in Form Load, Shown event, or Settings handler"
                        }
                    }
                    else {
                        $successes += @{
                            File    = $FilePath
                            Line    = $lineNumber
                            Method  = $methodName
                            Message = "ApplyTheme() correctly placed in $methodContext"
                        }
                    }
                }
                
                # Rule 3: DPI/Layout methods should be in Constructor
                if ($methodName -in @("ApplyDpiScaling", "ApplyRuntimeLayoutAdjustments", "ApplyFocusHighlighting")) {
                    if (-not $context.IsConstructor) {
                        # Additional check: look for InitializeComponent nearby (strong constructor indicator)
                        $hasInitializeComponentNearby = $false
                        $checkRange = [Math]::Max(0, $i - 10)..[Math]::Min($Lines.Count - 1, $i + 10)
                        foreach ($checkIdx in $checkRange) {
                            if ($Lines[$checkIdx] -match 'InitializeComponent\s*\(\s*\)') {
                                $hasInitializeComponentNearby = $true
                                break
                            }
                        }
                        
                        # Only warn if we're confident it's NOT in a constructor
                        if (-not $hasInitializeComponentNearby -and $context.MethodName -notmatch '^(Control_|Initialize|Setup|Constructor)') {
                            $warnings += @{
                                File       = $FilePath
                                Line       = $lineNumber
                                Method     = $methodName
                                Message    = "$methodName called outside constructor. Should be in constructor for proper initialization."
                                Context    = $methodContext
                                Severity   = "WARNING"
                                Rule       = "$methodName should be in constructor"
                            }
                        }
                        else {
                            # Likely in constructor but not detected - count as success
                            $successes += @{
                                File    = $FilePath
                                Line    = $lineNumber
                                Method  = $methodName
                                Message = "$methodName in constructor (near InitializeComponent or in $methodContext)"
                            }
                        }
                    }
                    else {
                        $successes += @{
                            File    = $FilePath
                            Line    = $lineNumber
                            Method  = $methodName
                            Message = "$methodName correctly placed in constructor"
                        }
                    }
                }
                
                # Rule 4: No theme calls in forbidden event handlers
                if ($context.IsEventHandler) {
                    $isForbidden = $false
                    foreach ($forbiddenPattern in $forbiddenEventHandlers) {
                        if ($context.MethodName -match $forbiddenPattern) {
                            $isForbidden = $true
                            break
                        }
                    }
                    
                    # Allow specific exceptions
                    $allowedExceptions = @("_Load", "_Shown", "OnDpiChanged", "Settings_Click")
                    $isException = $false
                    foreach ($exception in $allowedExceptions) {
                        if ($context.MethodName -match $exception) {
                            $isException = $true
                            break
                        }
                    }
                    
                    if ($isForbidden -and -not $isException) {
                        $violations += @{
                            File       = $FilePath
                            Line       = $lineNumber
                            Method     = $methodName
                            Message    = "$methodName called in forbidden event handler: $methodContext"
                            Context    = $methodContext
                            Severity   = "ERROR"
                            Rule       = "Theme methods should NOT be called in arbitrary event handlers"
                        }
                    }
                }
            }
        }
    }
    
    return @{
        Violations = $violations
        Warnings   = $warnings
        Successes  = $successes
    }
}

function Test-AutoScaleMode {
    param(
        [string]$FilePath,
        [string]$FileContent
    )
    
    $designerPath = $FilePath -replace '\.cs$', '.Designer.cs'
    
    if (-not (Test-Path $designerPath)) {
        return @{
            Message  = "No Designer file found"
            HasIssue = $false
        }
    }
    
    $designerContent = Get-Content $designerPath -Raw
    
    if ($designerContent -match 'AutoScaleMode\s*=\s*System\.Windows\.Forms\.AutoScaleMode\.Dpi') {
        return @{
            Message  = "AutoScaleMode.Dpi correctly set in Designer"
            HasIssue = $false
        }
    }
    elseif ($designerContent -match 'AutoScaleMode') {
        return @{
            Message  = "AutoScaleMode found but NOT set to Dpi"
            HasIssue = $true
        }
    }
    else {
        return @{
            Message  = "AutoScaleMode not found in Designer (may be inherited)"
            HasIssue = $false
        }
    }
}

#endregion

#region Main Execution

function Invoke-ThemeValidation {
    param([string]$FilePath)
    
    Write-ValidationHeader "Theme System Usage Validator"
    
    # Initialize export buffer
    $exportBuffer = [System.Collections.ArrayList]::new()
    $null = $exportBuffer.Add("=" * 80)
    $null = $exportBuffer.Add("Theme System Usage Validation Results")
    $null = $exportBuffer.Add("Generated: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')")
    $null = $exportBuffer.Add("=" * 80)
    $null = $exportBuffer.Add("")
    
    # Determine files to scan
    $filesToScan = @()
    
    if ($FilePath) {
        if (-not (Test-Path $FilePath)) {
            Write-ValidationResult -Level ERROR -Message "File not found: $FilePath"
            return 1
        }
        $filesToScan += Get-Item $FilePath
    }
    else {
        # Scan MainForm and all UserControls
        Write-Host "Scanning MainForm and all UserControls..." -ForegroundColor Cyan
        
        $mainForm = "Forms\MainForm\MainForm.cs"
        if (Test-Path $mainForm) {
            $filesToScan += Get-Item $mainForm
        }
        
        # Find all UserControls
        $userControls = Get-ChildItem -Path "Controls" -Recurse -Filter "*.cs" -File | 
            Where-Object { $_.Name -notmatch '\.Designer\.cs$' -and $_.Name -match '^Control_' }
        $filesToScan += $userControls
        
        # Also scan Forms (excluding MainForm)
        $forms = Get-ChildItem -Path "Forms" -Recurse -Filter "*.cs" -File |
            Where-Object { $_.Name -notmatch '\.Designer\.cs$' -and $_.Name -match 'Form\.cs$' -and $_.FullName -notmatch 'MainForm\\MainForm\.cs' }
        $filesToScan += $forms
    }
    
    Write-Host "Found $($filesToScan.Count) files to validate`n" -ForegroundColor White
    $null = $exportBuffer.Add("Files to validate: $($filesToScan.Count)")
    $null = $exportBuffer.Add("")
    
    # Validation results
    $totalViolations = 0
    $totalWarnings = 0
    $totalSuccesses = 0
    
    foreach ($file in $filesToScan) {
        $relativePath = $file.FullName -replace [regex]::Escape($PWD.Path + '\'), ''
        Write-Host "Validating: $relativePath" -ForegroundColor White
        $null = $exportBuffer.Add("-" * 80)
        $null = $exportBuffer.Add("File: $relativePath")
        $null = $exportBuffer.Add("")
        
        $content = Get-Content $file.FullName -Raw
        $lines = Get-Content $file.FullName
        
        # Run validations
        $results = Test-ThemeMethodUsage -FilePath $relativePath -FileContent $content -Lines $lines
        $autoScaleResult = Test-AutoScaleMode -FilePath $file.FullName -FileContent $content
        
        # Report violations
        foreach ($violation in $results.Violations) {
            Write-ValidationResult -Level ERROR `
                -Message $violation.Message `
                -File $violation.File `
                -LineNumber $violation.Line `
                -Context $violation.Context `
                -ExportBuffer $exportBuffer
            $totalViolations++
        }
        
        # Report warnings
        foreach ($warning in $results.Warnings) {
            Write-ValidationResult -Level WARNING `
                -Message $warning.Message `
                -File $warning.File `
                -LineNumber $warning.Line `
                -Context $warning.Context `
                -ExportBuffer $exportBuffer
            $totalWarnings++
        }
        
        # Report AutoScaleMode issues
        if ($autoScaleResult.HasIssue) {
            Write-ValidationResult -Level WARNING `
                -Message $autoScaleResult.Message `
                -File $relativePath `
                -ExportBuffer $exportBuffer
            $totalWarnings++
        }
        
        # Count successes
        $totalSuccesses += $results.Successes.Count
        
        if ($results.Violations.Count -eq 0 -and $results.Warnings.Count -eq 0 -and -not $autoScaleResult.HasIssue) {
            $null = $exportBuffer.Add("  No issues found")
        }
        
        $null = $exportBuffer.Add("")
        Write-Host ""
    }
    
    # Summary
    Write-ValidationHeader "Validation Summary"
    $null = $exportBuffer.Add("")
    $null = $exportBuffer.Add("=" * 80)
    $null = $exportBuffer.Add("VALIDATION SUMMARY")
    $null = $exportBuffer.Add("=" * 80)
    
    Write-Host "Files Scanned: " -NoNewline
    Write-Host $filesToScan.Count -ForegroundColor Cyan
    $null = $exportBuffer.Add("Files Scanned: $($filesToScan.Count)")
    
    Write-Host "Successes: " -NoNewline
    Write-Host $totalSuccesses -ForegroundColor Green
    $null = $exportBuffer.Add("Successes: $totalSuccesses")
    
    Write-Host "Warnings: " -NoNewline
    Write-Host $totalWarnings -ForegroundColor Yellow
    $null = $exportBuffer.Add("Warnings: $totalWarnings")
    
    Write-Host "Errors: " -NoNewline
    Write-Host $totalViolations -ForegroundColor Red
    $null = $exportBuffer.Add("Errors: $totalViolations")
    Write-Host ""
    $null = $exportBuffer.Add("")
    
    # Export to file
    try {
        $exportBuffer | Out-File -FilePath $ExportPath -Encoding UTF8 -Force
        Write-Host "Results exported to: " -NoNewline -ForegroundColor Cyan
        Write-Host (Resolve-Path $ExportPath).Path -ForegroundColor White
        $null = $exportBuffer.Add("Results exported to: $ExportPath")
    }
    catch {
        Write-Host "Warning: Failed to export results to file: $_" -ForegroundColor Yellow
    }
    
    # Determine exit code
    if ($totalViolations -gt 0) {
        Write-Host "❌ Validation FAILED - $totalViolations error(s) found" -ForegroundColor Red
        return 1
    }
    elseif ($Strict -and $totalWarnings -gt 0) {
        Write-Host "⚠️  Validation FAILED (strict mode) - $totalWarnings warning(s) found" -ForegroundColor Yellow
        return 1
    }
    elseif ($totalWarnings -gt 0) {
        Write-Host "⚠️  Validation PASSED with warnings - $totalWarnings warning(s) found" -ForegroundColor Yellow
        return 0
    }
    else {
        Write-Host "✅ Validation PASSED - No issues found" -ForegroundColor Green
        return 0
    }
}

# Execute validation
$exitCode = Invoke-ThemeValidation -FilePath $FilePath
exit $exitCode

#endregion
