using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EtherEngine.Core.Shapes
{
    public struct Polygon : IShape
    {
        private Vector2[] _vertices;
        public Vector2[] Vertices
        {
            get { return _vertices; }
            set
            {
                _vertices = value;
                _normals = GetNormals();
            }
        }

        private bool _preComputeNormals;

        private Vector2[] _normals;


        public Polygon(in Vector2[] vertices, bool preComputeNormals = true)
        {
            _vertices = vertices;
            _preComputeNormals = preComputeNormals;

            _normals = null;

            if (preComputeNormals)
                _normals = GetNormals();
        }


        public Polygon(List<Vector2> vertices, bool preComputeNormals = true) : this(vertices.ToArray(), preComputeNormals)
        {
        }

        public Vector2[] GetEdges()
        {
            Vector2[] edges = new Vector2[Vertices.Length];
            for (int i = 0; i < Vertices.Length; i++)
            {
                int j = (i + 1) % Vertices.Length;
                edges[i] = Vertices[i] - Vertices[j];
            }

            return edges;
        }

        public Vector2[] GetNormals(bool forceRecompute = false)
        {
            if (_preComputeNormals && !forceRecompute && _normals.Length !=0)
                return _normals;

            Vector2[] edges = GetEdges();
            Vector2[] normals = new Vector2[edges.Length];

            for (int i = 0; i < edges.Length; i++)
            {
                Vector2 normal = new Vector2(-edges[i].Y, edges[i].X);
                normals[i] = Vector2.Normalize(normal);
            }

            return normals;
        }

        public bool Equals(Polygon other)
        {
            return Vertices.SequenceEqual(other.Vertices);
        }

        //public override bool Equals(object obj)
        //{
        //    Polygon other = obj as Polygon;
        //    if (other == null) return false;
        //    return Equals(other);
        //}

        public override int GetHashCode()
        {
            return Vertices.GetHashCode();
        }

        public override string ToString()
        {
            string outString = "Polygon: ";

            for (int i = 0; i < Vertices.Length; i++)
            {
                outString += $"{Vertices[i]} ";
            }

            return outString;
        }

        public Vector2 GetCenter()
        {
            Vector2 center = Vector2.Zero;
            foreach (Vector2 vertex in Vertices)
            {
                center.X += vertex.X;
                center.Y += vertex.Y;
            }
            center.X /= Vertices.Length;
            center.Y /= Vertices.Length;
            return center;
        }

        public bool ContainsPoint(Vector2 point)
        {
            foreach (Vector2 normal in GetNormals())
            {
                Vector2 minMax = MinMaxProjection(this, normal);
                float pointProjection = Vector2.Dot(point, normal);
                if (minMax.X > pointProjection || minMax.Y < pointProjection)
                    return false;

            }

            return true;
        }

        public static Vector2 MinMaxProjection(Polygon poly, Vector2 axis)
        {
            float min = float.MaxValue;
            float max = float.MinValue;
            float project;

            foreach (Vector2 vertex in poly.Vertices)
            {
                project = Vector2.Dot(vertex, axis);
                max = Math.Max(max, project);
                min = Math.Min(min, project);
            }
            return new Vector2(min, max);
        }

        public static void BondingBox(Polygon poly, out Vector2 min, out Vector2 max)
        {
            min = new Vector2(float.MaxValue, float.MaxValue);
            max = new Vector2(float.MinValue, float.MinValue);
            float project;

            foreach (Vector2 vertex in poly.Vertices)
            {
                project = Vector2.Dot(vertex, Vector2.UnitX);
                max.X = Math.Max(max.X, project);
                min.X = Math.Min(min.X, project);

                project = Vector2.Dot(vertex, Vector2.UnitY);
                max.Y = Math.Max(max.Y, project);
                min.Y = Math.Min(min.Y, project);
            }

        }

        public void MoveCenter(Vector2 target)
        {
            throw new NotImplementedException();
        }
    }
}
