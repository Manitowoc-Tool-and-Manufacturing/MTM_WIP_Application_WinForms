# Analytics Redesign Proposal (Developer‑Facing)

## Goals
- Treat bulk actions the same as single actions (no special weighting or exclusion).
- Provide a clean, role‑aware analytics experience for users and leads.
- Keep the experience simple, explainable, and consistent across roles.

## Core Principles
- **Equal treatment of bulk and single actions**: Advanced Inventory and Advanced Remove count the same as single entries/removals.
- **Role‑based visibility**: Users see their own data; leads can view team and drill into individuals.
- **Clarity over complexity**: Focus on a small set of easy‑to‑explain metrics and trends.
- **Transparency**: Every number should have a one‑sentence explanation available on hover or in a glossary panel.

## Suggested Information Architecture
### 1) Landing Experience
- **Normal user**: “My Analytics” only.
- **Lead**: “Team Analytics” with user selector and drill‑down.

### 2) Top Summary (shared)
A single row of large, easy‑to‑read cards:
- Total Transactions
- Total Quantity Moved
- Unique Parts Handled
- Active Days (days with any activity)

### 3) Trends & Context
- Simple trends over time (daily/weekly counts)
- Optional toggle: Transactions vs Quantity
- Highlight peak days and quiet days

### 4) Personal History (detail)
- Recent activity list (date, type, part, quantity, from/to)
- Filters: date range, transaction type

### 5) Glossary Panel
- Always accessible; concise definitions for each metric

## Fairness & Advanced Tools
- Bulk/advanced entries should be **tagged** as “Advanced Inventory” or “Advanced Remove” for clarity.
- Those tagged entries are **treated the same as single entries/removals** in all analytics.
- This avoids special‑case logic and keeps interpretation consistent.

## Role‑Specific Behaviors
### Normal User
- Sees only their data.
- No team comparisons.
- Emphasis on clarity and personal trends.

### Lead
- Sees team summary + user list.
- Can select a user to view the same “My Analytics” layout for that person.
- Can export summary tables.

## UI Layout Sketch (Text‑Only)
- Header: Date range, Scope selector (Myself / Team), Refresh
- Row 1: Summary cards (4 metrics)
- Row 2: Trend chart(s)
- Row 3: Activity table
- Right side: Glossary / Definitions panel

## Delivery Format
- Implemented as HTML rendered in WebView2, following the same pattern as ReceivingAnalytics_Enhanced.html.
- Data injected from C# as JSON and rendered client‑side.
- Provide a “Print” action that uses a print‑friendly layout.

## Print‑Friendly Mockup (8.5 x 11)
The following layout fits on a single letter‑size page in portrait.