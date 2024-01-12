namespace FlightProfitOptimizer.Utils.Generators
{
    /// <summary>
    /// Utility class for generating unique passenger identifiers.
    /// </summary>
    public class PassengerIdGenerator
    {
        private static int nextId = 1;

        /// <summary>
        /// Generates and returns a unique passenger identifier.
        /// </summary>
        /// <returns>A unique integer identifier.</returns>
        public static int GenerateId()
        {
            return nextId++;
        }
        /// <summary>
        /// Resets the ID generation sequence.
        /// </summary>
        public static void Reset()
        {
            nextId = 1;
        }
    }
}
