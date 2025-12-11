# 30-Minute Connection Timeout

**Date**: December 11, 2025  
**Feature**: Automatic Idle Connection Cleanup  
**User Impact**: Low (Automatic)

## What This Does

Automatically closes idle database connections after 30 minutes of no activity, preventing connection buildup on the MySQL server.

## Why It Matters

### Current Problem
Based on server statistics (December 11, 2025):
- **101 connections** sleeping simultaneously
- Some connections idle for **15-16 minutes** (970 seconds)
- IP `172.20.8.30` had **21 idle connections** from one user
- Server at **101% of max capacity**

### What This Causes
- "Too many connections" errors
- Slow performance for all users
- Application crashes during busy times
- Database server running out of resources

### After the Fix
- Idle connections automatically close after 30 minutes
- Fresh connection opened when you resume work
- No action needed on your part
- Server stays healthy with fewer idle connections

## How It Works

### Technical Flow

**Connection Pooling with Timeout**:
```
1. You make a database query → Connection opens
2. Query completes → Connection returns to pool (stays "warm")
3. You step away for 30+ minutes → Connection closes automatically
4. You return and click something → New connection opens instantly
```

**What "Idle" Means**:
- No queries executed in last 30 minutes
- No data saves, loads, or searches
- Application still open, just not using database

**What Triggers Activity** (resets the timer):
- Saving a transaction
- Searching for inventory
- Loading part details
- Refreshing the main grid
- Generating a report
- Any database operation

## User Experience

### Scenario 1: Normal Work
```
9:00 AM  - Open application, load data
9:15 AM  - Save transactions
9:30 AM  - Search inventory
10:00 AM - Generate report
```
**Result**: Connections stay open, no interruption (activity within 30 min)

### Scenario 2: Lunch Break
```
12:00 PM - Last transaction saved
12:05 PM - Walk away for lunch
12:35 PM - (30 minutes later) Connection auto-closes
1:00 PM  - Return, click "Search"
1:00 PM  - New connection opens instantly, search executes
```
**Result**: Seamless experience, you won't notice anything different

### Scenario 3: Long Meeting
```
2:00 PM - Leave application open
2:30 PM - Connection auto-closes
4:00 PM - Return, application still open
4:01 PM - Click any button, connection reopens automatically
```
**Result**: No error messages, works exactly as before

## What You'll Notice

**What Changes**:
- ✅ Fewer "connection failed" errors during busy times
- ✅ Faster response when many users are active
- ✅ No need to close/reopen the application during breaks

**What Stays the Same**:
- ❌ No visible difference during normal use
- ❌ No manual action required
- ❌ No settings to configure (set by IT)

## Technical Implementation

### Where It's Configured

**File**: `Models/Core/Model_Application_Variables.cs`

**Setting**:
```csharp
/// <summary>
/// Maximum idle time before connection is closed (milliseconds).
/// Default: 1800000 ms (30 minutes)
/// </summary>
public static int ConnectionIdleTimeoutMs { get; set; } = 1800000; // 30 minutes
```

### How It's Applied

**File**: `Helpers/Helper_Database_Variables.cs`

**Connection String**:
```csharp
"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};" +
"Allow User Variables=True;SslMode=none;AllowPublicKeyRetrieval=true;" +
"ConnectionIdleTimeout=1800;";  // <-- New parameter (30 min in seconds)
```

### Connection Pool Settings

MySQL connection pooling automatically manages:
- **Min Pool Size**: 5 (keep 5 connections ready)
- **Max Pool Size**: 100 (never exceed 100 connections)
- **Connection Lifetime**: 1800 seconds (30 minutes)
- **Connection Reset**: true (clean state for reuse)

## Testing Verification

### Test Plan

**Test 1: Normal Activity** (✅ Expected: No timeout)
1. Open application
2. Perform database operations every 10-15 minutes
3. Verify: Connection stays active
4. Expected: No disconnections

