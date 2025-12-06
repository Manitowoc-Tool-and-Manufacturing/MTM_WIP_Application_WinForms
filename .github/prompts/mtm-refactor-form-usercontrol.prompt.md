# GitHub Custom Prompt: UI Designer Compliance Refactoring (Enhanced)

## Objective
You are tasked with analyzing and refactoring UI files to achieve 100% compliance with Designer patterns. Your goal is to make all UI elements fully editable through the IDE's Design View.

**CRITICAL REQUIREMENTS:**
1. **Error Handling**: ALL errors MUST use `Service_ErrorHandler` (NEVER direct message boxes)
2. **Logging**: ALL operations MUST use `Service_LoggingUtility` for structured CSV logging
3. **Naming Convention**: ALL controls MUST follow `{FormOrControlName}_{ControlType}_{Name}` pattern
   - Example: `InventoryTab_Button_Save`, `SettingsForm_TextBox_UserName`
   - If renaming required, ALL references across the ENTIRE codebase MUST be updated
4. **Code-Agnostic**: This prompt works for any UI framework with designer patterns

**IMPORTANT INSTRUCTION:** Before beginning any refactoring work, you MUST create a detailed, step-by-step plan that addresses ALL requirements outlined in this prompt. Do not prioritize speed over thoroughness. Your plan must demonstrate that you will complete every task specified in the personas, refactoring requirements, and success criteria sections.

**File Selection Logic:**
- If a specific file path is provided with this prompt, refactor that file.
- If NO file is specified, scan the codebase to identify the first file requiring designer compliance remediation, then refactor it.

**Pre-Refactoring Checklist:**
Before writing any code, your plan must explicitly address:
1. Which controls will be moved from main file to designer file
2. Access modifier analysis for each control (with justification)
3. Dependency mapping (what code references each control)
4. Property categorization (design-time vs runtime)
5. Event handler migration strategy
6. Validation approach to ensure designer compatibility
7. **Naming convention compliance** - verify ALL controls follow `{FormOrControlName}_{ControlType}_{Name}`
8. **Codebase-wide renaming plan** - if naming violations found, create comprehensive rename strategy
9. **Error handling audit** - identify and replace direct message boxes with `Service_ErrorHandler`
10. **Logging integration** - add `Service_LoggingUtility` calls for all refactoring operations
11. **Theme system compliance** - ensure Form/UserControl inherits from `ThemedForm`/`ThemedUserControl` and `Core_AppThemes` is applied correctly (no manual calls if automatic)
12. **UI Scaling compliance** - ensure `AutoScaleMode = AutoScaleMode.Dpi` and `AutoScaleDimensions = new SizeF(96F, 96F)`

Only proceed with refactoring after presenting this comprehensive plan.

## Personas & Responsibilities

### 👨‍🔬 Persona 1: The Code Archaeologist
**Role:** Discovery & Analysis  
**AI Execution Mode:** Pattern Recognition & Static Analysis

#### Primary Objectives
1. **Inventory all UI elements** and their current locations
2. **Map dependency relationships** between controls and code
3. **Classify control lifecycles** (static vs dynamic)
4. **Identify blocking dependencies** preventing Designer migration
5. **Validate naming conventions** for all controls
6. **Audit error handling patterns** for Service_ErrorHandler compliance

#### Detection Patterns (Execute in Order)

**Pattern 1: Control Field Declarations**
```regex
Scan for: ^[\s]*(private|protected|internal|public)[\s]+([\w.<>]+)[\s]+([\w]+);
Location: Class-level fields in main code files (not designer files)
Categorize by: Type namespace (UI framework controls)
Naming Validation: Check if control name matches {FormOrControlName}_{ControlType}_{Name} pattern
  - Extract form/control name from class declaration
  - Identify control type from variable type
  - Validate naming structure
  - Flag violations for codebase-wide renaming
```

**Pattern 2: Control Instantiation Sites**
```regex
Scan for: ([\w]+)[\s]*=[\s]*new[\s]+([\w.<>]+)\(\)
Locations to check:
  - Constructor bodies (ANTI-PATTERN if found)
  - Load event handlers (ANTI-PATTERN if found)
  - Designer initialization method (CORRECT location)
  - Helper methods (ANTI-PATTERN if found)
  
Logging Requirement:
  - Log each instantiation site found outside designer
  - Use Service_LoggingUtility.Log($"[Refactor] Control {controlName} instantiated in {location} - requires migration")
```

**Pattern 3: Property Assignment Sites**
```regex
Scan for: ([\w]+)\.(Text|Size|Location|Font|Dock|Anchor|TabIndex|[\w]+)[\s]*=
Classify:
  - Design-time properties (Size, Location, Font) → Must be in designer initialization
  - Runtime properties (DataSource, Items, data-binding) → Can remain in Load event
  
Naming Validation on Assignment:
  - For each property assignment, verify control name follows {FormOrControlName}_{ControlType}_{Name}
  - If violation found, add to rename queue with all file locations
  
Error Handling Pattern Detection:
  - Search for MessageBox.Show() or equivalent direct dialogs
  - Flag for replacement with Service_ErrorHandler.ShowUserError() or appropriate method
```

**Pattern 4: Event Handler Subscriptions**
```regex
Scan for: ([\w]+)\.([\w]+)[\s]*\+=[\s]*(new[\s]+[\w.]+\()?this\.([\w]+)
Record: Control name, event name, handler method name, subscription location
```

**Pattern 5: Container Hierarchy**
```regex
Scan for: (this|[\w]+)\.Controls\.Add\(([\w]+)\)
Build tree: Parent → Child relationships
Flag orphans: Controls never added to any container
```

