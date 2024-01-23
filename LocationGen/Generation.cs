using LocationGen.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationGen
{
    // Класс входной точки
    internal class Generation
    {
        static void Main(string[] args)
        {
            // Создаём базовый этаж 5 уровня
            // Остальные параметры реализовывать рано
            Floor Floor = new Floor
            {
                FloorId = 1,
                FloorNumber = 1,
                FloorType = FloorTypes.Base,
                Style = 1
            };
            // Генерируем этаж
            Floor.FloorGen();
            // Выводим в консоль
            Floor.ShowMap();
            Console.ReadLine();
        }
    }
}
