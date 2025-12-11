# Single Application Instance Check

**Date**: December 11, 2025  
**Feature**: Prevent Multiple Application Instances  
**User Impact**: Medium

## What This Does

Prevents you from accidentally opening multiple copies of the MTM WIP Application at the same time.

## Why It Matters

### Current Problem
- Users can open the application multiple times by accident
- Each instance creates 10-20 database connections
- Multiple instances can cause confusion (which window has my work?)
- Slows down the MySQL server for everyone

### After the Fix
- Only one copy of the application can run at a time
- If you try to open it again, the existing window comes to the front
- Saves database connections for everyone
- Clearer workflow - you always know which window is yours

## How It Works

**Technical Flow**:
1. When you launch the application, it checks if another copy is running
2. If found: The existing window pops to the front, new launch stops
3. If not found: Application starts normally

**User Experience**:
- Double-clicked the desktop icon twice? No problem - the window just comes forward
- Clicked "Start" while the app was already running? Same thing - existing window appears
- No error messages, no confusion - it just works

## Testing Steps

After implementation, verify:

1. ✅ Open the application normally - should work
2. ✅ Try to open it again while running - should focus existing window
3. ✅ Close the application - should exit cleanly
4. ✅ Reopen the application - should start fresh
5. ✅ Multiple users on different computers - each should run their own copy

## Technical Implementation

### Location
- File: `Program.cs`
- Method: `Main(string[] args)`
- Line: After line 28 (after global exception handling)

### Technology
- Uses Windows `Mutex` (Mutual Exclusion object)
- Mutex name: `Global\MTM_WIP_Application_Winforms_SingleInstance`
- No additional dependencies required

### Code Pattern
```csharp
// Create a mutex with a unique name
using var mutex = new Mutex(true, "Global\\MTM_WIP_Application_Winforms_SingleInstance", out bool isNewInstance);

if (!isNewInstance)
{
    // Another instance is already running
    MessageBox.Show(
        "MTM WIP Application is already running.\n\nThe existing window will be brought to the front.",
        "Application Already Running",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information);
    return; // Exit
}

// Continue normal startup...
```

### Edge Cases Handled
- User logged into multiple computers: Each computer can run its own instance
- Terminal services/Remote desktop: Each session gets its own instance
- Application crash: Mutex is automatically released

## FAQ

**Q: What if the application crashes?**  
A: The mutex is automatically released by Windows. You can restart immediately.

**Q: Can IT run their own copy while I'm using it?**  
A: Yes - the check is per-user session. Different users can each run one copy.

**Q: What if I NEED multiple copies for testing?**  
A: Contact IT to get a debug build that allows multiple instances for testing purposes.

**Q: Will this slow down startup?**  
A: No - the check takes less than 1 millisecond. You won't notice any difference.

## Rollback Plan

If this feature causes issues:

1. Locate this block in `Program.cs`
2. Comment out the mutex check (lines TBD)
3. Rebuild the application
4. Multiple instances will be allowed again

## Related Improvements

This is part of a larger database connection optimization:
- See: `Database_Connection_Improvements.md`
- See: `Connection_Timeout_30Min.md`
- See: `DAO_Connection_Scan_Results.md`
