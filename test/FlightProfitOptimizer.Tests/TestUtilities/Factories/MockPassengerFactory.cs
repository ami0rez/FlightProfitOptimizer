using Bogus;
using FlightProfitOptimizer.Models;

namespace FlightProfitOptimizer.Tests.TestUtilities.Factories
{
    /// <summary>
    /// Factory class for creating mock Passenger instances for testing purposes.
    /// </summary>
    public static class MockPassengerFactory
    {
        /// <summary>
        /// Provides a Faker instance for generating Passenger objects with randomized data.
        /// </summary>
        public static Faker<Passenger> PassengerFaker => new Faker<Passenger>()
        .RuleFor(p => p.Id, f => f.IndexFaker + 1)
        .RuleFor(p => p.Age, f => f.Random.Int(2, 60))
        .RuleFor(p => p.Type, f => f.PickRandom<PassengerType>())
        .RuleFor(p => p.Family, f => f.Random.Char('A', 'Z').ToString());

        /// <summary>
        /// Generates a Passenger object with randomized data.
        /// </summary>
        /// <returns>A Passenger object.</returns>
        public static Passenger CreatePassenger()
        {
            return PassengerFaker.Generate();
        }

        /// <summary>
        /// Generates a Passenger object of a specific type.
        /// </summary>
        /// <param name="type">The type of the passenger.</param>
        /// <returns>A Passenger object of the specified type.</returns>
        public static Passenger CreatePassenger(PassengerType type)
        {
            return PassengerFaker.RuleFor(p => p.Type, type)
                    .RuleFor(p => p.Age, (f, p) => p.Type == PassengerType.Child ? f.Random.Int(2, 12) : f.Random.Int(13, 60))
                    .Generate();
        }

        /// <summary>
        /// Generates a Passenger object of a specific type and family.
        /// </summary>
        /// <param name="type">The type of the passenger.</param>
        /// <param name="family">The family identifier.</param>
        /// <returns>A Passenger object of the specified type and family.</returns>
        public static Passenger CreatePassenger(PassengerType type, string family)
        {
            return PassengerFaker.RuleFor(p => p.Type, type)
                    .RuleFor(p => p.Age, (f, p) => p.Type == PassengerType.Child ? f.Random.Int(2, 12) : f.Random.Int(13, 60))
                    .RuleFor(p => p.Family, family)
                    .Generate();
        }

        /// <summary>
        /// Creates an adult Passenger object.
        /// </summary>
        /// <returns>An adult Passenger object.</returns>
        public static Passenger CreateAdultPassenger()
        {
            return PassengerFaker.RuleFor(p => p.Type, PassengerType.Adult)
                    .RuleFor(p => p.Age, f => f.Random.Int(13, 60))
                    .Generate();
        }

        /// <summary>
        /// Creates a child Passenger object.
        /// </summary>
        /// <returns>A child Passenger object.</returns>
        public static Passenger CreateChild()
        {
            return PassengerFaker.RuleFor(p => p.Type, PassengerType.Child)
                        .RuleFor(p => p.Age, f => f.Random.Int(2, 12))
                        .Generate();
        }

        /// <summary>
        /// Creates an adult Passenger object that requires two seats.
        /// </summary>
        /// <returns>An adult Passenger object requiring two seats.</returns>
        public static Passenger CreateAdultRequiringTwoSeats()
        {
            return PassengerFaker.RuleFor(p => p.Type, PassengerType.AdultRequiringTwoSeats)
                        .RuleFor(p => p.Age, f => f.Random.Int(13, 60))
                        .Generate();
        }
    }
}
