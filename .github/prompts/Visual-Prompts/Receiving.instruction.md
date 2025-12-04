# Instruction: Receiving Query

## Context
This query retrieves Receiving data from the Infor Visual ERP database, including open POs scheduled for the current week.

## Tables
- RECEIVER
- RECEIVER_LINE
- PURCHASE_ORDER
- PURC_ORDER_LINE
- VENDOR
- PART
- WAREHOUSE
- LOCATION

## Primary Keys
- RECEIVER.ID (Primary Key)
- RECEIVER_LINE.RECEIVER_ID, RECEIVER_LINE.LINE_NO (Composite Primary Key)
- PURCHASE_ORDER.ID (Primary Key)
- PURC_ORDER_LINE.PURC_ORDER_ID, PURC_ORDER_LINE.LINE_NO (Composite Primary Key)
- VENDOR.ID (Primary Key)

## PO Status Codes
- **'O'** - Open
- **'P'** - Printed
- **'A'** - Approved
- **'C'** - Closed (exclude from queries)
- **Line Status** - Same codes apply to PURC_ORDER_LINE.LINE_STATUS

## Table Relationships
1. **RECEIVER ↔ PURCHASE_ORDER**
   - Relationship: Many-to-One (Multiple receivers can reference one PO)
   - Join: `RECEIVER.PURC_ORDER_ID = PURCHASE_ORDER.ID`
   - Foreign Key: FKEY0167 (RECEIVER.PURC_ORDER_ID → PURCHASE_ORDER.ID)

2. **RECEIVER ↔ RECEIVER_LINE**
   - Relationship: One-to-Many (One receiver has many line items)
   - Join: `RECEIVER.ID = RECEIVER_LINE.RECEIVER_ID`
   - Foreign Key: FKEY0172 (RECEIVER_LINE.RECEIVER_ID → RECEIVER.ID)

3. **RECEIVER_LINE ↔ PURC_ORDER_LINE**
   - Relationship: Many-to-One
   - Join: `RECEIVER_LINE.PURC_ORDER_ID = PURC_ORDER_LINE.PURC_ORDER_ID AND RECEIVER_LINE.PURC_ORDER_LINE_NO = PURC_ORDER_LINE.LINE_NO`
   - Foreign Key: FKEY0171 (RECEIVER_LINE.PURC_ORDER_ID, PURC_ORDER_LINE_NO → PURC_ORDER_LINE)

4. **PURCHASE_ORDER ↔ VENDOR**
   - Relationship: Many-to-One
   - Join: `PURCHASE_ORDER.VENDOR_ID = VENDOR.ID`
   - Foreign Key: FKEY0079 (PURCHASE_ORDER.VENDOR_ID → VENDOR.ID)

5. **PURC_ORDER_LINE ↔ PART**
   - Relationship: Many-to-One
   - Join: `PURC_ORDER_LINE.PART_ID = PART.ID`
   - Foreign Key: FKEY0101 (PURC_ORDER_LINE.PART_ID → PART.ID)

6. **RECEIVER_LINE ↔ LOCATION**
   - Relationship: Many-to-One (for receiving location)
   - Join: `RECEIVER_LINE.WAREHOUSE_ID = LOCATION.WAREHOUSE_ID AND RECEIVER_LINE.LOCATION_ID = LOCATION.ID`

## Key Columns

### RECEIVER Table
- **RECEIVER.ID** - Receiver identifier
- **RECEIVER.PURC_ORDER_ID** - Purchase order number
- **RECEIVER.RECEIVED_DATE** - Date received
- **RECEIVER.USER_ID** - User who received
- **RECEIVER.CREATE_DATE** - Date record created
- **RECEIVER.CARRIER_ID** - Shipping carrier
- **RECEIVER.BOL_ID** - Bill of lading

### RECEIVER_LINE Table
- **RECEIVER_LINE.LINE_NO** - Line item number
- **RECEIVER_LINE.PART_ID** - Part received
- **RECEIVER_LINE.RECEIVED_QTY** - Quantity received
- **RECEIVER_LINE.ACCEPTED_QTY** - Quantity accepted
- **RECEIVER_LINE.REJECTED_QTY** - Quantity rejected
- **RECEIVER_LINE.PURC_ORDER_ID** - Reference to purchase order
- **RECEIVER_LINE.PURC_ORDER_LINE_NO** - Reference to PO line

### PURCHASE_ORDER Table
- **PURCHASE_ORDER.ID** - PO number
- **PURCHASE_ORDER.VENDOR_ID** - Vendor identifier
- **PURCHASE_ORDER.ORDER_DATE** - PO creation date
- **PURCHASE_ORDER.DESIRED_RECV_DATE** - When PO should be received
- **PURCHASE_ORDER.PROMISE_DATE** - Vendor promised delivery date
- **PURCHASE_ORDER.STATUS** - PO status ('O'=Open, 'C'=Closed, etc.)
- **PURCHASE_ORDER.LAST_RECEIVED_DATE** - Most recent receipt date
- **PURCHASE_ORDER.BUYER** - Buyer name

