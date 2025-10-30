using System;

namespace MTM_WIP_Application_Winforms.Models
{
    #region DAO Result Models

    /// <summary>
    /// Generic result wrapper for DAO operations that return data.
    /// Provides success/failure status, error information, and strongly-typed data payload.
    /// </summary>
    /// <typeparam name="T">The type of data returned by the operation (e.g., List&lt;Model_Users&gt;, DataTable, string)</typeparam>
    /// <remarks>
    /// <para>
    /// <strong>Purpose:</strong>
    /// DaoResult&lt;T&gt; encapsulates the result of a DAO operation that retrieves or returns data,
    /// eliminating the need for try-catch blocks in calling code and providing consistent error handling.
    /// </para>
    /// <para>
    /// <strong>When to Use:</strong>
    /// Use DaoResult&lt;T&gt; when your DAO method returns data (SELECT operations, calculations, lookups).
    /// For operations that only indicate success/failure without returning data (INSERT, UPDATE, DELETE),
    /// use the non-generic <see cref="DaoResult"/> class instead.
    /// </para>
    /// <para>
    /// <strong>Factory Methods:</strong>
    /// Create results using static factory methods rather than constructors:
    /// <list type="bullet">
    /// <item><see cref="Success(T, string, int)"/> - Operation succeeded with data</item>
    /// <item><see cref="Success(string, int)"/> - Operation succeeded without data</item>
    /// <item><see cref="Failure(string, Exception)"/> - Operation failed with custom error message</item>
    /// <item><see cref="Failure(Exception)"/> - Operation failed, use exception message</item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Usage Pattern:</strong>
    /// <code>
    /// // DAO Layer (Data/Dao_Inventory.cs)
    /// internal static async Task&lt;DaoResult&lt;List&lt;Model_CurrentInventory&gt;&gt;&gt; GetAllInventoryAsync()
    /// {
    ///     try
    ///     {
    ///         var result = await Helper_Database_StoredProcedure.ExecuteDataTableWithStatusAsync(...);
    ///         
    ///         if (!result.IsSuccess)
    ///             return DaoResult&lt;List&lt;Model_CurrentInventory&gt;&gt;.Failure(result.StatusMessage);
    ///         
    ///         var inventory = MapDataTableToInventory(result.Payload);
    ///         return DaoResult&lt;List&lt;Model_CurrentInventory&gt;&gt;.Success(inventory, "Retrieved inventory successfully");
    ///     }
    ///     catch (MySqlException ex)
    ///     {
    ///         return DaoResult&lt;List&lt;Model_CurrentInventory&gt;&gt;.Failure(ex);
    ///     }
    /// }
    /// 
    /// // UI Layer (Forms/MainForm/MainForm.cs)
    /// private async void LoadInventory()
    /// {
    ///     var result = await Dao_Inventory.GetAllInventoryAsync();
    ///     
    ///     if (result.IsSuccess)
    ///     {
    ///         // Access data via result.Data
    ///         dataGridView.DataSource = result.Data;
    ///         LoggingUtility.Log($"Loaded {result.Data.Count} items: {result.StatusMessage}");
    ///     }
    ///     else
    ///     {
    ///         // Display user-friendly error message
    ///         MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    ///         LoggingUtility.LogApplicationError(result.Exception, result.ErrorMessage);
    ///     }
    /// }
    /// </code>
    /// </para>
    /// <para>
    /// <strong>Design Rationale:</strong>
    /// The generic DaoResult&lt;T&gt; and non-generic <see cref="DaoResult"/> exist as separate classes
    /// (rather than DaoResult&lt;T&gt; with DaoResult : DaoResult&lt;object&gt;) to provide cleaner API:
    /// operations that don't return data shouldn't force callers to specify a type parameter or handle null Data.
    /// </para>
    /// </remarks>
    public class DaoResult<T>
    {
        #region Properties

        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// The data returned by the operation (null if operation failed)
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Error message if the operation failed
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// Exception details if available
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Additional status information
        /// </summary>
        public string StatusMessage { get; set; } = string.Empty;

