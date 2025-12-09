# GitHub Copilot Prompt: Create Feature Specification from Template

## CRITICAL INSTRUCTION

You MUST create a feature specification that **EXACTLY** follows the structure and pattern defined in `feature-spec-template.md`. The delivered file's structure, section headers, subsection organization, and formatting MUST match the template precisely.

---

## Context

You are creating a feature specification for the **MTM WIP Application**, a .NET 8.0 WinForms application that manages work-in-progress inventory for Manitowoc Tool and Manufacturing.

**Template Location**: `.specify/templates/feature-spec-template.md`

**Reference Example**: `Documentation/FeatureDocuments/PlannedFeatures/QuickButton_Add_Feature_Specification.md`

---

## Requirements

### Mandatory Template Compliance

The feature specification MUST include ALL of the following sections in EXACT order:

1. **Title Block**
   - Feature name as H1 header
   - Version, Created date, Feature Type, Related Features

2. **Constitutional Alignment**
   - Reference to MTM WIP Application Constitution
   - How feature aligns with each relevant principle

3. **Overview**
   - Purpose (2-3 sentences)
   - User Goals (bulleted list)
   - Business Value (bulleted list)

4. **Technical Requirements**
   - Technology Stack Constraints
   - Naming Conventions
   - Code Organization

5. **Feature Behavior**
   - Initial Load Sequence
   - Conditional Logic
   - Logging requirements

6. **User Interface Layouts**
   - Layout descriptions with Display Conditions and Purpose
   - Visual Structure for each layout
   - Components with detailed properties
   - User Interaction Flow for each layout

7. **Validation Rules**
   - Field-by-field validation rules
   - Error messages
   - Overall form validation

8. **Database Operations**
   - DAO Implementation Mapping (table format)
   - Implementation Details
   - Stored Procedures Required

9. **User Interaction Flows**
   - Numbered step-by-step flows
   - Multiple scenarios (success, errors, edge cases)

10. **Error Handling Requirements**
    - User-Facing Error Messages
    - Internal Error Handling
    - Error Context Data

11. **Logging Requirements**
    - Events to Log
    - Log Format

12. **UI/UX Requirements**
    - Theme Integration
    - Accessibility
    - Responsiveness
    - Visual Feedback

13. **Performance Requirements**
    - Response Time Targets
    - UI Responsiveness
    - Data Loading Optimization

14. **Integration Requirements**
    - Parent Form Integration
    - Sibling Feature Integration
    - Database Integration
    - Logging Integration

15. **Testing Requirements**
    - Unit Testing
    - Integration Testing
    - UI Testing
    - Edge Case Testing

16. **Success Criteria**
    - Functional Requirements Met
    - Non-Functional Requirements Met
    - User Experience Goals Met

17. **Future Enhancements (Out of Scope)**

18. **References**
    - Related Documentation
    - Related Code Components

19. **Document History**

20. **Next Steps**

21. **Approval Section**

---

## Technical Constraints (NON-NEGOTIABLE)

### Technology Stack
- **.NET**: 8.0-windows (NEVER suggest newer versions)
- **C# Language**: 12.0 (NEVER suggest newer features)
- **WinForms**: Must inherit from `ThemedForm` or `ThemedUserControl`
- **MySQL**: 5.7.24 (FORBIDDEN: CTEs, window functions, JSON functions)
- **Database Access**: ONLY stored procedures (NO inline SQL)
- **Error Handling**: ONLY `Service_ErrorHandler` (NEVER `MessageBox.Show`)
- **Logging**: ONLY `LoggingUtility` (structured CSV format)

### Architectural Boundaries
1. **Forms → Services → DAOs → Database** (NEVER skip layers)
2. **DAOs ONLY call `Helper_Database_StoredProcedure`** (NO direct MySqlConnection)
3. **ALL DAO methods return `Model_Dao_Result<T>`** (standardized wrapper)
4. **ALL database operations are async** (use async/await)
5. **ALL errors use centralized handler** (Service_ErrorHandler)

### Code Organization (MANDATORY #region structure)
Every C# file MUST have regions in this exact order:
1. Fields
2. Properties
3. Constructors
4. Methods
5. Events
6. Helpers
7. Cleanup / Dispose

---

## Instructions for GitHub Copilot

When I provide you with a feature request, you will:

### Step 1: Confirm Understanding & Gather Context

**IF** the request is clear and detailed:
- Proceed to Step 2.

**IF** the request is vague, lacks context, or requires clarification:
- You MUST generate an **Interactive Questionnaire** to gather missing details.
- Do NOT ask simple text questions.
- Follow the **Interactive Questionnaire Requirements** below.

