using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationGen
{
    // Перечень Типов Комнат
    public enum RoomTypes
    {
        // Комната спавна
        Spawn,
        // Сокровищница
        Treasure,
        // Магазин
        Shop,
        // Проходная комната
        Empty,
        // Комната с противниками
        Fight,
        // Комната с головоломкой
        Puzzle,
        // Комната-выход для багнутой генерации
        Error,
        // Комната обмена
        Deal,
        // Отсутствие комнаты
        Out,
        // Комната босса
        Boss
    }
}
