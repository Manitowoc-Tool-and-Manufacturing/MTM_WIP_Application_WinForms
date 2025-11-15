# SuggestionTextBox Enhancement Implementation Prompt

## OBJECTIVE
Implement selected enhancements from the PROPOSED_IMPROVEMENTS section of a SuggestionTextBox control file. This prompt handles the workflow for cherry-picked improvements, determines whether to use /speckit.specify for complex changes, and implements simpler enhancements directly.

## INPUT REQUIREMENTS

Before executing this prompt:
1. **Target File**: Full path to the .cs file containing PROPOSED_IMPROVEMENTS section with checkboxes
2. **Selected Enhancements**: User has checked boxes next to desired improvements

## EXECUTION STEPS

### Step 1: Parse Selected Enhancements

Read the target file and extract all checked enhancements:

```regex
// \[x\] ([A-Z]+-\d+): (.+)
```

For each checked enhancement, extract:
- **ID**: Enhancement identifier (e.g., UI-001, PERF-003, ARCH-002)
- **Title**: Brief description
- **Priority**: Rating 1-10
- **Scope**: Rating 1-10 (refactor scope)
- **Category**: UI, UX, VAL, PERF, ARCH, DATA, SEC, ACCESS, TEST, DOC, BATCH
- **[NEEDS /speckit.specify]** marker (if present)

### Step 2: Categorize Enhancements by Implementation Strategy

Separate selected enhancements into three groups:

#### Group A: Direct Implementation (Scope 1-4, No [NEEDS /speckit.specify])
These can be implemented immediately in the current session:
- Small UI changes (tooltips, visual indicators)
- Simple validation additions
- Performance optimizations (caching, debouncing)
- Documentation improvements
- Accessibility tweaks

**Characteristics:**
- Changes isolated to single file or small set of files
- No database schema changes
- No new major features
- Low risk of breaking existing functionality

#### Group B: Feature Branch Required (Has [NEEDS /speckit.specify] marker OR Scope 5-7)
These need dedicated feature branches via /speckit.specify:
- New forms or major UI components
- Architectural refactors (MVVM, Command pattern)
- Database schema changes
- Security/permission systems
- Batch operations

**Characteristics:**
- Multi-file changes
- Requires testing across multiple scenarios
- May affect other parts of codebase
- Benefits from spec-driven development

#### Group C: Constitution Changes (Scope 8-10, architectural fundamentals)
These require constitutional amendments and team approval:
- Major architectural patterns (full MVVM migration)
- Codebase-wide refactors
- Breaking changes to established patterns
- New development standards

**Characteristics:**
- Affects how entire team works
- Requires updating templates and guidelines
- Needs architectural review
- Implementation spans multiple sprints

### Step 3: Present Implementation Plan

Display categorized enhancements to user:

```markdown
## Enhancement Implementation Plan

### ✅ Selected Enhancements: [COUNT]

┌─────────────────────────────────────────────────────────────────────────────┐
│ GROUP A: DIRECT IMPLEMENTATION ([COUNT] enhancements)                      │
│ These will be implemented in current session                               │
└─────────────────────────────────────────────────────────────────────────────┘

1. [ID]: [Title]
   Priority: [X]/10 | Scope: [Y]/10 | Est. Time: [Z] minutes

2. [ID]: [Title]
   Priority: [X]/10 | Scope: [Y]/10 | Est. Time: [Z] minutes

┌─────────────────────────────────────────────────────────────────────────────┐
│ GROUP B: FEATURE BRANCH REQUIRED ([COUNT] enhancements)                    │
│ Each will create a new feature branch via /speckit.specify                 │
└─────────────────────────────────────────────────────────────────────────────┘

1. [ID]: [Title]
   Priority: [X]/10 | Scope: [Y]/10 | [NEEDS /speckit.specify]
   Branch: [suggested-branch-name]
   
2. [ID]: [Title]
   Priority: [X]/10 | Scope: [Y]/10 | [NEEDS /speckit.specify]
   Branch: [suggested-branch-name]

┌─────────────────────────────────────────────────────────────────────────────┐
│ GROUP C: CONSTITUTION CHANGES ([COUNT] enhancements)                       │
│ Requires team review and constitutional amendment                          │
└─────────────────────────────────────────────────────────────────────────────┘

1. [ID]: [Title]
   Priority: [X]/10 | Scope: [Y]/10
   Impact: [Description of codebase-wide impact]

## Recommended Implementation Order

1. **Group A (Immediate)**: Implement now ([TIME] total)
2. **Group B (Feature Branches)**: Create specs, implement in order of priority
3. **Group C (Team Review)**: Schedule architectural review meeting

**Proceed with Group A now? (Y/N)**
**Create Group B feature branches? (Y/N for each, or ALL)**
**Group C requires manual team coordination (no automated action)**
```

