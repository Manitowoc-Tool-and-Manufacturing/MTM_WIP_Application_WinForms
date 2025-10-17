# Stored Procedure Builder - Clarification Questions

**Feature**: Interactive MySQL 5.7 Stored Procedure Builder  
**Date**: October 17, 2025  
**Status**: Awaiting Clarifications

---

## Overview

This document contains questions about unclear or missing details in the specification. Each question includes context to help you understand why we're asking, multiple options to choose from (where applicable), and our recommendation based on best practices.

**How to respond**: For each question, check the box next to your preferred option, or write your own answer if none of the options fit perfectly.

---

## Question 1: Error Recovery Strategy

**What we need to know**: How should the builder handle errors when users make mistakes during the wizard process?

**Why this matters**: Error handling affects user experience significantly. Users need clear guidance when something goes wrong, but we don't want to interrupt their workflow unnecessarily.

**Our Recommendation**: ✅ **Option B - Inline validation with warnings**  
This approach catches errors early without blocking progress, allowing users to fix issues at their own pace while still preventing export of broken procedures.

**Your Options** (check one):

- [ ] **A - Strict blocking validation**: Stop users from proceeding to the next wizard step if current step has any errors. Show error modal dialog that must be dismissed before continuing.
  - *Pros*: Ensures data quality at every step; prevents cascading errors
  - *Cons*: Can feel restrictive; may frustrate experienced users who want to work non-sequentially

- [x] **B - Inline validation with warnings** *(RECOMMENDED)*: Show validation errors inline with red highlights and warning icons, but allow users to proceed to other steps. Only block Export button if critical errors exist.
  - *Pros*: Flexible workflow; users can navigate freely while still being aware of issues
  - *Cons*: Users might miss warnings; requires clear visual indicators

- [ ] **C - Permissive with export-time validation**: Allow any input during wizard, only validate when user clicks Export. Show comprehensive error report at export time.
  - *Pros*: Maximum flexibility; no interruptions during design
  - *Cons*: Users discover problems late in process; may need to revisit multiple steps

- [ ] **D - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 2: Browser Compatibility and Fallbacks

**What we need to know**: What should happen when users try to access the builder from unsupported browsers (Firefox, Edge, Safari) or older Chrome versions?

**Why this matters**: The spec targets Chrome 86+, but users might try to access from other browsers. We need a clear strategy for handling this.

**Our Recommendation**: ✅ **Option B - Browser detection with graceful degradation**  
This balances broad accessibility with focused development effort, providing a usable experience in modern browsers while maintaining full features in Chrome.

**Your Options** (check one):

- [ ] **A - Hard block with error message**: Detect browser on load, show "Chrome 86+ Required" error message and prevent access if using unsupported browser.
  - *Pros*: Simple to implement; sets clear expectations
  - *Cons*: Completely blocks users who might have Chrome available but chose different browser

- [x] **B - Browser detection with graceful degradation** *(RECOMMENDED)*: Allow access from modern Firefox/Edge/Safari but disable advanced features (File System Access API, certain drag-drop behaviors). Show banner: "Some features limited - use Chrome 86+ for full experience."
  - *Pros*: Inclusive; most features work across browsers; clear communication
  - *Cons*: More testing needed; feature parity complexity

- [ ] **C - No browser checking**: Assume users have correct browser, let features fail naturally if incompatible. Provide troubleshooting guide for common issues.
  - *Pros*: Minimal development overhead; users self-select appropriate browser
  - *Cons*: Poor user experience when features fail silently

- [ ] **D - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 3: Session Persistence and Recovery

**What we need to know**: When a user's wizard session is auto-saved to localStorage, what information should be included in the recovery prompt?

**Why this matters**: Users need confidence that their work is safe, but too much information in the prompt can be overwhelming.

**Our Recommendation**: ✅ **Option B - Summary with last modified timestamp**  
Provides enough context for users to make an informed decision without overwhelming them with technical details.

**Your Options** (check one):

- [ ] **A - Minimal prompt**: Simple yes/no: "Resume previous session?" with no additional context.
  - *Pros*: Clean, simple interface
  - *Cons*: Users don't know what they're resuming; risky if session is old

