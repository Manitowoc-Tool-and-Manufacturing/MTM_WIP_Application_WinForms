namespace MTM_WIP_Application_Winforms.Models
{
    /// <summary>
    /// Represents the outcome of a startup operation.
    /// Used to standardize return values from startup services to the Orchestrator.
    /// </summary>
    public class Model_StartupResult
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the operation completed successfully.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Gets or sets a user-friendly message describing the result or error.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the underlying exception, if any.
        /// </summary>
        public Exception? Exception { get; set; }

        /// <summary>
        /// Gets or sets additional context data for logging/debugging.
        /// </summary>
        public Dictionary<string, object>? Context { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Model_StartupResult"/> class.
        /// </summary>
        public Model_StartupResult()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Model_StartupResult"/> class with success status.
        /// </summary>
        /// <param name="isSuccess">Whether the operation was successful.</param>
        /// <param name="message">Optional message.</param>
        public Model_StartupResult(bool isSuccess, string message = "")
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a success result.
        /// </summary>
        /// <param name="message">Optional success message.</param>
        /// <returns>A successful <see cref="Model_StartupResult"/>.</returns>
        public static Model_StartupResult Success(string message = "")
        {
            return new Model_StartupResult(true, message);
        }

        /// <summary>
        /// Creates a failure result.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="ex">Optional exception.</param>
        /// <param name="context">Optional context data.</param>
        /// <returns>A failed <see cref="Model_StartupResult"/>.</returns>
        public static Model_StartupResult Failure(string message, Exception? ex = null, Dictionary<string, object>? context = null)
        {
            return new Model_StartupResult
            {
                IsSuccess = false,
                Message = message,
                Exception = ex,
                Context = context
            };
        }

        #endregion
    }
}