        /// <summary>
        /// Number of rows affected (for update/delete operations)
        /// </summary>
        public int RowsAffected { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a successful result with data payload.
        /// </summary>
        /// <param name="data">The data returned by the successful operation.</param>
        /// <param name="statusMessage">Optional status message describing the operation (e.g., "Retrieved 42 inventory items").</param>
        /// <param name="rowsAffected">Optional count of database rows affected by the operation.</param>
        /// <returns>A DaoResult&lt;T&gt; with IsSuccess=true and the provided data.</returns>
        /// <example>
        /// <code>
        /// var users = new List&lt;Model_Users&gt; { user1, user2 };
        /// return DaoResult&lt;List&lt;Model_Users&gt;&gt;.Success(users, "Retrieved 2 users");
        /// </code>
        /// </example>
        public static DaoResult<T> Success(T data, string statusMessage = "", int rowsAffected = 0)
        {
            return new DaoResult<T>
            {
                IsSuccess = true,
                Data = data,
                StatusMessage = statusMessage,
                RowsAffected = rowsAffected
            };
        }

        /// <summary>
        /// Creates a successful result without data payload.
        /// Use when operation succeeded but doesn't return data (uncommon for DaoResult&lt;T&gt;).
        /// </summary>
        /// <param name="statusMessage">Optional status message describing the operation.</param>
        /// <param name="rowsAffected">Optional count of database rows affected by the operation.</param>
        /// <returns>A DaoResult&lt;T&gt; with IsSuccess=true and Data=default(T).</returns>
        /// <remarks>
        /// This overload is rarely used with DaoResult&lt;T&gt;. Consider using non-generic <see cref="DaoResult"/>
        /// for operations that don't return data.
        /// </remarks>
        /// <example>
        /// <code>
        /// // Prefer non-generic DaoResult for this pattern:
        /// return DaoResult.Success("Operation completed", rowsAffected: 1);
        /// </code>
        /// </example>
        public static DaoResult<T> Success(string statusMessage = "", int rowsAffected = 0)
        {
            return new DaoResult<T>
            {
                IsSuccess = true,
                StatusMessage = statusMessage,
                RowsAffected = rowsAffected
            };
        }

        /// <summary>
        /// Creates a failed result with custom error message and optional exception.
        /// </summary>
        /// <param name="errorMessage">User-friendly error message describing what went wrong.</param>
        /// <param name="exception">Optional exception that caused the failure (for logging/diagnostics).</param>
        /// <returns>A DaoResult&lt;T&gt; with IsSuccess=false and the provided error information.</returns>
        /// <example>
        /// <code>
        /// if (string.IsNullOrEmpty(partNumber))
        ///     return DaoResult&lt;Model_Part&gt;.Failure("Part number is required");
        /// 
        /// // With exception for logging
        /// catch (MySqlException ex)
        /// {
        ///     return DaoResult&lt;List&lt;Model_Part&gt;&gt;.Failure("Failed to retrieve parts", ex);
        /// }
        /// </code>
        /// </example>
        public static DaoResult<T> Failure(string errorMessage, Exception? exception = null)
        {
            return new DaoResult<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                Exception = exception
            };
        }