### Step 4: Implement Group A Enhancements

For each Group A enhancement (if user confirms):

#### 4.1: Create Implementation TODO List

Use manage_todo_list to track:
- [ ] Read enhancement specification from PROPOSED_IMPROVEMENTS
- [ ] Identify files to modify
- [ ] Implement changes
- [ ] Test changes
- [ ] Update documentation
- [ ] Build and verify
- [ ] Mark enhancement as complete (change [ ] to [x])

#### 4.2: Implementation Patterns by Category

**UI Enhancements (UI-XXX):**
```csharp
// Example: UI-001 Visual indication when RequiresColorCode changes
private Color _originalCheckboxBackColor;
private bool _requiresColorCodeChanged = false;

private void Control_Edit_PartID_CheckBox_RequiresColorCode_CheckedChanged(object? sender, EventArgs e)
{
    if (_currentPart == null) return;
    
    _requiresColorCodeChanged = (Control_Edit_PartID_CheckBox_RequiresColorCode.Checked != _originalRequiresColorCode);
    
    // Visual feedback
    if (_requiresColorCodeChanged)
    {
        Control_Edit_PartID_CheckBox_RequiresColorCode.BackColor = Color.FromArgb(255, 255, 200); // Light yellow
    }
    else
    {
        Control_Edit_PartID_CheckBox_RequiresColorCode.BackColor = _originalCheckboxBackColor;
    }
}

// Example: UI-002 Add tooltips
private void ConfigureTooltips()
{
    var tooltip = new ToolTip
    {
        AutoPopDelay = 5000,
        InitialDelay = 500,
        ReshowDelay = 100
    };
    
    tooltip.SetToolTip(Control_Edit_PartID_CheckBox_RequiresColorCode,
        "When checked, work orders for this part will require a color code selection.\n" +
        "Only enable for parts that come in multiple colors (e.g., cables, housings).");
}
```

**Validation Enhancements (VAL-XXX):**
```csharp
// Example: VAL-001 Real-time part number format validation
private void itemNumberTextBox_TextChanged(object? sender, EventArgs e)
{
    if (!ValidatePartNumberFormat(itemNumberTextBox.Text))
    {
        itemNumberTextBox.BackColor = Color.FromArgb(255, 230, 230); // Light red
        errorProvider.SetError(itemNumberTextBox, "Part number format: R-XXX-XX or F-XXX-XX");
    }
    else
    {
        itemNumberTextBox.BackColor = Color.White;
        errorProvider.SetError(itemNumberTextBox, string.Empty);
    }
}

private bool ValidatePartNumberFormat(string partNumber)
{
    if (string.IsNullOrWhiteSpace(partNumber)) return true; // Allow empty during typing
    
    // Match patterns like R-ABC-01, F-XYZ-123, etc.
    return Regex.IsMatch(partNumber, @"^[A-Z]-[A-Z0-9]+-[A-Z0-9]+$", RegexOptions.IgnoreCase);
}
```

**Performance Enhancements (PERF-XXX):**
```csharp
// Example: PERF-001 Data provider caching with TTL
private static Dictionary<string, CachedData<List<string>>> _suggestionCache = new();

private class CachedData<T>
{
    public T Data { get; set; }
    public DateTime ExpiresAt { get; set; }
    
    public bool IsExpired => DateTime.Now > ExpiresAt;
}

private async Task<List<string>> GetPartNumberSuggestionsAsync()
{
    const string cacheKey = "PartNumbers";
    
    // Check cache first
    if (_suggestionCache.TryGetValue(cacheKey, out var cached) && !cached.IsExpired)
    {
        LoggingUtility.Log($"[{nameof(Control_Edit_PartID)}] Using cached part numbers");
        return cached.Data;
    }
    
    // Load from database
    var suggestions = await GetSuggestionsFromDatabaseAsync("md_part_ids_Get_All", "PartID", "part numbers");
    
    // Cache for 5 minutes
    _suggestionCache[cacheKey] = new CachedData<List<string>>
    {
        Data = suggestions,
        ExpiresAt = DateTime.Now.AddMinutes(5)
    };
    
    return suggestions;
}
```

