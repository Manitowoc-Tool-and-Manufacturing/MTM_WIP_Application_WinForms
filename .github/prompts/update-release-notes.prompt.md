# Update Release Notes Prompt

**Purpose**: Maintain user-friendly release notes in `RELEASE_NOTES.md` based on technical changes documented in `PatchNotes.md`

**Target Audience**: End users, IT support staff, deployment engineers, and executives

---

## Prompt Instructions

When updating release notes, follow this workflow:

### Step 1: Review Technical Changes

Read the latest entry in `PatchNotes.md` to understand:
- What was changed (files, methods, violations fixed)
- Why it was changed (spec compliance, bug fix, feature request)
- Impact of the change (breaking changes, performance, user experience)
- Testing performed and validation results

### Step 2: Translate Technical to User-Friendly

Transform technical details into business impact:

**Technical Language** → **User-Friendly Language**:
- "Fixed 9 fire-and-forget async patterns" → "Improved error logging reliability during application shutdown"
- "Converted event handlers to async void" → "Enhanced error handling to prevent data loss"
- "Eliminated MessageBox.Show violations" → "Modern error dialogs with retry capabilities"
- "FR-008 Service_ErrorHandler adoption" → "Consistent error handling across the application"

### Step 3: Update Release Notes Sections

Update `RELEASE_NOTES.md` with the following structure:

#### 📋 Release Summary (Required)
- Brief 2-3 sentence overview of the release
- Key highlights (3-5 bullet points)
- Deployment risk assessment (🟢 Low / 🟡 Medium / 🔴 High)

#### 🎉 What's New (User-facing features)
- New capabilities users will notice
- UI improvements and enhancements
- Workflow optimizations
- Performance improvements

#### 🔧 Technical Changes (Developer-focused)
- Database compliance work
- Architecture improvements
- Code quality enhancements
- Files refactored with compliance metrics

#### 🐛 Bug Fixes (Prioritized by severity)
- **🔴 Critical**: Data loss, crashes, security issues
- **🟡 Moderate**: Functional issues, performance degradation
- **🟢 Minor**: UI glitches, cosmetic issues

Each bug fix should include:
- **Issue**: What was broken
- **Impact**: How it affected users/system
- **Fix**: What was done to resolve it
- **Risk**: Deployment risk level

#### ⚠️ Known Issues (Active limitations)
- Description of issue
- Impact assessment
- Workaround (if available)
- Status (tracked, planned, wontfix)

#### 📦 Deployment Notes
- **Installation Steps**: PowerShell commands for deployment
- **Database Changes**: Schema/procedure updates required
- **Rollback Procedure**: Steps to revert if needed
- **Rollback Risk**: Assessment of rollback difficulty

#### ✅ Testing Checklist
- **Pre-Deployment Validation**: Tests to run before production
- **Post-Deployment Verification**: Smoke tests for production
- **Monitoring**: What to watch for first 24 hours

#### 📚 Documentation
- Links to technical specs, compliance reports, user guides
- Related documentation updates

---

## Template Variables

When creating a new release entry, fill in these variables:

```markdown
**Version**: [MAJOR.MINOR.PATCH]
**Build**: [MAJOR.MINOR.PATCH.BUILD]
**Release Date**: [YYYY-MM-DD]
**Release Type**: [Major Release / Minor Release / Patch / Hotfix]
**Deployment Risk**: [🟢 Low / 🟡 Medium / 🔴 High]
```

---

## Examples

### Example 1: Bug Fix Translation

**PatchNotes.md** (Technical):
```markdown
### Change 4: OnFormClosing - CRITICAL PATH (Line 891)

**Before**:
_ = Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "MainForm_OnFormClosing");

**After**:
await Dao_ErrorLog.HandleException_GeneralError_CloseApp(ex, callerName: "MainForm_OnFormClosing");

**Impact**: Application is shutting down when this executes. Without await, error logs 
could be lost when process terminates before async operation completes.
```

**RELEASE_NOTES.md** (User-Friendly):
```markdown
#### 🐛 Bug Fixes

**🔴 Application Shutdown Error Logging** (MainForm.cs, Line 891)
- **Issue**: Error logs could be lost when application crashes during shutdown
- **Impact**: Missing diagnostic information made troubleshooting production issues difficult
- **Fix**: OnFormClosing now properly awaits error logging before process termination
- **Risk**: Low - preserves existing shutdown behavior while ensuring log completion
```

---

### Example 2: Technical Change Translation

**PatchNotes.md** (Technical):
```markdown
## Control_InventoryTab.cs - Complete Database Layer Standardization

Fixed 10 MessageBox.Show violations, added Service_DebugTracer to 7 methods,
fixed 3 column naming bugs.
```

**RELEASE_NOTES.md** (User-Friendly):
```markdown
#### 🔧 Technical Changes

**Database Layer Standardization (Phase 2.5)**

**FR-008 Service_ErrorHandler Adoption**:
- Eliminated 20 MessageBox.Show violations across boot sequence and UI forms
- Standardized error handling through Service_ErrorHandler API
- Modern error dialogs with retry, copy, and technical detail tabs

**Files Refactored**:
- ✅ `Controls/MainForm/Control_InventoryTab.cs` - 10 methods upgraded
```

