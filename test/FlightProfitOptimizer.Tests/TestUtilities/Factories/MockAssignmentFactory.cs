using FlightProfitOptimizer.Models;

namespace FlightProfitOptimizer.Tests.TestUtilities.Factories
{
    /// <summary>
    /// Factory class for creating mock instances of Assignment for testing purposes.
    /// </summary>
    public static class MockAssignmentFactory
    {
        /// <summary>
        /// Creates an empty Assignment object.
        /// </summary>
        /// <remarks>
        /// This method is typically used in unit tests where an instance of an Assignment
        /// with default values is needed for setup or validation.
        /// </remarks>
        /// <returns>An Assignment object with default values.</returns>
        public static Assignment CreateEmptyAssignment()
        {
            return new Assignment();
        }

        public static Assignment CreateAssignment(Family family)
        {
            return new Assignment
            {
                Cost = family.CalculateTotalFamilyCost(),
                Seats = family.CalculateTotalSeats(),
                Members = family.FamilyMembers,
                Family = family.Name
            };
        }

        public static Assignment CreateAssignment(Passenger passenger)
        {
            return new Assignment
            {
                Cost = passenger.CalculateTicketPrice(),
                Seats = passenger.CalculateSeatRequirement(),
                Members = new List<Passenger> { passenger }
            };
        }

        public static List<Assignment> CreateBasicAssignments()
        {
            var familyA = MockFamilyFactory.CreateEmptyFamily("A");
            familyA.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.Adult, "A"));
            familyA.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.Child, "A"));

            var familyB = MockFamilyFactory.CreateFullyPopulatedFamily();

            var member = MockPassengerFactory.CreatePassenger(PassengerType.Adult, "B");
            // Create a simple list of assignments for testing
            return new List<Assignment>
            {
                CreateAssignment(familyA),
                CreateAssignment(familyB),
                CreateAssignment(member),
            };
        }

        public static List<Assignment> CreateLargeNumberOfAssignments(int count)
        {
            var assignments = new List<Assignment>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                // Randomly decide whether to create a family assignment or individual passenger assignment
                if (random.Next(0, 2) == 0) // 50% chance
                {
                    // Create a family assignment
                    Family family = MockFamilyFactory.CreateRandomFamily();
                    assignments.Add(CreateAssignment(family));
                }
                else
                {
                    // Create an individual passenger assignment
                    Passenger passenger = MockPassengerFactory.CreatePassenger();
                    assignments.Add(CreateAssignment(passenger));
                }
            }

            return assignments;
        }
    }

}
