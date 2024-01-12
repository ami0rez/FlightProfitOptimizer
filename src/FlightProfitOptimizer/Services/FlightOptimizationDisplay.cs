using FlightProfitOptimizer.Models;

namespace FlightProfitOptimizer.Services
{
    /// <summary>
    /// Responsible for displaying the results of the flight optimization process.
    /// </summary>
    /// <remarks>
    /// This class provides a static method to display the optimal combination of passenger assignments.
    /// It visualizes key details such as passenger information, total revenue generated, and seats used.
    /// </remarks>
    public class FlightOptimizationDisplay
    {
        /// <summary>
        /// Displays the optimal combination of passenger assignments and summarizes the total revenue and seats used.
        /// </summary>
        /// <param name="selectedPassengers">List of passengers selected in the optimal combination.</param>
        /// <param name="totalSeats">Total available seats on the flight.</param>
        public static void DisplayOptimalCombination(List<Passenger> selectedPassengers, int totalSeats)
        {
            int totalRevenue = 0;
            int seatsUsed = 0;

            // Calculate column widths based on the maximum length of items in each column
            int idWidth = selectedPassengers.Max(p => p.Id.ToString().Length) + 5;
            int ageWidth = selectedPassengers.Max(p => p.Age.ToString().Length) + 2;
            int typeWidth = selectedPassengers.Max(p => p.Type.ToString().Length) + 2;
            int familyWidth = selectedPassengers.Max(p => (p.Family ?? "").ToString().Length) + 2;

            // Header
            Console.WriteLine("Selected Passengers:");
            Console.WriteLine($"{"".PadRight(idWidth)} | {"Age".PadRight(ageWidth)} | {"Type".PadRight(typeWidth)} | {"Family".PadRight(familyWidth)}");
            Console.WriteLine(new string('-', idWidth + ageWidth + typeWidth + familyWidth + 9)); // Header separator

            // Rows
            foreach (var passenger in selectedPassengers)
            {
                totalRevenue += passenger.CalculateTicketPrice();
                seatsUsed += passenger.CalculateSeatRequirement();

                string formattedId = passenger.Id.ToString("D3");

                Console.WriteLine($"{($"ID: {formattedId}").PadRight(idWidth)} | {passenger.Age.ToString().PadRight(ageWidth)} | {passenger.Type.ToString().PadRight(typeWidth)} | {passenger.Family?.ToString().PadRight(familyWidth)}");
            }

            // Summary
            Console.WriteLine("\nSummary:");
            Console.WriteLine($"Total Revenue Generated: {totalRevenue} Euros");
            Console.WriteLine($"Total Seats Used: {seatsUsed} / {totalSeats}");
        }
    }
}
