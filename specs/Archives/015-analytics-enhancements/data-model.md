# Data Model: Analytics & Inventory Management Enhancements

## Entities

### 1. SysVisual (New Table)
Stores metadata related to Infor Visual integration that doesn't exist in the core Visual database.

| Column | Type | Description |
|--------|------|-------------|
| `id` | INT (PK, AI) | Unique identifier |
| `json_shift_data` | JSON | User shift mappings: `{"USERNAME": int_shift}` |
| `json_user_fullnames` | JSON | User name mappings: `{"USERNAME": "Full Name"}` |
| `last_updated` | DATETIME | Timestamp of last sync |

### 2. UserShift (C# Model)
Represents a user's calculated shift assignment.

```csharp
public class Model_Visual_UserShift
{
    public string UserName { get; set; }
    public int ShiftNumber { get; set; } // 1, 2, 3, 4 (Weekend), 0 (Unknown)
    public string FullName { get; set; }
    public DateTime LastCalculated { get; set; }
}
```

### 3. MaterialHandlerScore (C# Model)
Represents a user's performance score.

```csharp
public class Model_Visual_MaterialHandlerScore
{
    public string UserName { get; set; }
    public int RawPoints { get; set; }
    public double WeightedScore { get; set; }
    public int TransactionCount { get; set; }
    public int TransferCount { get; set; } // 2pts
    public int AddRemoveCount { get; set; } // 1pt
}
```

## Relationships

- **SysVisual** is a singleton configuration table (effectively).
- **UserShift** is derived from `INVENTORY_TRANS` (Visual) and stored in `SysVisual`.
- **MaterialHandlerScore** is calculated from `INVENTORY_TRANS` and normalized using `UserShift` data.

## Validation Rules

- **Shift Number**: Must be 0, 1, 2, 3, or 4.
- **JSON Data**: Must be valid JSON format.
- **UserNames**: Must match Visual database usernames (uppercase, trimmed).
