using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationGen
{
    internal interface IRoom
    {
        int RoomId { get; set; }
        int Style { get; set; }
        int X { get; set; }
        int Y { get; set; }
        RoomTypes RoomType { get; set; }
        Floor Floor { get; set; }
        bool Mandatory { get; set; }
        void Reroll();
    }
}
