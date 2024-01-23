using LocationGen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationGen.Interfaces
{
    internal interface IFloor
    {
        int FloorId { get; set; }
        int FloorNumber { get; set; }
        int Style { get; set; }
        FloorTypes FloorType { get; set; }
        void FloorGen();
        void Rebuild();

    }
}
