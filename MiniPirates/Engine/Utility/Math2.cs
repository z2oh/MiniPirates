using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniPirates.Engine.Utility
{
    public static class Math2
    {

        public static Vector2 RotateVector(Vector2 vec, float angle)
        {
            float sin = (float)Math.Sin(angle);
            float cos = (float)Math.Cos(angle);

            return new Vector2(vec.X * cos - vec.Y * sin, vec.X * sin + vec.Y * cos);
        }
    }
}
