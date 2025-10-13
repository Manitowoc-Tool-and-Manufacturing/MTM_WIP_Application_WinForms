---
description: 'Performance optimization guidelines for MTM application'
applyTo: '**/*.cs'
---

<!-- Based on patterns from: https://github.com/github/awesome-copilot -->

# Performance Optimization Guidelines

## Overview

This file defines performance optimization standards and best practices for the MTM WIP Application to ensure responsive UI, efficient database operations, and optimal resource usage.

## Core Principles

### Performance by Design
- Consider performance from the start
- Measure before optimizing
- Focus on user-perceived performance
- Balance performance with maintainability

### Sub-100ms UI Response
- UI interactions must feel instantaneous
- Button clicks respond within 100ms
- Form field updates without lag
- Smooth scrolling and animations

### 30-Second Database Timeout
- Complex queries complete within timeout
- Use connection pooling efficiently
- Implement retry logic for transient failures
- Monitor slow queries

## Async/Await Patterns

### Always Use Async for I/O
- All database operations must be asynchronous
- File I/O should be asynchronous
- Network calls must be asynchronous
- Never block the UI thread

### Async Best Practices
```
// ✅ GOOD: Async all the way
public async Task<List<Item>> GetItemsAsync()
{
    return await _repository.GetItemsAsync();
}

// ❌ BAD: Blocking async call
public List<Item> GetItems()
{
    return _repository.GetItemsAsync().Result; // Causes deadlocks!
}
```

### ConfigureAwait Usage
- Use `ConfigureAwait(false)` in helper/DAO code that does not interact with the UI thread.
- Do not use `ConfigureAwait(false)` inside WinForms event handlers or code that needs to resume on the UI thread.
- WinForms controls require marshaling back to the main thread via `Invoke`, `BeginInvoke`, or the synchronization context.

## Database Performance

### Connection Pooling
- MinPoolSize: 5, MaxPoolSize: 100
- Let pool manage connection lifecycle
- Dispose connections properly with using statements
- Don't hold connections open unnecessarily

### Connection Pooling Pattern
```
using (var connection = new MySqlConnection(_connectionString))
{
    await connection.OpenAsync();
    // Execute query
    // Connection returned to pool automatically
}
```

### Query Optimization
- Use stored procedures for complex operations
- Index columns used in WHERE and JOIN clauses
- Limit result sets with appropriate filters
- Avoid SELECT * - specify needed columns
- Use EXPLAIN to analyze query plans

### Stored Procedure Performance
- Keep procedures focused and single-purpose
- Avoid cursors - use set-based operations
- Cache execution plans with parameterized procedures
- Monitor slow query log

## Memory Management

### Proper Disposal
- Dispose IDisposable resources with using statements
- Implement IAsyncDisposable for async resources
- Don't hold references to large objects longer than needed
- Clear collections when no longer needed

### Data Binding Collections
- Use `BindingList<T>` or `BindingSource` for WinForms data binding scenarios.
- Avoid refreshing entire `DataTable` instances when a targeted row update suffices.
- Suspend layout and binding (`BindingSource.SuspendBinding`) before bulk updates to reduce flicker.
- Resume binding and refresh the control after batch operations to keep the UI responsive.

### Memory Leak Prevention
- Unsubscribe from events when done
- Clear event handlers in Dispose methods
- Avoid circular references
- Monitor memory usage during development

## UI Performance

### DataGridView Performance
- Populate grids with pre-built `DataTable` or `BindingList<T>` instances to avoid per-cell updates.
- Enable double buffering on heavy grids to reduce flicker (`dataGridView.GetType().InvokeMember("DoubleBuffered", ...)`).
- Disable automatic column resizing during bulk loads and restore preferred sizing afterward.
- Filter and page data on the database side when possible to keep row counts manageable.

### Control Binding Performance
- Suspend layout (`SuspendLayout`) and resume after batch operations to minimize redraw.
- Avoid per-row calls to `Refresh()`; let the control repaint once after data updates.
- Keep bindings simple—calculate derived values in the DAO or helper layer instead of in binding expressions.
- Use caching for repetitive lookup data (e.g., combo box lists) instead of hitting the database repeatedly.

### Form Layout Performance
- Keep container hierarchies shallow; nested table layouts can become expensive.
- Reuse shared controls from `Controls/Shared` rather than instantiating duplicates across forms.
- Preload static resources (icons, images) once and reuse handles to avoid repeated disk access.
- Dispose hidden forms and dialogs that are no longer needed to release handles promptly.

## Startup Performance

### Application Startup
- Target: Sub-5 second startup time
- Lazy-load services when possible
- Defer non-critical initialization
- Show splash screen during startup

### Resource Initialization
- Initialize logging (`Service_DebugTracer`, `LoggingUtility`) before other subsystems to capture startup events.
- Validate database connectivity early so users receive actionable errors before the main form loads.
- Defer non-critical services (version checks, telemetry) until after the main UI is responsive.
- Cache frequently used lookup data after the splash screen while showing progress feedback.

