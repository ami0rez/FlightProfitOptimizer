using FlightProfitOptimizer.Utils.Generators;
using Xunit;

namespace FlightProfitOptimizer.Tests.UnitTests.Utils.Generators
{
    /// <summary>
    /// Contains unit tests for <see cref="PassengerIdGenerator"/>.
    /// </summary>
    public class PassengerIdGeneratorTests
    {

        /// <summary>
        /// Verifies that the ID generator produces unique IDs for a given number of passengers.
        /// </summary>
        [Fact]
        public void GeneratePassengerIDs_ShouldBeUnique()
        {
            // Arrange: Set up a scenario to generate a number of passenger IDs
            var passengerCount = 100;
            var ids = new HashSet<int>();

            // Act: Generate unique IDs for each passenger
            for (int i = 0; i < passengerCount; i++)
            {
                int id = PassengerIdGenerator.GenerateId();
                ids.Add(id);
            }

            // Assert: Ensure that all generated IDs are unique
            Assert.Equal(passengerCount, ids.Count);
        }
    }
}
