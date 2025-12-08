namespace MTM_WIP_Application_Winforms.Models.Analytics
{
    /// <summary>
    /// Represents the performance score for a material handler.
    /// </summary>
    public class Model_Visual_MaterialHandlerScore
    {
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Shift { get; set; }
        
        /// <summary>
        /// Total weighted score based on transaction types.
        /// </summary>
        public double TotalScore { get; set; }
        
        /// <summary>
        /// Letter grade (A, B, C, D, F) based on relative performance.
        /// </summary>
        public string Grade { get; set; } = "F";
        
        /// <summary>
        /// Raw counts of transactions by type.
        /// </summary>
        public Dictionary<string, int> TransactionCounts { get; set; } = new Dictionary<string, int>();
        
        /// <summary>
        /// Factor used to normalize scores across shifts (e.g., 1.0 for busiest shift, >1.0 for slower shifts).
        /// </summary>
        public double ShiftVolumeFactor { get; set; } = 1.0;

        public int TotalTransactions { get; set; }

        /// <summary>
        /// Count of 1-point moves (non-Receive/Pick).
        /// </summary>
        public int OnePointMoves => TransactionCounts.Where(kvp => !kvp.Key.Equals("Receive", System.StringComparison.OrdinalIgnoreCase) && !kvp.Key.Equals("Pick", System.StringComparison.OrdinalIgnoreCase)).Sum(kvp => kvp.Value);

        /// <summary>
        /// Count of 2-point moves (Receive/Pick).
        /// </summary>
        public int TwoPointMoves => TransactionCounts.Where(kvp => kvp.Key.Equals("Receive", System.StringComparison.OrdinalIgnoreCase) || kvp.Key.Equals("Pick", System.StringComparison.OrdinalIgnoreCase)).Sum(kvp => kvp.Value);

        /// <summary>
        /// Total score after shift adjustment.
        /// </summary>
        public double AdjustedScore => TotalScore; // TotalScore is already adjusted in service logic, or we can separate RawScore and AdjustedScore. 
                                                   // Looking at service logic: item.TotalScore *= item.ShiftVolumeFactor; 
                                                   // So TotalScore IS the adjusted score.

        /// <summary>
        /// Average time per transaction in minutes (placeholder).
        /// </summary>
        public double AverageTimeMinutes { get; set; } = 0.0;
    }
}