**Pattern 6: Constructor Dependencies**
```pseudocode
// Detection algorithm (language-agnostic)
foreach (var controlInit in ConstructorStatements)
{
    // Dependency analysis
    if (controlInit.DependsOnParameter || 
        controlInit.AccessesRuntimeData ||
        controlInit.CallsAsyncMethod)
    {
        Flag as "Blocking Dependency";
        Document: "Requires decoupling before Designer migration";
        
        // Log discovery
        Service_LoggingUtility.Log($"[Refactor] Blocking dependency in constructor: {controlInit.Description}");
    }
    
    // Error handling pattern detection
    if (controlInit.ContainsDirectMessageBox())
    {
        Flag as "Error Handling Violation";
        Document: "Replace MessageBox/direct dialog with Service_ErrorHandler";
        Service_LoggingUtility.Log($"[Refactor] Direct message box found in constructor - line {lineNumber}");
    }
}
```

**Pattern 7: Theme System Compliance**
```regex
Scan for: :[\s]*(Form|UserControl)
Action:
  - Flag for update to : ThemedForm or : ThemedUserControl
Scan for: Core_Themes\.ApplyTheme|ApplyTheme\(
Action:
  - Flag for removal (handled automatically by base class)
  - Verify Core_AppThemes usage aligns with new patterns
```

**Pattern 8: UI Scaling Compliance**
```regex
Scan for: AutoScaleMode\s*=\s*AutoScaleMode\.(\w+)
Action:
  - If not Dpi, flag for update to AutoScaleMode.Dpi
Scan for: AutoScaleDimensions\s*=\s*new\s+SizeF\(([^)]+)\)
Action:
  - If not (96F, 96F), flag for update to new SizeF(96F, 96F)
```

#### Output Format (JSON Schema)

```json
{
  "controlInventory": [
    {
      "controlName": "string",
      "controlType": "string (e.g., Button, TextBox, Panel)",
      "accessModifier": "private|protected|internal|public",
      "declaredIn": {
        "file": "string (e.g., Form_HtmlViewer.cs)",
        "lineNumber": "integer"
      },
      "instantiatedIn": {
        "file": "string",
        "method": "string (e.g., Constructor, InitializeComponent, Form_Load)",
        "lineNumber": "integer"
      },
      "propertiesSetIn": [
        {
          "property": "string",
          "value": "string",
          "location": "string (method name)",
          "lineNumber": "integer",
          "category": "design-time|runtime"
        }
      ],
      "eventsSubscribedIn": [
        {
          "eventName": "string",
          "handlerMethod": "string",
          "location": "string",
          "lineNumber": "integer"
        }
      ],
      "addedToContainer": {
        "parentControl": "string (e.g., this, panelMain)",
        "location": "string",
        "lineNumber": "integer"
      },
      "visualHierarchy": {
        "parent": "string|null",
        "children": ["string"]
      },
      "blockingDependencies": [
        {
          "type": "constructor-parameter|async-call|runtime-data",
          "description": "string",
          "lineNumber": "integer"
        }
      ],
      "lifecycle": "static|dynamic",
      "designerCompatible": "yes|no|requires-refactoring",
      "migrationPriority": "high|medium|low",
      "namingCompliance": {
        "followsConvention": "boolean",
        "expectedName": "string ({FormOrControlName}_{ControlType}_{Name})",
        "actualName": "string",
        "referencesInCodebase": [
          {
            "file": "string",
            "lineNumber": "integer",
            "context": "string (code snippet)"
          }
        ]
      },
      "errorHandlingIssues": [
        {
          "type": "direct-messagebox|missing-logging|missing-error-handler",
          "location": "string (file:line)",
          "currentCode": "string",
          "recommendedFix": "string"
        }
      ]
    }
  ],
  "summary": {
    "totalControls": "integer",
    "inDesignerFile": "integer",
    "inConstructor": "integer",
    "inLoadEvent": "integer",
    "fullyCompliant": "integer",
    "requiresRefactoring": "integer",
    "namingViolations": "integer",
    "errorHandlingViolations": "integer",
    "totalCodebaseReferences": "integer (for renaming scope)"
  }
}
```

#### Validation Rules
- ✅ **Complete Scan**: All files with Form/UserControl inheritance analyzed
- ✅ **Zero False Positives**: Non-UI fields excluded (verify namespace)
- ✅ **Dependency Completeness**: All runtime dependencies identified with line numbers
- ✅ **Hierarchy Integrity**: Parent-child relationships match actual container structure
- ✅ **Naming Convention Compliance**: All controls validated against {FormOrControlName}_{ControlType}_{Name}

---

### 👷‍♀️ Persona 2: The Designer Architect
**Role:** `.Designer` File Construction & Integrity  
**AI Execution Mode:** Code Generation & Structural Validation

#### Primary Objectives
1. **Validate Designer file structure** against IDE parser requirements
2. **Enforce canonical InitializeComponent() ordering**
3. **Separate design-time from runtime properties**
4. **Ensure Dispose pattern correctness**
5. **Integrate error handling and logging** throughout designer operations

#### Structural Validation Checklist

**Requirement 1: File Header**
```pseudocode
// Required structure (validate with AST parser)
namespace YourNamespace
{
    partial class YourForm  // Must be partial/split class
    {
        region "Designer generated code"
        // Designer initialization method must exist here
        endregion
        
        region "Component Disposal"
        // Dispose method must exist here
        endregion
    }
}

// Error Handling & Logging Requirements:
// 1. If designer file generation fails, use Service_ErrorHandler.HandleException()
// 2. Log all designer file operations with Service_LoggingUtility
```

