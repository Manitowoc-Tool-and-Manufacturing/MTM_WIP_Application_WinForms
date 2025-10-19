# Speckit System Update Summary

**Date**: 2025-10-18
**Scope**: Comprehensive update to all speckit prompt files and templates
**Decisions**: Based on Answers.md - 1B, 2A, 3A, 4B, 5A, 6A+Other

---

## Overview

This update enhances the speckit system with:
1. **HTML-based clarification system** (8th grade reading level)
2. **MCP tool integration** (relevant tools per prompt)
3. **Premium request maximization** (all prompts)
4. **Instruction file references** (templates)
5. **Task completion tracking** (implement prompt)
6. **Table formatting standards** (all relevant prompts)

---

## New Files Created (2)

### 1. `.specify/templates/clarification-questions-template.html`
- **Purpose**: Interactive HTML form for user-friendly clarification questions
- **Features**:
  - Up to 15 questions (increased from 5 chat-based)
  - 8th grade reading level (no technical jargon)
  - 4 main options + "Other" option with text input per question
  - Default-selected recommended answers
  - Progress tracking (visual bar + counter)
  - "Save to Clipboard" button (activates when all answered)
  - Generates agent-ready markdown format
- **User Experience**:
  - Plain language questions ("How should people log in?")
  - Technical details hidden in data attributes
  - Beautiful gradient design with hover states
  - Mobile-responsive layout
- **Technical Details**:
  - Stores technical context in data-* attributes
  - JavaScript handles progress tracking and clipboard export
  - Converts user-friendly answers to agent-ready format

### 2. `.specify/templates/clarification-answers-template.md`
- **Purpose**: Agent-ready format for clarification answers
- **Structure**:
  - Question ID and category
  - Plain language question (what user saw)
  - Technical question (what it actually means)
  - User answer
  - Technical interpretation
  - Which spec sections to update
- **Usage**: Generated alongside HTML, contains technical details for agent consumption

---

## Updated Prompt Files (7)

### 1. `speckit.clarify.prompt.md`
**Major Changes**:
- ✅ Added premium request maximization guidance
- ✅ Changed from 5 sequential chat questions to 15 HTML form questions
- ✅ Integrated HTML generation workflow using new template
- ✅ Added 8th grade reading level requirements
- ✅ Implemented "Other" option (5th choice) for all questions
- ✅ Added technical context hiding (data attributes)
- ✅ Updated workflow to: generate HTML → wait for answers → integrate all 15

**Key Features**:
- Questions presented in interactive HTML instead of chat
- User-friendly language with technical details hidden
- Batch processing of all answers instead of sequential
- Clipboard export of agent-ready format

### 2. `speckit.implement.prompt.md`
**Major Changes**:
- ✅ Added premium request maximization section at top
- ✅ Added task completion tracking guidance
- ✅ Emphasized jumping between tasks to maximize session value
- ✅ Added documentation requirements for partial completion
- ✅ Referenced `mark_task_complete` MCP tool usage

**Key Additions**:
```markdown
**Task Completion Tracking**:
- Partially completed: Add note documenting what was done
- Fully completed: Mark [x] and add completion summary
- Use `mark_task_complete` MCP tool to update tasks.md automatically
```

### 3. `speckit.tasks.prompt.md`
**Major Changes**:
- ✅ Added premium request maximization guidance
- ✅ Added MCP tools section with relevant tools:
  - `parse_tasks` - Validate generated tasks
  - `load_instructions` - Verify instruction file references
  - `mark_task_complete` - Track task generation progress
  - `analyze_spec_context` - Understand feature scope
- ✅ Added usage examples for each tool
- ✅ Emphasized completing entire task breakdown in one session

### 4. `speckit.specify.prompt.md`
**Major Changes**:
- ✅ Added premium request maximization guidance
- ✅ Added MCP tools section:
  - `analyze_spec_context` - Check existing documentation
  - `check_checklists` - Validate requirements quality
- ✅ Added usage examples
- ✅ Emphasized completing validation without stopping

### 5. `speckit.plan.prompt.md`
**Major Changes**:
- ✅ Added premium request maximization guidance
- ✅ Added MCP tools section:
  - `analyze_spec_context` - Understand current state
  - `verify_ignore_files` - Phase 0 setup
  - `check_checklists` - Validate prerequisites
- ✅ Added usage examples with absolute paths
- ✅ Emphasized completing all phases without stopping

### 6. `speckit.analyze.prompt.md`
**Major Changes**:
- ✅ Added premium request maximization guidance
- ✅ Expanded MCP tools section:
  - `check_checklists` - Validate prerequisites
  - `analyze_dependencies` - Component relationships
  - `suggest_refactoring` - Improvement opportunities
  - `parse_tasks` - Task structure analysis
  - `analyze_spec_context` - Load artifacts
