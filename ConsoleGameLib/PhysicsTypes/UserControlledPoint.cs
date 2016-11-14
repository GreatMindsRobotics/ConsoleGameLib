using ConsoleGameLib.CoreTypes;
using ConsoleGameLib.PhysicsTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameLib
{
    public class UserControlledPoint: PhysicsPoint
    {
        public event EventHandler OnLegalUp;
        public event EventHandler OnLegalLeft;
        public event EventHandler OnLegalRight;
        public event EventHandler OnLegalDown;


        public bool Active = true;
        public bool CanMoveUp = true;
        public bool CanMoveLeft = true;
        public bool CanMoveRight = true;
        public bool CanMoveDown = true;

        public int UpDistance = 1;
        public int LeftDistance = 1;
        public int DownDistance = 1;
        public int RightDistance = 1;

        public ConsoleKey MoveUpKey = ConsoleKey.W;
        public ConsoleKey MoveLeftKey = ConsoleKey.A;
        public ConsoleKey MoveRightKey = ConsoleKey.D;
        public ConsoleKey MoveDownKey = ConsoleKey.S;

        public UserControlledPoint(bool gravity, Point pos, bool interactsWithEnvironment, ConsoleColor drawColor, PhysicsWorld world)
            : base(gravity,pos,interactsWithEnvironment,drawColor,world)
        {
            
        }

        public override void Update()
        {
            if(Console.KeyAvailable)
            {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;

                if(CanMoveUp && pressedKey == MoveUpKey && (!World.Contents.ContainsPoint(new Point(Position.X,Position.Y+1)) || !InteractsWithEnvironment))
                {
                    Position.Y+= UpDistance;
                    OnLegalUp?.Invoke(this,EventArgs.Empty);
                }
                else if(CanMoveLeft && pressedKey == MoveLeftKey && (!World.Contents.ContainsPoint(new Point(Position.X - 1, Position.Y)) || !InteractsWithEnvironment))
                {
                    Position.X-= LeftDistance;
                    OnLegalLeft?.Invoke(this, EventArgs.Empty);
                }
                else if (CanMoveRight && pressedKey == MoveRightKey && (!World.Contents.ContainsPoint(new Point(Position.X + 1, Position.Y)) || !InteractsWithEnvironment))
                {
                    Position.X+=RightDistance;
                    OnLegalRight?.Invoke(this, EventArgs.Empty);
                }
                else if (CanMoveDown && pressedKey == MoveDownKey && (!World.Contents.ContainsPoint(new Point(Position.X, Position.Y - 1)) || !InteractsWithEnvironment))
                {
                    Position.Y-=DownDistance;
                    OnLegalDown?.Invoke(this, EventArgs.Empty);
                }
            }

            base.Update();
        }
    }
}
