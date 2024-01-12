using FlightProfitOptimizer.Tests.TestUtilities.Factories;
using FlightProfitOptimizer.Common.Exceptions;
using Xunit;

namespace FlightProfitOptimizer.Tests.UnitTests.Models
{

    /// <summary>
    /// Contains unit tests for the <see cref="FamilyTests"/>.
    /// </summary>
    public class FamilyTests
    {
        /// <summary>
        /// Tests if a passenger can be successfully added to an empty family.
        /// </summary>
        [Fact]
        public void AddMember_ShouldAddPassengerToFamily()
        {
            // Arrange
            var family = MockFamilyFactory.CreateEmptyFamily();
            var passenger = MockPassengerFactory.CreateAdultPassenger();

            // Act
            family.AddMember(passenger);

            // Assert
            Assert.Contains(passenger, family.FamilyMembers);
        }

        /// <summary>
        /// Ensures that adding a passenger to a family does not exceed family composition limits.
        /// </summary>
        [Fact]
        public void AddMember_ShouldAddPassengerToFamily_WithoutExceedingLimits()
        {
            // Arrange
            var family = MockFamilyFactory.CreateEmptyFamily();
            var adultPassenger = MockPassengerFactory.CreateAdultPassenger();

            // Act & Assert
            family.AddMember(adultPassenger); // Should not throw
            Assert.Contains(adultPassenger, family.FamilyMembers);
        }

        /// <summary>
        /// Tests if adding a passenger to a full family throws an InvalidFamilySizeException.
        /// </summary>
        [Fact]
        public void AddMember_ExceedingFamilySize_ShouldThrowInvalidFamilySizeException()
        {
            // Arrange
            var family = MockFamilyFactory.CreateFullyPopulatedFamily();
            var newPassenger = MockPassengerFactory.CreateAdultPassenger();

            // Act & Assert
            var exception = Assert.Throws<InvalidFamilySizeException>(() => family.AddMember(newPassenger));
            Assert.Equal("Maximum members in a family cannot exceed 5.", exception.Message);
        }

        /// <summary>
        /// Validates that adding a child without an adult in the family throws a NoAdultForChildException.
        /// </summary>
        [Fact]
        public void AddMember_AddingChildWithoutAdult_ShouldThrowNoAdultForChildException()
        {
            // Arrange
            var family = MockFamilyFactory.CreateEmptyFamily();
            var childPassenger = MockPassengerFactory.CreateChild();

            // Act & Assert
            var exception = Assert.Throws<NoAdultForChildException>(() => family.AddMember(childPassenger));
            Assert.Equal("A child must be accompanied by an adult in the family.", exception.Message);
        }

        /// <summary>
        /// Tests if adding more than the maximum allowed number of children to a family throws a MaximumChildrenException.
        /// </summary>
        [Fact]
        public void AddMember_ExceedingMaximumChildren_ShouldThrowMaximumChildrenException()
        {
            // Arrange
            var family = MockFamilyFactory.CreateFamilyWithMaxChildren();
            var newChild = MockPassengerFactory.CreateChild();

            // Act & Assert
            var exception = Assert.Throws<MaximumChildrenException>(() => family.AddMember(newChild));
            Assert.Equal("Cannot add more children to the family.", exception.Message);
        }

        /// <summary>
        /// Verifies that adding more than the maximum allowed number of adults to a family throws a MaximumAdultsException.
        /// </summary>
        [Fact]
        public void AddMember_ExceedingMaximumAdults_ShouldThrowMaximumAdultsException()
        {
            // Arrange
            var family = MockFamilyFactory.CreateFamilyWithMaxAdults();
            var newAdult = MockPassengerFactory.CreateAdultPassenger();

            // Act & Assert
            var exception = Assert.Throws<MaximumAdultsException>(() => family.AddMember(newAdult));
            Assert.Equal("Cannot add more adults to the family.", exception.Message);
        }


        /// <summary>
        /// Confirms that the total cost calculation for a family is accurate.
        /// </summary>
        [Fact]
        public void CalculateFamilyTotal_ShouldReturnCorrectTotalPrice()
        {
            // Arrange
            var family = MockFamilyFactory.CreateEmptyFamily();
            family.AddMember(MockPassengerFactory.CreateAdultPassenger()); // 250
            family.AddMember(MockPassengerFactory.CreateChild());  // 150

            // Act
            int totalPrice = family.CalculateTotalFamilyCost();

            // Assert
            Assert.Equal(400, totalPrice); // 250 (adult) + 150 (child)
        }

        /// <summary>
        /// Checks that the total seat calculation for a family is correct.
        /// </summary>
        [Fact]
        public void CalculateSeats_ShouldReturnCorrectNumberOfSeats()
        {
            // Arrange
            var family = MockFamilyFactory.CreateEmptyFamily();
            family.AddMember(MockPassengerFactory.CreateAdultRequiringTwoSeats()); // 2 seats
            family.AddMember(MockPassengerFactory.CreateChild());// 1 seat

            // Act
            int totalSeats = family.CalculateTotalSeats();

            // Assert
            Assert.Equal(3, totalSeats); // 2 (adult requiring two seats) + 1 (child)
        }

    }
}
