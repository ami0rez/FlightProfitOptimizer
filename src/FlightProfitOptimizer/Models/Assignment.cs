namespace FlightProfitOptimizer.Models
{
    /// <summary>
    /// Represents a seating assignment for a group of passengers, potentially comprising a family unit, within a flight.
    /// </summary>
    /// <remarks>
    /// This class is used to manage and organize the seating assignments for passengers. It can represent
    /// either individual passengers or a group of passengers belonging to the same family.
    /// </remarks>
    public class Assignment
    {
        /// <summary>
        /// Gets or sets the total cost for the group of passengers in this assignment.
        /// </summary>
        /// <remarks>
        /// The cost is calculated based on the ticket prices for each passenger in the group.
        /// </remarks>
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the total number of seats occupied by the group of passengers.
        /// </summary>
        /// <remarks>
        /// This property is used to ensure proper seating allocation and to maintain the maximum seating capacity constraints.
        /// </remarks>
        public int Seats { get; set; }

        /// <summary>
        /// Gets or sets the list of passengers included in this assignment.
        /// </summary>
        /// <remarks>
        /// The list contains all passengers that are part of this specific seating assignment.
        /// </remarks>
        public List<Passenger> Members { get; set; }

        /// <summary>
        /// Gets or sets the family identifier if the assignment is for a family group. Null if the assignment is for individual passengers.
        /// </summary>
        /// <remarks>
        /// This property is used to group passengers by family units. It's null for individual passengers not belonging to any family group.
        /// </remarks>
        public string? Family { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Assignment"/> class.
        /// </summary>
        public Assignment()
        {
            Members = new List<Passenger>();
        }
    }
}