## Caching Strategies

### When to Cache
- Expensive-to-compute data
- Frequently accessed reference data
- Slow database queries
- External API calls

### Caching Pattern
```
private readonly MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

public async Task<List<Item>> GetItemsCachedAsync()
{
    return await _cache.GetOrCreateAsync("items_key", async entry =>
    {
        entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
        return await _repository.GetItemsAsync();
    });
}
```

### Cache Invalidation
- Invalidate cache when data changes
- Use appropriate expiration times
- Consider memory constraints
- Monitor cache hit rates

## Parallel Operations

### When to Use Parallel
- Independent I/O operations
- CPU-bound operations on collections
- Multiple database queries
- Non-dependent service calls

### Parallel Pattern
```
// ✅ GOOD: Parallel independent operations
var tasks = new[]
{
    _service1.GetDataAsync(),
    _service2.GetDataAsync(),
    _service3.GetDataAsync()
};
var results = await Task.WhenAll(tasks);

// ❌ BAD: Sequential when could be parallel
var data1 = await _service1.GetDataAsync();
var data2 = await _service2.GetDataAsync();
var data3 = await _service3.GetDataAsync();
```

## Collection Performance

### Choose Right Collection
- List<T>: Fast indexed access, slower insert/remove
- Dictionary<TKey, TValue>: Fast lookups by key
- HashSet<T>: Fast membership tests
- ObservableCollection<T>: UI-bound collections

### Collection Optimization
- Pre-allocate capacity if size known
- Use AddRange instead of multiple Add calls
- Clear collections instead of creating new ones
- Use LINQ efficiently

## LINQ Performance

### LINQ Best Practices
- Use deferred execution when possible
- Avoid multiple enumeration (use ToList() when needed)
- Prefer query syntax for readability
- Be aware of LINQ-to-Objects vs LINQ-to-SQL

### LINQ Optimization Example
```
// ✅ GOOD: Single enumeration
var items = GetItems().Where(x => x.IsActive).ToList();
var count = items.Count;
var total = items.Sum(x => x.Value);

// ❌ BAD: Multiple enumerations
var query = GetItems().Where(x => x.IsActive);
var count = query.Count(); // Enumerates
var total = query.Sum(x => x.Value); // Enumerates again
```

## String Performance

### StringBuilder for Concatenation
```
// ✅ GOOD: StringBuilder for loops
var sb = new StringBuilder();
foreach (var item in items)
{
    sb.AppendLine(item.ToString());
}
var result = sb.ToString();

// ❌ BAD: String concatenation in loop
var result = string.Empty;
foreach (var item in items)
{
    result += item.ToString() + Environment.NewLine;
}
```

### String Comparison
- Use StringComparison.OrdinalIgnoreCase for case-insensitive
- Use == for reference equality
- Use Equals for value equality
- Consider string interning for repeated strings

## Logging Performance

### Structured Logging Performance
```
// ✅ GOOD: Capture details lazily when calling LoggingUtility
LoggingUtility.Log($"[Perf] Processed {count} items for user {userId}");

// ❌ BAD: String concatenation in logging utilities
LoggingUtility.Log("[Perf] Processed " + count + " items for user " + userId);
```

### Log Level Checks
- Use `Service_DebugTracer` performance tracing when detailed metrics are needed.
- Avoid assembling verbose diagnostic strings unless the relevant debug level is enabled.

## File I/O Performance

### Async File Operations
- Use FileStream with async methods
- Buffer I/O operations appropriately
- Close files promptly
- Use using statements for automatic disposal

### File Reading Pattern
```
await using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
using var reader = new StreamReader(stream);
var content = await reader.ReadToEndAsync();
```

## Performance Monitoring

### Metrics to Track
- Application startup time
- UI response times
- Database query durations
- Memory usage
- GC collections

### Performance Logging
```
var stopwatch = Stopwatch.StartNew();
await PerformOperationAsync();
stopwatch.Stop();

if (stopwatch.ElapsedMilliseconds > 1000)
{
    _logger.LogWarning("Slow operation detected: {ElapsedMs}ms", stopwatch.ElapsedMilliseconds);
}
```

## Performance Testing

### Load Testing
- Test with realistic data volumes
- Test concurrent operations
- Measure memory under load
- Identify performance bottlenecks

### Profiling
- Use performance profilers (dotMemory, dotTrace)
- Analyze CPU and memory usage
- Identify hot paths
- Optimize based on measurements

## Anti-Patterns to Avoid

### Common Performance Pitfalls
- Using .Result or .Wait() on async methods
- Not disposing IDisposable resources
- String concatenation in loops
- Excessive property change notifications
- N+1 query problems in database access
- Loading entire database tables into memory
- Synchronous I/O on UI thread
- Not using connection pooling
