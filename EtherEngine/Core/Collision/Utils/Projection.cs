using EtherEngine.Utils;
using Microsoft.Xna.Framework;
using System;

namespace EtherEngine.Core.Collision.Utils
{
    public static class Projection
    {
        public static MinMax ProjectVertices(Vector2[] vertices, Vector2 axis)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            float project;

            foreach (Vector2 vertex in vertices)
            {
                project = Vector2.Dot(vertex, axis);
                max = Math.Max(max, project);
                min = Math.Min(min, project);
            }
            return new MinMax(min, max);
        }

        public static MinMax ProjectCircle(Vector2 center, float radius, Vector2 axis)
        {
            Vector2 pointedRadius = radius * axis;

            return new MinMax(Vector2.Dot(axis, center + pointedRadius),
                              Vector2.Dot(axis, center - pointedRadius));
        }
    }
}
