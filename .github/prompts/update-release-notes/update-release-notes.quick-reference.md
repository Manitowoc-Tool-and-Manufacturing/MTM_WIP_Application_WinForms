# Quick Reference: Update Release Notes

**Prompt File**: `.github/prompts/update-release-notes.prompt.md`  
**Target File**: `RELEASE_NOTES.md`  
**Source**: `PatchNotes.md`

---

## Quick Usage

**Simple Request**:
```
Update RELEASE_NOTES.md with the latest changes from PatchNotes.md
```

**Specific Change**:
```
Add release notes for [FeatureName/BugFix] based on PatchNotes.md entry
```

**New Release**:
```
Create version 5.3.0 release notes based on recent PatchNotes.md entries
```

---

## File Purpose Comparison

| File | Audience | Detail Level | Purpose |
|------|----------|--------------|---------|
| **RELEASE_NOTES.md** | Users, IT Staff, Executives | High-level, business impact | Deployment communication |
| **PatchNotes.md** | Developers, QA Engineers | Code-level, technical detail | Implementation documentation |
| **CHANGELOG.md** | All stakeholders | Summary, version history | Historical reference |

---

## Translation Quick Guide

### Technical → User-Friendly Examples

| Technical Term | User-Friendly Translation |
|----------------|---------------------------|
| "Fire-and-forget async pattern" | "Background task handling" |
| "Async/await compliance" | "Improved error logging reliability" |
| "MessageBox.Show violation" | "Old-style popup message" |
| "Service_ErrorHandler adoption" | "Modern error dialogs with retry" |
| "FR-004 specification" | "Database reliability standard" |
| "OnFormClosing critical path" | "Application shutdown process" |
| "DaoResult pattern" | "Structured error handling" |

---

## Section Quick Start

### 🎉 What's New (User-visible changes)
Ask yourself: "What will users notice?"
- New features they can use
- UI improvements they can see
- Workflows that are faster/easier

### 🔧 Technical Changes (Developer work)
Ask yourself: "What did we improve under the hood?"
- Code quality improvements
- Database compliance work
- Architecture enhancements

### 🐛 Bug Fixes (Problems solved)
Ask yourself: "What was broken and how did we fix it?"
- What failed before
- How users were affected
- What works now

---

## Risk Level Quick Assessment

**🟢 Low Risk** → Copy files, test, rollback = easy  
**🟡 Medium Risk** → Some database work, moderate testing needed  
**🔴 High Risk** → Schema changes, complex rollback, extensive testing

---

## Common Patterns

### Pattern 1: Database Compliance Fix
```markdown
**Files Refactored**:
- ✅ `Path/To/File.cs` - [N] methods upgraded
  - Eliminated [N] MessageBox.Show violations
  - Added proper async/await patterns
  - Enhanced error context capture
```

### Pattern 2: Critical Bug Fix
```markdown
**🔴 [Bug Title]** (File.cs, Line XXX)
- **Issue**: [What was broken]
- **Impact**: [How it affected users]
- **Fix**: [What was changed]
- **Risk**: [Deployment risk level]
```

### Pattern 3: User Feature
```markdown
**[Feature Name]**
- [User-visible improvement 1]
- [User-visible improvement 2]
- [User-visible improvement 3]
```

---

## Essential Checklist

Before publishing:
- [ ] User-friendly language (no jargon)
- [ ] Deployment steps included
- [ ] Testing checklist provided
- [ ] Known issues documented
- [ ] Links verified
- [ ] Version numbers consistent

---

**Use the full prompt for detailed guidance and examples.**
