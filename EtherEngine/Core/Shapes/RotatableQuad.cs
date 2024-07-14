using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Core.Shapes
{
    public struct RotatableQuad : IShape
    {
        private float _x;
        private float _y;
        private float _width;
        private float _height;
        private float _rotation;
        private Vector2[] _vertices = new Vector2[4];
        private Vector2[] _normals;

        public float X
        {
            get => _x;
            set
            {
                _x = value;
                SetVertices();
            }
        }

        public float Y
        {
            get => _y;
            set
            {
                _y = value;
                SetVertices();
            }
        }
        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                SetVertices();
            }
        }
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                SetVertices();
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                SetVertices();
            }
        }

        public Vector2 Center
        {
            get => GetCenter();
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }


        public Vector2[] Normals { get { return _normals; } }

        public Vector2[] Vertices{ get { return _vertices; } }

        public Vector2 GetCenter()
        {
            return new Vector2(X, Y);
        }

        public RotatableQuad(float x, float y, float width, float height, float rotation = 0.0f)
        {
            _normals = new Vector2[2];
            _normals[0] = Vector2.Zero;
            _normals[1] = Vector2.Zero; //initialize the normals to avoid allocation.

            _x = x;
            _y = y;
            _width = width;
            _height = height;

            _rotation = rotation;

            SetVertices();
        }

        public RotatableQuad(in Vector2 center, float width, float height, float rotation = 0.0f) :
            this(center.X, center.Y, width, height, rotation)
        { }

        public RotatableQuad(in StaticQuad quad, float rotation = 0.0f) :
            this(quad.X, quad.Y, quad.Width, quad.Height, rotation)
        { }

        private void SetVertices()
        {
            float _sin = MathF.Sin(_rotation);
            float _cos = MathF.Cos(_rotation);

            _normals[0].X = _sin; //vector pointing up + rotation
            _normals[0].Y = -_cos;

            _normals[1].X = _cos; // vector pointing right + rotation
            _normals[1].Y = _sin;

            _vertices[0] = new Vector2(-Width / 2, -Height / 2);
            _vertices[1] = new Vector2(Width / 2, -Height / 2);
            _vertices[2] = new Vector2(Width / 2, Height / 2);
            _vertices[3] = new Vector2(-Width / 2, Height / 2);

            float transX;
            float transY;

            for (int i = 0; i < 4; i++)
            {
                transX = _vertices[i].X * _cos - _vertices[i].Y * _sin;
                transY = _vertices[i].X * _sin + _vertices[i].Y * _cos;
                _vertices[i].X = X + transX;
                _vertices[i].Y = Y + transY;
            }
        }

        public bool ContainsPoint(in Vector2 point)
        {

            Vector2 tempPoint = point - GetCenter();

            float pointProjection = MathF.Abs(Vector2.Dot(tempPoint, _normals[0]));
            if (Width / 2 < pointProjection)
                return false;

            pointProjection = MathF.Abs(Vector2.Dot(tempPoint, _normals[1]));
            if (Height / 2 < pointProjection)
                return false;

            return true;
        }

        public bool Equals(in RotatableQuad quad)
        {
            return X == quad.X &&
                Y == quad.Y &&
                Width == quad.Width &&
                Height == quad.Height &&
                Rotation == quad.Rotation;
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
