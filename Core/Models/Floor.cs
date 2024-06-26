using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSystem.Core.Floors
{
    public class Floor
    {
        public int FloorNumber { get; }
        public int WaitingPassengers { get; set; }

        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
            WaitingPassengers = 0;
        }
    }

}
