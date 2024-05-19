using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Shapes
{
    public class RotatableQuad : StaticQuad
    {
        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                sin = MathF.Sin(rotation);
                cos = MathF.Cos(rotation);

                normals[0].X = sin; //vector pointing up + rotation
                normals[0].Y = -cos;

                normals[1].X = cos; // vector pointing right + rotation
                normals[1].Y = sin;

                SetVertices();
            }
        }


        private float sin;
        private float cos;
        private Vector2[] normals;

        public Vector2[] Normals { get { return normals; } }

        public RotatableQuad(float x, float y, float width, float height, float rotation=0.0f) :
            base(x, y, width, height)
        {
            normals = new Vector2[2];
            normals[0] = Vector2.Zero;
            normals[1] = Vector2.Zero; //initialize the normals to avoid allocation.
            this.Rotation = rotation;
        }

        public RotatableQuad(Vector2 center, float width, float height, float rotation = 0.0f) : 
            this(center.X, center.Y, width, height, rotation){}

        public  new Vector2 GetMin()
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
            vertices[0] = new Vector2(-Width / 2, -Height / 2);
            vertices[1] = new Vector2(Width / 2, -Height / 2);
            vertices[2] = new Vector2(Width / 2, Height / 2);
            vertices[3] = new Vector2(-Width / 2, Height / 2);

            for (int i = 0; i < 4; i++)
            {
                float transX = vertices[i].X * cos - vertices[i].Y * sin;
                float transY = vertices[i].X * sin + vertices[i].Y * cos;
                vertices[i].X = X + transX;
                vertices[i].Y = Y + transY;
            }
        }

        public new bool ContainsPoint(Vector2 point)
        {
           
            point -= GetCenter();

            float pointProjection = MathF.Abs(Vector2.Dot(point, normals[0]));
            if (Width/2 < pointProjection)
                return false;

            pointProjection = MathF.Abs(Vector2.Dot(point, normals[1]));
            if (Height / 2 < pointProjection)
                return false;

            return true;
        }


    }
}
