using Microsoft.Xna.Framework;
using System;

namespace EtherUtils
{
    public static class MathUtils
    {
        public static float InverseLerp(float a, float b, float x)
        {
            return (x - a) / (b - a);
        }

        public static float Distance(Vector2 a, Vector2 b)
        {
            Vector2 distanceVec = a - b;
            return distanceVec.Length();
        }

        public static Vector2 Direction(Vector2 a, Vector2 b)
        {
            Vector2 distanceVec = a - b;
            return Normalize(distanceVec);
        }

        public static Vector2 Normalize(Vector2 value)
        {
            if (value == Vector2.Zero)
            {
                return value;
            }
            float num = 1f / MathF.Sqrt(value.X * value.X + value.Y * value.Y);
            value.X *= num;
            value.Y *= num;
            return value;
        }

        public static Vector2 Abs(Vector2 value)
        {
            return new Vector2(MathF.Abs(value.X), MathF.Abs(value.Y));
        }


    }
}
