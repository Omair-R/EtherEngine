using Microsoft.Xna.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace EtherEngine.Components
{
    public struct TransformComponent
    {
        public Vector2 Position;
        public Vector2 Scale;
        public float Rotation;
    }

    public static class TransformHelper
    {
        public static Matrix GetTransform(ref TransformComponent transform)
        {
            return Matrix.CreateTranslation(new Vector3(transform.Position.X, transform.Position.Y, 0)) *
                    Matrix.CreateRotationZ(transform.Rotation) *
                    Matrix.CreateScale(new Vector3(transform.Scale.X, transform.Scale.Y, 1));
        }
    }
}
