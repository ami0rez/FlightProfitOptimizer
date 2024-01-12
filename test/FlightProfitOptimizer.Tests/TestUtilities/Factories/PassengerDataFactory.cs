using FlightProfitOptimizer.Models;
using FlightProfitOptimizer.Tests.TestData.Models;
using Newtonsoft.Json;

namespace FlightProfitOptimizer.Tests.TestUtilities.Factories
{
    /// <summary>
    /// Factory class for creating passenger and family data from a JSON file.
    /// </summary>
    public static class PassengerDataFactory
    {
        /// <summary>
        /// Reads passenger data from a JSON file and creates corresponding Passenger and Family objects.
        /// </summary>
        /// <param name="filePath">The file path of the JSON data.</param>
        /// <returns>A tuple containing a list of passengers and a list of families derived from the JSON data.</returns>
        /// <remarks>
        /// This method is particularly useful for loading test data or initializing application data from a pre-defined JSON format.
        /// </remarks>
        public static Tuple<List<Passenger>, List<Family>> CreatePassengersFromJson(string filePath)
        {
            var json = File.ReadAllText(filePath);
            var passengerData = JsonConvert.DeserializeObject<List<PassengerData>>(json);

            var passengers = new List<Passenger>();
            var families = new Dictionary<string, Family>();

            foreach (var data in passengerData)
            {
                var passenger = new Passenger
                {
                    Id = data.Id,
                    Age = data.Age,
                    Type = data.Type == "Adulte" ? (data.RequiresTwoSeats ? PassengerType.AdultRequiringTwoSeats : PassengerType.Adult) : PassengerType.Child,
                    Family = data.Family != "-" ? data.Family : string.Empty
                };

                passengers.Add(passenger);

                if (data.Family != "-")
                {
                    if (!families.ContainsKey(data.Family))
                    {
                        families[data.Family] = new Family { Name = data.Family, FamilyMembers = new List<Passenger>() };
                    }
                    families[data.Family].FamilyMembers.Add(passenger);
                }
            }
            return Tuple.Create(passengers, families.Values.ToList());
        }
    }
}
