# Data Model: Color Code & Work Order Tracking

**Feature**: Color Code & Work Order Tracking  
**Date**: 2025-11-13  
**Database**: MySQL 5.7.24

## Entity Relationship Diagram

```
┌─────────────────────┐         ┌──────────────────────┐
│  md_color_codes     │         │    md_part_ids       │
├─────────────────────┤         ├──────────────────────┤
│ ColorCode PK        │         │ ID (Auto)            │
│ IsUserDefined       │         │ PartID (Unique)      │
│ CreatedDate         │         │ RequiresColorCode    │ ◄─┐
└─────────────────────┘         │ IssuedBy             │   │
         ▲                      └──────────────────────┘   │
         │                               ▲                 │
         │ FK (soft)                     │                 │
         │                               │ FK (soft)       │
┌────────┴─────────────────┐    ┌────────┴────────────┐   │
│   inv_inventory          │    │  inv_transaction    │   │
├──────────────────────────┤    ├─────────────────────┤   │
│ ID PK (Auto)             │    │ ID PK (Auto)        │   │
│ PartID                   │◄───┤ PartID              │   │
│ Location                 │    │ TransactionType     │   │
│ Operation                │    │ BatchNumber         │   │
│ Quantity                 │    │ FromLocation        │   │
│ ItemType                 │    │ ToLocation          │   │
│ ReceiveDate              │    │ Operation           │   │
│ LastUpdated              │    │ Quantity            │   │
│ User                     │    │ Notes               │   │
│ BatchNumber              │    │ User                │   │
│ Notes                    │    │ ItemType            │   │
│ ColorCode (NEW)          │    │ ReceiveDate         │   │
│ WorkOrder (NEW)          │    │ ColorCode (NEW)     │   │
└──────────────────────────┘    │ WorkOrder (NEW)     │   │
                                └─────────────────────┘   │
                                                          │
        Part flagged with RequiresColorCode=TRUE ────────┘
        triggers ColorCode/WorkOrder requirement
```

## Entities

### md_color_codes (NEW)

**Purpose**: Master table of predefined and user-defined color codes for inventory segregation

**Columns**:
| Column | Type | Constraints | Description |
|--------|------|-------------|-------------|
| ColorCode | VARCHAR(50) | PRIMARY KEY | Color name (e.g., "RED", "BLUE", "Blueberry") |
| IsUserDefined | BOOLEAN | NOT NULL DEFAULT FALSE | TRUE for custom colors added via "OTHER" |
| CreatedDate | DATETIME | NOT NULL DEFAULT CURRENT_TIMESTAMP | When color was added to system |

**Validation Rules**:
- ColorCode must be unique (enforced by PRIMARY KEY)
- ColorCode stored in Title Case (e.g., "Blueberry", not "blueberry")
- "Unknown" is system-reserved, cannot be selected by users
- "OTHER" triggers custom color dialog, not stored as literal "OTHER"

**Initial Data**:
```sql
INSERT INTO md_color_codes (ColorCode, IsUserDefined) VALUES
('Red', FALSE),
('Blue', FALSE),
('Green', FALSE),
('Yellow', FALSE),
('Orange', FALSE),
('Purple', FALSE),
('Pink', FALSE),
('White', FALSE),
('Black', FALSE),
('Unknown', FALSE);  -- System reserved for legacy data
```

---

### md_part_ids (MODIFIED)

**Purpose**: Master table of part numbers with metadata flags

**New Column**:
| Column | Type | Constraints | Default | Description |
|--------|------|-------------|---------|-------------|
| RequiresColorCode | BOOLEAN | NOT NULL | FALSE | TRUE if part requires color code and work order tracking |

**Existing Columns** (unchanged):
- ID (Auto-increment primary key)
- PartID (VARCHAR, unique, indexed)
- IssuedBy (VARCHAR, user who created part)

**Validation Rules**:
- RequiresColorCode can be toggled via Settings > Add/Edit Part ID forms
- Changing RequiresColorCode flag triggers app restart prompt
- Part flagging affects UI behavior across Inventory, Remove, Transfer tabs

**Relationships**:
- One-to-many with inv_inventory (one part can have many inventory records)
- One-to-many with inv_transaction (one part can have many transactions)
- Determines dynamic UI behavior (show/hide color code fields)

---

### inv_inventory (MODIFIED)

**Purpose**: Current inventory snapshot with location, quantity, and tracking data

