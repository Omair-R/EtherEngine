using Microsoft.Xna.Framework;

namespace EtherEngine.Shapes
{
    public interface IShape
    {
        public bool ContainsPoint(Vector2 point);

        public Vector2 GetCenter();
    }
}
