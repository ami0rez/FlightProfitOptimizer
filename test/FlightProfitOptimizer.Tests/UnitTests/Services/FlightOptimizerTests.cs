using FlightProfitOptimizer.Models;
using FlightProfitOptimizer.Services;
using FlightProfitOptimizer.Tests.TestUtilities.Factories;
using System.Diagnostics;
using Xunit;

namespace FlightProfitOptimizer.Tests.UnitTests.Services
{
    /// <summary>
    /// Unit tests for the FlightOptimizer service, focusing on optimizing passenger assignments for revenue maximization.
    /// </summary>
    public class FlightOptimizerTests
    {
        /// <summary>
        /// Sanity Check :
        /// Tests if the GetTopAssignments method correctly identifies the optimal distribution of passengers for various scenarios.
        /// </summary>
        /// <param name="totalPlaces">Total available seats on the flight.</param>
        /// <param name="numberOfPassengers">Number of passengers to generate for the test.</param>
        [Theory]
        [InlineData(200, 10)]
        [InlineData(150, 100)]
        [InlineData(100, 50)]
        [InlineData(50, 25)]
        public void GetTopAssignments_ShouldReturnOptimalDistribution_SanityCheck(int totalPlaces, int numberOfPassengers)
        {
            // Arrange: Create a list of passengers and families
            var (passengers, families) = PassengerGenerator.GeneratePassengers(numberOfPassengers);

            var assignments = AssignmentManager.GenerateListOfAssignments(passengers, families);

            // Act
            var topAssignments = FlightOptimizer.GetTopAssignments(assignments, totalPlaces);

            // Assert
            Assert.NotNull(topAssignments);
            Assert.NotEmpty(topAssignments);

            // Verify seat allocation
            int totalSeatsAllocated = topAssignments.Sum(a => a.Seats);
            Assert.True(totalSeatsAllocated <= totalPlaces, "Total seats allocated should not exceed total available places.");

            // Sanity check for revenue maximization
            int totalRevenue = topAssignments.Sum(a => a.Cost);
            Assert.True(totalRevenue >= assignments.Max(a => a.Cost), "Total revenue should be at least as high as the revenue of the most valuable individual assignment.");
        }

