# Research: Modern JSON-Driven Help System

**Feature**: `016-new-help-system`
**Date**: 2025-12-06

## Decisions

### 1. Content Storage Format
- **Decision**: Use structured JSON files for help content.
- **Rationale**: Decouples content from code, allows updates without recompilation, and maps easily to C# objects. Matches existing pattern in `RELEASE_NOTES.json`.
- **Alternatives Considered**:
  - *Markdown*: Requires a Markdown parser/renderer dependency (e.g., Markdig). JSON is native to .NET and sufficient for our needs.
  - *SQLite*: Overkill for read-only static content.
  - *Embedded Resources*: Requires recompilation to update content.

### 2. Rendering Engine
- **Decision**: Use `Microsoft.Web.WebView2`.
- **Rationale**: Modern, high-performance rendering engine that supports latest HTML/CSS/JS features. Already used in `SettingsForm_ViewReleaseNotesHTML.cs`.
- **Alternatives Considered**:
  - *WebBrowser (IE)*: Deprecated, poor rendering support.
  - *RichTextBox*: Limited formatting capabilities.

### 3. Search Implementation
- **Decision**: In-memory search using LINQ on loaded JSON models.
- **Rationale**: Dataset is small enough (dozens of files, hundreds of topics) that in-memory search is instant (<10ms). No need for a complex search indexer like Lucene.net.
- **Alternatives Considered**:
  - *Lucene.net*: Too heavy for this use case.
  - *SQL Full-Text Search*: Content is not in database.

### 4. Navigation Structure
- **Decision**: Two-level hierarchy (Category -> Topics).
- **Rationale**: Simple enough for users to understand, maps well to file system (1 file = 1 category), and fits the sidebar UI pattern.
- **Alternatives Considered**:
  - *Nested Tree*: Adds complexity to JSON structure and UI rendering.

## Unknowns Resolved

- **WebView2 Availability**: Confirmed available as it's already used in the project.
- **JSON Performance**: Confirmed `System.Text.Json` is performant enough for loading all help files at startup or on-demand.