**Requirement 2: Component Container**
```pseudocode
// MANDATORY field for Designer compatibility
private ComponentContainer components = null;

// Validation rule
if (!DesignerFile.Contains("ComponentContainer") && !DesignerFile.Contains("components"))
{
    Flag: "Missing components field - Designer will fail";
    
    // Error handling
    Service_ErrorHandler.HandleException(
        new Exception("Designer file missing component container field"),
        Enum_ErrorSeverity.High,
        contextData: { "File": DesignerFilePath },
        callerName: "ValidateDesignerStructure"
    );
    
    // Logging
    Service_LoggingUtility.Log($"[Refactor] Missing component container in {DesignerFilePath}");
}
```

**Requirement 3: Dispose Pattern**
```pseudocode
// Required implementation (check signature and body)
protected override method Dispose(bool disposing)
{
    if (disposing && (components != null))
    {
        components.Dispose();
    }
    base.Dispose(disposing);
}

// Validation rules
- Check: Method signature matches exactly
- Check: Null check for components
- Check: base class Dispose called AFTER component cleanup
- Log: Service_LoggingUtility.Log($"[Refactor] Validated Dispose pattern in {FileName}")
```

**Requirement 4: Designer Initialization Method Sequencing**
```pseudocode
// MANDATORY ORDER (violations break Designer parser)
private method InitializeDesigner()
{
    // Start logging
    Service_LoggingUtility.Log($"[Refactor] Initializing designer components for {FormName}");
    
    // SECTION 1: Resource Manager (if needed)
    var resources = new ResourceManager(CurrentFormType);
    
    // SECTION 2: Control Instantiation (ALL controls)
    // NAMING CONVENTION ENFORCEMENT: {FormOrControlName}_{ControlType}_{Name}
    this.{FormName}_{ControlType}_{Name} = new ControlType();
    // Example: this.InventoryTab_Button_Save = new Button();
    //          this.SettingsForm_TextBox_UserName = new TextBox();
    
    // Validation during instantiation
    foreach (control in instantiatedControls)
    {
        if (!ValidateNamingConvention(control.Name))
        {
            Service_LoggingUtility.Log($"[Refactor] Naming violation: {control.Name} should be {GetExpectedName(control)}");
            AddToRenameQueue(control);
        }
    }
    
    // SECTION 3: Layout Suspension (containers first)
    this.SuspendLayout();
    this.containerPanel.SuspendLayout();
    
    // SECTION 4: Property Assignments (design-time only)
    // SIZE, LOCATION, TEXT, FONT, COLOR
    // NEVER: DataSource, Items.Add(), SelectedIndex
    
    // SECTION 5: Event Subscriptions
    // MOVED TO CODEBEHIND: Event handlers must be subscribed in the Constructor, NOT here.
    // this.{FormName}_Button_Submit.Click += EventHandler(this.OnSubmitClick); // REMOVE
    
    // SECTION 6: Container Hierarchy (children before parents)
    this.{FormName}_Panel_Main.Controls.Add(this.{FormName}_Button_Submit);
    this.Controls.Add(this.{FormName}_Panel_Main);
    
    // SECTION 7: Form/Control Properties
    this.AutoScaleDimensions = new SizeF(96F, 96F);
    this.AutoScaleMode = AutoScaleMode.Dpi;
    this.ClientSize = new Size(800, 600);
    this.Name = "{FormName}";
    this.Text = "Window Title";
    
    // SECTION 8: Layout Resumption (reverse order of suspension)
    this.{FormName}_Panel_Container.ResumeLayout(false);
    this.ResumeLayout(false);
    
    // Completion logging
    Service_LoggingUtility.Log($"[Refactor] Designer initialization completed for {FormName}");
}
```

#### Property Categorization Engine

**Algorithm: Design-Time vs Runtime Classification**
```pseudocode
// Whitelist: Properties ALLOWED in Designer Initialization
designTimeProperties = Set(
    // Layout
    "Location", "Size", "Dock", "Anchor", "Padding", "Margin",
    
    // Appearance
    "Text", "Font", "ForeColor", "BackColor", "BackgroundImage",
    "BorderStyle", "FlatStyle",
    
    // Behavior
    "Enabled", "Visible", "TabIndex", "TabStop",
    
    // Form-specific
    "AutoScaleDimensions", "AutoScaleMode", "ClientSize", 
    "FormBorderStyle", "MaximizeBox", "MinimizeBox", "Name"
)

// Blacklist: Properties FORBIDDEN in Designer Initialization
runtimeProperties = Set(
    "DataSource", "DataMember", "DisplayMember", "ValueMember",
    "SelectedIndex", "SelectedItem", "SelectedValue",
    "Items" (collection manipulation), "Nodes" (tree manipulation),
    "Value" (data-bound values), "DataBindings"
)

// Classification logic with error handling
foreach (assignment in PropertyAssignments)
{
    try
    {
        if (runtimeProperties.Contains(assignment.PropertyName))
        {
            MoveTo: LoadEvent or InitializationMethod
            AddComment: "// Runtime data binding - cannot be in Designer"
            Service_LoggingUtility.Log($"[Refactor] Moved runtime property {assignment.PropertyName} to Load event")
        }
        else if (designTimeProperties.Contains(assignment.PropertyName))
        {
            MoveTo: DesignerInitialization
            Service_LoggingUtility.Log($"[Refactor] Kept design-time property {assignment.PropertyName} in Designer")
        }
        else
        {
            FlagForManualReview: "Unknown property category"
            Service_LoggingUtility.Log($"[Refactor] Unknown property {assignment.PropertyName} requires manual review")
        }
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(
            ex,
            Enum_ErrorSeverity.Medium,
            contextData: { "Property": assignment.PropertyName, "Control": assignment.ControlName },
            callerName: "ClassifyProperties"
        )
    }
}
```

#### Output Format (Validation Report)

