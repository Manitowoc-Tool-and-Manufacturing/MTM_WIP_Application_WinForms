# Implementation Plan: Help System Enhancements / Contact Support System

**Branch**: `017-help-system-enhancements` | **Date**: 2025-12-07 | **Spec**: [specs/017-help-system-enhancements/spec.md](specs/017-help-system-enhancements/spec.md)
**Input**: Feature specification from `specs/017-help-system-enhancements/spec.md`

## Summary

Implement the Contact Support System inside the Help Viewer with database-backed submissions (UserFeedback, UserFeedbackComments), per-category email notifications, Developer Tools management, and window/control mapping configurability. All constitutional requirements are embedded: centralized error handling (Service_ErrorHandler), structured logging (LoggingUtility), validation (Service_Validation), WebView2 security (local templates, JS bridge), and annual tracking number generation with MySQL 5.7.24–compatible sequences. Data access uses stored procedures via Helper_Database_StoredProcedure and returns Model_Dao_Result<T>.

## Technical Context

**Language/Version**: C# 12 / .NET 8.0-windows (WinForms)  
**Primary Dependencies**: MySql.Data 9.4.0, Microsoft.Extensions.DependencyInjection 8.0.0, Microsoft.Extensions.Logging 8.0.0, ClosedXML 0.105.0, Microsoft.Web.WebView2 1.0.2792.45, HtmlSanitizer (planned)  
**Storage**: MySQL 5.7.24 (no JSON/CTE/window functions; stored procedures only)  
**Testing**: xUnit 2.6.2 (integration tests via BaseIntegrationTest), manual UI validation for WinForms  
**Target Platform**: Windows desktop (WebView2 runtime required)  
**Project Type**: WinForms desktop application with layered architecture (Forms → Services/DAOs → Stored Procedures)  
**Performance Goals**: Submit + confirmation ≤ 2s; filtering/sorting in Developer Tools ≤ 1s; CSV export for 10k rows ≤ 3s; email queue trigger ≤ 1 minute  
**Constraints**: WebView2 must load local templates only; secure JS bridge only; MySQL 5.7.24 feature set; all DB access via Helper_Database_StoredProcedure; no MessageBox for errors; single-instance Help Viewer  
**Scale/Scope**: Internal WIP app (tens to low hundreds of users), single WinForms app, ~6 new tables/SP sets for feedback and mappings

## Constitution Check

**Gate (pre-design): PASS** — Plan aligns with constitution:
- User Trust (I): Service_ErrorHandler for all errors; retries/logging for DB/email failures; single-instance Help Viewer
- Operational Transparency (II): LoggingUtility for submissions, status changes, DB errors, email attempts
- Data Quality (III): Service_Validation before DB; DB constraints + stored procedures; XSS/SQL injection mitigations
- Consistent UX (IV): User-friendly names only; Help access from all screens; clear validation messaging
- Performance (V): Explicit SLAs (2s submit, 1s filter, 3s export)
- Security & Access (VI): Role-based access for Developer Tools; secure JS bridge; local-only templates; input sanitization
- Communication Clarity (VII): User-facing names; confirmations with tracking numbers; clear errors
- Maintainability (VIII): XML docs required (FR-045); nullable annotations (FR-046); DI-friendly services/DAOs
- Testability (IX): Stored procedures + DAO contracts; measurable SCs; edge cases enumerated
- Resilience (X): Email retry/backoff; DB failure handling; role-change handling (FR-047)
- Feedback Accessibility/Responsiveness (XI, XII): P1 priority; per-category notifications; View My Submissions
- Privacy/Retention (XIII): Soft delete via IsActive/IsDuplicate; retention handled via DB policy (future task)
- Spam Prevention (XIV): Duplicates handled manually; rate limiting deferred to future (note for tasks)

## Project Structure

### Documentation (this feature)

```text
specs/017-help-system-enhancements/
├── plan.md          # This file (/speckit.plan output)
├── research.md      # Phase 0 output (clarifications/best practices)
├── data-model.md    # Phase 1 output (entities, validation, states)
├── quickstart.md    # Phase 1 output (how to run/apply DB assets)
├── contracts/       # Phase 1 output (DAO/SP contracts)
└── tasks.md         # Phase 2 output (/speckit.tasks)
```

### Source Code (repository root)

```text
Core/                    # Theming, DI, utilities
Data/                    # DAOs (Helper_Database_StoredProcedure only)
Services/                # Service_* (ErrorHandler, Logging, FeedbackManager, etc.)
Forms/                   # WinForms; ThemedForm inheritance
Controls/                # User controls; ThemedUserControl inheritance
Helpers/                 # Helper_* utilities
Models/                  # DTOs, enums, result models
Database/UpdatedStoredProcedures/  # Stored procedure scripts
Documentation/Help/Templates/      # HTML templates (local-only)
Resources/               # Assets (logo, default JSON)
specs/017-help-system-enhancements # Feature docs (this plan)
```

**Structure Decision**: Single WinForms application with layered architecture already present; no new projects introduced. All new code will live under existing Data/, Services/, Forms/, Helpers/, Models/, and Database/UpdatedStoredProcedures/ as appropriate.

## FR-001 Help Button Coverage Snapshot

Source: [specs/017-help-system-enhancements/FR-001-Table.md](specs/017-help-system-enhancements/FR-001-Table.md)
- **Total mapped components**: 27
- **Help buttons implemented (YES)**: 2 (Inventory tab, Help Viewer built-in)
- **Help buttons missing (NO)**: 24 (all remaining tabs/forms/dialogs listed in FR-001 table)
- **Priority focus for implementation**: MainForm tabs (Advanced Inventory, Remove, Advanced Removal, Transfer), Settings panels (About, Database, Part Numbers, Operations, Locations, Inventory Types, Users, Shortcuts, Theme), specialized forms (Print, Transactions, Transaction Lifecycle, Visual Dashboard, Analytics, Analytics Viewer, Logs Viewer, Error Reports, Release Notes), dialogs (Quick Button Edit, Shortcut Edit, PO Details)

## Complexity Tracking

No constitutional violations requiring justification. XML docs and nullable annotations mandated (FR-045, FR-046).

## Phase 5 – XML Documentation
- Inventory every public surface area touched by this plan.
- Produce or update XML comments (summary/param/returns/exception) matching implementation intent.
- Call out any intentional exclusions and obtain reviewer sign-off before build.
