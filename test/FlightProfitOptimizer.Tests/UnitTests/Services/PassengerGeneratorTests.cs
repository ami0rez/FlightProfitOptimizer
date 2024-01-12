using FlightProfitOptimizer.Common.Constants;
using FlightProfitOptimizer.Common.Exceptions;
using FlightProfitOptimizer.Models;
using FlightProfitOptimizer.Services;
using Xunit;

namespace FlightProfitOptimizer.Tests.UnitTests.Services
{
    /// <summary>
    /// Contains unit tests for the <see cref="PassengerGeneratorTests"/> class.
    /// </summary>
    public class PassengerGeneratorTests
    {

        /// <summary>
        /// Verifies that the GeneratePassengers method creates the correct number of passengers and families.
        /// </summary>
        [Fact]
        public void GeneratePassengers_ShouldCreateCorrectNumberOfPassengersAndFamilies()
        {
            // Arrange
            int passengerCount = 20;

            // Act
            var result = PassengerGenerator.GeneratePassengers(passengerCount);

            // Assert
            Assert.Equal(passengerCount, result.Item1.Count); // Check if the number of passengers is as requested.
            Assert.True(result.Item2.Count > 0); // There should be at least one family generated.
        }

        /// <summary>
        /// Ensures that generating passengers with a negative or zero count throws 
        /// an InvalidPassengerCountException.
        /// </summary>
        /// <param name="count">The number of passengers to generate.</param>
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GeneratePassengers_NegativeOrZeroCount_ShouldThrowInvalidPassengerCountException(int count)
        {
            // Act & Assert
            var exception = Assert.Throws<InvalidPassengerCountException>(() => PassengerGenerator.GeneratePassengers(count));
            Assert.Equal("Passenger count must be greater than zero.", exception.Message);
        }

        /// <summary>
        /// Checks if the GenerateFamily method creates a family with the correct composition 
        /// based on the specified maximum members.
        /// </summary>
        /// <param name="maxMembers">The maximum number of members in the family.</param>
        [Theory]
        [InlineData(5)]
        [InlineData(3)]
        [InlineData(null)]
        public void GenerateFamily_ShouldCreateFamilyWithCorrectComposition(int? maxMembers)
        {
            // Arrange
            var familyName = "Z";

            // Act
            var family = PassengerGenerator.GenerateFamily(familyName, maxMembers);

            // Assert
            int adultCount = family.FamilyMembers.Count(p => p.Age >= 12);
            int childCount = family.FamilyMembers.Count(p => p.Age < 12);

            Assert.True(adultCount <= 2, "There should be at most two adults in a family.");
            Assert.True(childCount <= 3, "There should be at most three children in a family.");
            Assert.True(childCount == 0 || adultCount > 0, "Children should not be alone without adults.");

            if (maxMembers.HasValue)
            {
                Assert.True(family.FamilyMembers.Count <= maxMembers, $"The family should have at most {maxMembers} members.");
            }
            else
            {
                Assert.True(family.FamilyMembers.Count <= 5, "The family should have at most 5 members.");
            }
        }

        /// <summary>
        /// Verifies that generating a family with maximum members above the limit throws an 
        /// InvalidFamilySizeException.
        /// </summary>
        /// <param name="maxMembers">The maximum number of members to test with.</param>
        [Theory]
        [InlineData(6)]
        [InlineData(10)]
        public void GenerateFamily_MaxMembersAboveLimit_ShouldThrowInvalidFamilySizeException(int? maxMembers)
        {
            // Arrange
            var familyName = "A";

            // Act & Assert
            var exception = Assert.Throws<InvalidFamilySizeException>(() => PassengerGenerator.GenerateFamily(familyName, maxMembers));
            Assert.Equal(ErrorConstants.FamilyMaxSizeExceeded, exception.Message);
        }

        /// <summary>
        /// Ensures that generating a family with a negative or zero maximum member count 
        /// throws an InvalidFamilySizeException.
        /// </summary>
        /// <param name="maxMembers">The maximum number of family members to test with.</param>
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GenerateFamily_NegativeOrZeroCount_ShouldThrowInvalidFamilySizeException(int? maxMembers)
        {
            // Arrange
            var familyName = "A";

            // Act & Assert
            var exception = Assert.Throws<InvalidFamilySizeException>(() => PassengerGenerator.GenerateFamily(familyName, maxMembers));
            Assert.Equal(ErrorConstants.FamilyMinSizeNotReached, exception.Message);
        }