```json
{
  "designerFileValidation": {
    "fileExists": "boolean",
    "isPartialClass": "boolean",
    "hasRegionBlock": "boolean",
    "hasComponentsField": "boolean",
    "hasDisposeMethod": "boolean",
    "disposeSignatureCorrect": "boolean",
    "initializeComponentExists": "boolean",
    "sectionOrder": {
      "instantiation": "integer (line number)",
      "suspendLayout": "integer",
      "properties": "integer",
      "events": "integer",
      "hierarchy": "integer",
      "formProperties": "integer",
      "resumeLayout": "integer",
      "orderCorrect": "boolean"
    }
  },
  "propertyClassification": [
    {
      "controlName": "string",
      "propertyName": "string",
      "currentLocation": "InitializeComponent|Form_Load|Constructor",
      "classification": "design-time|runtime|unknown",
      "action": "keep|move-to-FormLoad|move-to-InitializeComponent",
      "lineNumber": "integer"
    }
  ],
  "violations": [
    {
      "severity": "error|warning",
      "rule": "string (e.g., Missing components field)",
      "location": "string",
      "lineNumber": "integer",
      "fix": "string (proposed correction)"
    }
  ]
}
```

#### Validation Rules
- ✅ **Structural Compliance**: All mandatory sections present in correct order
- ✅ **Property Purity**: Zero runtime properties in InitializeComponent()
- ✅ **Dispose Integrity**: Dispose pattern matches exact signature and logic
- ✅ **Parser Compatibility**: File parses without errors in IDE Designer

---

### 🔐 Persona 3: The Access Control Specialist
**Role:** Security, Encapsulation & API Surface Area  
**AI Execution Mode:** Dependency Analysis & Privilege Reduction

#### Primary Objectives
1. **Map global dependencies** for every UI control
2. **Minimize access modifiers** via principle of least privilege
3. **Detect reflection-based access** patterns
4. **Design encapsulation wrappers** for unavoidable external access
5. **Enforce naming conventions** across entire codebase with automated renaming
6. **Integrate error handling and logging** for all access control operations

#### Dependency Mapping Algorithm

**Step 1: Global Reference Search & Naming Validation**
```pseudocode
// For each control in inventory
foreach (control in ControlInventory)
{
    // Naming convention validation FIRST
    expectedName = $"{FormOrControlName}_{control.Type}_{control.Purpose}"
    if (control.Name != expectedName)
    {
        Service_LoggingUtility.Log($"[Refactor] Naming violation: '{control.Name}' should be '{expectedName}'")
        control.RequiresRename = true
        control.ExpectedName = expectedName
    }
    
    // Execute solution-wide search for current name
    references = SearchEntireSolution($"{control.Name}")
    
    // Categorize by assembly/namespace
    externalReferences = FilterReferences(references, 
        where: r.AssemblyName != CurrentAssembly OR r.NamespaceName != CurrentNamespace)
    
    // Categorize by access pattern
    directAccess = FilterReferences(references,
        where: r.Pattern matches property/field access)
    
    reflectionAccess = FilterReferences(references,
        where: r.Pattern contains reflection methods)
    
    // Log findings
    Service_LoggingUtility.Log($"[Refactor] Control '{control.Name}': {references.Count} total refs, {externalReferences.Count} external")
    
    // Store results with rename information
    control.DependencyMap = new DependencyMap
    {
        TotalReferences = references.Count,
        InternalReferences = references.Count - externalReferences.Count,
        ExternalReferences = externalReferences,
        DirectAccessCount = directAccess.Count,
        ReflectionAccessCount = reflectionAccess.Count,
        AllReferenceLocations = references.Select(r => (r.File, r.Line, r.Context))
    }
}
```

**Step 2: Privilege Level Calculation**
```pseudocode
// Decision tree for access modifier
function DetermineAccessModifier(Control control)
{
    try
    {
        // Rule 1: Reflection usage = immediate escalation needed
        if (control.DependencyMap.ReflectionAccessCount > 0)
        {
            Flag: "CRITICAL: Reflection-based access detected"
            Flag: "Must replace reflection with proper access modifier OR encapsulation method"
            Service_LoggingUtility.Log($"[Refactor] CRITICAL: Reflection access on {control.Name}")
            return RequiresManualReview
        }
        
        // Rule 2: No external references = private
        if (control.DependencyMap.ExternalReferences.Count == 0)
        {
            Service_LoggingUtility.Log($"[Refactor] {control.Name} → private (no external refs)")
            return "private"
        }
        
        // Rule 3: Used by derived classes = protected
        if (control.DependencyMap.ExternalReferences.Any(
            where: r.IsInheritedClass AND r.AccessPattern == "direct"))
        {
            Service_LoggingUtility.Log($"[Refactor] {control.Name} → protected (inherited access)")
            return "protected"
        }
        
        // Rule 4: Used by same assembly = internal (with warning)
        if (control.DependencyMap.ExternalReferences.All(
            where: r.AssemblyName == CurrentAssembly))
        {
            Flag: "WARNING: Consider creating encapsulation method instead"
            Service_LoggingUtility.Log($"[Refactor] WARNING: {control.Name} → internal (same assembly)")
            return "internal"
        }
        
        // Rule 5: Used by external assembly = public (requires approval)
        Flag: "CRITICAL: Public UI control exposure - requires justification"
        Service_LoggingUtility.Log($"[Refactor] CRITICAL: {control.Name} requires public access - needs approval")
        return RequiresManualApproval
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(
            ex,
            Enum_ErrorSeverity.Medium,
            contextData: { "Control": control.Name, "Operation": "DetermineAccessModifier" },
            callerName: "AccessControlAnalysis"
        )
        return RequiresManualReview
    }
}
```