- ✅ Added comprehensive usage examples
- ✅ Emphasized running all detection passes in one session

### 7. `speckit.constitution.prompt.md`
**Major Changes**:
- ✅ Added premium request maximization guidance
- ✅ Emphasized template propagation without stopping
- ✅ Added guidance on completing sync report in same session

---

## Updated Template Files (3)

### 1. `spec-template.md`
**Major Changes**:
- ✅ Added "Relevant Instruction Files" section after Key Entities
- ✅ Listed 7 core instruction files with descriptions:
  - Implementation phase files (csharp, mysql, testing, documentation)
  - Quality assurance files (security, performance, code-review)
- ✅ Added guidance on when to reference during workflow
- ✅ Noted that specs remain technology-agnostic

**New Section Format**:
```markdown
## Relevant Instruction Files

### For Implementation Phase:
- `.github/instructions/csharp-dotnet8.instructions.md` - C# patterns
- ...

### For Quality Assurance:
- `.github/instructions/security-best-practices.instructions.md` - Security
- ...

**When to reference**: Implementation team should review during planning and tasks phases.
```

### 2. `plan-template.md`
**Major Changes**:
- ✅ Added comprehensive "Relevant Instruction Files" section
- ✅ Organized into Core Development and Quality & Security categories
- ✅ Listed 8 instruction files with detailed descriptions
- ✅ Added "When to Use" guidance:
  - During task generation (`/speckit.tasks`)
  - During implementation (`/speckit.implement`)
  - During code review
- ✅ Added example of instruction file reference format in tasks

**New Section Includes**:
- Core development: csharp, mysql, documentation
- Quality & security: testing, integration-testing, security, performance, code-review
- Usage timing for each workflow phase
- Example task with instruction file references

### 3. `checklist-template.md`
**Major Changes**:
- ✅ Added "Markdown Table Formatting Standards" section
- ✅ Defined 4 critical formatting rules:
  1. Consistent spacing around cell content
  2. Aligned pipes for readability
  3. Minimum 3 dashes for header separators
  4. Verify rendering requirement
- ✅ Added visual examples of correct vs incorrect formatting
- ✅ Positioned before checklist items to ensure awareness

**Formatting Rules Added**:
```markdown
1. **Consistent Spacing**: `| Option | Description |` ✅
2. **Aligned Pipes**: Vertical alignment for readability
3. **Header Separators**: At least 3 dashes `|--------|` ✅
4. **Verify Rendering**: Test in markdown preview
```

---

## Premium Request Maximization Pattern

**Applied to ALL 7 prompt files** with context-specific wording:

### Common Elements:
1. **Encouragement to continue** working through related tasks
2. **Only stop when blocked** by missing user input or complex decisions
3. **Document partial progress** when switching between tasks
4. **Complete workflows** (generation → validation → next steps) in one session

### Context-Specific Variations:

**speckit.clarify.prompt.md**:
```markdown
- Generate complete HTML files with all questions in a single session
- Process returned answers immediately and update spec.md without stopping
- Only pause for user input when waiting for clarification answers
```

**speckit.implement.prompt.md**:
```markdown
- Work through multiple related tasks in a single session
- Jump between tasks when beneficial to maintain momentum
- Document partial progress using task completion notes
- Continue until natural checkpoint (phase boundary, complex blocker)
```

**speckit.tasks.prompt.md**:
```markdown
- Generate complete task lists with all phases in a single session
- Include comprehensive instruction file references
- Create parallel execution opportunities
- Do NOT generate partial task lists or stop after one phase
```

---

## MCP Tool Integration Summary

### Tool Distribution Strategy (Option B - Relevant Tools per Prompt):

| Prompt       | MCP Tools Added | Purpose |
|--------------|-----------------|---------|
| constitution | None            | No automation needs |
| plan         | 3 tools         | analyze_spec_context, verify_ignore_files, check_checklists |
| specify      | 2 tools         | analyze_spec_context, check_checklists |
| clarify      | None added      | HTML generation replaces interactive flow |
| checklist    | Already had 1   | check_checklists (existing) |
| analyze      | 5 tools         | check_checklists, analyze_dependencies, suggest_refactoring, parse_tasks, analyze_spec_context |
| tasks        | 4 tools         | parse_tasks, load_instructions, mark_task_complete, analyze_spec_context |
| implement    | Already comprehensive | 20+ tools documented (existing, updated with completion guidance) |

### Usage Patterns Documented:

Each prompt with MCP tools now includes:
1. **Tool descriptions** - What each tool does
2. **Input parameters** - With absolute path requirements
3. **Output descriptions** - What data is returned
4. **Use cases** - When to call the tool
5. **Code examples** - TypeScript-style usage snippets

