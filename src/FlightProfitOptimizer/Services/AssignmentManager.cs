using FlightProfitOptimizer.Models;

namespace FlightProfitOptimizer.Services
{
    public class AssignmentManager
    {
        /// <summary>
        /// Generates a list of assignments for passengers and families based on flight seat allocation.
        /// </summary>
        /// <param name="passengers">List of all passengers.</param>
        /// <param name="families">List of families.</param>
        /// <returns>List of assignments for optimizing seat allocation and revenue.</returns>
        public static List<Assignment> GenerateListOfAssignments(List<Passenger> passengers, List<Family> families)
        {
            var addedFamilies = new List<Family>();
            var assignments = new List<Assignment>();
            foreach (var passenger in passengers)
            {
                if (string.IsNullOrEmpty(passenger.Family))
                {
                    var cost = passenger.CalculateTicketPrice();
                    var numberOfSeats = passenger.CalculateSeatRequirement();
                    assignments.Add(new Assignment
                    {
                        Cost = cost,
                        Members = new List<Passenger> { passenger },
                        Seats = numberOfSeats,
                    });
                }
                else
                {
                    if (!addedFamilies.Any(family => family.Name == passenger.Family))
                    {
                        var family = families.First(family => family.Name == passenger.Family);
                        addedFamilies.Add(family);
                        var cost = family.CalculateTotalFamilyCost();
                        var numberOfSeats = family.CalculateTotalSeats();
                        assignments.Add(new Assignment
                        {
                            Cost = cost,
                            Members = family.FamilyMembers,
                            Seats = numberOfSeats,
                            Family = passenger.Family
                        });
                    }
                }
            }
            return assignments;
        }

        /// <summary>
        /// Extracts all passengers from a list of assignments.
        /// </summary>
        /// <param name="assignments">List of assignments.</param>
        /// <returns>List of all passengers included in the assignments.</returns>
        public static List<Passenger> GetMembersFromAssignments(List<Assignment> assignments)
        {
            var memebers = assignments.SelectMany(assignment => assignment.Members).ToList();
            return memebers;
        }
    }
}
