namespace FlightProfitOptimizer.Common.Constants
{
    /// <summary>
    /// Centralizes error message strings to ensure consistency and facilitate potential localization.
    /// </summary>
    public static class ErrorConstants
    {
        /// <summary>
        /// Error message displayed when the maximum allowed family size is exceeded.
        /// </summary>
        public const string FamilyMaxSizeExceeded = "Maximum members in a family cannot exceed 5.";

        /// <summary>
        /// Error message displayed when a family size is less than the minimum required size.
        /// </summary>
        public const string FamilyMinSizeNotReached = "A family must consist of at least one member.";

        /// <summary>
        /// Error message displayed when an attempt to generate passengers results in zero or negative count.
        /// </summary>
        public const string PassengerMinSizeNotReached = "Passenger count must be greater than zero.";

        /// <summary>
        /// Error message displayed when the maximum number of adults in a family is reached.
        /// </summary>
        public const string MaxAdultsReached = "Cannot add more adults to the family.";

        /// <summary>
        /// Error message displayed when the maximum number of children in a family is reached.
        /// </summary>
        public const string MaxChildrenReached = "Cannot add more children to the family.";

        /// <summary>
        /// Error message displayed when a child is added to a family without an accompanying adult.
        /// </summary>
        public const string ChildNeedsAdult = "A child must be accompanied by an adult in the family.";
    }
}
