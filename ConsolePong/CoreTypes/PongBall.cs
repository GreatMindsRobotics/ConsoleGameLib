using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;

namespace ConsolePong.CoreTypes
{
    public class PongBall : ConsoleSprite
    {
        private Point _speed = new Point(2, 1);

        public PongBall()
            : base("", new Point(5, 5))
        {
            /*DrawChars = @"
  ▄█▄
▄█▀ ▀█▄
▀█▄ ▄█▀
  ▀█▀";*/

            DrawChars = @"
▄▀▄
 ▀";
        }

        public override void Update()
        {
            base.Update();

            _location.X += _speed.X;
            if (_location.X < 0 || _location.X + _size.Width > Console.BufferWidth - 1)
            {
                _speed.X *= -1;
            }

            _location.Y += _speed.Y;
            if (_location.Y < 0 || _location.Y + _size.Height > Console.BufferHeight)
            {
                _speed.Y *= -1;
            }
        }

    }
}
