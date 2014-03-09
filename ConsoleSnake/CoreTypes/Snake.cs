using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;
using ConsoleGameLib.Helpers;

namespace ConsoleSnake.CoreTypes
{
    class Snake : ConsoleSpriteCollection<SnakePiece>
    {
        private ConsoleColor? _foregroundColor;
        public ConsoleColor? ForegroundColor
        {
            get { return _foregroundColor; }
            set { _foregroundColor = value; }
        }

        private ConsoleColor? _backgroundColor;
        public ConsoleColor? BackgroundColor
        {
            get { return _backgroundColor; }
            set { _backgroundColor = value; }
        }


        public Snake(Point location, Direction initialDirection, ConsoleColor headForegroundColor, ConsoleColor headBackgroundColor, ConsoleColor bodyFogregroundColor, ConsoleColor bodyBackgroundColor)
        {
            //Create snake head
            SnakePiece head = new SnakePiece(location, headForegroundColor, headBackgroundColor);
            head.CurrentDirection = initialDirection;

            Add(head);
        }

        public Snake() : this(new Point(10, 10), Direction.Right)
        {
            //Pass-through ctor
        }

        public Snake(Point location, Direction initialDirection) : this(location, initialDirection, ConsoleColor.Red, ConsoleColor.Red, ConsoleColor.White, ConsoleColor.White)
        {
            //Pass-through ctor
        }

        public Snake(SnakePiece head)
        {
            Add(head);
        }

        public Direction CurrentDirection
        {
            get { return this[0].CurrentDirection; }
            set { this[0].CurrentDirection = value; }
        }

        public bool Intersects(ConsoleSprite sprite)
        {
            return this[0].Intersects(sprite);
        }


        /// <summary>
        /// Grows the snake by a specified number of pieces.
        /// </summary>
        /// <param name="numberOfPieces">Length, in SnakePieces to grow by. Default is 1 piece.</param>
        public void Grow(int numberOfPieces = 1)
        {
            for (int i = 0; i < numberOfPieces; i++)
            {
                SnakePiece tail = this[Count - 1];
                SnakePiece newPiece = new SnakePiece(tail.DrawChars, tail.Location, _foregroundColor, _backgroundColor);
                newPiece.CurrentDirection = tail.CurrentDirection;
                newPiece.BecomeTail();

                Add(newPiece);
            }
        }

        public override void Update()
        {
            //Important: Update before switching direction!
            base.Update();

            for (int i = this.Count - 1; i > 0; i--)
            {
                this[i].CurrentDirection = this[i - 1].CurrentDirection;
            }
        }

        public override void Draw()
        {
            if (_foregroundColor.HasValue)
            {
                Console.ForegroundColor = _foregroundColor.Value;
                Console.BackgroundColor = _foregroundColor.Value;
            }

            base.Draw();
        }
    }
}
