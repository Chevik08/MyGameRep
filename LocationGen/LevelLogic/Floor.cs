using LocationGen.Interfaces;
using System;
using System.Collections.Generic;
using LocationGen.Enums;

namespace LocationGen
{
    internal class Floor : IFloor
    {
        internal int FloorId;
        internal int FloorNumber;
        internal int Style;
        internal FloorTypes FloorType;
        internal List<IRoom> map;
        internal List<IRoom> RoomPool = new List<IRoom> { };
        internal const int Size = 13;
        internal const int MandatoryNum = 6;

        int IFloor.FloorId { get { return FloorId; } set { FloorId = value; } }
        int IFloor.FloorNumber { get { return FloorNumber; } set { FloorNumber = value; } }
        int IFloor.Style { get { return Style; } set { Style = value; } }
        FloorTypes IFloor.FloorType { get { return FloorType; } set { FloorType = value; } }

        public Floor()
        {
            FloorId = -1;
            FloorNumber = -1;
            Style = -1;
            FloorType = FloorTypes.Base;
            map = new List<IRoom>();
            BuildMap();
        }

        public Floor(int floorId, int floorNumber, int style,
            FloorTypes floorType)
        {
            FloorId = floorId;
            FloorNumber = floorNumber;
            Style = style;
            FloorType = floorType;
            map = new List<IRoom>();
            BuildMap();
        }

        // Метод создания отсутствия комнаты
        Room CreateOutRoom(int x, int y)
        {
            return new Room(RoomTypes.Out, -1, false, null, -1, x, y);
        }