---

## Quality Checklist

Before publishing release notes, verify:

- [ ] **User-Friendly Language**: No technical jargon in user-facing sections
- [ ] **Complete Impact Assessment**: All changes have risk and impact documented
- [ ] **Deployment Guidance**: Clear installation and rollback procedures
- [ ] **Testing Checklists**: Comprehensive pre/post deployment tests
- [ ] **Documentation Links**: All referenced documents are accessible
- [ ] **Known Issues**: Active limitations are documented with workarounds
- [ ] **Version Consistency**: Version numbers match across all files
- [ ] **Grammar and Spelling**: Professional presentation
- [ ] **Emoji Usage**: Consistent visual indicators (🔴🟡🟢 for severity)

---

## Cross-Reference Requirements

Ensure consistency across documentation files:

| File | Purpose | Update Trigger |
|------|---------|----------------|
| **RELEASE_NOTES.md** | User-friendly release notes | Every release / major fix |
| **PatchNotes.md** | Technical implementation details | Every code change |
| **CHANGELOG.md** | Historical version log | Every release |
| **AGENTS.md** | Development guidelines | Architecture changes |

---

## Deployment Risk Assessment Guide

Use this rubric to assign deployment risk levels:

### 🟢 Low Risk
- Bug fixes with no behavior changes
- UI cosmetic improvements
- Documentation updates
- No database schema changes
- Easy rollback (copy files back)

### 🟡 Medium Risk
- New features with limited scope
- Database compliance refactoring
- Error handling improvements
- Minor stored procedure updates
- Moderate rollback complexity

### 🔴 High Risk
- Major architecture changes
- Database schema migrations
- Breaking API changes
- Authentication/security updates
- Complex rollback procedure required

---

## Audience-Specific Views

### For End Users
Focus on: What's New, Bug Fixes, Known Issues

### For IT Support Staff
Focus on: Deployment Notes, Testing Checklist, Known Issues, Support section

### For Developers
Focus on: Technical Changes, Documentation links, PatchNotes.md reference

### For Executives
Focus on: Release Summary, Deployment Risk, Key Highlights

---

## Prompt Usage Example

**User Request**: "Update release notes for MainForm.cs async/await compliance fix"

**Agent Workflow**:
1. Read `PatchNotes.md` entry for MainForm.cs (latest entry)
2. Extract key facts:
   - 9 fire-and-forget patterns fixed
   - OnFormClosing is critical path
   - No database changes
   - Build succeeded with no new warnings
3. Translate to user language:
   - "Improved error logging reliability"
   - "Critical fix for shutdown error capture"
   - "Safe to deploy - no database updates needed"
4. Update `RELEASE_NOTES.md`:
   - Add to "What's New" if user-visible
   - Add to "Technical Changes" section
   - Add critical fix to "Bug Fixes" with 🔴 severity
   - Update deployment notes (low risk)
   - Add testing checklist items
5. Verify cross-references and links
6. Run quality checklist

---

## Common Pitfalls to Avoid

❌ **Don't**: Use code snippets in user-facing sections
✅ **Do**: Describe what the code change accomplishes for users

❌ **Don't**: Reference line numbers or method names in summaries
✅ **Do**: Describe the functional impact in business terms

❌ **Don't**: Assume technical knowledge (e.g., "async/await patterns")
✅ **Do**: Use plain language (e.g., "background task handling")

❌ **Don't**: Skip deployment guidance for "small" changes
✅ **Do**: Always provide installation steps and rollback procedures

❌ **Don't**: Forget to update version numbers in multiple places
✅ **Do**: Maintain version consistency across all documentation

---

## Integration with Database Compliance Initiative

When documenting database compliance work:

1. **Link to Spec**: Reference the FR-xxx requirement being addressed
2. **Show Metrics**: Include before/after compliance percentages
3. **List Files**: Enumerate all files refactored with change counts
4. **Explain Business Value**: Connect technical compliance to user benefits
   - Better error messages → Less downtime
   - Proper async patterns → Fewer crashes
   - Standardized logging → Faster issue resolution

---

## Maintenance Schedule

**Update Frequency**:
- **Real-time**: When hotfix deployed (critical bugs)
- **Weekly**: During active development sprints
- **Per Release**: For minor/major version bumps
- **Quarterly**: Review and archive old releases

**Archival Process**:
- Move releases > 6 months old to "Previous Releases" section
- Keep only summary (version, date, key highlights)
- Link to detailed CHANGELOG.md for historical reference

---

## Agent Guidelines

When an agent is asked to "update release notes":

1. **Verify latest PatchNotes.md entry** - Read the most recent technical documentation
2. **Identify target audience** - Who needs to know about this change?
3. **Translate technical to business** - Use the examples and rubric above
4. **Assess deployment risk** - Use the 🟢🟡🔴 rubric
5. **Update all sections** - Don't skip deployment notes or testing checklists
6. **Cross-reference** - Ensure links to specs, checklists, and PatchNotes.md are correct
7. **Quality check** - Run through the checklist before completing
8. **Maintain consistency** - Match existing tone and formatting

---

**Template Version**: 1.0  
**Last Updated**: 2025-10-25  
**Maintained By**: Database Compliance Agent
