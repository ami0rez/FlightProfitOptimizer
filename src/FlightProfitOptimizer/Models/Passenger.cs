namespace FlightProfitOptimizer.Models
{
    /// <summary>
    /// Represents an individual passenger for flight seat assignment and pricing.
    /// </summary>
    /// <remarks>
    /// Contains details about the passenger and provides methods to calculate ticket pricing
    /// and seating requirements based on passenger type.
    /// </remarks>
    public class Passenger
    {
        /// <summary>
        /// Gets or sets the identifier for the passenger.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the age of the passenger.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the type of the passenger (e.g., adult, child, adult requiring two seats).
        /// </summary>
        public PassengerType Type { get; set; }

        /// <summary>
        /// Gets or sets the optional family identifier if the passenger is part of a family group.
        /// </summary>
        public string? Family { get; set; }

        /// <summary>
        /// Calculates the ticket price for the passenger based on their type.
        /// </summary>
        /// <returns>The price of the ticket.</returns>
        public int CalculateTicketPrice()
        {
            return Type switch
            {
                PassengerType.AdultRequiringTwoSeats => 500,
                PassengerType.Adult => 250,
                PassengerType.Child => 150,
                _ => 0,
            };
        }

        /// <summary>
        /// Determines the number of seats required for the passenger based on their type.
        /// </summary>
        /// <returns>The required number of seats.</returns>
        public int CalculateSeatRequirement()
        {
            return Type switch
            {
                PassengerType.AdultRequiringTwoSeats => 2,
                PassengerType.Adult => 1,
                PassengerType.Child => 1,
                _ => 0,
            };
        }
    }
}