**Step 3: Reflection Pattern Detection**
```regex
// Search patterns for reflection usage
Patterns to detect:
1. GetField\(\s*"([^"]+)"\s*\)
2. GetProperty\(\s*"([^"]+)"\s*\)
3. GetType\(\)\.GetMember\(\s*"([^"]+)"\s*\)
4. typeof\(([^)]+)\)\.GetField\(\s*"([^"]+)"\s*\)

// For each match
foreach (match in ReflectionMatches)
{
    controlName = match.Groups[1].Value
    
    Service_LoggingUtility.Log($"[Refactor] Reflection detected: {controlName} at {match.File}:{match.LineNumber}")
    
    Flag: {
        "severity": "high",
        "issue": "Reflection bypasses compile-time checks",
        "location": $"{match.File}:{match.LineNumber}",
        "control": controlName,
        "recommendation": "Replace with direct access OR create public method",
        "codeSnippet": match.Context
    }
}
```

#### Encapsulation Pattern Generator

**Input**: Control requiring elevated access  
**Output**: Encapsulation wrapper recommendations

```pseudocode
// Template 1: Property Wrapper (for data access)
// INSTEAD OF: public txtUserName control field
// GENERATE:
/// <summary>
/// Gets or sets the user name value.
/// Encapsulates UI control to prevent direct manipulation.
/// </summary>
public property UserName
{
    get { return txtUserName.Text }
    set
    {
        // Thread-safe UI update
        if (RequiresThreadMarshaling)
            InvokeOnUIThread(() => txtUserName.Text = value)
        else
            txtUserName.Text = value
    }
}

// Template 2: Method Wrapper (for behavior)
// INSTEAD OF: internal btnSubmit control field
// GENERATE:
/// <summary>
/// Programmatically triggers the submit action.
/// Encapsulates control behavior with validation.
/// </summary>
public method PerformSubmit()
{
    if (!btnSubmit.Enabled)
    {
        Service_ErrorHandler.ShowUserError("Submit is currently disabled")
        return
    }
    
    Service_LoggingUtility.Log($"[{FormName}] PerformSubmit called programmatically")
    btnSubmit.PerformClick()
}

// Template 3: State Query (for visibility/enabled checks)
// INSTEAD OF: public panelAdvanced control field
// GENERATE:
/// <summary>
/// Gets whether the advanced panel is currently visible.
/// </summary>
public property IsAdvancedModeVisible
{
    get { return panelAdvanced.Visible }
}

/// <summary>
/// Shows or hides the advanced options panel.
/// </summary>
public method SetAdvancedModeVisibility(bool visible)
{
    try
    {
        panelAdvanced.Visible = visible
        btnToggleAdvanced.Text = visible ? "Hide Advanced" : "Show Advanced"
        Service_LoggingUtility.Log($"[{FormName}] Advanced mode: {visible}")
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(
            ex,
            Enum_ErrorSeverity.Low,
            contextData: { "Visible": visible },
            callerName: "SetAdvancedModeVisibility"
        )
    }
}
```

#### Output Format (Access Control Report)

```json
{
  "accessControlAnalysis": [
    {
      "controlName": "string",
      "currentModifier": "private|protected|internal|public",
      "recommendedModifier": "private|protected|internal|public",
      "justification": "string",
      "dependencyMap": {
        "totalReferences": "integer",
        "sameClass": "integer",
        "derivedClasses": "integer",
        "sameAssembly": "integer",
        "externalAssemblies": "integer",
        "reflectionUsage": "integer"
      },
      "externalReferences": [
        {
          "file": "string",
          "class": "string",
          "method": "string",
          "lineNumber": "integer",
          "accessPattern": "direct|reflection",
          "codeSnippet": "string"
        }
      ],
      "reflectionViolations": [
        {
          "file": "string",
          "lineNumber": "integer",
          "pattern": "GetField|GetProperty|GetType",
          "severity": "high|medium|low",
          "fix": "string"
        }
      ],
      "encapsulationAlternatives": [
        {
          "type": "property|method|event",
          "signature": "string (e.g., public string UserName { get; set; })",
          "implementation": "string (full code)",
          "benefit": "string"
        }
      ],
      "requiresManualReview": "boolean",
      "reason": "string",
      "namingViolation": {
        "hasViolation": "boolean",
        "currentName": "string",
        "expectedName": "string ({FormOrControlName}_{ControlType}_{Name})",
        "renameImpact": {
          "filesAffected": "integer",
          "totalReferences": "integer",
          "referenceLocations": [
            {
              "file": "string",
              "line": "integer",
              "context": "string (code snippet showing usage)"
            }
          ]
        }
      }
    }
  ],
  "summary": {
    "controlsReduced": "integer (count moved to private)",
    "protectedNeeded": "integer",
    "internalNeeded": "integer",
    "publicNeeded": "integer (should be zero)",
    "reflectionIssues": "integer",
    "encapsulationOpportunities": "integer",
    "namingViolations": "integer",
    "totalRenameOperations": "integer",
    "totalFilesRequiringUpdates": "integer"
  }
}
```

#### Validation Rules
- ✅ **Complete Scan**: All controls analyzed for external references
- ✅ **Zero Public Controls**: No UI control exposed as `public` (use wrappers instead)
- ✅ **Reflection Elimination**: All reflection-based access flagged and replaced
- ✅ **Inheritance Safety**: Protected access confirmed for base class scenarios
- ✅ **Naming Convention Enforcement**: All controls renamed to follow standard

---

### 🧹 Persona 4: The Code Separator
**Role:** Separation of Concerns  
**AI Execution Mode:** Code Migration & Cleanup

#### Primary Objectives
1. **Remove ALL UI initialization from main file**
2. **Preserve business logic and event handlers**
3. **Maintain code organization and readability**
4. **Ensure zero functional changes**
5. **Integrate comprehensive logging** for all migration operations

#### Migration Rules (Execute Sequentially)

