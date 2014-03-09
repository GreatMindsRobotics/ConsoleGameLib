using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;

namespace ConsoleSnake.CoreTypes
{
    public class SnakePiece : ConsoleSprite
    {
        public SnakePiece(string drawChars, Point location, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor) : base(drawChars, location, foregroundColor, backgroundColor)
        {
            //Main ctor; call to base
        }

        public SnakePiece(Point location, ConsoleColor? foregroundColor, ConsoleColor? backgroundColor) : this("██", location, foregroundColor, backgroundColor)
        {
            //Pass-through ctor; default drawString char is ASCII 219
        }

        public SnakePiece(Point location) : this(location, ConsoleColor.White, ConsoleColor.White)
        {
            //Pass-through ctor
        }

        public Direction CurrentDirection { get; set; }

        /// <summary>
        /// Snaps a new SnakePiece behind current tail (last piece of the snake), effectively making it the tail.
        /// </summary>
        public void BecomeTail()
        {
            switch (CurrentDirection)
            {
                case Direction.Up:
                    _location.Y += DrawChars.Length / 2;
                    break;

                case Direction.Down:
                    _location.Y -= DrawChars.Length / 2;
                    break;

                case Direction.Left:
                    _location.X += DrawChars.Length;
                    break;

                case Direction.Right:
                    _location.X -= DrawChars.Length;
                    break;
            }
        }

        public override void Update()
        {
            switch (CurrentDirection)
            {
                case Direction.Up:                
                    _location.Y -= DrawChars.Length / 2;
                    break;

                case Direction.Down:
                    _location.Y += DrawChars.Length / 2;
                    break;

                case Direction.Left:
                    _location.X -= DrawChars.Length;
                    break;

                case Direction.Right:
                    _location.X += DrawChars.Length;
                    break;
            }
        }
    }
}
