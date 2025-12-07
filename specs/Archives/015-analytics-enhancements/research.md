# Research: Analytics & Inventory Management Enhancements

**Feature**: Analytics & Inventory Management Enhancements
**Date**: December 4, 2025

## Decisions

### 1. User Shift Calculation Logic
**Decision**: Use a heuristic approach based on the last 50 transactions per user.
**Rationale**: Infor Visual does not explicitly store shift assignments for all users in a reliable way. Analyzing transaction timestamps provides a data-driven, self-correcting method to determine actual working hours.
**Alternatives Considered**:
- *Manual Assignment*: Too much maintenance overhead for admins.
- *HR Database Integration*: No direct access to HR systems available.

### 2. Fair Grading Policy (Shift Weighting)
**Decision**: Implement a `ShiftVolumeFactor` normalization.
**Rationale**: 3rd shift and weekends typically have lower transaction volumes. Comparing raw counts unfairly penalizes these workers. Normalizing by shift average creates a level playing field.
**Formula**: `Score = (RawPoints) * (GlobalAvgTransactions / ShiftAvgTransactions)`

### 3. PO Details Form Refactor
**Decision**: Replace DataGridView with a "Form View" (TextBoxes + Navigation).
**Rationale**: PO lines often contain extensive specification text that is unreadable in a grid cell. A form view allows for a RichTextBox to display full specs, improving readability and reducing errors.
**Trade-off**: Users lose the "bird's eye view" of all lines at once, but gain detail clarity. Navigation buttons mitigate the loss of overview.

### 4. JSON Storage for User Metadata
**Decision**: Use JSON columns in `sys_visual` table for shift and name data.
**Rationale**: MySQL 5.7 supports JSON (text-based). This allows flexible schema evolution for user metadata without altering table structure for every new attribute.
**Constraint**: MySQL 5.7 JSON functions are limited, so parsing will be handled in C# application layer (Newtonsoft.Json).

## Unknowns & Clarifications

### Resolved
- **Infor Visual Data Source**: Confirmed access via existing CSV exports or direct DB connection (depending on environment). Will use existing `Helper_Database_StoredProcedure` patterns.
- **MySQL Version**: Confirmed 5.7.24. No CTEs or Window Functions allowed. All complex aggregation must happen in C# or standard SQL GROUP BY.

### Open Questions
- None at this stage.