- [x] **B - Summary with last modified timestamp** *(RECOMMENDED)*: Show procedure name (if entered), step last completed, and timestamp. Example: "Resume 'inv_inventory_Add_Item'? Last edited: 2 hours ago (Step 4: DML Operations)"
  - *Pros*: Clear context; users can make informed decision
  - *Cons*: Slightly more complex UI

- [ ] **C - Detailed preview**: Show full summary of all wizard data (parameters count, operations count, validations count) with "View Details" expandable section.
  - *Pros*: Maximum transparency; users see exactly what they're restoring
  - *Cons*: Information overload; slower to parse visually

- [ ] **D - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 4: SQL Preview Responsiveness

**What we need to know**: How quickly should the live SQL preview update as users make changes in the wizard?

**Why this matters**: Real-time updates provide instant feedback but can cause performance issues. Debouncing improves performance but creates lag.

**Our Recommendation**: ✅ **Option B - 300ms debounce with loading indicator**  
Industry-standard debounce timing that feels responsive while preventing performance issues during rapid typing.

**Your Options** (check one):

- [ ] **A - Instant real-time (no debounce)**: Update SQL preview immediately on every keystroke or selection change.
  - *Pros*: True real-time feedback; feels very responsive
  - *Cons*: Performance issues with large procedures; excessive re-rendering

- [x] **B - 300ms debounce with loading indicator** *(RECOMMENDED)*: Wait 300 milliseconds after user stops typing/clicking before updating preview. Show subtle loading spinner during generation.
  - *Pros*: Balanced performance and responsiveness; standard UX pattern
  - *Cons*: Slight delay noticeable to users; requires state management

- [ ] **C - Manual refresh button**: Update preview only when user clicks "Refresh Preview" button. Show "Preview outdated" indicator when wizard data changes.
  - *Pros*: Best performance; user controls when update happens
  - *Cons*: Extra click required; preview often out of sync

- [ ] **D - Hybrid approach**: Real-time for simple operations (< 5 operations), debounced for complex procedures (5+ operations).
  - *Pros*: Optimized per use case
  - *Cons*: Inconsistent behavior; harder to implement

- [ ] **E - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 5: Metadata Refresh Strategy

**What we need to know**: The spec mentions refreshing database metadata every 5 minutes or via manual button. Should this happen automatically, or only when user explicitly requests it?

**Why this matters**: Background refresh keeps data fresh but adds complexity and potential performance impact. Manual-only refresh is simpler but data may become stale.

**Our Recommendation**: ✅ **Option C - Manual refresh with staleness detection**  
Gives users control while protecting them from stale data through smart detection and warnings.

**Your Options** (check one):

- [ ] **A - Automatic background refresh every 5 minutes**: Silently fetch fresh metadata on interval. Show toast notification if changes detected: "Schema updated - 3 new columns detected in Inventory table."
  - *Pros*: Always current; proactive detection of changes
  - *Cons*: Background network traffic; potential mid-wizard disruption

- [ ] **B - Automatic refresh on wizard step navigation**: Refresh metadata when user navigates to DML Operations or Validation steps (where metadata is needed). Cache for 5 minutes.
  - *Pros*: Just-in-time freshness; reduced unnecessary requests
  - *Cons*: Still automatic; may cause unexpected delays during navigation

- [x] **C - Manual refresh with staleness detection** *(RECOMMENDED)*: Provide "Refresh Metadata" button. Track metadata age. Show warning banner if metadata is >10 minutes old: "Schema data may be outdated. Refresh?" Include auto-refresh option in settings for power users.
  - *Pros*: User control; clear communication; optional automation
  - *Cons*: Users must take action; risk of working with stale data if ignored

- [ ] **D - No refresh capability**: Load metadata once on builder open, cache for entire session. Require page reload to refresh.
  - *Pros*: Simplest implementation; predictable behavior
  - *Cons*: Stale data risk; poor experience if schema changes during session

- [ ] **E - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 6: Drag-and-Drop Accessibility

