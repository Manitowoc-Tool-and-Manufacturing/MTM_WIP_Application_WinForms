---
description: Identify underspecified areas in the current feature spec by generating an HTML clarification form (up to 15 questions) with user-friendly language and encoding answers back into the spec.
---

## Agent Communication Rules

**⚠️ EXTREMELY IMPORTANT - Maximize Premium Request Value**:

This prompt involves multiple analysis and generation steps. To maximize the value of each premium request:

- **Continue through ALL workflow steps** unless blocked by missing data or user input
- **Generate complete HTML files** with all questions in a single session
- **Process returned answers immediately** and update spec.md without stopping
- **Only pause for user input** when waiting for clarification answers
- **Document progress** if partial completion occurs (note which step completed)

**Do NOT stop prematurely** after generating the HTML - continue to wait for answers and complete the spec update in the same session when possible.

---

## User Input

```text
$ARGUMENTS
```

You **MUST** consider the user input before proceeding (if not empty).

## Outline

Goal: Detect and reduce ambiguity or missing decision points in the active feature specification and record the clarifications directly in the spec file.

Note: This clarification workflow is expected to run (and be completed) BEFORE invoking `/speckit.plan`. If the user explicitly states they are skipping clarification (e.g., exploratory spike), you may proceed, but must warn that downstream rework risk increases.

Execution steps:

1. Run `.specify/scripts/powershell/check-prerequisites.ps1 -Json -PathsOnly` from repo root **once** (combined `--json --paths-only` mode / `-Json -PathsOnly`). Parse minimal JSON payload fields:
   - `FEATURE_DIR`
   - `FEATURE_SPEC`
   - (Optionally capture `IMPL_PLAN`, `TASKS` for future chained flows.)
   - If JSON parsing fails, abort and instruct user to re-run `/speckit.specify` or verify feature branch environment.
   - For single quotes in args like "I'm Groot", use escape syntax: e.g 'I'\''m Groot' (or double-quote if possible: "I'm Groot").

2. Load the current spec file. Perform a structured ambiguity & coverage scan using this taxonomy. For each category, mark status: Clear / Partial / Missing. Produce an internal coverage map used for prioritization (do not output raw map unless no questions will be asked).

   Functional Scope & Behavior:
   - Core user goals & success criteria
   - Explicit out-of-scope declarations
   - User roles / personas differentiation

   Domain & Data Model:
   - Entities, attributes, relationships
   - Identity & uniqueness rules
   - Lifecycle/state transitions
   - Data volume / scale assumptions

   Interaction & UX Flow:
   - Critical user journeys / sequences
   - Error/empty/loading states
   - Accessibility or localization notes

   Non-Functional Quality Attributes:
   - Performance (latency, throughput targets)
   - Scalability (horizontal/vertical, limits)
   - Reliability & availability (uptime, recovery expectations)
   - Observability (logging, metrics, tracing signals)
   - Security & privacy (authN/Z, data protection, threat assumptions)
   - Compliance / regulatory constraints (if any)

   Integration & External Dependencies:
   - External services/APIs and failure modes
   - Data import/export formats
   - Protocol/versioning assumptions

   Edge Cases & Failure Handling:
   - Negative scenarios
   - Rate limiting / throttling
   - Conflict resolution (e.g., concurrent edits)

   Constraints & Tradeoffs:
   - Technical constraints (language, storage, hosting)
   - Explicit tradeoffs or rejected alternatives

   Terminology & Consistency:
   - Canonical glossary terms
   - Avoided synonyms / deprecated terms

   Completion Signals:
   - Acceptance criteria testability
   - Measurable Definition of Done style indicators

   Misc / Placeholders:
   - TODO markers / unresolved decisions
   - Ambiguous adjectives ("robust", "intuitive") lacking quantification

   For each category with Partial or Missing status, add a candidate question opportunity unless:
   - Clarification would not materially change implementation or validation strategy
   - Information is better deferred to planning phase (note internally)

