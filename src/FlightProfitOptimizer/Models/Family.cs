using FlightProfitOptimizer.Common.Constants;
using FlightProfitOptimizer.Common.Exceptions;

namespace FlightProfitOptimizer.Models
{
    /// <summary>
    /// Represents a family group for the purpose of flight seat assignment.
    /// </summary>
    /// <remarks>
    /// This class encapsulates the logic for managing a family unit in the context of flight seating,
    /// ensuring adherence to business rules such as maximum family size and child-adult ratios.
    /// </remarks>
    public class Family
    {
        /// <summary>
        /// Gets or sets the name identifier for the family.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets the list of passengers in the family.
        /// </summary>
        public List<Passenger> FamilyMembers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Family"/> class.
        /// </summary>
        public Family()
        {
            FamilyMembers = new List<Passenger>();
        }

        /// <summary>
        /// Adds a passenger to the family, ensuring compliance with family composition rules.
        /// </summary>
        /// <param name="passenger">The passenger to add to the family.</param>
        /// <exception cref="InvalidFamilySizeException">Thrown when the family exceeds the maximum size.</exception>
        /// <exception cref="NoAdultForChildException">Thrown when a child is added without an accompanying adult in the family.</exception>
        /// <exception cref="MaximumChildrenException">Thrown when the maximum number of children in the family is exceeded.</exception>
        /// <exception cref="MaximumAdultsException">Thrown when the maximum number of adults in the family is exceeded.</exception>
        public void AddMember(Passenger passenger)
        {
            if (FamilyMembers.Count >= 5)
            {
                throw new InvalidFamilySizeException(ErrorConstants.FamilyMaxSizeExceeded);
            }

            if (passenger.Type == PassengerType.Child)
            {
                if (FamilyMembers.Count(p => p.Type == PassengerType.Adult || p.Type == PassengerType.AdultRequiringTwoSeats) == 0)
                {
                    throw new NoAdultForChildException();
                }
                if (FamilyMembers.Count(p => p.Type == PassengerType.Child) >= 3)
                {
                    throw new MaximumChildrenException();
                }
            }
            else if (passenger.Type == PassengerType.Adult || passenger.Type == PassengerType.AdultRequiringTwoSeats)
            {
                if (FamilyMembers.Count(p => p.Type == PassengerType.Adult || p.Type == PassengerType.AdultRequiringTwoSeats) >= 2)
                {
                    throw new MaximumAdultsException();
                }
            }

            FamilyMembers.Add(passenger);
        }

        /// <summary>
        /// Calculates the total price for all passengers in the family.
        /// </summary>
        /// <returns>The total price.</returns>
        public int CalculateTotalFamilyCost()
        {
            return FamilyMembers.Sum(passenger => passenger.CalculateTicketPrice());
        }

        /// <summary>
        /// Calculates the total number of seats required for all passengers in the family.
        /// </summary>
        /// <returns>The total number of seats required.</returns>
        public int CalculateTotalSeats()
        {
            return FamilyMembers.Sum(passenger => passenger.CalculateSeatRequirement());
        }
    }
}