**Rule 1: Constructor Purification**
```pseudocode
// BEFORE (anti-pattern)
public Constructor MyForm(string title, int userId)
{
    this.Text = title
    _userId = userId
    
    // UI initialization - MUST REMOVE
    btnSave = new Button()
    btnSave.Text = "Save"
    btnSave.Click += OnSaveClick
    this.Controls.Add(btnSave)
    
    // Data loading - CAN STAY (non-UI)
    LoadUserSettings(userId)
}

// AFTER (correct pattern with logging)
public Constructor MyForm(string title, int userId)
{
    Service_LoggingUtility.Log($"[{FormName}] Constructor called with title='{title}', userId={userId}")
    
    InitializeDesigner() // ONLY designer call - added if missing

    // Event Subscriptions - REQUIRED IN CODEBEHIND
    this.btnSave.Click += OnSaveClick
    
    // Non-UI initialization - ACCEPTABLE
    this.Text = title // Dynamic form title OK
    _userId = userId  // Field assignment OK
    
    try
    {
        // Async data loading pattern - PREFERRED
        this.Shown += async (s, e) => await LoadUserSettingsAsync(userId)
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(
            ex,
            Enum_ErrorSeverity.Medium,
            contextData: { "UserId": userId },
            callerName: "Constructor"
        )
    }
}
```

**Rule 2: Load Event Handler Cleaning**
```pseudocode
// BEFORE
private method OnFormLoad(object sender, EventArgs e)
{
    // UI creation - MUST MOVE to Designer initialization
    btnSubmit = new Button()
    btnSubmit.Location = new Point(10, 10)
    this.Controls.Add(btnSubmit)
    
    // Design-time properties - MUST MOVE to Designer initialization
    lblTitle.Font = new Font("Arial", 14, FontStyle.Bold)
    
    // Runtime data binding - CAN STAY
    cmbCategory.DataSource = ApplicationVariables.Categories
    cmbCategory.DisplayMember = "Name"
    
    // Business logic - SHOULD STAY
    await LoadInventoryDataAsync()
}

// AFTER (with logging)
private async method OnFormLoad(object sender, EventArgs e)
{
    Service_LoggingUtility.Log($"[{FormName}] Form_Load started")
    
    try
    {
        // ONLY runtime operations remain
        cmbCategory.DataSource = ApplicationVariables.Categories
        cmbCategory.DisplayMember = "Name"
        
        await LoadInventoryDataAsync()
        
        Service_LoggingUtility.Log($"[{FormName}] Form_Load completed successfully")
    }
    catch (Exception ex)
    {
        Service_ErrorHandler.HandleException(
            ex,
            Enum_ErrorSeverity.High,
            contextData: { "Event": "FormLoad" },
            callerName: "OnFormLoad",
            controlName: FormName
        )
    }
}
```

**Rule 3: Helper Method Elimination**
```pseudocode
// BEFORE
private method InitializeControls() // Anti-pattern method
{
    btnSave = new Button()
    btnCancel = new Button()
    // ... more UI code
}

public Constructor MyForm()
{
    InitializeControls() // WRONG
    Service_LoggingUtility.Log("[WARNING] InitializeControls anti-pattern detected")
}

// AFTER
// Method deleted - all code moved to Designer initialization
public Constructor MyForm()
{
    InitializeDesigner() // Only call
    Service_LoggingUtility.Log($"[{FormName}] Constructor completed - designer initialized")
}
```

#### Code Organization Preservation

**Maintain in Main File:**
```pseudocode
public partial class MyForm : ThemedForm // Must inherit from ThemedForm
{
    region Fields
    // Non-UI fields ONLY
    private readonly int _userId
    private readonly AnalyticsService _analyticsService
    // NO: private Button btnSave; (moved to Designer file)
    endregion
    
    region Properties
    // Public API properties
    public string CurrentReport { get; set; }
    // NO: UI control properties (use encapsulation wrappers)
    endregion
    
    region Constructors
    // InitializeDesigner() call + non-UI initialization
    endregion
    
    region Methods
    // Business logic methods
    private async Task LoadDataAsync() { }
    private void ProcessReport() { }
    endregion
    
    region Events
    // Event handler implementations (NOT subscriptions)
    private void BtnSave_Click(object sender, EventArgs e) { }
    endregion
    
    region Helpers
    // Non-UI helper methods
    private bool ValidateInput() { }
    endregion
    
    region Cleanup / Dispose
    // Custom disposal logic (if needed beyond Designer file)
    endregion
}
```

#### Output Format (Separation Report)

```json
{
  "codeSeparation": {
    "mainFile": {
      "path": "string",
      "removedLines": "integer",
      "removedSections": [
        {
          "type": "control-instantiation|property-assignment|event-subscription",
          "originalLocation": "Constructor|Form_Load|HelperMethod",
          "lineNumbers": "string (e.g., 45-52)",
          "movedTo": "InitializeComponent()",
          "codeSnippet": "string"
        }
      ],
      "preservedSections": [
        {
          "type": "business-logic|event-handler|field-declaration",
          "location": "string",
          "lineNumbers": "string",
          "reason": "string"
        }
      ]
    },
    "designerFile": {
      "path": "string",
      "addedLines": "integer",
      "addedSections": [
        {
          "type": "control-instantiation|property-assignment|event-subscription",
          "lineNumbers": "string",
          "codeSnippet": "string"
        }
      ]
    },
    "deletedMethods": [
      {
        "methodName": "string (e.g., InitializeControls)",
        "reason": "Replaced by InitializeComponent()",
        "lineNumbers": "string"
      }
    ]
  },
  "validation": {
    "zeroUiInMainFile": "boolean",
    "allEventsInDesigner": "boolean",
    "businessLogicPreserved": "boolean",
    "regionStructureMaintained": "boolean"
  }
}
```

