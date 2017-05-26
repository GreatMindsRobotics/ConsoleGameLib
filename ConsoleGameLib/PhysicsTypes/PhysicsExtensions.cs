using ConsoleGameLib.CoreTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameLib.PhysicsTypes
{
    public static class PhysicsExtensions
    {
        public static bool ContainsPoint(this List<PhysicsPoint> points, Point point, bool mustInteractWithEnvironment = true)
        {
            foreach(PhysicsPoint entry in points)
            {
                if(entry.Position.X == point.X && entry.Position.Y == point.Y && (mustInteractWithEnvironment == true ? entry.InteractsWithEnvironment : true))
                {
                    return true;
                }
            }
            return false;
        }

        public static Point BottomLeft(this List<PhysicsPoint> points)
        {
            Point lowest = points[0].Position;

            foreach(PhysicsPoint point in points)
            {
                if(point.Position.X < lowest.X)
                {
                    lowest.X = point.Position.X;
                }
                if(point.Position.Y < lowest.Y)
                {
                    lowest.Y = point.Position.Y;
                }
            }
            return lowest;
        }

        public static Point TopRight(this List<PhysicsPoint> points)
        {
            Point highest = points[0].Position;

            foreach (PhysicsPoint point in points)
            {
                if (point.Position.X > highest.X)
                {
                    highest.X = point.Position.X;
                }
                if (point.Position.Y > highest.Y)
                {
                    highest.Y = point.Position.Y;
                }
            }
            return highest;
        }
    }
}
