using Microsoft.Xna.Framework;
using System;

namespace EtherEngine.Shapes
{
    public class StaticQuad :IShape
    {
        protected Vector2[] vertices = new Vector2[4];
        protected float x;
        protected float y;
        protected float width;
        protected float height;

        public Vector2[] Vertices
        {
            get { return vertices; }

        }

        public float X { 
            get => x; 
            set 
            { 
                x = value;
                SetVertices();
            } 
        }

        public float Y { 
            get => y; 
            set
            {
                y = value;
                SetVertices();
            }
        }
        public float Width { 
            get => width;
            set
            {
                width = value;
                SetVertices();
            }
        }
        public float Height { 
            get => height;
            set
            {
                height = value;
                SetVertices();
            }
        }

        public Vector2 Center
        {
            get => GetCenter();
            set {
                X = value.X;
                Y = value.Y;
            }
        }


        public StaticQuad(float x, float y, float width, float height)
        {

            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public StaticQuad(Vector2 center, float width, float height)
        {
            this.X = center.X;
            this.Y = center.Y;
            this.Width = width;
            this.Height = height;
        }

        public StaticQuad(Vector2 min, Vector2 max)
        {
            this.Width = max.X - min.X;
            this.Height = max.Y - min.Y;
            this.X = min.X + this.Width / 2;
            this.Y = min.Y + this.Height / 2;
        }


        public Vector2 GetMin()
        {
            return new Vector2(X - this.Width / 2, Y - this.Height / 2);
        }

        public Vector2 GetMax()
        {
            return new Vector2(X + this.Width / 2, Y + this.Height / 2);
        }

        public Vector2 GetCenter()
        {
            return new Vector2(X, Y);
        }

        protected void SetVertices()
        {
            
            vertices[0] = new Vector2(X - Width / 2, Y - Height / 2);
            vertices[1] = new Vector2(X + Width / 2, Y - Height / 2);
            vertices[2] = new Vector2(X + Width / 2, Y + Height / 2);
            vertices[3] = new Vector2(X - Width / 2, Y + Height / 2);
        }

        public bool ContainsPoint(Vector2 point)
        {
            var min = GetMin();
            var max = GetMax();

            return point.X >=min.X && 
                point.X <=max.X && 
                point.Y >=min.Y && 
                point.Y <=max.Y;
        }

        public bool Equals(StaticQuad quad)
        {
            return X == quad.X &&
                Y == quad.Y &&
                Width == quad.Width && 
                Height == quad.Height;
        }

        public override bool Equals(object obj)
        {
            StaticQuad quad = obj as StaticQuad;
            if (quad == null) return false;
            return this.Equals(quad);
        }

        public override int GetHashCode()
        {
            return new { X, Y, Width, Height }.GetHashCode();
        }

        public override string ToString()
        {
            return $"Quad:X={X}, Y={Y}, Width={Width}, Height={Height}";
        }
    }
}
