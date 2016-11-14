using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleGameLib;
using System.Text;
using ConsoleGameLib.CoreTypes;

namespace ConsoleGameLib.PhysicsTypes
{
    /// <summary>
    /// Work in progress. Will allow for many points treated as one.
    /// </summary>
    public class PhysicsObject
    {
        public List<PhysicsPoint> Contents = new List<PhysicsPoint>();

        public PhysicsWorld World;

        public Point Velocity;

        public bool ObeysGravity = false;

        /// <summary>
        /// True if physics should be calculated for the whole object. False if each point in the object should be calculated individually when it comes to physics.
        /// </summary>
        public bool Unified = true;


        public bool InteractsWithEnvironment = false;

        public PhysicsObject()
        {

        }

        public void Update()
        {
            if (Unified)
            {
                bool hitsFloor = false;
                bool hitsLeft = false;
                bool hitsRight = false;
                bool hitsTop = false;
                Point bottomLeft = Contents.BottomLeft();
                Point topRight = Contents.TopRight();
                foreach (PhysicsPoint point in Contents)
                {
                    if(InteractsWithEnvironment && point.Position.Y == bottomLeft.Y && World.Contents.ContainsPoint(new Point(point.Position.X,point.Position.Y - 1)))
                    {
                        hitsFloor = true;
                    }
                    if (InteractsWithEnvironment && point.Position.Y == topRight.Y && World.Contents.ContainsPoint(new Point(point.Position.X, point.Position.Y + 1)))
                    {
                        hitsTop = true;
                    }
                    if (InteractsWithEnvironment && point.Position.Y == bottomLeft.Y && World.Contents.ContainsPoint(new Point(point.Position.X - 1, point.Position.Y)))
                    {
                        hitsLeft = true;
                    }
                    if (InteractsWithEnvironment && point.Position.Y == topRight.Y && World.Contents.ContainsPoint(new Point(point.Position.X + 1, point.Position.Y)))
                    {
                        hitsRight = true;
                    }
                }


                foreach (PhysicsPoint point in Contents)
                {
                    if (hitsTop || hitsFloor)
                    {
                        point.Velocity.Y = 0;
                    }
                    if(hitsLeft || hitsRight)
                    {
                        point.Velocity.X = 0;
                    }
                }
                
            }
            else
            {
                foreach (PhysicsPoint point in Contents)
                {
                    point.Update();
                }
            }
        }

        public void Draw()
        {
            foreach(PhysicsPoint point in Contents)
            {
                point.Draw();
            }
        }


    }
}