        /// <summary>
        /// Creates a failed result from an exception, using the exception message as the error message.
        /// </summary>
        /// <param name="exception">The exception that caused the failure.</param>
        /// <returns>A DaoResult&lt;T&gt; with IsSuccess=false and exception details.</returns>
        /// <remarks>
        /// Prefer <see cref="Failure(string, Exception)"/> when you want to provide a user-friendly error message
        /// separate from the technical exception message. Use this overload when the exception message itself
        /// is appropriate for end users.
        /// </remarks>
        /// <example>
        /// <code>
        /// catch (ArgumentException ex)
        /// {
        ///     // Exception message is user-friendly
        ///     return DaoResult&lt;Model_User&gt;.Failure(ex);
        /// }
        /// </code>
        /// </example>
        public static DaoResult<T> Failure(Exception exception)
        {
            return new DaoResult<T>
            {
                IsSuccess = false,
                ErrorMessage = exception.Message,
                Exception = exception
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Implicitly converts successful DaoResult&lt;T&gt; to the underlying data type.
        /// Returns default(T) if the result failed.
        /// </summary>
        /// <param name="result">The DaoResult&lt;T&gt; to convert.</param>
        /// <returns>The Data property if IsSuccess=true, otherwise default(T).</returns>
        /// <remarks>
        /// <para>
        /// This implicit conversion allows direct assignment from DaoResult&lt;T&gt; to T,
        /// but you lose access to success/failure status. Use this pattern only when you've
        /// already checked IsSuccess separately.
        /// </para>
        /// <para>
        /// <strong>Warning:</strong> For reference types, this returns null on failure, which can lead to
        /// NullReferenceException if not handled. Prefer explicit result.Data access with IsSuccess checking.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Implicit conversion (use cautiously)
        /// DaoResult&lt;string&gt; result = GetUserName();
        /// string? userName = result; // Implicit conversion, could be null if failed
        /// 
        /// // Preferred pattern (explicit checking)
        /// if (result.IsSuccess)
        /// {
        ///     string userName = result.Data; // Safe access
        /// }
        /// </code>
        /// </example>
        public static implicit operator T?(DaoResult<T> result)
        {
            return result.IsSuccess ? result.Data : default;
        }

        /// <summary>
        /// Returns a human-readable string representation of the result.
        /// </summary>
        /// <returns>
        /// Success format: "Success: {StatusMessage} (Rows: {RowsAffected})"
        /// Failure format: "Failed: {ErrorMessage}"
        /// </returns>
        /// <example>
        /// <code>
        /// var result = DaoResult&lt;int&gt;.Success(42, "Retrieved count", rowsAffected: 1);
        /// Console.WriteLine(result); // Output: "Success: Retrieved count (Rows: 1)"
        /// 
        /// var failResult = DaoResult&lt;int&gt;.Failure("Database timeout");
        /// Console.WriteLine(failResult); // Output: "Failed: Database timeout"
        /// </code>
        /// </example>
        public override string ToString()
        {
            if (IsSuccess)
            {
                return $"Success: {StatusMessage} (Rows: {RowsAffected})";
            }
            return $"Failed: {ErrorMessage}";
        }

        #endregion
    }

    /// <summary>
    /// Non-generic result wrapper for DAO operations that don't return data.
    /// Provides success/failure status and error information without a data payload.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <strong>Purpose:</strong>
    /// DaoResult encapsulates the result of a DAO operation that performs an action without returning data,
    /// such as INSERT, UPDATE, DELETE, or validation operations. It eliminates the need for try-catch blocks
    /// in calling code and provides consistent error handling.
    /// </para>
    /// <para>
    /// <strong>When to Use:</strong>
    /// Use DaoResult (non-generic) when your DAO method indicates success/failure without returning data:
    /// <list type="bullet">
    /// <item>INSERT operations that don't return the inserted row</item>
    /// <item>UPDATE operations that return only row count</item>
    /// <item>DELETE operations</item>
    /// <item>Validation or connectivity checks</item>
    /// </list>
    /// For operations that return data (SELECT operations, calculations, lookups),
    /// use the generic <see cref="DaoResult{T}"/> class instead.
    /// </para>
    /// <para>
    /// <strong>Factory Methods:</strong>
    /// Create results using static factory methods rather than constructors:
    /// <list type="bullet">
    /// <item><see cref="Success(string, int)"/> - Operation succeeded</item>
    /// <item><see cref="Failure(string, Exception)"/> - Operation failed with custom error message</item>
    /// <item><see cref="Failure(Exception)"/> - Operation failed, use exception message</item>
    /// </list>
    /// </para>
    /// <para>
    /// <strong>Usage Pattern:</strong>
    /// <code>
    /// // DAO Layer (Data/Dao_Inventory.cs)
    /// internal static async Task&lt;DaoResult&gt; DeleteInventoryAsync(int inventoryId)
    /// {
    ///     try
    ///     {
    ///         var parameters = new Dictionary&lt;string, object&gt;
    ///         {
    ///             ["InventoryId"] = inventoryId
    ///         };
    ///         
    ///         var result = await Helper_Database_StoredProcedure.ExecuteNonQueryWithStatusAsync(
    ///             Model_AppVariables.ConnectionString,
    ///             "inventory_Delete_ById",
    ///             parameters
    ///         );
    ///         
    ///         if (!result.IsSuccess)
    ///             return DaoResult.Failure(result.StatusMessage);
    ///         
    ///         return DaoResult.Success("Inventory deleted successfully", rowsAffected: 1);
    ///     }
    ///     catch (MySqlException ex)
    ///     {
    ///         return DaoResult.Failure($"Failed to delete inventory: {ex.Message}", ex);
    ///     }
    /// }
    /// 
    /// // UI Layer (Forms/MainForm/MainForm.cs)
    /// private async void btnDelete_Click(object sender, EventArgs e)
    /// {
    ///     var result = await Dao_Inventory.DeleteInventoryAsync(selectedId);
    ///     
    ///     if (result.IsSuccess)
    ///     {
    ///         MessageBox.Show(result.StatusMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
    ///         RefreshGrid();
    ///     }
    ///     else
    ///     {
    ///         MessageBox.Show(result.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    ///         LoggingUtility.LogApplicationError(result.Exception, result.ErrorMessage);
    ///     }
    /// }
    /// </code>
    /// </para>
    /// <para>
    /// <strong>Implicit Boolean Conversion:</strong>
    /// DaoResult can be implicitly converted to bool (returns IsSuccess value), enabling concise checking:
    /// <code>
    /// var result = await Dao_User.UpdatePasswordAsync(userId, newPassword);
    /// if (result) // Implicitly checks result.IsSuccess
    /// {
    ///     Console.WriteLine("Password updated successfully");
    /// }
    /// </code>
    /// However, this loses access to error details. Prefer explicit IsSuccess checking when you need error messages.
    /// </para>
    /// </remarks>
    public class DaoResult
    {
        #region Properties

        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Error message if the operation failed
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// Exception details if available
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Additional status information
        /// </summary>
        public string StatusMessage { get; set; } = string.Empty;

        /// <summary>
        /// Number of rows affected (for update/delete operations)
        /// </summary>
        public int RowsAffected { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a successful result without data payload.
        /// </summary>
        /// <param name="statusMessage">Optional status message describing the operation (e.g., "Deleted 3 inventory items").</param>
        /// <param name="rowsAffected">Optional count of database rows affected by the operation.</param>
        /// <returns>A DaoResult with IsSuccess=true and the provided status information.</returns>
        /// <example>
        /// <code>
        /// // Simple success
        /// return DaoResult.Success("Operation completed");
        /// 
        /// // Success with row count
        /// return DaoResult.Success("Updated inventory", rowsAffected: 5);
        /// </code>
        /// </example>
        public static DaoResult Success(string statusMessage = "", int rowsAffected = 0)
        {
            return new DaoResult
            {
                IsSuccess = true,
                StatusMessage = statusMessage,
                RowsAffected = rowsAffected
            };
        }

        /// <summary>
        /// Creates a failed result with custom error message and optional exception.
        /// </summary>
        /// <param name="errorMessage">User-friendly error message describing what went wrong.</param>
        /// <param name="exception">Optional exception that caused the failure (for logging/diagnostics).</param>
        /// <returns>A DaoResult with IsSuccess=false and the provided error information.</returns>
        /// <example>
        /// <code>
        /// // Validation failure
        /// if (string.IsNullOrEmpty(userName))
        ///     return DaoResult.Failure("Username is required");
        /// 
        /// // Database error with exception
        /// catch (MySqlException ex)
        /// {
        ///     return DaoResult.Failure("Failed to connect to database", ex);
        /// }
        /// </code>
        /// </example>
        public static DaoResult Failure(string errorMessage, Exception? exception = null)
        {
            return new DaoResult
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                Exception = exception
            };
        }

        /// <summary>
        /// Creates a failed result from an exception, using the exception message as the error message.
        /// </summary>
        /// <param name="exception">The exception that caused the failure.</param>
        /// <returns>A DaoResult with IsSuccess=false and exception details.</returns>
        /// <remarks>
        /// Prefer <see cref="Failure(string, Exception)"/> when you want to provide a user-friendly error message
        /// separate from the technical exception message. Use this overload when the exception message itself
        /// is appropriate for end users.
        /// </remarks>
        /// <example>
        /// <code>
        /// catch (ArgumentException ex)
        /// {
        ///     // Exception message is user-friendly
        ///     return DaoResult.Failure(ex);
        /// }
        /// 
        /// catch (MySqlException ex)
        /// {
        ///     // Prefer wrapping with context
        ///     return DaoResult.Failure($"Database operation failed: {ex.Message}", ex);
        /// }
        /// </code>
        /// </example>
        public static DaoResult Failure(Exception exception)
        {
            return new DaoResult
            {
                IsSuccess = false,
                ErrorMessage = exception.Message,
                Exception = exception
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Implicitly converts DaoResult to boolean, returning the IsSuccess value.
        /// Enables concise success checking in conditional statements.
        /// </summary>
        /// <param name="result">The DaoResult to convert.</param>
        /// <returns>True if the operation succeeded (IsSuccess=true), otherwise false.</returns>
        /// <remarks>
        /// <para>
        /// This implicit conversion allows using DaoResult directly in boolean contexts,
        /// but you lose access to error details. Use this pattern when you only need to check
        /// success/failure, and access error messages separately when needed.
        /// </para>
        /// <para>
        /// <strong>Trade-off:</strong> Conciseness vs. clarity. Explicit result.IsSuccess checking
        /// is more readable but more verbose. Use implicit conversion judiciously.
        /// </para>
        /// </remarks>
        /// <example>
        /// <code>
        /// // Implicit conversion (concise)
        /// var result = await Dao_User.UpdatePasswordAsync(userId, newPassword);
        /// if (result) // Implicitly uses result.IsSuccess
        /// {
        ///     Console.WriteLine("Updated successfully");
        /// }
        /// 
        /// // Explicit checking (clear)
        /// if (result.IsSuccess)
        /// {
        ///     Console.WriteLine($"Success: {result.StatusMessage}");
        /// }
        /// else
        /// {
        ///     Console.WriteLine($"Error: {result.ErrorMessage}");
        /// }
        /// </code>
        /// </example>
        public static implicit operator bool(DaoResult result)
        {
            return result.IsSuccess;
        }

        /// <summary>
        /// Returns a human-readable string representation of the result.
        /// </summary>
        /// <returns>
        /// Success format: "Success: {StatusMessage} (Rows: {RowsAffected})"
        /// Failure format: "Failed: {ErrorMessage}"
        /// </returns>
        /// <example>
        /// <code>
        /// var result = DaoResult.Success("Updated inventory", rowsAffected: 3);
        /// Console.WriteLine(result); // Output: "Success: Updated inventory (Rows: 3)"
        /// 
        /// var failResult = DaoResult.Failure("Connection timeout");
        /// Console.WriteLine(failResult); // Output: "Failed: Connection timeout"
        /// 
        /// // Useful for logging
        /// LoggingUtility.Log($"[DAO] {result}");
        /// </code>
        /// </example>
        public override string ToString()
        {
            if (IsSuccess)
            {
                return $"Success: {StatusMessage} (Rows: {RowsAffected})";
            }
            return $"Failed: {ErrorMessage}";
        }

        #endregion
    }

    #endregion
}
