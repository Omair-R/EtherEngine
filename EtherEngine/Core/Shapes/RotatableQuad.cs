using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Core.Shapes
{
    public class RotatableQuad : StaticQuad
    {
        private float _rotation;
        public float Rotation
        {
            get { return _rotation; }
            set
            {
                _rotation = value;
                _sin = MathF.Sin(_rotation);
                _cos = MathF.Cos(_rotation);

                _normals[0].X = _sin; //vector pointing up + rotation
                _normals[0].Y = -_cos;

                _normals[1].X = _cos; // vector pointing right + rotation
                _normals[1].Y = _sin;

                SetVertices();
            }
        }


        private float _sin;
        private float _cos;
        private Vector2[] _normals;

        public Vector2[] Normals { get { return _normals; } }

        private Vector2[] _unmovedVertices;

        public RotatableQuad(float x, float y, float width, float height, float rotation = 0.0f) :
            base(x, y, width, height)
        {
            _normals = new Vector2[2];
            _normals[0] = Vector2.Zero;
            _normals[1] = Vector2.Zero; //initialize the normals to avoid allocation.

            _unmovedVertices = new Vector2[4] {
                    new Vector2(-width / 2, -height / 2),
                    new Vector2(width / 2, -height / 2),
                    new Vector2(width / 2, height / 2),
                    new Vector2(-width / 2, height / 2),
            }; // This is to avoid allocation every time the quad moves.

            Rotation = rotation;
        }

        public RotatableQuad(Vector2 center, float width, float height, float rotation = 0.0f) :
            this(center.X, center.Y, width, height, rotation)
        { }

        public RotatableQuad(StaticQuad quad, float rotation = 0.0f) :
            this(quad.X, quad.Y, quad.Width, quad.Height, rotation)
        { }

        public new Vector2 GetMin()
        {
            throw new NotSupportedException();
        }

        public new Vector2 GetMax()
        {
            throw new NotSupportedException();
        }

        protected new void SetVertices()
        {
            //TODO: Optimize this.
            _vertices[0] = _unmovedVertices[0];
            _vertices[1] = _unmovedVertices[1];
            _vertices[2] = _unmovedVertices[2];
            _vertices[3] = _unmovedVertices[3];

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

        public new bool ContainsPoint(Vector2 point)
        {

            point -= GetCenter();

            float pointProjection = MathF.Abs(Vector2.Dot(point, _normals[0]));
            if (Width / 2 < pointProjection)
                return false;

            pointProjection = MathF.Abs(Vector2.Dot(point, _normals[1]));
            if (Height / 2 < pointProjection)
                return false;

            return true;
        }


    }
}
