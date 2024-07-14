using Microsoft.Xna.Framework;
using System;

namespace EtherEngine.Core.Shapes
{
    public struct StaticQuad : IShape
    {
        public float X;

        public float Y;

        public float Width;

        public float Height;


        public Vector2 Center
        {
            get => GetCenter();
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2[] Vertices
        {
            get
            {
                Vector2[] _vertices = new Vector2[4];
                _vertices[0] = new Vector2(X - Width / 2, Y - Height / 2);
                _vertices[1] = new Vector2(X + Width / 2, Y - Height / 2);
                _vertices[2] = new Vector2(X + Width / 2, Y + Height / 2);
                _vertices[3] = new Vector2(X - Width / 2, Y + Height / 2);
                return _vertices;
            }

        }

        public StaticQuad(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public StaticQuad(in Vector2 center, float width, float height)
        {
            X = center.X;
            Y = center.Y;
            Width = width;
            Height = height;
        }

        public StaticQuad(in Vector2 min, in Vector2 max)
        {
            Width = max.X - min.X;
            Height = max.Y - min.Y;
            X = min.X + Width / 2;
            Y = min.Y + Height / 2;
        }


        public Vector2 GetMin()
        {
            return new Vector2(X - Width / 2, Y - Height / 2);
        }

        public Vector2 GetMax()
        {
            return new Vector2(X + Width / 2, Y + Height / 2);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(X, Y);
        }


        public bool ContainsPoint(in Vector2 point)
        {
            var min = GetMin();
            var max = GetMax();

            return point.X >= min.X &&
                point.X <= max.X &&
                point.Y >= min.Y &&
                point.Y <= max.Y;
        }

        public bool Equals(in StaticQuad quad)
        {
            return X == quad.X &&
                Y == quad.Y &&
                Width == quad.Width &&
                Height == quad.Height;
        }

        public override int GetHashCode()
        {
            return new { X, Y, Width, Height }.GetHashCode();
        }

        public override string ToString()
        {
            return $"Quad:X={X}, Y={Y}, Width={Width}, Height={Height}";
        }

        public void MoveCenter(in Vector2 target)
        {
            X = target.X;
            Y = target.Y;
        }
    }
}
