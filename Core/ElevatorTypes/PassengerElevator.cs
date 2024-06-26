using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Core.ElevatorTypes
{
    public class PassengerElevator : Elevator
    {
        public PassengerElevator(int maxPassengerCapacity) : base(maxPassengerCapacity)
        {
        }
    }

}
