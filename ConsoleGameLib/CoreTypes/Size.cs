using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameLib.CoreTypes
{
    public struct Size
    {
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width;
        public int Height;
    }
}