**What we need to know**: Should the drag-and-drop interfaces (validation rules, DML operations, flow diagram) have keyboard-accessible alternatives for accessibility compliance?

**Why this matters**: Drag-and-drop is difficult for keyboard-only users and screen reader users. Accessibility is important for inclusive software.

**Our Recommendation**: ✅ **Option B - Dual interface (drag-drop + keyboard)**  
Modern web accessibility best practice that maintains the visual appeal of drag-drop while ensuring everyone can use the tool.

**Your Options** (check one):

- [ ] **A - Drag-and-drop only**: No keyboard alternatives. Focus on visual drag-drop experience for primary use case.
  - *Pros*: Simpler development; consistent interaction model
  - *Cons*: Excludes keyboard-only users; fails WCAG accessibility standards

- [x] **B - Dual interface (drag-drop + keyboard)** *(RECOMMENDED)*: Provide keyboard shortcuts for reordering (Ctrl+Up/Down arrows), context menus for actions, and "Move Up/Down" buttons next to each item. Screen reader announces changes.
  - *Pros*: WCAG compliant; inclusive design; multiple interaction preferences
  - *Cons*: More development and testing effort; UI may feel cluttered with extra buttons

- [ ] **C - Keyboard as primary, drag-drop as enhancement**: Build keyboard interface first (up/down buttons, tab navigation), add drag-drop as progressive enhancement.
  - *Pros*: Accessibility-first approach; guaranteed keyboard support
  - *Cons*: May deprioritize drag-drop polish

- [ ] **D - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 7: PHP API Error Handling

**What we need to know**: When the PHP backend API encounters errors (database connection lost, query fails, PHP script error), how should the JavaScript front-end handle these failures?

**Why this matters**: Backend errors are inevitable. Users need clear information about what went wrong and how to fix it, without exposing technical details that could be security risks.

**Our Recommendation**: ✅ **Option B - Structured error responses with retry**  
Balances user-friendly messaging with technical detail needed for troubleshooting, plus automatic recovery for transient failures.

**Your Options** (check one):

- [ ] **A - Generic error messages**: Show simple "An error occurred. Please refresh and try again." Hide all technical details from user.
  - *Pros*: Clean UX; no confusing technical jargon
  - *Cons*: Users can't self-diagnose; developers need access to browser console for debugging

- [x] **B - Structured error responses with retry** *(RECOMMENDED)*: PHP returns JSON with `{success: false, error_type: "connection_failed", user_message: "Database connection lost", technical_detail: "mysqli_connect failed"}`. UI shows user-friendly message in dialog with "Retry" and "Details" buttons. Details expand to show technical info for troubleshooting.
  - *Pros*: User-friendly with optional detail; supports auto-retry; developer-friendly debugging
  - *Cons*: Requires careful error classification in PHP; more complex error handling logic

- [ ] **C - Pass-through error messages**: Show PHP error messages directly to user (e.g., "mysqli_connect(): (HY000/2002): Connection refused").
  - *Pros*: Complete transparency; no information loss
  - *Cons*: Confusing to non-technical users; potential security information disclosure

- [ ] **D - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 8: Version History Depth

**What we need to know**: When tracking exported procedure versions for side-by-side comparison, how many previous versions should be retained?

**Why this matters**: Version history helps users understand changes, but unlimited storage can bloat localStorage and complicate the UI.

**Our Recommendation**: ✅ **Option B - Last 5 versions per procedure**  
Balances practical history tracking (covers recent iterations) with storage constraints and UI complexity.

**Your Options** (check one):

- [ ] **A - Last 2 versions only (current + previous)**: Store only current and immediately previous version.
  - *Pros*: Minimal storage; simple UI
  - *Cons*: Can't compare to earlier iterations; limited history

- [x] **B - Last 5 versions per procedure** *(RECOMMENDED)*: Keep 5 most recent exports per procedure name. Show dropdown in comparison view to select which versions to compare.
  - *Pros*: Covers typical iteration cycle; manageable storage; useful history
  - *Cons*: Storage grows with number of procedures; dropdown adds UI complexity

