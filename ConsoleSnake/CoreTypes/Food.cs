using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;

namespace ConsoleSnake.CoreTypes
{
    class Food : ConsoleSprite
    {
        public Food(string drawChars, Point location, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor) : base(drawChars, location, foregroundColor, backgroundColor)
        { 
            //Main ctor - call to base
        }

        public Food(Point location, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
            : this(new string((char)4, 1), location, foregroundColor, backgroundColor)
        {
            //Pass-through ctor
        }

        public Food(Point location) : this(location, ConsoleColor.Yellow, ConsoleColor.DarkBlue)
        {
            //Pass-through ctor
        }
    }
}
