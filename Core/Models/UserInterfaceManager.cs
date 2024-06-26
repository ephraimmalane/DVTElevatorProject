using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Core.Models
{
    public class UserInterfaceManager
    {
        private readonly ElevatorSystems elevatorSystem;

        public UserInterfaceManager(ElevatorSystems elevatorSystem)
        {
            this.elevatorSystem = elevatorSystem;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                Console.WriteLine("\nSelect an option:");
                Console.WriteLine("1. Call elevator to a floor");
                Console.WriteLine("2. Step simulation");
                Console.WriteLine("3. Display status");
                Console.WriteLine("4. Exit");

                int option;
                if (int.TryParse(Console.ReadLine(), out option))
                {
                    switch (option)
                    {
                        case 1:
                            await CallElevatorAsync();
                            break;
                        case 2:
                            await StepSimulationAsync();
                            break;
                        case 3:
                            DisplayStatus();
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        private async Task CallElevatorAsync()
        {
            try
            {
                Console.Write("Enter floor number: ");
                if (int.TryParse(Console.ReadLine(), out int floorNumber))
                {
                    if (floorNumber < 0 || floorNumber >= elevatorSystem.FloorsCount)
                    {
                        throw new ArgumentException("Invalid floor number. Please enter a valid number.");
                    }

                    Console.Write("Enter number of passengers: ");
                    if (int.TryParse(Console.ReadLine(), out int passengers))
                    {
                        if (passengers < 1)
                        {
                            throw new ArgumentException("Number of passengers must be at least 1.");
                        }

                        elevatorSystem.RequestElevator(floorNumber, passengers);
                        Console.WriteLine($"Elevator called to floor {floorNumber} with {passengers} passengers.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of passengers. Please enter a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid floor number. Please enter a valid number.");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }

        private async Task StepSimulationAsync()
        {
            await elevatorSystem.StepSimulationAsync();
            Console.WriteLine("Simulation stepped. Elevators updated.");
        }

        private void DisplayStatus()
        {
            elevatorSystem.DisplayStatus();
        }
    }

}