        // Построение карты с пустыми комнатами
        public void BuildMap()
        {
            map.Clear();
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    map.Add(CreateOutRoom(i, j));
                }
            }
        }

        public List<IRoom> GetMap()
        {
            return map;
        }

        public void ShowMap()
        {
            // TODO: Убрать счётчик
            int counter = 0;
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write("{0, 8} ", map[counter].RoomType);
                    counter++;
                }
                Console.WriteLine();
            }
        }


        // Алгоритм генерации этажа
        public void FloorGen()
        {
            // Сначала определить количество комнат на этаже
            RoomPool.Clear();
            // Множители
            int MaxMul = 3;
            int MinMul = 1;
            double mul = new Random().NextDouble() * (MaxMul - MinMul) + MinMul;
            int RoomNums = Convert.ToInt32(FloorNumber * mul + MandatoryNum);
            Console.WriteLine(RoomNums);
            // Теперь задать точку спавна
            // Спавн в центре
            int h = Size / 2;  // Высота
            int w = Size / 2;  // Ширина
            Room Spawn = CreateOutRoom(w, h);
            Spawn.RoomType = RoomTypes.Spawn;
            // Сохранение переменных
            int orig_h = Size / 2;
            int orig_w = Size / 2;
            int pred_h;
            int pred_w;
            SetRoom(w, h, Spawn);
            // Создание комнат
            int roomCounter = 1;
            RoomPool.Add(GetRoom(w, h));
            Random Random = new Random();
            // Пока все комнаты не заспавнились, продолжаем цикл
            while (roomCounter < RoomNums)
            {
                pred_h = h;
                pred_w = w;
                // TODO: Убрать магические числа
                int vector = Random.Next(0, 4);
                switch (vector)
                {
                    case 0:
                        h += (int)Movements.UP;
                        break;
                    case 1:
                        h += (int)Movements.DOWN;
                        break;
                    case 2:
                        w += (int)Movements.LEFT;
                        break;
                    case 3:
                        w += (int)Movements.RIGHT;
                        break;
                }
                // Создание дефолтной комнатки
                Room room = new Room
                {
                    RoomType = RoomTypes.Empty
                };
                if ((w + 1 < Size) & (w - 1 >= 0) & (h - 1 >= 0) & (h + 1 < Size))
                {
                    if ((GetRoom(w, h).RoomType == RoomTypes.Out))
                    {
                        SetRoom(w, h, room);
                        RoomPool.Add(GetRoom(w, h));
                        roomCounter++;
                    }
                    else
                    {
                        // Если комната уже занята, возвращаемся назад
                        w = pred_w;
                        h = pred_h;
                    }
                }
                else
                {
                    // Если ушли до границы карты, то вернёмся на спавн
                    w = orig_w;
                    h = orig_h;
                }
            };
            FloorFill(Spawn);
        }

        // Добавление особых комнат на карту
        void FloorFill(Room spawn)
        {
            Room BossRoom = new Room
            {
                RoomType = RoomTypes.Boss,
            };
            Room Treasure = new Room
            {
                RoomType = RoomTypes.Treasure,
            };
            // Босс всегда находится с какого-то края карты
            List<Room> maxPool = new List<Room>
            {
                null,
                null,
                null,
                null
            };
            int max_w = 0;
            int max_h = 0;
            int min_w = 32;
            int min_h = 32;
            Random random = new Random();
            // Сначала смотрим координаты самых удалённых комнат
            // Затем выбираем одну из них
            foreach (Room room in RoomPool)
            {
                if (room.X > max_w) { max_w = room.X; maxPool[0] = room; continue; }
                if (room.X < min_w) { min_w = room.X; maxPool[1] = room; continue; }
                if (room.Y > max_h) { max_h = room.Y; maxPool[2] = room; continue; }
                if (room.Y < min_h) { min_h = room.Y; maxPool[3] = room; continue; }
            }
            // Отсортируем по удалению от спавна пузырьком
            // TODO: Пузырёк заменить. Неэффективно
            for (int i = 0; i < maxPool.Count; i++)
            {
                for (int j = 0; j < maxPool.Count - 1; j++)
                {
                    if (GetRange(maxPool[j], spawn) < GetRange(maxPool[j + 1], spawn))
                    {
                        (maxPool[j + 1], maxPool[j]) = (maxPool[j], maxPool[j + 1]);
                    }
                }
            }
            // Создаём нужные комнаты
            if (GetRoom(maxPool[0].X, maxPool[0].Y).RoomType == RoomTypes.Empty)
            {
                SetRoom(maxPool[0].X, maxPool[0].Y, BossRoom);
            }
            if (GetRoom(maxPool[1].X, maxPool[1].Y).RoomType == RoomTypes.Empty)
            {
                SetRoom(maxPool[1].X, maxPool[1].Y, Treasure);
            }
            // Теперь создадим боевые комнаты (идентификаторами пренебречь)
            Room BattleRoom = new Room
            {
                RoomType = RoomTypes.Fight,
            };
            Room PuzzleRoom = new Room
            {
                RoomType = RoomTypes.Puzzle,
            };
            foreach (Room room in RoomPool)
            {
                if (room.RoomType == RoomTypes.Empty)
                {
                    double chance = random.NextDouble();
                    if (chance <= 0.8) { continue; }
                    else if (chance <= 0.9) { SetRoom(room.X, room.Y, BattleRoom); }
                    else { SetRoom(room.X, room.Y, PuzzleRoom); }

                }
            }

        }

        double GetRange(Room loc1, Room loc2)
        {
            return Math.Sqrt(Math.Pow(loc1.X - loc2.X, 2) - Math.Pow(loc1.Y - loc2.Y, 2));
        }

        void SetRoom(int x, int y, Room room)
        {
            room.X = x;
            room.Y = y;
            map[(Size * y) + x] = room;
        }

        Room GetRoom(int x, int y)
        {
            return (Room)map[(Size * y) + x];
        }

        public void Rebuild()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "Floor\n{ Id: " + FloorId + "\n" + "Number: " + FloorNumber + "\n"
                + "Type: " + FloorType + "\n" + "Style: " + Style + "}\n";
        }
    }
}
