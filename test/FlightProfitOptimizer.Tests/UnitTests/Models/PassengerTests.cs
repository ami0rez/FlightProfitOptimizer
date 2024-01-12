using FlightProfitOptimizer.Models;
using FlightProfitOptimizer.Tests.TestUtilities.Factories;
using Xunit;

namespace FlightProfitOptimizer.Tests.UnitTests.Models
{
    /// <summary>
    /// Contains unit tests for the <see cref="FamilyTests"/>.
    /// </summary>
    public class PassengerTests
    {
        /// <summary>
        /// Tests if the properties of a passenger can be set and retrieved correctly.
        /// </summary>
        [Fact]
        public void PassengerProperties_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var passenger = MockPassengerFactory.CreatePassenger();

            // Act
            passenger.Id = 1;
            passenger.Age = 30;
            passenger.Type = PassengerType.Adult;
            passenger.Family = "A";

            // Assert
            Assert.Equal(1, passenger.Id);
            Assert.Equal(30, passenger.Age);
            Assert.Equal(PassengerType.Adult, passenger.Type);
            Assert.Equal("A", passenger.Family);
        }

        /// <summary>
        /// Verifies that the ticket price calculation is correct based on the passenger type.
        /// </summary>
        /// <param name="type">The type of the passenger.</param>
        /// <param name="expectedPrice">The expected ticket price for the given passenger type.</param>
        [Theory]
        [InlineData(PassengerType.AdultRequiringTwoSeats, 500)]
        [InlineData(PassengerType.Adult, 250)]
        [InlineData(PassengerType.Child, 150)]
        public void GetPrice_ShouldReturnCorrectPriceBasedOnPassengerType(PassengerType type, int expectedPrice)
        {
            // Arrange
            var passenger = MockPassengerFactory.CreatePassenger(type);

            // Act
            int price = passenger.CalculateTicketPrice();

            // Assert
            Assert.Equal(expectedPrice, price);
        }

        /// <summary>
        /// Tests that an unknown passenger type returns a ticket price of zero.
        /// </summary>
        [Fact]
        public void GetPrice_UnknownType_ShouldReturnZero()
        {
            // Arrange
            var passenger = MockPassengerFactory.CreatePassenger((PassengerType)(-1));// Invalid type

            // Act
            int price = passenger.CalculateTicketPrice();

            // Assert
            Assert.Equal(0, price);
        }

        /// <summary>
        /// Confirms that the number of seats required is calculated correctly based on the passenger type.
        /// </summary>
        /// <param name="type">The type of the passenger.</param>
        /// <param name="expectedSeats">The expected number of seats required for the given passenger type.</param>
        [Theory]
        [InlineData(PassengerType.AdultRequiringTwoSeats, 2)]
        [InlineData(PassengerType.Adult, 1)]
        [InlineData(PassengerType.Child, 1)]
        public void GetSeats_ShouldReturnCorrectNumberOfSeatsBasedOnPassengerType(PassengerType type, int expectedSeats)
        {
            // Arrange
            var passenger = MockPassengerFactory.CreatePassenger(type);

            // Act
            int seats = passenger.CalculateSeatRequirement();

            // Assert
            Assert.Equal(expectedSeats, seats);
        }

        /// <summary>
        /// Ensures that an unknown passenger type returns a seat requirement of zero.
        /// </summary>
        [Fact]
        public void GetSeats_UnknownType_ShouldReturnZero()
        {
            // Arrange
            var passenger = MockPassengerFactory.CreatePassenger((PassengerType)(-1));// Invalid type

            // Act
            int seats = passenger.CalculateSeatRequirement();

            // Assert
            Assert.Equal(0, seats);
        }
    }
}
