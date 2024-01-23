using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationGen
{
    internal class Room : IRoom
    {
        /// <summary>
        /// Класс описания комнаты
        /// </summary>
        /// <param name="Type">
        /// Тип комнаты, её предназначение
        /// </param>
        /// <param name="Id">
        /// Идентификатор комнаты в пуле комнат
        /// </param>
        /// <param name="Mandatory">
        /// Обязательность комнаты при спавне
        /// </param>
        /// <param name="Floor">
        /// Этаж, которому принадлежит комната
        /// </param>
        /// <param name="Style">
        /// Идентификатор стиля комнаты
        /// </param>
        /// <param name="X">
        /// Положение комнаты по горизонтали
        /// </param>
        /// <param name="Y">
        /// Положение комнаты по вертикали
        /// </param>
         
        internal RoomTypes RoomType;
        internal int RoomId;
        internal bool Mandatory;
        internal Floor Floor = null;
        internal int Style;
        internal int X;
        internal int Y;

        // Геттеры и сеттеры
        int IRoom.X { get { return X; } set { X = value; } }
        int IRoom.Y { get { return Y; } set { Y = value; } }
        int IRoom.RoomId { get { return RoomId; } set { RoomId = value; } }
        RoomTypes IRoom.RoomType { get { return RoomType; } set { RoomType = value; } }
        int IRoom.Style { get { return Style; } set { Style = value; } }
        Floor IRoom.Floor { get { return Floor; } set { Floor = value; } }
        bool IRoom.Mandatory { get { return Mandatory; } set { Mandatory = value; } }

        // Базовый конструктор
        public Room()
        {
            RoomType = RoomTypes.Empty;
            RoomId = -1;
            Mandatory = false;
            Floor = null;
            Style = -1;
        }

        // Полный конструктор
        public Room(RoomTypes type, int id, bool mandatory, Floor floor, int style, int x, int y)
        {
            RoomType = type;
            RoomId = id;
            Mandatory = mandatory;
            Floor = floor;
            Style = style;
            X = x;
            Y = y;
        }


        // Метод пересборки комнаты
        // TODO: В будущем переделать
        void IRoom.Reroll()
        {
            throw new NotImplementedException();
        }

        // Метод вывода экземпляра класса в виде строки
        public override string ToString()
        {
            return "Room \n{Id: " + RoomId + "\n" + "Type: " + RoomType + "\n" + 
                "Mandatory: " + Mandatory + "\n" + "Style: " + Style + "\n"
                + "Floor: " + Floor + "\n}";
        }

        // Хэширование для сравнения идентичных комнат
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
