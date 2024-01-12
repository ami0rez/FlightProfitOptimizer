using FlightProfitOptimizer.Services;
using Xunit;

namespace FlightProfitOptimizer.Tests.UnitTests.Services
{
    /// <summary>
    /// Contains unit tests for the <see cref="FlightOptimizationDisplay"/> class.
    /// </summary>
    public class FlightOptimizationDisplayTests
    {
        /// <summary>
        /// Verifies that the DisplayOptimalCombination method correctly displays the 
        /// summary information of the flight optimization.
        /// </summary>
        [Fact]
        public void DisplayOptimalCombination_ShouldDisplayCorrectInformation()
        {
            // Arrange: Generate passengers and calculate total revenue and seats used
            var (passengers, families) = PassengerGenerator.GeneratePassengers(200);
            int totalSeats = 200;
            int totalRevenue = passengers.Sum(p => p.CalculateTicketPrice());

            // Redirect console output for capturing the display result
            var originalOutput = Console.Out;
            var stringWriter = new StringWriter();

            // Act: Display the optimal combination of passengers
            Console.SetOut(stringWriter);
            FlightOptimizationDisplay.DisplayOptimalCombination(passengers, totalSeats);
            
            // Reset console output to its original state
            Console.SetOut(originalOutput);

            // Assert: Check if the expected summary information is displayed
            string output = stringWriter.ToString();
            Assert.Contains($"Total Revenue Generated: {totalRevenue} Euros", output);
            Assert.Contains($"Total Seats Used: {passengers.Sum(p => p.CalculateSeatRequirement())} / {totalSeats}", output);
        }
    }
}
