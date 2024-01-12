using FlightProfitOptimizer.Models;
using FlightProfitOptimizer.Services;
using FlightProfitOptimizer.Tests.TestUtilities.Factories;
using Xunit;

namespace FlightProfitOptimizer.Tests.UnitTests.Services
{
    /// <summary>
    /// Contains unit tests for the <see cref="AssignmentManager"/>.
    /// </summary>
    public class AssignmentManagerTests
    {
        /// <summary>
        /// Tests whether <see cref="AssignmentManager.GenerateListOfAssignments"/> correctly assigns 
        /// passengers and families into assignments.
        /// </summary>
        [Fact]
        public void GenerateListOfAssignments_ShouldCorrectlyAssignPassengersAndFamilies()
        {
            // Arrange: Create a list of passengers and families
            var (passengers, families) = PassengerGenerator.GeneratePassengers(10); // Generate 10 passengers

            //// Act
            var assignments = AssignmentManager.GenerateListOfAssignments(passengers, families);

            // Assert: Check if assignments are correctly generated
            Assert.NotNull(assignments);
            Assert.NotEmpty(assignments);

            foreach (var assignment in assignments)
            {
                if (assignment.Family != default)
                {
                    // Ensure family members are together
                    var familyMembers = families.First(f => f.Name == assignment.Family).FamilyMembers;
                    Assert.Equal(familyMembers.Count, assignment.Members.Count);
                    Assert.All(assignment.Members, member => Assert.Contains(member, familyMembers));
                }
                else
                {
                    // Individual passenger
                    Assert.Single(assignment.Members);
                }

                // Check if cost and seats are correctly calculated
                int expectedCost = assignment.Members.Sum(member => member.CalculateTicketPrice());
                int expectedSeats = assignment.Members.Sum(member => member.CalculateSeatRequirement());
                Assert.Equal(expectedCost, assignment.Cost);
                Assert.Equal(expectedSeats, assignment.Seats);
            }
        }

        /// <summary>
        /// Tests whether <see cref="AssignmentManager.GetMembersFromAssignments"/> correctly extracts all 
        /// members from assignments.
        /// </summary>
        [Fact]
        public void GetMembersFromAssignments_ShouldExtractAllMembers()
        {
            // Arrange: Create mock assignments with members
            var firstAssignment = MockAssignmentFactory.CreateEmptyAssignment();
            firstAssignment.Members.Add(MockPassengerFactory.CreateAdultPassenger());
            firstAssignment.Members.Add(MockPassengerFactory.CreateChild());

            var secondAssignment = MockAssignmentFactory.CreateEmptyAssignment();
            firstAssignment.Members.Add(MockPassengerFactory.CreateAdultRequiringTwoSeats());
            
            var mockAssignments = new List<Assignment>
            {
                firstAssignment,
                secondAssignment
            };

            // Act: Extract members from the assignments
            var expectedMembers = mockAssignments.SelectMany(a => a.Members).ToList();
            var actualMembers = AssignmentManager.GetMembersFromAssignments(mockAssignments);

            // Assert: Ensure all expected members are present in the extracted list
            Assert.Equal(expectedMembers.Count, actualMembers.Count);
            Assert.All(expectedMembers, expectedMember => Assert.Contains(expectedMember, actualMembers));
        }
    }
}
