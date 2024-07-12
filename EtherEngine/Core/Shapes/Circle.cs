using Microsoft.Xna.Framework;

namespace EtherEngine.Core.Shapes
{
    public struct Circle : IShape
    {
        public float Radius { get; set; }
        public Vector2 Center { get; set; }

        public Circle(Vector2 center, float radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool ContainsPoint(Vector2 point)
        {
            var distance = Vector2.Distance(Center, point);
            return distance < Radius;
        }

        public bool Equals(Circle other)
        {
            return Center == other.Center && Radius == other.Radius;
        }

        //public override bool Equals(object obj)
        //{
        //    Circle other = obj as Circle;
        //    if (other == null) return false;
        //    return Equals(other);
        //}

        public override int GetHashCode()
        {
            return new { Center, Radius }.GetHashCode();
        }

        public override string ToString()
        {
            return $"Circle:C={Center} R={Radius}";
        }

        public Vector2 GetCenter()
        {
            return Center;
        }

        public void MoveCenter(Vector2 target)
        {
            Center = target;
        }
    }
}
