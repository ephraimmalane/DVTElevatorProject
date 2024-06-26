using ElevatorSystem.Core.Interfaces;

namespace ElevatorSystem.Core
{
    public class Elevator : IElevator
    {
        public int CurrentFloor { get; set; }
        public bool IsMoving { get; set; }
        public bool IsStationary => !IsMoving;
        public Direction CurrentDirection { get; set; }
        public int PassengerCount { get; set; }
        public int DestinationFloor { get; set; } = -1; // -1 means no destination
        public int MaxPassengerCapacity { get; }

        public Elevator(int maxPassengerCapacity)
        {
            MaxPassengerCapacity = maxPassengerCapacity;
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Stationary
    }

}