**Accessibility Enhancements (ACCESS-XXX):**
```csharp
// Example: ACCESS-001 Screen reader labels
private void ConfigureAccessibility()
{
    Control_Edit_PartID_TextBox_Part.AccessibleName = "Part Number";
    Control_Edit_PartID_TextBox_Part.AccessibleDescription = "Enter or select the part number to edit";
    
    Control_Edit_PartID_TextBox_ItemType.AccessibleName = "Item Type";
    Control_Edit_PartID_TextBox_ItemType.AccessibleDescription = "Select the category for this part (Raw, WIP, Finished Goods, etc.)";
    
    Control_Edit_PartID_CheckBox_RequiresColorCode.AccessibleName = "Requires Color Code";
    Control_Edit_PartID_CheckBox_RequiresColorCode.AccessibleDescription = "Check if work orders for this part must specify a color code";
    
    saveButton.AccessibleName = "Save Changes";
    saveButton.AccessibleDescription = "Save all changes to this part";
}

// Example: ACCESS-002 Tab order optimization
private void ConfigureTabOrder()
{
    Control_Edit_PartID_TextBox_Part.TabIndex = 0;
    itemNumberTextBox.TabIndex = 1;
    Control_Edit_PartID_TextBox_ItemType.TabIndex = 2;
    Control_Edit_PartID_CheckBox_RequiresColorCode.TabIndex = 3;
    saveButton.TabIndex = 4;
    resetButton.TabIndex = 5;
}
```

**Documentation Enhancements (DOC-XXX):**
```markdown
// Example: DOC-001 Create user guide
Create file: Documentation/UserGuides/PartEditingWorkflow.md

# Part Editing Workflow Guide

## Overview
This guide explains how to edit existing parts in the MTM WIP Application.

## When to Edit Parts
- Changing item type classification (Raw → WIP → Finished Goods)
- Enabling/disabling color code requirement
- Correcting part number typos (with caution - see warnings)

## Step-by-Step Instructions

### 1. Select the Part
[Screenshot of part selection]
- Click in the "Part Number" field
- Start typing the part number or press F4 to see full list
- Select the part from the suggestion list

### 2. Review Current Settings
[Screenshot of loaded part]
- Part number: The unique identifier
- Item Type: Current classification
- Requires Color Code: Whether work orders need color selection
- Issued By: Who created this part

### 3. Make Changes
[Screenshot of editing]
- Modify any field except "Issued By"
- See visual indicators for changed fields

### 4. Save Changes
[Screenshot of save confirmation]
- Click "Save" or press Ctrl+S
- Review the confirmation message
- Changes take effect immediately

## Common Issues and Solutions

### "Part number already exists"
**Cause**: The new part number is already in use
**Solution**: Choose a different part number or edit the existing part instead

### "Part is in active work orders"
**Warning**: Changing RequiresColorCode affects ongoing work
**Solution**: Verify with production before making changes

## RequiresColorCode Decision Guide

**Enable for parts that:**
- Come in multiple colors (cables, housings, labels)
- Require color tracking for inventory accuracy
- Must match specific customer color requirements

**Leave disabled for parts that:**
- Only available in one color (raw materials, unpainted parts)
- Color is not relevant to functionality
- Color is added later in assembly
```

#### 4.3: Apply Constitution Compliance Patterns

Ensure all implementations follow constitution:
- Use Service_ErrorHandler for errors
- Use LoggingUtility for logging
- Async/await for I/O operations
- Proper event subscription/unsubscription in Dispose
- XML documentation on new methods

### Step 5: Handle Group B Feature Branches

For each Group B enhancement (if user confirms):

#### 5.1: Generate Feature Description

Extract from enhancement details and create natural language description:

```text
[Enhancement Title]

[USER BENEFIT section from enhancement]

[WHY NEEDED section from enhancement]

[IMPLEMENTATION notes as technical context]

Priority: [X]/10
Scope Impact: [Y]/10 (affects [estimate] files across [areas])
```

#### 5.2: Invoke /speckit.specify

For each Group B enhancement user confirms:

```markdown
**Ready to create feature branch for: [ID] - [Title]**

This enhancement requires a dedicated feature branch because:
- Scope: [Y]/10 (affects multiple files/systems)
- [Specific reason from NEEDS /speckit.specify marker]

Feature description:
---
[Generated description from 5.1]
---

**Proceed with /speckit.specify? (Y/N)**
```

If Yes:
- Run instructions from suggestiontextbox-constitution-audit.prompt.md Step 4 (call speckit.specify.prompt.md)
- Pass generated feature description as $ARGUMENTS
- Let speckit.specify handle branch creation, spec generation, planning

#### 5.3: Track Feature Branch Creation

After each /speckit.specify invocation:
- Record branch name and spec file path
- Add to summary report
- Continue to next Group B enhancement (if any)

