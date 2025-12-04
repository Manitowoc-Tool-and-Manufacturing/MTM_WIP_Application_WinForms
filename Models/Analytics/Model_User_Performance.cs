
    /// <summary>
    /// Represents aggregated performance metrics for a user.
    /// </summary>
    public class Model_User_Performance
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user’s full name.
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the shift the user belongs to.
        /// </summary>
        public string Shift { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the total transaction count.
        /// </summary>
        public int TotalTransactions { get; set; }

        /// <summary>
        /// Gets or sets the total quantity processed.
        /// </summary>
        public long TotalQuantity { get; set; }

        /// <summary>
        /// Gets or sets the number of unique parts handled.
        /// </summary>
        public int UniqueParts { get; set; }

        /// <summary>
        /// Gets or sets the count of rapid-fire events.
        /// </summary>
        public int RapidFireCount { get; set; }

        /// <summary>
        /// Gets or sets the count of ping-pong movements.
        /// </summary>
        public int PingPongCount { get; set; }

        /// <summary>
        /// Gets or sets the count of transactions outside the assigned shift.
        /// </summary>
        public int OutsideShiftCount { get; set; }

        /// <summary>
        /// Gets the calculated quality score (0-100).
        /// </summary>
        public double QualityScore
        {
            get
            {
                // Arbitrary scoring: Start at 100, deduct for flags
                // Normalized by volume (more transactions = more chance for flags, but ratio matters)
                if (TotalTransactions == 0) return 100;
                
                // OffShift is considered positive (helping out), so removed from penalty
                double penalty = (RapidFireCount * 0.5) + (PingPongCount * 5);
                double score = 100 - (penalty / TotalTransactions * 100);
                return Math.Max(0, Math.Min(100, score));
            } 
        }
    }