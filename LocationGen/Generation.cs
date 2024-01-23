using LocationGen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationGen
{
    internal class Generation
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world");
            Floor Floor = new Floor
            {
                FloorId = 1,
                FloorNumber = 5,
                FloorType = FloorTypes.Base,
                Style = 1
            };
            Console.WriteLine(Floor.ToString());
            Floor.FloorGen();
            Floor.ShowMap();
            Console.ReadLine();
        }
    }
}
