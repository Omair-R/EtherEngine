using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EtherEngine.Components
{
    public class CameraComponent
    {
        public Matrix GetTransformMatrix(in TransformComponent transform, Viewport viewport)
        {
            return Matrix.CreateTranslation(new Vector3(-transform.Position.X, -transform.Position.Y, 0)) *
                Matrix.CreateRotationZ(transform.Rotation) *
                Matrix.CreateScale(new Vector3(transform.Scale.X, transform.Scale.Y, 1)) *
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }
    }

    public class FollowComponent
    {
        public Guid EntityUID;
    }

}
