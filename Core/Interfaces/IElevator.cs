using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Core.Interfaces
{
    public interface IElevator
    {
        int CurrentFloor { get; set; }
        bool IsMoving { get; set; }
        bool IsStationary { get; }
        Direction CurrentDirection { get; set; }
        int PassengerCount { get; set; }
        int DestinationFloor { get; set; }
        int MaxPassengerCapacity { get; }
    }

}