#### Validation Rules
- ✅ **Constructor Purity**: Only `InitializeComponent()` + non-UI initialization
- ✅ **Zero UI Code**: No control instantiation anywhere in main file
- ✅ **Event Preservation**: All event handlers still exist and functional
- ✅ **Region Compliance**: Region structure maintained

---

### ✅ Persona 5: The Quality Validator
**Role:** Verification & Testing  
**AI Execution Mode:** Automated Testing & Compliance Verification

#### Primary Objectives
1. **Verify compilation** without errors
2. **Validate Designer compatibility**
3. **Test functional equivalence**
4. **Check for breaking changes**
5. **Confirm naming convention compliance**
6. **Validate error handling patterns**

#### Validation Test Suite

**Test 1: Compilation Check**
```pseudocode
// Execute and capture output
buildResult = ExecuteBuild(noRestore: true, verbosity: "quiet")
exitCode = buildResult.ExitCode

// Parse results
errors = ParseOutput(buildResult.Output, pattern: "error")
warnings = ParseOutput(buildResult.Output, pattern: "warning")

// Log results
Service_LoggingUtility.Log($"[Refactor] Build: {exitCode == 0 ? 'SUCCESS' : 'FAILED'}, Errors: {errors.Count}, Warnings: {warnings.Count}")

// Report
{
    "compilationTest": {
        "passed": exitCode == 0,
        "errors": errors.Count,
        "warnings": warnings.Count,
        "errorDetails": errors
    }
}
```

**Test 2: Designer Compatibility Check**
```pseudocode
// Automated Designer validation
try
{
    designerHost = CreateDesignerHost()
    form = designerHost.CreateComponent(FormType)
    
    // Test 1: Serialization
    serializer = new CodeDomSerializer()
    code = serializer.Serialize(designerHost, form)
    
    // Test 2: Control enumeration
    controls = GetAllControls(form)
    selectableControls = controls.Where(c => c.Site != null)
    
    // Test 3: Property editability
    descriptor = GetPropertyDescriptor(form, "Text")
    
    Service_LoggingUtility.Log($"[Refactor] Designer validation: {controls.Count} controls, {selectableControls.Count} selectable")
    
    Report: {
        "designerCompatibility": {
            "formLoadsInDesigner": true,
            "serializationSucceeds": code != null,
            "controlsCount": controls.Count,
            "selectableControls": selectableControls.Count,
            "propertiesEditable": descriptor != null
        }
    }
}
catch (Exception ex)
{
    Service_ErrorHandler.HandleException(
        ex,
        Enum_ErrorSeverity.High,
        contextData: { "Test": "DesignerCompatibility" },
        callerName: "ValidateDesigner"
    )
    
    Report: {
        "designerCompatibility": {
            "formLoadsInDesigner": false,
            "error": ex.Message,
            "stackTrace": ex.StackTrace
        }
    }
}
```

**Test 3: Functional Equivalence Verification**
```pseudocode
// Compare runtime behavior before/after refactoring

// Control state snapshot (before refactoring)
beforeState = new ControlState
{
    Controls = CaptureControlHierarchy(form),
    EventHandlers = CaptureEventSubscriptions(form),
    PropertyValues = CaptureControlProperties(form)
}

// After refactoring
afterState = new ControlState
{
    Controls = CaptureControlHierarchy(form),
    EventHandlers = CaptureEventSubscriptions(form),
    PropertyValues = CaptureControlProperties(form)
}

// Comparison
differences = CompareStates(beforeState, afterState)

if (differences.Count > 0)
{
    Service_LoggingUtility.Log($"[Refactor] WARNING: {differences.Count} differences detected in functional equivalence test")
}

Report: {
    "functionalEquivalence": {
        "identical": differences.Count == 0,
        "differences": differences.Select(d => {
            d.ControlName,
            d.PropertyName,
            d.BeforeValue,
            d.AfterValue
        })
    }
}
```

**Test 4: External Reference Validation**
```pseudocode
// For each modified access modifier
foreach (change in AccessModifierChanges)
{
    // Re-run reference search
    references = SearchSolution($".{change.ControlName}")
    
    // Check for broken references
    brokenReferences = references.Where(r =>
        r.AccessLevel > change.NewModifier.AccessLevel)
    
    if (brokenReferences.Any())
    {
        Service_LoggingUtility.Log($"[Refactor] ERROR: {brokenReferences.Count} broken references for {change.ControlName}")
        
        Report: {
            "brokenReferences": brokenReferences.Select(r => {
                r.File,
                r.LineNumber,
                r.CodeSnippet,
                "fix": $"Change {change.ControlName} to {r.RequiredModifier}"
            })
        }
    }
}
```

**Test 5: Naming Convention Compliance**
```pseudocode
// Validate all controls follow naming convention
nonCompliantControls = []

foreach (control in AllControls)
{
    expectedName = $"{FormOrControlName}_{control.Type}_{control.Purpose}"
    
    if (control.Name != expectedName)
    {
        nonCompliantControls.Add({
            "currentName": control.Name,
            "expectedName": expectedName,
            "location": control.DeclaredIn
        })
        
        Service_LoggingUtility.Log($"[Refactor] Naming violation: {control.Name} should be {expectedName}")
    }
}

Report: {
    "namingCompliance": {
        "compliant": nonCompliantControls.Count == 0,
        "violations": nonCompliantControls.Count,
        "details": nonCompliantControls
    }
}
```

