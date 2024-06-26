using ElevatorSystem.Core.ElevatorTypes;
using ElevatorSystem.Core.Floors;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Core.Models
{
    public class ElevatorSystems
    {
        public List<Elevator> elevators;
        public List<Floor> floors; 

   


        public ElevatorSystems(int elevatorCount, int floorCount)
        {
            elevators = new List<Elevator>();
            for (int i = 0; i < elevatorCount; i++)
            {
                // Create passenger elevators by default
                elevators.Add(new PassengerElevator(maxPassengerCapacity: 10)
                {
                    CurrentFloor = 0,
                    IsMoving = false,
                    CurrentDirection = Direction.Stationary,
                    PassengerCount = 0
                });
            }

            floors = new List<Floor>();
            for (int i = 0; i < floorCount; i++)
            {
                floors.Add(new Floor(i));
            }
        }

        public int FloorsCount => floors.Count;

        // Method to handle requesting an elevator to a floor with passengers
        public void RequestElevator(int floorNumber, int passengers)
        {
            var availableElevators = GetAvailableElevators(floorNumber);

            if (!availableElevators.Any())
            {
                Console.WriteLine("No available elevators. Please try again later.");
                return;
            }

            var chosenElevator = SelectElevator(availableElevators, floorNumber);

            // Check if adding passengers exceeds max capacity
            if (chosenElevator.PassengerCount + passengers > chosenElevator.MaxPassengerCapacity)
            {
                Console.WriteLine($"Adding {passengers} passengers would exceed elevator capacity. Dispatching another elevator.");
                return;
            }

            AssignElevator(chosenElevator, floorNumber, passengers);
        }

        private List<Elevator> GetAvailableElevators(int floorNumber)
        {
            return elevators
                .Where(e => !e.IsMoving || (e.CurrentDirection == Direction.Up && e.CurrentFloor <= floorNumber) || (e.CurrentDirection == Direction.Down && e.CurrentFloor >= floorNumber))
                .ToList();
        }

        private Elevator SelectElevator(List<Elevator> availableElevators, int floorNumber)
        {
            availableElevators = MergeSort(availableElevators, floorNumber);
            return availableElevators.First();
        }

        private void AssignElevator(Elevator elevator, int floorNumber, int passengers)
        {
            if (elevator.CurrentFloor < floorNumber)
            {
                elevator.CurrentDirection = Direction.Up;
            }
            else if (elevator.CurrentFloor > floorNumber)
            {
                elevator.CurrentDirection = Direction.Down;
            }
            else
            {
                elevator.CurrentDirection = Direction.Stationary;
            }

            elevator.IsMoving = true;
            elevator.DestinationFloor = floorNumber;
            floors[floorNumber].WaitingPassengers += passengers;
        }

        private List<Elevator> MergeSort(List<Elevator> list, int floorNumber)
        {
            if (list.Count <= 1)
                return list;

            var middle = list.Count / 2;
            var left = list.GetRange(0, middle);
            var right = list.GetRange(middle, list.Count - middle);

            left = MergeSort(left, floorNumber);
            right = MergeSort(right, floorNumber);

            return Merge(left, right, floorNumber);
        }

        private List<Elevator> Merge(List<Elevator> left, List<Elevator> right, int floorNumber)
        {
            var result = new List<Elevator>();

            while (left.Any() && right.Any())
            {
                if (Math.Abs(left.First().CurrentFloor - floorNumber) <= Math.Abs(right.First().CurrentFloor - floorNumber))
                {
                    result.Add(left.First());
                    left.RemoveAt(0);
                }
                else
                {
                    result.Add(right.First());
                    right.RemoveAt(0);
                }
            }

            result.AddRange(left);
            result.AddRange(right);

            return result;
        }

        // Method to simulate time passing and update elevator positions asynchronously
        public async Task StepSimulationAsync()
        {
            await Task.Delay(1000); // Simulating some delay asynchronously

            foreach (var elevator in elevators)
            {
                if (elevator.IsMoving)
                {
                    if (elevator.CurrentDirection == Direction.Up)
                    {
                        elevator.CurrentFloor++;
                    }
                    else if (elevator.CurrentDirection == Direction.Down)
                    {
                        elevator.CurrentFloor--;
                    }

                    if (elevator.CurrentFloor == elevator.DestinationFloor)
                    {
                        elevator.IsMoving = false;
                        elevator.CurrentDirection = Direction.Stationary;
                        elevator.PassengerCount += floors[elevator.CurrentFloor].WaitingPassengers;
                        floors[elevator.CurrentFloor].WaitingPassengers = 0;
                    }
                }
            }
        }

        // Method to display the status of each elevator and waiting passengers on each floor
        public void DisplayStatus()
        {
            Console.WriteLine("Elevator Status:");
            for (int i = 0; i < elevators.Count; i++)
            {
                var elevator = elevators[i];
                Console.WriteLine($"Elevator {i} -> Floor: {elevator.CurrentFloor}, Direction: {elevator.CurrentDirection}, Moving: {elevator.IsMoving}, Passengers: {elevator.PassengerCount}/{elevator.MaxPassengerCapacity}");
            }

            Console.WriteLine("\nFloor Status:");
            foreach (var floor in floors)
            {
                Console.WriteLine($"Floor {floor.FloorNumber} -> Waiting Passengers: {floor.WaitingPassengers}");
            }
        }
       
    }

 

}