### Step 6: Handle Group C Constitution Changes

For Group C enhancements:

```markdown
**CONSTITUTION CHANGE REQUIRED**

The following enhancements require constitutional amendments:

1. [ID]: [Title]
   Scope: [X]/10 - Affects fundamental architecture patterns
   Impact: [Detailed description]
   
   **Required Actions:**
   - Schedule architectural review meeting
   - Update .specify/memory/constitution.md
   - Update code templates in .specify/templates/
   - Update instruction files in .github/instructions/
   - Propagate changes to all active branches
   - Update team documentation

**These cannot be automated. Manual coordination required.**

Create GitHub issue for tracking? (Y/N)
```

If Yes, create issue with detailed description and checklist.

### Step 7: Mark Completed Enhancements

After successful implementation:

For each completed enhancement, update the PROPOSED_IMPROVEMENTS section:

```csharp
// Change from:
// [ ] UI-001: Visual indication when RequiresColorCode flag changes

// To:
// [x] UI-001: Visual indication when RequiresColorCode flag changes
//     ✅ Implemented: 2025-11-15
//     Files Modified: Control_Edit_PartID.cs (added CheckedChanged handler)
```

### Step 8: Build and Verify

After all Group A implementations:

```powershell
dotnet build
```

Verify:
- [ ] Build succeeds with no errors
- [ ] No new warnings introduced
- [ ] All implemented enhancements tested manually
- [ ] Documentation updated (if applicable)

### Step 9: Generate Summary Report

```markdown
## Enhancement Implementation Summary

**Date**: [DATE]
**File**: [TARGET_FILE]
**Total Selected**: [COUNT] enhancements

### ✅ Implemented Directly (Group A)
1. [ID]: [Title] - ✅ Complete ([TIME] minutes)
2. [ID]: [Title] - ✅ Complete ([TIME] minutes)
3. [ID]: [Title] - ✅ Complete ([TIME] minutes)

**Total Time**: [X] minutes
**Files Modified**: [LIST]
**Build Status**: ✅ Success

### 🌿 Feature Branches Created (Group B)
1. [ID]: [Title]
   Branch: [BRANCH_NAME]
   Spec: [SPEC_FILE]
   Status: Ready for implementation

2. [ID]: [Title]
   Branch: [BRANCH_NAME]
   Spec: [SPEC_FILE]
   Status: Ready for implementation

### 🔍 Pending Review (Group C)
1. [ID]: [Title]
   Issue: #[ISSUE_NUMBER]
   Status: Awaiting architectural review

## Next Steps

1. **Test Group A changes** thoroughly in development environment
2. **Review feature specs** for Group B branches before implementation
3. **Schedule team meeting** to discuss Group C constitutional changes

## Verification Checklist

Group A (Direct Implementation):
- [ ] All checkboxes marked [x] in PROPOSED_IMPROVEMENTS
- [ ] Build succeeds
- [ ] Manual testing completed
- [ ] Constitution compliance verified
- [ ] No regressions in existing functionality

Group B (Feature Branches):
- [ ] Specs reviewed for completeness
- [ ] Implementation approach validated
- [ ] Dependencies identified
- [ ] Ready for development

Group C (Constitution Changes):
- [ ] Architectural review scheduled
- [ ] Impact analysis documented
- [ ] Team consensus on approach
```

## EXAMPLE USAGE

**User checks boxes in Control_Edit_PartID.cs:**
```csharp
// [x] UI-001: Visual indication when RequiresColorCode flag changes
// [x] UI-002: Add tooltips explaining RequiresColorCode implications
// [ ] UI-003: Create validation summary panel
// [x] PERF-001: Implement data provider result caching
// [x] ARCH-001: Extract common SuggestionTextBox configuration [NEEDS /speckit.specify]
// [x] DOC-001: Create user guide for part editing workflow
```

**Agent categorizes:**
- Group A: UI-001, UI-002, PERF-001, DOC-001 (4 items, ~45 minutes)
- Group B: ARCH-001 (1 item, needs feature branch)
- Group C: None

**Agent implements:**
1. Directly implements UI-001, UI-002, PERF-001, DOC-001
2. Creates feature branch for ARCH-001 via /speckit.specify
3. Marks all as complete
4. Builds successfully
5. Provides summary report

## VERSION
v1.0 - 2025-11-15

## RELATED PROMPTS
- `suggestiontextbox-constitution-audit.prompt.md` - Generates PROPOSED_IMPROVEMENTS
- `speckit.specify.prompt.md` - Creates feature branches for complex enhancements