**Example from speckit.tasks.prompt.md**:
```typescript
// Before generating tasks - understand context
analyze_spec_context(feature_dir: "/absolute/path/to/specs/feature-name")

// After generating tasks - validate structure
parse_tasks(tasks_file: "/absolute/path/to/specs/feature-name/tasks.md")
```

---

## Table Formatting Standards

**Applied to 2 locations**:

### 1. speckit.checklist.prompt.md
Added comprehensive "Markdown Table Formatting Standards" section with:
- 4 formatting rules with visual examples
- Correct vs incorrect comparisons
- Code block examples showing proper alignment
- Testing requirements

### 2. checklist-template.md
Added same formatting standards section that will appear in generated checklists:
- Ensures all generated checklists follow standards
- Users see formatting guidance when they open checklist files
- Examples help users maintain formatting when adding items

---

## HTML Clarification System Details

### User-Facing Features:
1. **Plain Language**:
   - ❌ "Should this implement OAuth2 or session-based authentication?"
   - ✅ "How should people log into the system?"

2. **Visual Design**:
   - Purple gradient header
   - Card-based question layout
   - Recommended answers have gold badge
   - Progress bar shows completion
   - Green highlight on answered questions

3. **Interaction**:
   - Click option cards to select
   - "Other" reveals text input field
   - Save button disabled until all answered
   - Success message on clipboard copy

### Technical Implementation:
1. **Data Storage**:
   ```html
   <div class="question-card" 
        data-question-id="Q1"
        data-question-category="scope"
        data-question-text="Plain language"
        data-technical-context="Technical details">
   ```

2. **Answer Export Format**:
   ```markdown
   ## Q1: Scope
   **Question**: Should this work on phones and computers?
   **Technical Context**: Responsive design requirements
   **Answer**: Option B - Both phones and computers
   **Technical Interpretation**: 
   - Implement responsive design patterns
   - Support mobile (320px+) and desktop (1024px+)
   ```

3. **JavaScript Features**:
   - Progress tracking (updates as questions answered)
   - Button state management (enable when complete)
   - Clipboard API integration
   - Custom answer field toggle
   - Selected state management

---

## Instruction File Reference Format

**Standardized across spec-template.md and plan-template.md**:

### In Templates:
```markdown
## Relevant Instruction Files

### For Implementation Phase:
- `.github/instructions/[file].instructions.md` - [Description]

### For Quality Assurance:
- `.github/instructions/[file].instructions.md` - [Description]

**When to reference**: [Workflow phase guidance]
```

### In Generated Tasks (from tasks-template.md):
```markdown
- [ ] T100 - Task description
  - **Reference**: .github/instructions/[file].instructions.md - [Specific guidance]
```

### Benefits:
1. **Discoverability**: Team knows which instruction files exist
2. **Context**: Understand when to reference each file
3. **Traceability**: Tasks point to specific guidance
4. **Consistency**: Standardized format across artifacts

---

## Task Completion Note Format

**Added to speckit.implement.prompt.md** (not tasks.prompt.md per Decision 4B):

### Fully Completed Task:
```markdown
- [x] **T001** – Task description
  - **Completed**: 2025-10-18 - Successfully implemented feature X, verified build
  - **Reference**: .github/instructions/[file].instructions.md
```

### Partially Completed Task:
```markdown
- [ ] **T002** – Task description  
  - **Completed**: 2025-10-18 - Created base class and interface. Still need: validation logic
  - **Reference**: .github/instructions/[file].instructions.md
```

### Benefits:
- **Progress visibility**: See what work was done
- **Session continuity**: Pick up where previous agent left off
- **Accountability**: Clear timestamp and summary
- **Planning**: Know what remains for incomplete tasks

---

## Impact Summary

### For Users:
- ✅ **Easier clarifications**: HTML form instead of chat back-and-forth
- ✅ **Plain language**: 8th grade reading level, no jargon
- ✅ **More questions**: Up to 15 instead of 5 for better coverage
- ✅ **Better guidance**: Instruction file references in specs and plans
- ✅ **Progress tracking**: Completion notes show what was done

### For Agents:
- ✅ **Clearer instructions**: Premium request maximization in every prompt
- ✅ **Better tools**: MCP tools integrated where relevant
- ✅ **Less ambiguity**: Table formatting standards prevent rendering issues
- ✅ **More context**: Instruction file references show where to find patterns
- ✅ **Better tracking**: Task completion notes enable session continuity

### For Implementation Quality:
- ✅ **Fewer clarification cycles**: 15 questions up front vs 5 sequential
- ✅ **Better consistency**: Standardized instruction file references
- ✅ **Higher completion**: Premium request maximization reduces partial work
- ✅ **Better documentation**: Completion notes create audit trail

