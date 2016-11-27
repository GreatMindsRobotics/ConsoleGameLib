using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameLib.CoreTypes
{
    public struct Size
    {
        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width;
        public int Height;

        public override int GetHashCode()
        {
            // Uses http://stackoverflow.com/questions/263400/what-is-the-best-algorithm-for-an-overridden-system-object-gethashcode
            unchecked // Overflow is fine, just wrap
            {
                int hash = 29;
                hash = hash * 89 + Width.GetHashCode();
                hash = hash * 89 + Height.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Size))
            {
                return false;
            }
            Size oth = (Size)obj;
            return Width == oth.Width && Height == oth.Height;
        }

        public static bool operator ==(Size left, Size right)
        {
            // Struct, so no need for null check
            return left.Width == right.Width && left.Height == right.Height;
        }

        public static bool operator !=(Size left, Size right)
        {
            // Struct, so no need for null check
            return left.Width != right.Width || left.Height != right.Height;
        }

        public static Size operator +(Size left, Size right)
        {
            return new Size(left.Width + right.Width, left.Height + right.Height);
        }

        public static Size operator -(Size left, Size right)
        {
            return new Size(left.Width - right.Width, left.Height - right.Height);
        }

        public static Size operator *(Size point, int scalar)
        {
            return new Size(point.Width * scalar, point.Height * scalar);
        }

        public static Size operator /(Size point, int scalar)
        {
            return new Size(point.Width / scalar, point.Width / scalar);
        }
    }
}