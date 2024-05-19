using Microsoft.Xna.Framework;

namespace EtherEngine.Camera
{
    public interface ICamera
    {
        public void SetZoom(float zoom);
        public void SetRotationRadians(float radians);
        public void SetRotationDegrees(float degrees);
        public void Move(Vector2 extent);
        public void MoveTo(Vector2 target);
        public Matrix GetTransformation();
    }
}