---

## Files Modified Summary

| Category | Count | Files |
|----------|-------|-------|
| **New Templates** | 2 | clarification-questions-template.html, clarification-answers-template.md |
| **Prompt Updates** | 7 | constitution, plan, specify, clarify, checklist, analyze, tasks |
| **Template Updates** | 3 | spec-template.md, plan-template.md, checklist-template.md |
| **Already Updated** | 2 | speckit.implement.prompt.md (earlier), tasks-template.md (earlier) |
| **Total Modified** | 14 | 2 new + 7 prompts + 3 templates + 2 previous = 14 files |

---

## Testing Recommendations

### For HTML Clarification System:
1. Generate HTML file using updated clarify prompt
2. Open in browser (Chrome, Firefox, Edge)
3. Verify all 15 questions render correctly
4. Test "Other" option reveals text input
5. Test progress bar updates as questions answered
6. Test save button enables when all answered
7. Test clipboard copy generates correct markdown

### For MCP Tools:
1. Test each tool call with absolute paths
2. Verify output format matches documentation
3. Test error handling for missing files
4. Verify tool recommendations in appropriate contexts

### For Instruction File References:
1. Generate spec with new template
2. Verify instruction file section appears
3. Generate plan with new template
4. Verify comprehensive instruction file section
5. Generate tasks with references
6. Verify reference format is consistent

### For Premium Request Maximization:
1. Run each prompt with typical input
2. Verify agent continues through multiple steps
3. Verify agent only stops when truly blocked
4. Check completion notes are added when switching tasks

---

## Maintenance Notes

### When Adding New Instruction Files:
1. Update `spec-template.md` Relevant Instruction Files section
2. Update `plan-template.md` Relevant Instruction Files section
3. Update `tasks-template.md` examples with new references
4. Update relevant prompt files if tool-specific

### When Adding New MCP Tools:
1. Add to `speckit.implement.prompt.md` (comprehensive list)
2. Add to relevant specific prompts (Option B strategy)
3. Update `.mcp/MCP-TOOLS-REFERENCE.md`
4. Add usage examples in prompt files

### When Updating Clarification Workflow:
1. Update both HTML template and markdown template together
2. Ensure data attribute names match between templates
3. Test JavaScript functionality after HTML changes
4. Verify markdown export format matches agent expectations

---

## Rollout Checklist

- [x] Create HTML clarification templates (2 files)
- [x] Update speckit.clarify.prompt.md (HTML system)
- [x] Update speckit.implement.prompt.md (completion tracking)
- [x] Update speckit.tasks.prompt.md (MCP tools)
- [x] Update speckit.specify.prompt.md (MCP tools + premium)
- [x] Update speckit.plan.prompt.md (MCP tools + premium)
- [x] Update speckit.analyze.prompt.md (MCP tools + premium)
- [x] Update speckit.checklist.prompt.md (premium + tables)
- [x] Update speckit.constitution.prompt.md (premium)
- [x] Update spec-template.md (instruction references)
- [x] Update plan-template.md (instruction references)
- [x] Update checklist-template.md (table formatting)
- [x] Create this summary document
- [ ] Test HTML clarification system end-to-end
- [ ] Validate MCP tool integrations
- [ ] Test premium request maximization in practice
- [ ] Update AGENTS.md if needed
- [ ] Commit changes with descriptive message

---

## Commit Message Suggestion

```
feat: comprehensive speckit system enhancements

- Add HTML clarification system with 8th grade reading level (up to 15 questions)
- Integrate relevant MCP tools into all prompt files (Option B strategy)
- Add premium request maximization guidance to all prompts
- Add instruction file references to spec and plan templates
- Add table formatting standards to checklist workflows
- Add task completion tracking format to implement prompt

Breaking Changes:
- speckit.clarify now generates HTML instead of chat questions (max 15 vs 5)
- HTML clarification requires user to open file, answer, and paste back

New Templates:
- .specify/templates/clarification-questions-template.html
- .specify/templates/clarification-answers-template.md

Updated Files: 14 total (7 prompts, 3 templates, 2 previously updated, 2 new)

Closes: #[issue-number] (if applicable)
```

---

## Next Steps

1. **Test the system**: Run through each speckit command to validate changes
2. **Document workflow**: Update any workflow diagrams or guides
3. **Train team**: Share this summary with implementation team
4. **Monitor usage**: Collect feedback on HTML clarification system
5. **Iterate**: Refine based on real-world usage patterns

---

**Generated**: 2025-10-18
**Author**: GitHub Copilot
**Review Status**: Pending user review and testing
