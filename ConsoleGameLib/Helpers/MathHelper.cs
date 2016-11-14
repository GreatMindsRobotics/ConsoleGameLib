using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleGameLib.Helpers
{
    public static class MathHelper
    {
        public static float Clamp(float min, float max, float val)
        {
            if(val > max)
            {
                return max;
            }
            if(val < min)
            {
                return min;
            }
            return val;
        }
        public static float ClampMin(float min, float val)
        {
            if(val < min)
            {
                return min;
            }
            return val;
        }
        public static float ClampMax(float max, float val)
        {
            if(val > max)
            {
                return max;
            }
            return val;
        }
    }
}