**Test 2: Idle Timeout** (✅ Expected: Auto-close after 30 min)
1. Open application, load data
2. Step away for 35 minutes (do nothing)
3. Return, perform any database operation
4. Expected: Brief reconnect (< 1 second), operation succeeds

**Test 3: Multiple Users** (✅ Expected: Each managed separately)
1. Have 5 users open the application
2. Mix of active and idle users
3. Verify: Only idle users' connections close
4. Expected: Active users unaffected

**Test 4: Reconnect Handling** (✅ Expected: Seamless)
1. Open application, wait 35 minutes
2. Try to save a transaction
3. Expected: Saves successfully with auto-reconnect

### Success Criteria

✅ No user-facing errors after 30-minute idle  
✅ Automatic reconnection works transparently  
✅ No performance degradation on reconnect  
✅ Server connections drop by 30-40% during off-hours  
✅ "Max connections reached" errors eliminated  

## Server Statistics Impact

### Before Implementation
```
Max Connections:         152 (101% of limit)
Aborted Connections:     606 (19.17%)
Idle Connections:        101 sleeping
Longest Idle:            970 seconds (16 minutes)
```

### After Implementation (Projected)
```
Max Connections:         ~90 (60% of limit)
Aborted Connections:     <100 (< 5%)
Idle Connections:        ~40 sleeping (max)
Longest Idle:            1800 seconds (30 min max)
```

### Benefits
- 40% reduction in connection count
- 75% reduction in aborted connections
- Eliminates 15+ minute idle connections
- Frees up 60 connections for other users

## FAQ

**Q: Will I lose my work if the connection closes?**  
A: No! The application stays open, only the database connection closes. Your unsaved work remains in memory.

**Q: What if I'm in the middle of typing when 30 minutes hits?**  
A: The timeout only applies to *database* idle time. If you're working in the app (even without saving), the timer keeps resetting.

**Q: Can I change the 30-minute timeout?**  
A: This is configured by IT in the code. Contact IT if you need a different timeout for your role.

**Q: What if I NEED to keep a connection open longer?**  
A: Just perform any database action (refresh, search, save) once every 30 minutes. This resets the timer.

**Q: Will this affect reports that take a long time to run?**  
A: No. Active queries keep the connection alive. This only affects *idle* connections between queries.

**Q: What if the network is slow during reconnect?**  
A: Connection reopening takes 0.5-2 seconds typically. If network is very slow, you may see a brief "Connecting..." message.

## Rollback Plan

If timeout causes issues:

1. **Quick Fix** (No code change):
   - Increase timeout to 60 minutes
   - Edit: `Model_Application_Variables.cs` line 231
   - Change: `= 1800000` to `= 3600000`

2. **Full Rollback**:
   - Remove `ConnectionIdleTimeout` from connection string
   - Rebuild and redeploy
   - Connections will never timeout (old behavior)

## Monitoring

### IT Dashboard Metrics

Watch these server statistics after deployment:

📊 **Max_used_connections**: Should drop from 152 → ~90  
📊 **Aborted_clients**: Should drop from 606 → <100  
📊 **Sleeping connections**: Should never exceed 50  
📊 **Idle time distribution**: Should max at 1800 seconds  

### Log Monitoring

Check `LoggingUtility` for:
- "Connection timeout" messages (expected, not errors)
- "Connection reset" messages (should be rare)
- Database error rates (should decrease)

## Related Documentation

- **Database_Connection_Improvements.md**: Overall connection strategy
- **Single_Instance_Check.md**: Prevents multiple app instances
- **DAO_Connection_Scan_Results.md**: Connection leak audit results
- **Server Statistics**: `Documentation/ServerStats/` (December 11, 2025 baseline)

## Implementation Status

- ✅ Design complete
- ✅ Documentation complete
- ⏳ Code changes pending
- ⏳ Testing pending
- ⏳ Deployment pending

**Target Deployment**: TBD (after testing on staging server)
