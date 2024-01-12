using FlightProfitOptimizer.Models;

namespace FlightProfitOptimizer.Services
{
    /// <summary>
    /// Provides services for optimizing flight seat assignments and revenue.
    /// </summary>
    public class FlightOptimizer
    {
        
        /// <summary>
        /// Identifies the optimal combination of passenger assignments to maximize revenue without exceeding seat capacity.
        /// </summary>
        /// <param name="assignments">List of potential passenger assignments.</param>
        /// <param name="totalSeats">Total available seats on the flight.</param>
        /// <returns>A list of assignments representing the optimal combination for maximum revenue.</returns>
        public static List<Assignment> GetTopAssignments(List<Assignment> assignments, int totalPlaces)
        {
            int[,] dp = new int[assignments.Count + 1, totalPlaces + 1];

            for (int i = 1; i <= assignments.Count; i++)
            {
                for (int j = 1; j <= totalPlaces; j++)
                {
                    if (assignments[i - 1].Seats <= j)
                    {
                        dp[i, j] = Math.Max(dp[i - 1, j], assignments[i - 1].Cost + dp[i - 1, j - assignments[i - 1].Seats]);
                    }
                    else
                    {
                        dp[i, j] = dp[i - 1, j];
                    }
                }
            }

            List<Assignment> optimalCombination = new List<Assignment>();
            int remainingPlaces = totalPlaces;

            for (int i = assignments.Count; i > 0 && remainingPlaces > 0; i--)
            {
                if (dp[i, remainingPlaces] != dp[i - 1, remainingPlaces])
                {
                    optimalCombination.Insert(0, assignments[i - 1]);
                    remainingPlaces -= assignments[i - 1].Seats;
                }
            }
            return optimalCombination;
        }
    }
}
