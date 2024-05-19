using Microsoft.Xna.Framework;

namespace EtherEngine.Shapes
{
    public class Circle: IShape
    {
        public float Radius { get; set; }
        public Vector2 Center { get; set; }

        public Circle(Vector2 center, float radius) 
        {
            this.Center = center;
            this.Radius = radius;
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

        public override bool Equals(object obj)
        {
            Circle other = obj as Circle;
            if (other == null) return false;
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return new { this.Center, this.Radius }.GetHashCode();
        }

        public override string ToString() 
        {
            return $"Circle:C={this.Center} R={this.Radius}";
        }

        public Vector2 GetCenter()
        {
            return this.Center;
        }
    }
}
