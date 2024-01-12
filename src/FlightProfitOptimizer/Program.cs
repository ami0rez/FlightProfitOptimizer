using FlightProfitOptimizer.Models;
using FlightProfitOptimizer.Services;

int totalSeats = 200; // Assuming the total number of seats on the plane is 200

bool continueProgram = true;

while (continueProgram)
{
    Console.Clear(); // Clear the console for a fresh start

    // Step 1: Generate Passengers and Families
    Tuple<List<Passenger>, List<Family>> generatedResult = PassengerGenerator.GeneratePassengers(250);

    // Step 2: Generate Assignments
    var assignments = AssignmentManager.GenerateListOfAssignments(generatedResult.Item1, generatedResult.Item2);

    // Step 3: Optimize Assignments for Revenue Maximization
    var topAssignments = FlightOptimizer.GetTopAssignments(assignments, totalSeats);

    // Step 4: Display the Optimal Combination
    var selectedPassengers = AssignmentManager.GetMembersFromAssignments(topAssignments);
    FlightOptimizationDisplay.DisplayOptimalCombination(selectedPassengers, totalSeats);

    // Ask the user if they want to regenerate passengers
    Console.Write("\nPress 'R' and Enter to regenerate passengers or any other key and Enter to exit: ");
    var input = Console.ReadKey();

    continueProgram = input.Key == ConsoleKey.R;
}