### PURC_ORDER_LINE Table
- **PURC_ORDER_LINE.PURC_ORDER_ID** - PO number
- **PURC_ORDER_LINE.LINE_NO** - Line number
- **PURC_ORDER_LINE.PART_ID** - Part ordered
- **PURC_ORDER_LINE.ORDER_QTY** - Quantity ordered
- **PURC_ORDER_LINE.TOTAL_RECEIVED_QTY** - Total quantity received to date
- **PURC_ORDER_LINE.USER_ORDER_QTY** - User-entered order quantity
- **PURC_ORDER_LINE.DESIRED_RECV_DATE** - Line-specific receive date
- **PURC_ORDER_LINE.PROMISE_DATE** - Line-specific promise date
- **PURC_ORDER_LINE.LINE_STATUS** - Line status ('O'=Open, 'C'=Closed, etc.)
- **PURC_ORDER_LINE.UNIT_PRICE** - Price per unit
- **PURC_ORDER_LINE.TOTAL_AMT_ORDERED** - Total line amount

### VENDOR Table
- **VENDOR.ID** - Vendor identifier
- **VENDOR.NAME** - Vendor name

### PART Table
- **PART.ID** - Part number
- **PART.DESCRIPTION** - Part description

### LOCATION Table
- **LOCATION.ID** - Receiving location identifier
- **LOCATION.WAREHOUSE_ID** - Warehouse identifier

## Calculated Fields
- **Remaining Quantity** = ORDER_QTY - TOTAL_RECEIVED_QTY
- **Variance** = RECEIVED_QTY - ORDERED_QTY
- **Acceptance Rate** = (ACCEPTED_QTY / RECEIVED_QTY) * 100
- **Outstanding PO Lines** = Lines where (ORDER_QTY - TOTAL_RECEIVED_QTY) > 0

## Query Patterns

### Pattern 1: POs Due This Week (Monday-Friday)
To find all open POs scheduled for the current week:

**User Selection:** Allow user to select which date field to filter by:
- **PO Desired Date** - PURCHASE_ORDER.DESIRED_RECV_DATE
- **PO Promise Date** - PURCHASE_ORDER.PROMISE_DATE
- **Line Desired Date** - PURC_ORDER_LINE.DESIRED_RECV_DATE
- **Line Promise Date** - PURC_ORDER_LINE.PROMISE_DATE
- **Any of the Above** - Check all four date fields (OR condition)

```sql
WHERE 
    PURCHASE_ORDER.STATUS <> 'C'  -- Not closed
    AND PURC_ORDER_LINE.LINE_STATUS <> 'C'  -- Line not closed
    AND (
        -- Use selected date field(s) based on user preference
        -- If "Any", use all four with OR:
        PURC_ORDER_LINE.DESIRED_RECV_DATE BETWEEN @WeekStart AND @WeekEnd
        OR PURCHASE_ORDER.DESIRED_RECV_DATE BETWEEN @WeekStart AND @WeekEnd
        OR PURC_ORDER_LINE.PROMISE_DATE BETWEEN @WeekStart AND @WeekEnd
        OR PURCHASE_ORDER.PROMISE_DATE BETWEEN @WeekStart AND @WeekEnd
        
        -- If user selects specific field, use only that field:
        -- Example: PURC_ORDER_LINE.DESIRED_RECV_DATE BETWEEN @WeekStart AND @WeekEnd
    )
    AND (PURC_ORDER_LINE.ORDER_QTY - PURC_ORDER_LINE.TOTAL_RECEIVED_QTY) > 0
```

**Example for Week of 11/24/2025 - 11/28/2025:**
- @WeekStart = '2025-11-24' (Monday)
- @WeekEnd = '2025-11-28' (Friday)

**Note:** Check both header-level (PURCHASE_ORDER) and line-level (PURC_ORDER_LINE) dates, as some systems use one or the other. Provide user with option to select which date field is most relevant for their workflow.

### Pattern 2: Recent Receipts
To find recently received items:

```sql
WHERE 
    RECEIVER.RECEIVED_DATE >= @StartDate
    AND RECEIVER.RECEIVED_DATE <= @EndDate
ORDER BY 
    RECEIVER.RECEIVED_DATE DESC
```

### Pattern 3: Outstanding PO Lines
To find all open PO lines with outstanding quantity:

```sql
WHERE 
    PURCHASE_ORDER.STATUS <> 'C'
    AND PURC_ORDER_LINE.LINE_STATUS <> 'C'
    AND (PURC_ORDER_LINE.ORDER_QTY - PURC_ORDER_LINE.TOTAL_RECEIVED_QTY) > 0
ORDER BY 
    PURC_ORDER_LINE.DESIRED_RECV_DATE,
    PURCHASE_ORDER.ID
```

## Additional Related Tables
- **RECEIVER_LINE_DEL** - Delivery schedule details
- **RECEIVER_BINARY** - Attachments/documents
- **CONSIGN_RECEIVER** - Consignment receipts (if applicable)

## Constraints
- Read-only access
- No stored procedures