- [ ] **C - Last 10 versions per procedure**: Extended history for thorough tracking.
  - *Pros*: Comprehensive audit trail
  - *Cons*: Higher storage usage; potentially overwhelming version list

- [ ] **D - Unlimited with export option**: Store all versions in localStorage with "Export Version History" to save to file and clear browser storage.
  - *Pros*: Complete history; user control over archival
  - *Cons*: localStorage limits can be reached; requires user maintenance

- [ ] **E - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 9: Template Validation Before Use

**What we need to know**: When a user selects a template (built-in or custom), should the builder validate that required database tables/columns exist before applying the template?

**Why this matters**: Templates reference specific database structures. If those don't exist, the procedure will be invalid. Early validation prevents wasted effort.

**Our Recommendation**: ✅ **Option B - Validate with substitution suggestions**  
Provides safety while offering practical solutions when exact matches don't exist.

**Your Options** (check one):

- [ ] **A - No validation**: Apply template as-is, let user fix any schema mismatches manually in wizard.
  - *Pros*: Simplest; templates work even with schema differences
  - *Cons*: User may not realize tables are missing until export/test

- [x] **B - Validate with substitution suggestions** *(RECOMMENDED)*: Check if template's referenced tables exist. If missing, show dialog: "Template references 'Inventory_History' table which doesn't exist. Suggestions: Use 'History_Inventory' instead?" User can accept substitution, select different table, or cancel.
  - *Pros*: Intelligent assistance; prevents broken procedures; flexible
  - *Cons*: Requires fuzzy matching logic; false positives possible

- [ ] **C - Strict validation with blocking**: If template references non-existent tables/columns, show error and block template loading: "Cannot load template - required tables missing."
  - *Pros*: Guarantees template integrity
  - *Cons*: Templates only work with exact schema matches; reduced template portability

- [ ] **D - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## Question 10: Procedure Naming Uniqueness Check

**What we need to know**: When user enters a procedure name, should the builder check if a procedure with that name already exists in the database?

**Why this matters**: Duplicate procedure names cause errors on export. Early detection saves users from discovering the problem after completing the wizard.

**Our Recommendation**: ✅ **Option B - Check with override option**  
Prevents accidents while allowing intentional replacements of existing procedures.

**Your Options** (check one):

- [ ] **A - No uniqueness check**: Allow any procedure name, rely on MySQL's "DROP PROCEDURE IF EXISTS" to handle duplicates.
  - *Pros*: No additional API calls; users can freely replace procedures
  - *Cons*: No warning about overwrites; accidental replacements possible

- [x] **B - Check with override option** *(RECOMMENDED)*: Query database for existing procedure when name is entered. If exists, show warning icon with tooltip: "Procedure 'inv_inventory_Add_Item' already exists. Exporting will replace it." Allow user to proceed if intentional.
  - *Pros*: Prevents accidental overwrites; informed user decisions
  - *Cons*: Extra database query; requires PHP API endpoint

- [ ] **C - Block duplicate names**: If procedure exists, show error: "Name already in use. Choose a different name or delete existing procedure first."
  - *Pros*: Forces unique names; prevents overwrites completely
  - *Cons*: Users can't intentionally update procedures; rigid workflow

- [ ] **D - Version suffix auto-increment**: If name exists, automatically suggest "procedure_name_v2", "procedure_name_v3", etc.
  - *Pros*: Never conflicts; automatic resolution
  - *Cons*: Clutters database with numbered versions; user may want to replace, not version

- [ ] **E - Custom approach**: _(Write your answer below)_
  - Your answer: _______________________________________________

---

## How to Submit Your Answers

1. Check the box `[x]` next to your preferred option for each question
2. For custom approaches, write your answer in the space provided
3. Save this file and notify the team that clarifications are complete
4. We'll update the specification based on your answers and proceed to planning

---

## Summary

- **Total Questions**: 10
- **Categories Covered**: Error Handling, Browser Compatibility, Data Persistence, Performance, Accessibility, API Design, Version Control, Template System, Database Integration
- **Estimated Time to Complete**: 15-20 minutes

Your answers will help us create a more detailed implementation plan with fewer assumptions and reduced risk of rework.
