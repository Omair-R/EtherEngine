using Microsoft.Xna.Framework;

namespace EtherEngine.Core.Shapes
{
    public interface IShape
    {
        public bool ContainsPoint(in Vector2 point);

        public Vector2 GetCenter();

        public void MoveCenter(in Vector2 target);
    }
}
