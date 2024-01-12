using Bogus;
using FlightProfitOptimizer.Models;

namespace FlightProfitOptimizer.Tests.TestUtilities.Factories
{
    /// <summary>
    /// Factory class for creating mock Family instances for testing.
    /// </summary>
    public static class MockFamilyFactory
    {
        /// <summary>
        /// Provides a Faker instance for generating Family objects with randomized data.
        /// </summary>
        public static Faker<Family> FamilyFaker => new Faker<Family>()
        .RuleFor(p => p.Name, f => f.Random.Char('A', 'Z').ToString());

        /// <summary>
        /// Generates an empty Family object with randomized data.
        /// </summary>
        public static Family CreateEmptyFamily()
        {
            return FamilyFaker.Generate();
        }

        /// <summary>
        /// Generates a Family object with a specific family name.
        /// </summary>
        /// <param name="familyName">The specified name for the family.</param>
        /// <returns>A Family object with the specified name.</returns>
        public static Family CreateEmptyFamily(string familyName)
        {
            return FamilyFaker.RuleFor(p => p.Name, familyName)
                    .Generate();
        }

        /// <summary>
        /// Generates a fully populated Family object with two adults and three children.
        /// </summary>
        /// <returns>A fully populated Family object.</returns>
        public static Family CreateFullyPopulatedFamily()
        {
            var family = FamilyFaker.Generate();
            family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            family.AddMember(MockPassengerFactory.CreateChild());
            family.AddMember(MockPassengerFactory.CreateChild());
            family.AddMember(MockPassengerFactory.CreateChild());
            return family;
        }

        /// <summary>
        /// Creates a Family object with one adult and the maximum number of children.
        /// </summary>
        /// <returns>A Family object with maximum children.</returns>
        public static Family CreateFamilyWithMaxChildren()
        {
            var family = FamilyFaker.Generate();
            family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            for (int i = 0; i < 3; i++)
            {
                family.AddMember(MockPassengerFactory.CreateChild());
            }
            return family;
        }

        /// <summary>
        /// Creates a Family object with one adult and one child.
        /// </summary>
        /// <returns>A Family object with maximum children.</returns>
        public static Family CreateAdultWithChild()
        {
            var family = FamilyFaker.Generate();
            family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            family.AddMember(MockPassengerFactory.CreateChild());
            return family;
        }

        /// <summary>
        /// Creates a Family object with one adult and one two children.
        /// </summary>
        /// <returns>A Family object with maximum children.</returns>
        public static Family CreateAdultWithTwoChildren()
        {
            var family = FamilyFaker.Generate();
            family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            family.AddMember(MockPassengerFactory.CreateChild());
            family.AddMember(MockPassengerFactory.CreateChild());
            return family;
        }

        /// <summary>
        /// Generates a Family object with the maximum number of adults.
        /// </summary>
        /// <returns>A Family object with maximum adults.</returns>
        public static Family CreateFamilyWithMaxAdults()
        {
            var family = FamilyFaker.Generate();
            family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            return family;
        }

        /// <summary>
        /// Generates a random Family object with the random number of adults and children.
        /// </summary>
        /// <returns>A Family object with maximum adults.</returns>
        public static Family CreateRandomFamily()
        {
            var family = new Family();
            var random = new Random();

            int adultCount = random.Next(1, 3); // 1 or 2 adults
            int childCount = random.Next(0, 4); // 0 to 3 children

            for (int i = 0; i < adultCount; i++)
            {
                family.AddMember(MockPassengerFactory.CreateAdultPassenger());
            }

            for (int i = 0; i < childCount; i++)
            {
                family.AddMember(MockPassengerFactory.CreateChild());
            }

            return family;
        }
    }

}
