using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;

namespace ConsolePong
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.RunGame<PongGame>(ConsoleColor.Red, ConsoleColor.White);
        }
    }
}
