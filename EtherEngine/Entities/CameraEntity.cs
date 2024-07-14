using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static System.Formats.Asn1.AsnWriter;


namespace EtherEngine.Entities
{
    public class CameraEntity : EntityWrapper
    {
        public CameraEntity(EtherScene scene, Guid? id = null, string tag = null) : base(scene.EntityManager.MakeEntity(id, tag), scene)
        {
            _entity.AddComponent(new CameraComponent());
            _entity.AddComponent(new TransformComponent());
        }

        public static CameraEntity Wrap(EtherEntity entity, EtherScene scene)
        {
            if (entity.HasComponent<CameraComponent>() && entity.HasComponent<TransformComponent>())
            {
                return new CameraEntity(scene, entity.GetUid(), entity.GetTag());
            }

            throw new Exception("Not a camera");
        }


        private Matrix _GetTransform(in TransformComponent cameraTransform, Viewport viewport)
        {
            return Matrix.CreateTranslation(new Vector3(-cameraTransform.Position.X, -cameraTransform.Position.Y, 0)) *
                Matrix.CreateRotationZ(cameraTransform.Rotation) *
                Matrix.CreateScale(new Vector3(cameraTransform.Scale.X, cameraTransform.Scale.Y, 1)) *
                Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }

        public Matrix GetTransform()
        {
            ref var transform = ref _entity.GetComponent<TransformComponent>();

            return _GetTransform(transform, _scene.GraphicsDevice.Viewport);
        }

        public void Follow(EtherEntity entity)
        {
            _entity.AddComponent(new FollowComponent { 
                EntityUID = entity.GetUid(), 
                FollowType = FollowType.Instant,
            });
        }

        public void Follow(EtherEntity entity, in PIDDriveComponent pid)
        {
            _entity.AddComponent(new FollowComponent
            {
                EntityUID = entity.GetUid(),
                FollowType = FollowType.PID,
            });

            _entity.AddComponent(pid);
        }

        public void Unfollow()
        {
            if (_entity.HasComponent<FollowComponent>()) _entity.RemoveComponent<FollowComponent>();
            if (_entity.HasComponent<PIDDriveComponent>()) _entity.RemoveComponent<PIDDriveComponent>();
        }

    }
}