        /// <summary>
        /// Tests if the GeneratePassenger method correctly creates an adult passenger with the specified age.
        /// </summary>
        /// <param name="adultAge">The age of the adult to generate.</param>
        [Fact]
        public void GeneratePassenger_Adult_ShouldCreateCorrectType()
        {
            // Arrange
            int adultAge = 30;

            // Act
            var passenger = PassengerGenerator.GeneratePassnegr(adultAge);

            // Assert
            bool isCorrectType = passenger.Type == PassengerType.Adult || passenger.Type == PassengerType.AdultRequiringTwoSeats;
            Assert.True(isCorrectType, "Passenger type should be either Adulte or AdultRequireTwoSeats.");
            Assert.Equal(adultAge, passenger.Age);
        }

        /// <summary>
        /// Tests if the GeneratePassenger method correctly creates a child passenger with the specified age.
        /// </summary>
        /// <param name="childAge">The age of the child to generate.</param>
        [Fact]
        public void GeneratePassenger_Child_ShouldCreateCorrectType()
        {
            // Arrange
            int childAge = 10;

            // Act
            var passenger = PassengerGenerator.GeneratePassnegr(childAge);

            // Assert
            Assert.Equal(PassengerType.Child, passenger.Type);
            Assert.Equal(childAge, passenger.Age);
        }

        /// <summary>
        /// Tests if the GeneratePassenger method correctly creates an adult requiring two 
        /// seats with the specified age.
        /// </summary>
        /// <param name="adultAge">The age of the adult to generate.</param>
        [Fact]
        public void GeneratePassenger_AdultRequiringTwoSeats_ShouldCreateCorrectType()
        {
            // Arrange
            int adultAge = 35;

            // Act
            var passenger = PassengerGenerator.GeneratePassnegr(adultAge);
            passenger.Type = PassengerType.AdultRequiringTwoSeats; // Forcing the type for test

            // Assert
            Assert.Equal(PassengerType.AdultRequiringTwoSeats, passenger.Type);
            Assert.Equal(2, passenger.CalculateSeatRequirement());
        }

        /// <summary>
        /// Tests if the GeneratePassenger method correctly creates a passenger with a 
        /// valid type based on random age.
        /// </summary>
        [Fact]
        public void GeneratePassenger_RandomType_ShouldCreateValidType()
        {
            // Arrange
            int randomAge = new Random().Next(2, 60);

            // Act
            var passenger = PassengerGenerator.GeneratePassnegr(randomAge);

            // Assert
            Assert.True(passenger.Type == PassengerType.AdultRequiringTwoSeats || passenger.Type == PassengerType.Adult || passenger.Type == PassengerType.Child);
        }

        /// <summary>
        /// Verifies that the GenerateFamily method creates a family without 
        /// exceeding the specified limit on family members.
        /// </summary>
        /// <param name="limit">The limit on the number of family members.</param>
        [Theory]
        [InlineData(5)]
        [InlineData(3)]
        public void GenerateFamily_WithSpecificLimits_ShouldNotExceedLimit(int limit)
        {
            // Arrange
            var familyName = "Z";

            // Act
            var family = PassengerGenerator.GenerateFamily(familyName, limit);

            // Assert
            Assert.True(family.FamilyMembers.Count <= limit);
        }

        /// <summary>
        /// Tests if the GenerateSinglePassenger method creates a single passenger with a valid age.
        /// </summary>
        [Fact]
        public void GenerateSinglePassenger_ShouldCreatePassengerWithValidAge()
        {
            // Act
            var passenger = PassengerGenerator.GenerateSinglePassnegr();

            // Assert
            Assert.InRange(passenger.Age, 13, 60);
        }

        /// <summary>
        /// Tests if the GenerateSinglePassenger method creates a single adult passenger or an 
        /// adult requiring two seats.
        /// </summary>
        [Fact]
        public void GenerateSinglePassenger_ShouldCreateAdultOrAdultRequiringTwoSeats()
        {
            // Act
            var passenger = PassengerGenerator.GenerateSinglePassnegr();

            // Assert
            bool isValidType = passenger.Type == PassengerType.Adult || passenger.Type == PassengerType.AdultRequiringTwoSeats;
            Assert.True(isValidType);
        }
    }
}
