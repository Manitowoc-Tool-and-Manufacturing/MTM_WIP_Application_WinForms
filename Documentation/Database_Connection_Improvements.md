# Database Connection Improvements

**Date**: December 11, 2025  
**Status**: Implementation Plan  
**Priority**: Critical

## Problem Summary

Your MySQL server is experiencing connection issues that are affecting application performance:

- **Connection Pool Nearly Full**: 152 connections used (should be under 120)
- **Idle Connections**: Many connections sitting idle for 15+ minutes
- **Double Connection Usage**: Every data change opens 2 connections instead of 1
- **Multiple App Instances**: Users running several copies of the application

## What We're Fixing

### 1. Remove Double-Write to Test Database
**Current Issue**: Every time you save data, the app writes to BOTH the production database AND the test database. This doubles the number of connections.

**The Fix**: Only write to the database you're actively using. Test database writes will be handled separately during testing.

**User Impact**: ✅ Faster save operations, fewer connection errors

---

### 2. Single Application Instance Check
**Current Issue**: Users can open multiple copies of the application, each creating its own set of database connections.

**The Fix**: The application will check if it's already running. If you try to open it again, it will focus the existing window instead of opening a new one.

**User Impact**: ✅ Prevents accidental multiple instances, reduces connection usage

---

### 3. Automatic Connection Timeout (30 Minutes)
**Current Issue**: When you step away from the application for lunch or a meeting, your connection stays open indefinitely.

**The Fix**: After 30 minutes of no database activity, idle connections are automatically closed and will reconnect when you return.

**User Impact**: ✅ No action needed on your part - this happens automatically in the background

---

## Timeline

- **Phase 1** (Day 1): Remove double-write logic
- **Phase 2** (Day 2): Add single-instance check
- **Phase 3** (Day 3): Implement connection timeout
- **Testing** (Day 4): Verify all changes work correctly

## What You'll Notice

✅ **Faster Performance**: Data saves will be quicker  
✅ **Fewer Errors**: "Too many connections" errors will disappear  
✅ **Cleaner Desktop**: Can't accidentally open multiple app windows  
✅ **Automatic Cleanup**: Idle connections clean themselves up

## Technical Details (For IT Team)

### Database Name Locations
- `Model_Shared_Users.cs` line 22-26: Conditional database selection (DEBUG vs RELEASE)
- `Helper_Database_StoredProcedure.cs` line 491-494: Dual-write logic (TO BE REMOVED)
- `Helper_Database_Variables.cs`: Connection string builder

### Dual-Write Logic Location
- File: `Helper_Database_StoredProcedure.cs`
- Method: `ExecuteScalarWithStatusAsync`
- Lines: 480-517
- **Action**: Comment out dual-write block

### Implementation Tracking
- Branch: `001-validate-fix-daos`
- Commit: TBD after implementation
