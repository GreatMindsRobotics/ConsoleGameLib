using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameLib.CoreTypes
{
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }
}