3. Generate (internally) a prioritized queue of candidate clarification questions (**maximum 15** instead of 5). Do NOT output them all at once to chat. Instead, you will generate an HTML clarification form. Apply these constraints:
    - Maximum of 15 total questions (increased from 5 for better coverage).
    - Each question must be answerable with EITHER:
       * A multiple‑choice selection (4 distinct options: A, B, C, D), PLUS
       * An "Other" option (E) that reveals a text input field for custom answers
   - Only include questions whose answers materially impact architecture, data modeling, task decomposition, test design, UX behavior, operational readiness, or compliance validation.
   - Ensure category coverage balance: attempt to cover the highest impact unresolved categories first; avoid asking two low-impact questions when a single high-impact area (e.g., security posture) is unresolved.
   - Exclude questions already answered, trivial stylistic preferences, or plan-level execution details (unless blocking correctness).
   - Favor clarifications that reduce downstream rework risk or prevent misaligned acceptance tests.
   - If more than 15 categories remain unresolved, select the top 15 by (Impact * Uncertainty) heuristic.

4. **HTML Clarification Form Generation** (replaces sequential chat-based questioning):
    
    **CRITICAL CHANGE**: Instead of asking questions one-by-one in chat, generate an interactive HTML file that:
    
    a. **User-Friendly Language Requirements**:
       - Write questions at 8th grade reading level (no technical jargon)
       - Use plain language that non-programmers can understand
       - Avoid terms like "architecture", "API", "database schema", "authentication mechanism"
       - Instead use: "how it works", "where data is stored", "how people log in"
       - Example transformations:
         * ❌ "Should this implement OAuth2 or session-based authentication?"
         * ✅ "How should people log into the system?"
    
    b. **HTML Structure** (use `.specify/templates/clarification-questions-template.html`):
       - Load the template and replace `[FEATURE_NAME]`, `[DATE]`, `[TOTAL_QUESTIONS]`
       - For each question, generate HTML question card with:
         * Question number and category badge
         * Plain-language question title (8th grade level)
         * Context box explaining why we're asking (user-friendly)
         * 4 main options (A, B, C, D) as selectable cards
         * 5th "Other" option (E) that reveals text input when selected
         * Default-select your recommended answer (mark with "RECOMMENDED" badge)
         * Store technical details in data attributes (not visible to user):
           - `data-question-id="Q1"`
           - `data-question-category="scope"`
           - `data-question-text="Plain language question"`
           - `data-technical-context="Technical interpretation of question"`
           - `data-answer-text="Technical meaning of this option"` (on each option)
    
    c. **Question Card HTML Format**:
       ```html
       <div class="question-card" data-question-id="Q1" data-question-category="scope" 
            data-question-text="Should this work on phones and computers?" 
            data-technical-context="Responsive design requirements and platform support">
           <span class="question-number">Question 1</span>
           <h2 class="question-title">Should this work on phones and computers, or just computers?</h2>
           <div class="question-context">
               We need to know if people will use this on their phones, or if it's only for computer screens.
               This helps us design the right kind of interface.
           </div>
           
           <div class="options">
               <div class="option recommended">
                   <input type="radio" name="Q1" value="A" id="Q1-A" checked>
                   <div class="option-content">
                       <div class="option-label">
                           Just computers <span class="recommended-badge">RECOMMENDED</span>
                       </div>
                       <div class="option-description">
                           This will only work on regular computer screens (laptops and desktops).
                       </div>
                   </div>
               </div>
               
               <div class="option">
                   <input type="radio" name="Q1" value="B" id="Q1-B"
                          data-answer-text="Responsive design: mobile (320px+) and desktop (1024px+)">
                   <div class="option-content">
                       <div class="option-label">Both phones and computers</div>
                       <div class="option-description">
                           This will work on phone screens, tablets, and computer screens.
                       </div>
                   </div>
               </div>
               
               <!-- Options C, D similar format -->
               
               <div class="option">
                   <input type="radio" name="Q1" value="OTHER" id="Q1-OTHER">
                   <div class="option-content">
                       <div class="option-label">Other (type your answer)</div>
                       <div class="option-description">
                           None of these options work for you? Type your own answer below.
                       </div>
                   </div>
               </div>
           </div>
           
           <div class="custom-answer">
               <label for="Q1-custom">Your answer:</label>
               <input type="text" id="Q1-custom" placeholder="Type your answer here (keep it short)">
           </div>
       </div>
       ```
    
    d. **Save to Clipboard Functionality**:
       - Button remains disabled until ALL questions answered
       - When enabled and clicked, generates agent-ready format:
         * Technical question phrasing (not user-friendly version)
         * Technical answer interpretation (not plain language)
         * Includes all data attributes for agent parsing
       - Uses `.specify/templates/clarification-answers-template.md` structure
       - Copies to clipboard in markdown format ready for agent consumption
    
    e. **Progress Tracking**:
       - Visual progress bar showing N of 15 answered
       - Questions marked as "answered" get green highlight
       - Clear indication when form is complete
    
    f. **Write HTML File**:
       - Save to: `FEATURE_DIR/clarifications-[DATE].html`
       - Report path to user
       - Instruct user to: "Open this HTML file in your browser, answer the questions, click 'Save to Clipboard', then paste the answers back here."

