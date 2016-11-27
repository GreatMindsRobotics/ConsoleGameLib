using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameLib.CoreTypes
{
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;

        public override int GetHashCode()
        {
            // Uses http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked // Overflow is fine, just wrap
            {
                int hash = 37;
                hash = hash * 67 + X.GetHashCode();
                hash = hash * 67 + Y.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Point))
            {
                return false;
            }
            Point oth = (Point)obj;
            return X == oth.X && Y == oth.Y;
        }

        public static bool operator ==(Point left, Point right)
        {
            // Struct, so no need for null check
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Point left, Point right)
        {
            // Struct, so no need for null check
            return left.X != right.X || left.Y != right.Y;
        }

        public static Point operator +(Point left, Point right)
        {
            return new Point(left.X + right.X, left.Y + right.Y);
        }

        public static Point operator -(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public static Point operator -(Point unary)
        {
            return new Point(-unary.X, -unary.Y);
        }

        public static Point operator *(Point point, int scalar)
        {
            return new Point(point.X * scalar, point.Y * scalar);
        }

        public static Point operator /(Point point, int scalar)
        {
            return new Point(point.X / scalar, point.Y / scalar);
        }
    }
}
