using ConsoleGameLib.CoreTypes;
using ConsoleGameLib.Helpers;
using ConsoleGameLib.PhysicsTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGameLib
{
    public class PhysicsPoint
    {
        public bool ObeysGravity = false;

        public Point Position;

        public Point Velocity = new Point(0,0);

        public bool InteractsWithEnvironment = true;

        public bool ObeysDrag = true;


        public ConsoleColor DrawColor = ConsoleColor.Green;

        public PhysicsWorld World;

        


        public PhysicsPoint(bool gravity, Point pos, bool interactsWithEnvironment, ConsoleColor drawColor, PhysicsWorld world)
        {
            ObeysGravity = gravity;
            Position = pos;
            InteractsWithEnvironment = interactsWithEnvironment;
            DrawColor = drawColor;
            World = world;
        }



        public virtual void Update()
        {
            Point tempVel = Velocity;
            while (tempVel.X != 0 || tempVel.Y != 0)
            {
                if (tempVel.Y > 0)
                {
                    if (!World.Contents.ContainsPoint(new Point(Position.X, Position.Y + 1)) || !InteractsWithEnvironment)
                    {
                        Position.Y++;
                        tempVel.Y--;
                    }
                    else if (InteractsWithEnvironment)
                    {
                        Velocity.Y = 0;
                        tempVel.Y = 0;
                    }
                }
                else if (tempVel.Y < 0)
                {
                    if (World.Contents.ContainsPoint(new Point(Position.X, Position.Y - 1)) && InteractsWithEnvironment)
                    {
                        Velocity.Y = 0;
                        tempVel.Y = 0;

                    }
                    else
                    {
                        Position.Y--;
                        tempVel.Y++;
                    }
                }

                if (tempVel.X > 0)
                {
                    if (!World.Contents.ContainsPoint(new Point(Position.X + 1, Position.Y)) || !InteractsWithEnvironment)
                    {
                        Position.X++;
                        tempVel.X--;
                    }
                    else if (InteractsWithEnvironment)
                    {
                        Velocity.X = 0;
                        tempVel.X = 0;
                    }
                }
                else if (tempVel.X < 0)
                {
                    if (World.Contents.ContainsPoint(new Point(Position.X - 1, Position.Y)) && InteractsWithEnvironment)
                    {
                        Velocity.X = 0;
                        tempVel.X = 0;

                    }
                    else
                    {
                        Position.X--;
                        tempVel.X++;
                    }
                }
            }
            if(ObeysDrag && World.CurrentDragUpdateInCycle >= World.LinearDragCalculationInterval)
            {
                int velX = Velocity.X;
                if (velX != 0)
                {
                    Velocity.X -= Velocity.X > 0 ? World.LinearDrag : -1 * World.LinearDrag;
                }
                if(velX > 0 && Velocity.X < 0)
                {
                    Velocity.X = (int)MathHelper.ClampMin(0,Velocity.X);
                }
                else if(velX < 0 && Velocity.X > 0)
                {
                    Velocity.X = (int)MathHelper.ClampMax(0, Velocity.X);
                }

            }
            if (ObeysGravity && World.CurrentGravityUpdateInCycle >= World.GravityCalculationInterval)
            {
                Velocity.Y -= Velocity.Y > World.TerminalFallVelocity ? World.GravitationalAcceleration : 0;
                Velocity.Y = (int)MathHelper.ClampMin(World.TerminalFallVelocity,Velocity.Y);
            }
            if(World.Contents.ContainsPoint(new Point(Position.X,Position.Y - 1)) && InteractsWithEnvironment)
            {
                Velocity.Y = 0;
            }

        }

        public void Draw()
        {
            Console.ForegroundColor = DrawColor;
            if (Position.Y > 0 && Position.X < World.ScreenSize.Width)
            {
                Console.SetCursorPosition(Position.X, World.ScreenSize.Height - Position.Y);
                Console.Write('█');
            }
        }
    }
}