        /// <summary>
        /// Tests the GetTopAssignments method using a simplified scenario to ensure correct optimization.
        /// </summary>
        [Fact]
        public void GetTopAssignments_ShouldReturnOptimalDistribution_SimplifiedScenario()
        {
            // Arrange
            var familyA = MockFamilyFactory.CreateEmptyFamily("A");
            familyA.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.Adult, "A"));
            familyA.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.Adult, "A"));
            familyA.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.Child, "A"));
            familyA.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.Child, "A"));

            var familyB = MockFamilyFactory.CreateEmptyFamily("B");
            familyB.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.AdultRequiringTwoSeats, "B")); // 2 seats
            familyB.AddMember(MockPassengerFactory.CreatePassenger(PassengerType.Child, "B"));

            var individualAdults = new List<Passenger>
            {
                MockPassengerFactory.CreatePassenger(PassengerType.Adult, string.Empty), // 250€
                MockPassengerFactory.CreatePassenger(PassengerType.Adult, string.Empty)  // 250€
            };

            var passengers = familyA.FamilyMembers.Concat(familyB.FamilyMembers).Concat(individualAdults).ToList();
            var families = new List<Family> { familyA, familyB };
            var assignments = AssignmentManager.GenerateListOfAssignments(passengers, families);
            int totalPlaces = 10;

            // Act
            var topAssignments = FlightOptimizer.GetTopAssignments(assignments, totalPlaces);

            // Assert
            int totalRevenue = topAssignments.Sum(a => a.Cost);
            int totalSeatsAllocated = topAssignments.Sum(a => a.Seats);
            Assert.Equal(1950, totalRevenue); // As calculated manually
            Assert.Equal(9, totalSeatsAllocated);
            Assert.True(totalSeatsAllocated <= totalPlaces);
        }

        /// <summary>
        /// Ensures that the FlightOptimizer service produces results matching a given scenario from the table 
        /// provided with the test 
        /// typically provided in a job test.
        /// </summary>
        [Fact]
        public void FlightOptimizer_ShouldMatchProvidedScenario()
        {
            // Arrange
            // Create passengers and families based on the provided table
            var (passengers, families) = PassengerDataFactory.CreatePassengersFromJson(@"TestData\passengerData.json");

            // Determine the expected results based on job test rules
            int expectedRevenue = 12950;
            int totalSeats = 63;

            // Act
            var assignments = AssignmentManager.GenerateListOfAssignments(passengers, families);
            var topAssignments = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            int actualRevenue = topAssignments.Sum(a => a.Cost);
            Assert.Equal(expectedRevenue, actualRevenue);
        }

        /// <summary>
        /// Confirms that the GetTopAssignments method fully utilizes available seat capacity.
        /// </summary>
        /// <param name="totalSeats">The total capacity of seats available on the flight.</param>
        [Fact]
        public void GetTopAssignments_FullCapacityUtilization_UsesAllSeats()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily())
            };
            int totalSeats = 20; // Assume this is the total capacity.

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.Equal(totalSeats, CalculateTotalSeatsUsed(result));
        }

        /// <summary>
        /// Checks if the GetTopAssignments method correctly handles scenarios where not all seats are used.
        /// </summary>
        /// <param name="totalSeats">The total capacity of seats available on the flight.</param>
        [Fact]
        public void GetTopAssignments_PartialCapacityUtilization_DoesNotUseAllSeats()
        {
            // Arrange
            int totalSeats = 20; // Assume this is the total capacity.
            var assignments = new List<Assignment>
            {
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateAdultWithChild()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateAdultWithTwoChildren()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily())
            };

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.True(CalculateTotalSeatsUsed(result) < totalSeats);
        }

        /// <summary>
        /// Verifies the behavior of the GetTopAssignments method with an empty list of assignments.
        /// </summary>
        [Fact]
        public void GetTopAssignments_EmptyAssignments_ReturnsEmptyList()
        {
            // Arrange
            var assignments = new List<Assignment>();
            int totalSeats = 10;

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests the GetTopAssignments method with a single assignment input.
        /// </summary>
        [Fact]
        public void GetTopAssignments_SingleAssignment_ReturnsSingleAssignment()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateAdultWithChild())
            };
            int totalSeats = 5;

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.Single(result);
        }

        /// <summary>
        /// Assesses how the GetTopAssignments method handles situations where the seat requirements exceed total seats.
        /// </summary>
        /// <param name="totalSeats">The total available seats on the flight.</param>
        [Fact]
        public void GetTopAssignments_ExceedingTotalSeats_OmitsSomeAssignments()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFamilyWithMaxChildren()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),
            };
            int totalSeats = 15; // Total seats available on the flight

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.True(CalculateTotalSeatsUsed(result) <= totalSeats);
        }

        /// <summary>
        /// Evaluates the response of GetTopAssignments to invalid total seat inputs.
        /// </summary>
        /// <param name="totalSeats">The total available seats, where invalid values are provided for testing.</param>
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void GetTopAssignments_InvalidTotalSeats_ReturnsEmptyList(int totalSeats)
        {
            // Arrange
            var assignments = MockAssignmentFactory.CreateBasicAssignments();

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.Empty(result);
        }

        /// <summary>
        /// Tests whether GetTopAssignments selects the combination of assignments that maximizes revenue.
        /// </summary>
        [Fact]
        public void GetTopAssignments_MaximizeRevenue_SelectsHighestRevenueCombination()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),         // 950 € / 5 Seats
                MockAssignmentFactory.CreateAssignment(MockPassengerFactory.CreateAdultRequiringTwoSeats()),    // 500 € / 2 Seats
                MockAssignmentFactory.CreateAssignment(MockPassengerFactory.CreateAdultRequiringTwoSeats()),    // 500 € / 2 Seats
                MockAssignmentFactory.CreateAssignment(MockPassengerFactory.CreateAdultRequiringTwoSeats()),    // 500 € / 2 Seats
            };
            int totalSeats = 10; // Total seats available on the flight

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.Equal(1950, CalculateTotalRevenue(result));
        }

        /// <summary>
        /// Verifies that GetTopAssignments prioritizes assignments with higher revenue for the same seat usage.
        /// </summary>
        [Fact]
        public void GetTopAssignments_SameSeatDifferentRevenue_PrefersHigherRevenue()
        {
            var highestRevenueAssignment = new Assignment { Cost = 500, Seats = 5 };
            // Arrange
            var assignments = new List<Assignment>
            {
                new Assignment { Cost = 300, Seats = 5 },
                highestRevenueAssignment,
                new Assignment { Cost = 400, Seats = 5 }
            };

            int totalSeats = 10; // Total seats available on the flight

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            // The assignment with the highest revenue
            Assert.True(result?.Contains(highestRevenueAssignment));
        }

        /// <summary>
        /// Tests the optimization of the GetTopAssignments method in scenarios with mixed revenue and seat requirements.
        /// </summary>
        [Fact]
        public void GetTopAssignments_MixedRevenueAndSeatRequirements_MaximizesRevenue()
        {
            // Arrange
            var assignments = new List<Assignment>
            {
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),        // 950 € / 5 Seats
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFamilyWithMaxChildren()),        // 700 € / 4 Seats
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily()),         // 950 € / 5 Seats
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateAdultWithTwoChildren()),         // 550 € / 3 Seats
                MockAssignmentFactory.CreateAssignment(MockPassengerFactory.CreateAdultRequiringTwoSeats()),    // 500 € / 2 Seats
            };
            int totalSeats = 15;

            int optimalRevenue = 2950; // 950 + 950 + 550 + 500 = 2950 €
            int optimalSeatsCount = 15; // 5 + 5 + 3 + 2 = 15

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            int totalRevenue = CalculateTotalRevenue(result);
            int totalSeatsCount = CalculateTotalSeatsUsed(result);
            // Assert
            Assert.Equal(totalRevenue, optimalRevenue);
            Assert.Equal(totalSeatsCount, optimalSeatsCount);
        }

        /// <summary>
        /// Confirms that the GetTopAssignments method prefers assignments with high revenue and low seat usage.
        /// </summary>
        [Fact]
        public void GetTopAssignments_HighRevenueLowSeatUsage_PrefersHighRevenue()
        {
            // Arrange

            // The desired assignment
            var highRevenueLowSeatAssignment = MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateFullyPopulatedFamily());
            var assignments = new List<Assignment>
            {
                highRevenueLowSeatAssignment, // High revenue, low seat usage
                MockAssignmentFactory.CreateAssignment(MockFamilyFactory.CreateAdultWithTwoChildren()), // Lower revenue, higher seat usage
                MockAssignmentFactory.CreateAssignment(MockPassengerFactory.CreateAdultRequiringTwoSeats()),
            };
            int totalSeats = 10; // Total seats available on the flight

            // Act
            var result = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

            // Assert
            Assert.Contains(highRevenueLowSeatAssignment, assignments);
            Assert.True(CalculateTotalRevenue(result) >= highRevenueLowSeatAssignment.Cost);
        }


        /// <summary>
        /// Evaluates the performance of GetTopAssignments on a large data set within an acceptable time frame.
        /// </summary>
        /// <param name="numberOfAssignments">The number of assignments to be generated for the test.</param>
        /// <param name="totalSeats">The total available seats on the flight.</param>
        /// <param name="milliseconds">The threshold in milliseconds for the acceptable performance time frame.</param>
        [Theory]
        [InlineData(1000, 500, 500)]
        [InlineData(500, 200, 100)]
        public void GetTopAssignments_Performance_OnLargeDataSet(int numberOfAssignments, int totalSeats, int milliseconds)
        {
            // Arrange
            var largeNumberOfAssignments = MockAssignmentFactory.CreateLargeNumberOfAssignments(numberOfAssignments);

            var stopwatch = new Stopwatch();

            // Act
            stopwatch.Start();
            var result = FlightOptimizer.GetTopAssignments(largeNumberOfAssignments, totalSeats);
            stopwatch.Stop();

            // Assert
            Assert.NotEmpty(result); // Ensure some assignments are selected

            // Example: Ensure that the processing time is within an acceptable range, e.g., under 2 seconds
            Assert.True(stopwatch.ElapsedMilliseconds < milliseconds, $"Execution time {stopwatch.ElapsedMilliseconds}ms exceeded performance threshold.");
        }

        /// <summary>
        /// Calculates the total number of seats used by the selected assignments.
        /// </summary>
        /// <param name="assignments">The list of assignments from which to calculate total seats used.</param>
        /// <returns>The total number of seats used by the given assignments.</returns>
        private int CalculateTotalSeatsUsed(List<Assignment> assignments)
        {
            int totalSeatsUsed = 0;
            foreach (var assignment in assignments)
            {
                totalSeatsUsed += assignment.Seats;
            }
            return totalSeatsUsed;
        }

        /// <summary>
        /// Calculates the total revenue generated from the selected assignments.
        /// </summary>
        /// <param name="assignments">The list of assignments from which to calculate total revenue.</param>
        /// <returns>The total revenue generated by the given assignments.</returns>
        private int CalculateTotalRevenue(List<Assignment> assignments)
        {
            return assignments.Sum(a => a.Cost);
        }
    }
}