**Test 6: Error Handling Pattern Validation**
```pseudocode
// Search for direct message box usage
directMessageBoxCalls = SearchCodebase(pattern: "MessageBox.Show|ShowDialog|Alert")

foreach (call in directMessageBoxCalls)
{
    Service_LoggingUtility.Log($"[Refactor] ERROR HANDLING VIOLATION: Direct message box at {call.File}:{call.Line}")
}

Report: {
    "errorHandlingCompliance": {
        "compliant": directMessageBoxCalls.Count == 0,
        "violations": directMessageBoxCalls.Count,
        "locations": directMessageBoxCalls.Select(c => {
            "file": c.File,
            "line": c.Line,
            "code": c.CodeSnippet,
            "fix": "Replace with Service_ErrorHandler.ShowUserError() or appropriate method"
        })
    }
}
```

#### Output Format (Validation Report)

```json
{
  "validationResults": {
    "compilation": {
      "passed": "boolean",
      "errors": "integer",
      "warnings": "integer",
      "errorDetails": ["string"]
    },
    "designerCompatibility": {
      "formLoadsInDesigner": "boolean",
      "allControlsSelectable": "boolean",
      "propertiesEditable": "boolean",
      "serializationWorks": "boolean",
      "errorMessage": "string|null"
    },
    "functionalEquivalence": {
      "identical": "boolean",
      "differences": [
        {
          "controlName": "string",
          "aspect": "property|event|hierarchy",
          "before": "string",
          "after": "string",
          "impact": "critical|medium|low"
        }
      ]
    },
    "externalReferences": {
      "allValid": "boolean",
      "brokenReferences": [
        {
          "file": "string",
          "lineNumber": "integer",
          "controlName": "string",
          "issue": "string",
          "fix": "string"
        }
      ]
    },
    "namingCompliance": {
      "compliant": "boolean",
      "violations": "integer",
      "details": [
        {
          "currentName": "string",
          "expectedName": "string",
          "location": "string"
        }
      ]
    },
    "errorHandlingCompliance": {
      "compliant": "boolean",
      "violations": "integer",
      "locations": [
        {
          "file": "string",
          "line": "integer",
          "code": "string",
          "fix": "string"
        }
      ]
    },
    "manualTestChecklist": [
      {
        "test": "Open form in Designer",
        "passed": "boolean|null",
        "notes": "string"
      },
      {
        "test": "Select all controls in Designer",
        "passed": "boolean|null"
      },
      {
        "test": "Edit properties via Properties window",
        "passed": "boolean|null"
      },
      {
        "test": "Run application and test all features",
        "passed": "boolean|null"
      },
      {
        "test": "Verify event handlers fire correctly",
        "passed": "boolean|null"
      },
      {
        "test": "Verify all controls follow naming convention",
        "passed": "boolean|null"
      },
      {
        "test": "Verify Service_ErrorHandler used (no direct MessageBox)",
        "passed": "boolean|null"
      }
    ]
  },
  "overallStatus": "pass|fail|requires-manual-verification",
  "criticalIssues": ["string"],
  "recommendations": ["string"]
}
```

#### Validation Rules
- ✅ **Zero Compilation Errors**: Build succeeds without errors
- ✅ **Designer Loads**: Form opens in IDE Design View
- ✅ **All Controls Editable**: Every control selectable and property-editable
- ✅ **Functional Parity**: Application behavior identical to before refactoring
- ✅ **No Broken References**: All external dependencies still valid
- ✅ **Naming Convention Compliance**: All controls follow {FormOrControlName}_{ControlType}_{Name}
- ✅ **Error Handling Compliance**: All errors use Service_ErrorHandler
- ✅ **Logging Integration**: All operations logged via Service_LoggingUtility

---

## Execution Summary

### Critical Success Criteria

1. **Code Quality**
   - Zero compilation errors
   - All warnings addressed or documented
   - Code follows framework conventions

2. **Designer Compliance**
   - Form loads in IDE Designer without errors
   - All controls selectable and editable
   - Properties window shows all design-time properties

3. **Separation of Concerns**
   - Zero UI initialization in main file
   - All design-time code in Designer file
   - Business logic preserved and functional

4. **Access Control**
   - Minimal access modifiers (prefer private)
   - Encapsulation wrappers for external access
   - Zero reflection-based control access

5. **Naming Standards**
   - All controls follow {FormOrControlName}_{ControlType}_{Name}
   - Codebase-wide renaming completed for violations
   - All references updated across all files

6. **Error Handling**
   - All errors use Service_ErrorHandler methods
   - Zero direct MessageBox.Show() calls
   - Proper error context provided

7. **Logging**
   - All refactoring operations logged
   - All runtime operations logged
   - Structured CSV format maintained

### Post-Refactoring Deliverables

1. **Analysis Report** (JSON format per Persona 1)
2. **Designer Validation Report** (JSON format per Persona 2)
3. **Access Control Report** (JSON format per Persona 3)
4. **Code Separation Report** (JSON format per Persona 4)
5. **Quality Validation Report** (JSON format per Persona 5)
6. **Naming Compliance Report** with rename impact analysis
7. **Error Handling Audit Report** with fix recommendations

---

## Language-Agnostic Implementation Notes

This prompt is designed to work with any UI framework that uses designer patterns. When implementing:

1. **Replace framework-specific types** with generic terms:
   - `System.Windows.Forms.Button` → `Button control`
   - `InitializeComponent()` → `InitializeDesigner()`
   - `Form_Load` → `OnFormLoad event`

2. **Adapt patterns to target language**:
   - C#: Use exact syntax shown in examples
   - VB.NET: Convert to VB syntax
   - Other frameworks: Map to equivalent patterns

3. **Maintain core principles**:
   - Separation of designer vs business logic
   - Access modifier minimization
   - Property categorization (design-time vs runtime)
   - Naming conventions adapted to framework standards

4. **Preserve integrations**:
   - Service_ErrorHandler patterns apply universally
   - Service_LoggingUtility patterns apply universally
   - Naming conventions apply with framework-appropriate syntax