**New Columns**:
| Column | Type | Constraints | Default | Index | Description |
|--------|------|-------------|---------|-------|-------------|
| ColorCode | VARCHAR(50) | NOT NULL | 'Unknown' | idx_colorcode | Color tag for work order segregation |
| WorkOrder | VARCHAR(10) | NOT NULL | 'Unknown' | idx_workorder | Work order number (format: WO-######) |

**Existing Columns** (unchanged):
- ID, PartID, Location, Operation, Quantity, ItemType, ReceiveDate, LastUpdated, User, BatchNumber, Notes

**Validation Rules**:
- ColorCode and WorkOrder mandatory when PartID has RequiresColorCode=TRUE
- ColorCode must exist in md_color_codes (soft FK, not enforced by DB)
- WorkOrder must match format WO-###### (validated in application layer)
- Default "Unknown" for legacy data pre-dating feature
- Users cannot manually select "Unknown" (system default only)

**Indexes**:
```sql
CREATE INDEX idx_colorcode ON inv_inventory(ColorCode);
CREATE INDEX idx_workorder ON inv_inventory(WorkOrder);
CREATE INDEX idx_partid_location ON inv_inventory(PartID, Location);  -- Existing
```

**Relationships**:
- Many-to-one with md_part_ids (via PartID, not enforced FK)
- Soft reference to md_color_codes (via ColorCode, not enforced FK)

---

### inv_transaction (MODIFIED)

**Purpose**: Historical transaction log (IN/OUT/TRANSFER operations)

**New Columns**:
| Column | Type | Constraints | Default | Description |
|--------|------|-------------|---------|-------------|
| ColorCode | VARCHAR(50) | NOT NULL | 'Unknown' | Color code at time of transaction |
| WorkOrder | VARCHAR(10) | NOT NULL | 'Unknown' | Work order number at time of transaction |

**Existing Columns** (unchanged):
- ID, TransactionType, BatchNumber, PartID, FromLocation, ToLocation, Operation, Quantity, Notes, User, ItemType, ReceiveDate

**Validation Rules**:
- ColorCode and WorkOrder copied from inv_inventory during transaction creation
- Immutable after transaction recorded (historical preservation)
- Transfer operations preserve ColorCode/WorkOrder unchanged

**Indexes**:
```sql
CREATE INDEX idx_partid ON inv_transaction(PartID);  -- Existing
CREATE INDEX idx_user ON inv_transaction(User);      -- Existing
```

**Relationships**:
- Many-to-one with md_part_ids (via PartID, not enforced FK)
- Historical snapshot, not updated when inv_inventory changes

---

## Data Flows

### Inventory Entry Flow

```
1. User enters PartID in Inventory Tab
   ↓
2. System checks Model_Application_Variables.ColorFlaggedParts
   ↓
3a. IF PartID NOT flagged:
    - Hide ColorCode/WorkOrder fields
    - Use DEFAULT values on save ("Unknown")
   
3b. IF PartID IS flagged:
    - Show ColorCode/WorkOrder fields
    - Mark both as required
    - Validate WorkOrder format on lose focus
    ↓
4. User selects ColorCode from SuggestionTextBox
   ↓
5a. IF ColorCode = predefined (Red, Blue, etc.):
    - Use selected value
   
5b. IF ColorCode = "OTHER":
    - Show custom color dialog
    - User enters custom name (max 15 chars)
    - Prompt: "Save to database?"
    - If Yes: Add to md_color_codes with Title Case
    ↓
6. User enters Work Order number
   ↓
7. System validates and auto-formats:
    - "64153" → "WO-064153"
    - "WO-1234" → "WO-001234"
    - "ABC123" → ERROR: "Invalid format..."
    ↓
8. User clicks Save
   ↓
9. System validates required fields
   ↓
10a. IF validation fails:
     - Show error via Service_ErrorHandler
     - Block save, return focus
   
10b. IF validation passes:
     - Call Dao_Inventory.AddAsync(... ColorCode, WorkOrder)
     - Stored procedure inserts into inv_inventory
     - Stored procedure inserts into inv_transaction
     ↓
11. Success confirmation
```

### Search & Remove Flow

```
1. User searches for PartID in Remove Tab
   ↓
2. System checks Model_Application_Variables.ColorFlaggedParts
   ↓
3a. IF PartID NOT flagged:
    - Hide ColorCode/WorkOrder columns
   
3b. IF PartID IS flagged:
    - Show ColorCode/WorkOrder columns after PartID
    - Auto-sort results by ColorCode ASC, then Location ASC
    - "Unknown" entries sorted to end
    ↓
4. User selects row(s) to remove
   ↓
5. System removes entire quantity (no partial removal)
   ↓
6. Transaction logged with ColorCode/WorkOrder preserved
```

### Transfer Flow

```
1. User searches for PartID in Transfer Tab
   ↓
2. IF PartID flagged: Show ColorCode/WorkOrder columns (read-only)
   ↓
3. User selects row, enters ToLocation
   ↓
4. System transfers inventory with ColorCode/WorkOrder unchanged
   ↓
5. Transaction logged with preserved ColorCode/WorkOrder
```

### Settings Configuration Flow

```
1. User opens Settings > Add/Edit Part ID
   ↓
2. Form displays "Requires Color Code & Work Order" checkbox
   ↓
3. User checks/unchecks checkbox
   ↓
4. User clicks Save
   ↓
5. System updates md_part_ids.RequiresColorCode
   ↓
6. System sets Form_Settings.requiresRestart = TRUE
   ↓
7. User closes Settings form
   ↓
8. Prompt: "Changes require restart. Restart now?"
   ↓
9a. User clicks Yes:
    - Application.Restart()
    - Cache refreshes on startup
   
9b. User clicks No:
    - Changes saved but cache stale until manual restart
```

## State Transitions

### Part Number State Machine

```
┌─────────────────────────────────┐
│  Part Exists in md_part_ids     │
│  RequiresColorCode = FALSE      │
└─────────────┬───────────────────┘
              │
              │ User sets RequiresColorCode=TRUE
              │ in Settings > Edit Part ID
              ▼
┌─────────────────────────────────┐
│  Part Flagged for Color Codes   │
│  RequiresColorCode = TRUE        │
└─────────────┬───────────────────┘
              │
              ├─ Inventory Tab: Shows ColorCode/WorkOrder fields
              ├─ Remove Tab: Shows ColorCode/WorkOrder columns, auto-sorts
              ├─ Transfer Tab: Shows ColorCode/WorkOrder columns (read-only)
              └─ Advanced Inventory: Blocks entry, prompts redirect
              
              │ User sets RequiresColorCode=FALSE
              │ (rare, data migration scenario)
              ▼
┌─────────────────────────────────┐
│  Part Unflagged                  │
│  RequiresColorCode = FALSE       │
│  Existing inventory retains      │
│  ColorCode/WorkOrder data        │
│  (historical preservation)       │
└──────────────────────────────────┘
```

### Color Code Lifecycle

```
┌──────────────────────┐
│ Predefined Colors    │
│ (Red, Blue, etc.)    │
│ IsUserDefined=FALSE  │
└──────┬───────────────┘
       │
       │ User selects from dropdown
       ▼
┌──────────────────────┐         ┌────────────────────┐
│ Assigned to          │         │ User selects       │
│ Inventory Record     │         │ "OTHER"            │
└──────────────────────┘         └────────┬───────────┘
                                          │
                                          ▼
                                 ┌────────────────────┐
                                 │ Custom Color Dialog│
                                 │ Max 15 chars       │
                                 └────────┬───────────┘
                                          │
                                          ▼
                                 ┌────────────────────┐
                                 │ Title Case Format  │
                                 │ "blueberry" →      │
                                 │ "Blueberry"        │
                                 └────────┬───────────┘
                                          │
                                  Prompt: Save to DB?
                                          │
                        ┌─────────────────┴─────────────────┐
                        │ YES                               │ NO
                        ▼                                   ▼
               ┌────────────────────┐            ┌──────────────────┐
               │ Add to             │            │ Use once, don't  │
               │ md_color_codes     │            │ persist          │
               │ IsUserDefined=TRUE │            └──────────────────┘
               └────────────────────┘
                        │
                        │ Duplicate check
                        ▼
               ┌────────────────────┐
               │ IF exists: Silent  │
               │ reuse, no error    │
               │ ELSE: Insert new   │
               └────────────────────┘
```

## Performance Considerations

### Indexes

**New Indexes**:
```sql
CREATE INDEX idx_colorcode ON inv_inventory(ColorCode);
CREATE INDEX idx_workorder ON inv_inventory(WorkOrder);
CREATE INDEX idx_requires_colorcode ON md_part_ids(RequiresColorCode);
```

**Existing Indexes** (utilized):
```sql
-- inv_inventory
idx_partid_location ON (PartID, Location)  -- Used for searches
idx_operation ON (Operation)                -- Existing
idx_receivedate ON (ReceiveDate)            -- Existing

-- inv_transaction
idx_partid ON (PartID)                      -- Used for transaction history
idx_user ON (User)                          -- Existing
```

### Query Optimization

**Cached Data** (in-memory, loaded at startup):
- `Model_Application_Variables.ColorFlaggedParts` (List<string> of PartIDs)
- `Model_Application_Variables.ColorCodes` (DataTable of md_color_codes)

**Database-Side Sorting**:
```sql
-- Remove/Transfer search results
SELECT * FROM inv_inventory
WHERE PartID = @PartID
ORDER BY 
    CASE WHEN ColorCode = 'Unknown' THEN 1 ELSE 0 END,  -- Unknown to end
    ColorCode ASC,                                       -- Alphabetical by color
    Location ASC;                                        -- Then by location
```

**Expected Query Performance**:
- Color code lookup: <5ms (cached in memory)
- Part flag check: <1ms (cached in memory)
- Inventory search with color sort: <100ms (indexed columns, up to 1000 records)
- Insert with color validation: <50ms (stored procedure with status output)

---

## Migration Scripts

See `Database/UpdatedTables/` for complete migration SQL:
1. `001_add_md_color_codes_table.sql`
2. `002_add_requires_colorcode_to_parts.sql`
3. `003_add_colorcode_workorder_to_inventory.sql`
4. `004_add_colorcode_workorder_to_transaction.sql`

All scripts include:
- Forward migration (ALTER TABLE)
- Index creation
- Data validation checks
- Rollback statements (commented)
