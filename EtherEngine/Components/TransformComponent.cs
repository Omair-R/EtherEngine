using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace EtherEngine.Components
{
    public struct TransformComponent
    {
        public Vector2 Position = Vector2.Zero;
        public Vector2 Scale = Vector2.One;
        public float Rotation = 0.0f;

        public TransformComponent()
        {
        }
    }

    public static class TransformHelper
    {
        public static Matrix GetTransform(in TransformComponent transform)
        {
            return Matrix.CreateTranslation(new Vector3(transform.Position.X, transform.Position.Y, 0)) *
                    Matrix.CreateRotationZ(transform.Rotation) *
                    Matrix.CreateScale(new Vector3(transform.Scale.X, transform.Scale.Y, 1));
        }
    }
}
