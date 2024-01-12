namespace FlightProfitOptimizer.Tests.TestData.Models
{
    /// <summary>
    /// Represents passenger data used for testing purposes.
    /// </summary>
    public class PassengerData
    {
        /// <summary>
        /// Gets or sets the unique identifier of the passenger.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the passenger (e.g., 'Adult', 'Child').
        /// </summary>
        public string? Type { get; set; }

        /// <summary>
        /// Gets or sets the age of the passenger.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the family identifier to which the passenger belongs, if any.
        /// </summary>
        public string? Family { get; set; }

        /// <summary>
        /// Indicates whether the passenger requires two seats.
        /// </summary>
        public bool RequiresTwoSeats { get; set; }
    }
}
