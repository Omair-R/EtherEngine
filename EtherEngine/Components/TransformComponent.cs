using Microsoft.Xna.Framework;

namespace EtherEngine.Components
{
    public struct TransformComponent
    {
        Vector2 Position;
        Vector2 Scale;
        float Rotation;

        public Matrix GetTransform()
        {
            return Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 1));
        }
    }
}