#### Interactive Questionnaire Requirements

**Format**: HTML file using a standardized template structure
**Question Types**: Multiple choice with suggested answers where applicable
**Language Style**: Plain English, completely code-agnostic (no technical jargon)
**Target Audience**: End users (non-technical stakeholders, business users)
**Content**: Each question must address one specific missing clarification point
**Reasoning**: Include brief explanation for why each question is being asked

**Template Structure**:
- Clean, accessible HTML5 format
- Responsive design (mobile-friendly)
- Clear section breaks between question groups
- Radio buttons for multiple choice selections
- **Custom Answer Integration**:
  - Every question MUST include a textbox labeled "Add your own details or modifications:"
  - If user selects a radio button + fills textbox → Merged Answer: "[Selected Option] + [Custom Details]"
  - If user only fills textbox → Answer: Textbox content
  - If user only selects radio → Answer: Selected option
- Submit button to compile responses
- Response summary section showing final merged answers

**Question Design Principles**:
- Use simple, conversational language
- Provide 3-5 realistic answer options per question
- Include one "suggested" answer marked clearly
- Explain the business impact or user benefit
- Avoid technical terms unless necessary
- Group related questions into logical sections

**JavaScript Functionality Required**:
- Real-time preview of merged answers
- Validation (at least one input per required question)
- Summary generation combining selected options with custom text
- Export responses as plain text or JSON
- Auto-save to browser localStorage

**Example Question Format**:
> **Question 5**: How should the system respond when a user tries to save without making any changes?
>
> **Why we're asking**: This helps us design a user experience that feels natural and prevents confusion.
>
> **Options**:
> - [ ] Show an error message and prevent saving
> - [x] Show a friendly reminder that nothing changed (Suggested)
> - [ ] Save anyway without any message
> - [ ] Ask the user if they're sure they want to save
> - [ ] I need help deciding
>
> **Add your own details or modifications**:
> [_______________________________________________]
>
> **Reasoning for suggested answer**: Users appreciate being informed without feeling blocked.

**HTML Template Location**: `.github/templates/html/feature-spec-questionnaire.html`

**Output Deliverable**:
Use the template at `.github/templates/html/feature-spec-questionnaire.html` as your base. Customize the `questionData` JavaScript object to include questions specific to the unclear aspects of the feature request. Generate a complete, standalone HTML file that can be opened in any browser.

### Step 2: Create Specification Document

**CRITICAL**: Use the `feature-spec-template.md` as your EXACT structural blueprint.

For each section:
1. **Read the template section carefully**
2. **Preserve all subsection headers exactly**
3. **Fill in feature-specific content**
4. **Maintain markdown formatting consistency**
5. **Keep section order identical to template**

### Step 3: Populate All Sections

**DO NOT skip or combine sections**. Every section in the template MUST appear in the specification with feature-specific content.

**Database Operations Section**:
- Research existing DAOs in `Data/` folder
- Check if required methods exist
- Mark status as: Exists | Needs Creation | Verify
- Provide exact method signatures from codebase

**UI Layout Sections**:
- Describe EVERY UI component with exact properties
- Include Text, Size, Tab Order, Enabled Logic, Action
- Provide pixel-perfect layouts where relevant
- Reference existing controls when applicable

**User Interaction Flows**:
- Provide numbered, step-by-step flows
- Include success paths, error paths, edge cases
- Always end with "All actions logged"

**Validation Rules**:
- Specify EXACT error messages (quoted strings)
- Define validation triggers
- Describe user feedback mechanisms

### Step 4: Constitutional Alignment

For each constitutional principle referenced:
- Explain HOW this feature adheres to it
- Provide specific examples
- Reference relevant code patterns or services

### Step 5: Technical Accuracy

**DAO Mapping**:
- Check `Data/` folder for existing DAOs
- Verify stored procedure existence in `Database/UpdatedStoredProcedures/`
- Provide accurate method signatures
- Note missing components

**Naming Conventions**:
- Follow project standards exactly
- Forms: `{Feature}Form`
- Controls: `Control_{Feature}_{Purpose}`
- DAOs: `Dao_{Entity}`
- Methods: `{Action}Async`

### Step 6: Completeness Check

Before delivering, verify:
- ✅ ALL 21 template sections present
- ✅ Section order matches template exactly
- ✅ All subsections included
- ✅ Markdown formatting consistent
- ✅ Technical constraints respected
- ✅ DAO mappings accurate
- ✅ Error messages quoted and specific
- ✅ Performance targets defined
- ✅ Success criteria measurable

---

## Quality Standards

### Level of Detail Required

