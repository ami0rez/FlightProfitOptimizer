using FlightProfitOptimizer.Common.Constants;
using FlightProfitOptimizer.Common.Exceptions;
using FlightProfitOptimizer.Models;
using FlightProfitOptimizer.Utils.Generators;

namespace FlightProfitOptimizer.Services
{
    /// <summary>
    /// Provides functionality for generating passengers and organizing them into families for flight assignments.
    /// </summary>
    public class PassengerGenerator
    {

        private static Random random = new Random();

        /// <summary>
        /// Generates a list of passengers and families for flight seating assignments.
        /// </summary>
        /// <param name="count">The number of passengers to generate.</param>
        /// <returns>A tuple containing a list of passengers and a list of families.</returns>
        /// <exception cref="InvalidPassengerCountException">Thrown if the count is less than or equal to zero.</exception>
        public static Tuple<List<Passenger>, List<Family>> GeneratePassengers(int count)
        {
            if (count <= 0)
            {
                throw new InvalidPassengerCountException(ErrorConstants.PassengerMinSizeNotReached);
            }

            PassengerIdGenerator.Reset();

            List<Passenger> passengers = new List<Passenger>();
            List<Family> families = new List<Family>();

            while (passengers.Count < count)
            {
                var emptySeats = count - passengers.Count;
                var addFamily = random.Next(0, 2) == 1 && emptySeats > 1;

                if (addFamily)
                {
                    var familyName = GetFamilyName(families.Count());
                    Family family;
                    if (passengers.Count + 5 >= count)
                    {
                        family = GenerateFamily(familyName, emptySeats);
                    }
                    else
                    {
                        family = GenerateFamily(familyName);
                    }
                    families.Add(family);
                    passengers.AddRange(family.FamilyMembers);
                }
                else
                {
                    var passenger = GenerateSinglePassnegr();
                    passengers.Add(passenger);
                }
            }
            return Tuple.Create(passengers, families);
        }

        /// <summary>
        /// Generates a family name based on a numeric index.
        /// </summary>
        /// <param name="index">The index used to generate the family name.</param>
        /// <returns>The generated family name.</returns>
        private static string GetFamilyName(int index)
        {
            // Start with an empty string for the family name
            string familyName = string.Empty;
            while (index >= 0)
            {
                // Modulo operation to find the current "digit"
                int remainder = index % 26;
                // Prepend the corresponding letter to the family name
                familyName = (char)('A' + remainder) + familyName;
                // Prepare for the next iteration
                index = index / 26 - 1;
            }
            return familyName;
        }

        /// <summary>
        /// Creates a family with a specified name and an optional maximum number of members.
        /// </summary>
        /// <param name="familyName">The name of the family.</param>
        /// <param name="maxMembers">Optional maximum number of family members.</param>
        /// <returns>A new family instance.</returns>
        /// <exception cref="InvalidFamilySizeException">Thrown if the specified maxMembers is invalid.</exception>
        public static Family GenerateFamily(string familyName, int? maxMembers = null)
        {
            if (maxMembers > 5)
            {
                throw new InvalidFamilySizeException(ErrorConstants.FamilyMaxSizeExceeded);
            }
            if (maxMembers <= 0)
            {
                throw new InvalidFamilySizeException(ErrorConstants.FamilyMinSizeNotReached);
            }

            var family = new Family
            {
                Name = familyName
            };
            int familyMembers = random.Next(2, (maxMembers ?? 5) + 1);
            int parentsCount = random.Next(1, 2);
            int childsCount = familyMembers - parentsCount;
            if (childsCount >= 4)
            {
                childsCount = 3;
            }
            for (int i = 0; i < parentsCount; i++)
            {
                int randomAge = random.Next(13, 60);
                Passenger passenger = GeneratePassnegr(randomAge, familyName);
                family.FamilyMembers.Add(passenger);
            }

            for (int i = 0; i < childsCount; i++)
            {
                int randomAge = random.Next(2, 12);
                Passenger passenger = GeneratePassnegr(randomAge, familyName);
                family.FamilyMembers.Add(passenger);
            }
            return family;
        }

        /// <summary>
        /// Generates a single passenger.
        /// </summary>
        /// <returns>A newly created passenger.</returns>
        public static Passenger GenerateSinglePassnegr()
        {
            int randomAge = random.Next(13, 60);
            Passenger passenger = GeneratePassnegr(randomAge);
            return passenger;
        }

        /// <summary>
        /// Creates a passenger with a specified age and optionally assigns them to a family.
        /// </summary>
        /// <param name="age">The age of the passenger.</param>
        /// <param name="familyName">Optional family name to assign the passenger to.</param>
        /// <returns>A new passenger instance.</returns>
        public static Passenger GeneratePassnegr(int age, string? familyName = null)
        {
            bool isAdult = age > 12;
            Passenger passenger = new Passenger
            {
                Id = PassengerIdGenerator.GenerateId(),
                Age = age,
                Type = isAdult ? random.Next(0, 2) == 0 ? PassengerType.AdultRequiringTwoSeats : PassengerType.Adult : PassengerType.Child,
            };
            if (!string.IsNullOrEmpty(familyName))
            {
                passenger.Family = familyName;
            }
            return passenger;
        }

    }
    
}
