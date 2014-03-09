using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleGameLib.CoreTypes;
using ConsolePong.CoreTypes;


namespace ConsolePong
{
    public class PongGame : Game
    {
        PongBall ball = new PongBall();

        public override void InitGame()
        {
            base.InitGame();

            _targetFrameRate = 1000.0f / 15.0f;     //15 FPS;
        }

        public override void Update()
        {
            base.Update();

            ball.Update();
        }

        public override void Draw()
        {
            base.Draw();

            ball.Draw();
        }
    }
}