**High Detail (Maximum Specificity)**:
- UI component properties (exact text, sizes, colors, tab order)
- Error messages (exact quoted strings)
- Database parameters (exact types and names)
- Performance targets (specific millisecond values)
- User interaction flows (numbered steps)

**Medium Detail (Clear Guidance)**:
- Feature behavior logic
- Validation rules
- Integration requirements
- Testing scenarios

**Low Detail (Conceptual Overview)**:
- Future enhancements
- Business value propositions

### Examples of Good vs. Poor Content

**❌ POOR (Too Vague)**:
- "Button should be styled appropriately"
- "Add validation for the field"
- "Query the database for data"
- "Handle errors gracefully"

**✅ GOOD (Specific and Actionable)**:
- "Button: Text='Save', Size=100x35, Tab Order=6, Primary action styling"
- "Validation: Non-empty string, must exist in Parts table, Error: 'Please select a valid Part Number'"
- "Call `Dao_Part.GetAllActiveParts()` → Bind PartId to AutoComplete source"
- "Use `Service_ErrorHandler.HandleException()` with context: {UserId, Operation, PartId}"

---

## Feature Request Input Format

When you receive a feature request, expect this information:

**Minimum Required**:
- Feature name
- Primary purpose
- Target users
- Basic workflow description

**Ideal Input Includes**:
- Feature name and category
- User stories or scenarios
- UI mockups or wireframes
- Related features or dependencies
- Special requirements or constraints
- Known edge cases
- Performance expectations

---

## Output Format

### File Naming
`{FeatureName}_Feature_Specification.md`

Example: `InventorySearch_Feature_Specification.md`

### File Location
`Documentation/FeatureDocuments/PlannedFeatures/`

### Document Metadata
```markdown
# {Feature Name} - Feature Specification

**Version**: 1.0.0
**Created**: {Current Date in YYYY-MM-DD format}
**Feature Type**: {Type}
**Related Features**: {List or "None"}
```

---

## Validation Checklist

Before delivering the specification, confirm:

### Structure Compliance
- [ ] ALL 21 sections from template present
- [ ] Section order matches template exactly
- [ ] All subsections included
- [ ] Markdown formatting consistent (H1, H2, H3, H4 hierarchy)
- [ ] Tables formatted correctly
- [ ] Lists properly indented

### Technical Accuracy
- [ ] Technology stack versions correct (.NET 8.0, C# 12.0, MySQL 5.7.24)
- [ ] No forbidden MySQL features (CTEs, window functions, JSON)
- [ ] DAO methods use `Model_Dao_Result<T>` return type
- [ ] Error handling uses `Service_ErrorHandler`
- [ ] Logging uses `LoggingUtility`
- [ ] All database ops are async

### Content Completeness
- [ ] Constitutional alignment explained for each principle
- [ ] User goals clearly defined
- [ ] Business value articulated
- [ ] UI layouts fully described
- [ ] All components have exact properties
- [ ] Validation rules specific with error messages
- [ ] DAO mapping table complete
- [ ] Stored procedures defined
- [ ] User flows numbered and detailed
- [ ] Error handling comprehensive
- [ ] Performance targets specified
- [ ] Success criteria measurable

### Quality Standards
- [ ] No vague language ("should", "appropriate", "properly")
- [ ] Error messages quoted exactly
- [ ] Component sizes specified (WxH)
- [ ] Tab order defined
- [ ] Response times in milliseconds
- [ ] References to existing code accurate

---

## Example Prompt Usage

**User Input**:
```
Create a feature specification for a new Inventory Search feature that allows users to search inventory by Part Number, Operation, or Work Order with autocomplete suggestions.
```

**Your Response**:
1. Ask clarifying questions about scope, UI, filters, results display
2. Create specification using EXACT template structure
3. Research existing DAOs (`Dao_Inventory`, `Dao_Part`, `Dao_Operation`)
4. Define UI layout with exact component properties
5. Map database operations to existing/new stored procedures
6. Define user flows for search, filter, results, errors
7. Specify validation, error handling, logging, performance
8. Include success criteria and testing requirements
9. Verify all 21 sections present and complete

---

## Ready to Begin

When you receive a feature request, respond with:

1. **Confirmation of template usage**
2. **Clarifying questions** (if needed)
3. **Complete specification document** following the EXACT template structure

Remember: The template structure is NON-NEGOTIABLE. Every section, every subsection, in exact order.

---

**Template Path**: `.specify/templates/feature-spec-template.md`
**Reference Example**: `Documentation/FeatureDocuments/PlannedFeatures/QuickButton_Add_Feature_Specification.md`
**Target Location**: `Documentation/FeatureDocuments/PlannedFeatures/{FeatureName}_Feature_Specification.md`
