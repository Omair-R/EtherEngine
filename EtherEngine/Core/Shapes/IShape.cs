using Microsoft.Xna.Framework;

namespace EtherEngine.Core.Shapes
{
    public interface IShape
    {
        public bool ContainsPoint(Vector2 point);

        public Vector2 GetCenter();

        public void MoveCenter(Vector2 target);
    }
}