5. **Wait for User Answers**:
    - User will open HTML, answer questions, and paste clipboard content back
    - Clipboard content will be in agent-ready markdown format from template
    - Parse the pasted answers to extract:
      * Question ID
      * Selected option or custom text
      * Technical interpretation from data attributes

6. **Integration after receiving answers** (same as before, but batch process all 15):
    - Maintain in-memory representation of the spec (loaded once at start) plus the raw file contents.
    - Ensure a `## Clarifications` section exists (create it just after the highest-level contextual/overview section per the spec template if missing).
    - Under it, create (if not present) a `### Session YYYY-MM-DD` subheading for today.
    - For each answer received:
      * Append a bullet line: `- Q[N]: <plain question> → A: <selected option or custom answer>`
      * Apply the technical interpretation to appropriate spec sections:
        - Functional ambiguity → Update or add bullet in Functional Requirements
        - User interaction / actor distinction → Update User Stories or Actors
        - Data shape / entities → Update Data Model (add fields, types, relationships)
        - Non-functional constraint → Add/modify in Non-Functional / Quality Attributes
        - Edge case / negative flow → Add bullet under Edge Cases / Error Handling
        - Terminology conflict → Normalize term across spec
      * If clarification invalidates earlier ambiguous statement, replace (don't duplicate)
    - Save the spec file AFTER all integrations (single atomic write)
    - Preserve formatting: do not reorder unrelated sections; keep heading hierarchy intact

7. Validation (performed after write):
   - Clarifications session contains exactly one bullet per accepted answer (no duplicates)
   - Total asked questions ≤ 15
   - Updated sections contain no lingering vague placeholders the answers resolved
   - No contradictory earlier statement remains
   - Markdown structure valid
   - Terminology consistency: same canonical term used across all updated sections

8. Write the updated spec back to `FEATURE_SPEC`.

9. Report completion:
   - Number of questions asked (out of 15 max)
   - Path to HTML file generated
   - Path to updated spec
   - Sections touched (list names)
   - Coverage summary table listing each taxonomy category with Status: Resolved, Deferred, Clear, Outstanding
   - If any Outstanding or Deferred remain, recommend whether to proceed to `/speckit.plan`
   - Suggested next command

Behavior rules:
- If no meaningful ambiguities found (or all potential questions would be low-impact), respond: "No critical ambiguities detected worth formal clarification." and suggest proceeding.
- If spec file missing, instruct user to run `/speckit.specify` first (do not create a new spec here).
- Never exceed 5 total asked questions (clarification retries for a single question do not count as new questions).
- Avoid speculative tech stack questions unless the absence blocks functional clarity.
- Respect user early termination signals ("stop", "done", "proceed").
 - If no questions asked due to full coverage, output a compact coverage summary (all categories Clear) then suggest advancing.
 - If quota reached with unresolved high-impact categories remaining, explicitly flag them under Deferred with rationale.

Context for prioritization: $ARGUMENTS
