using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine.Entities
{
    public class CameraEntity : EntityWrapper
    {
        public static CameraEntity Create(EtherScene scene, Guid? id = null, string tag = null)
        {
            var cameraEntity = new CameraEntity(scene.entityManager.MakeEntity(id, tag), scene);
            cameraEntity.AddComponent(new CameraComponent());
            cameraEntity.AddComponent(new TransformComponent());

            return cameraEntity;
        }

        public static CameraEntity Wrap(EtherEntity entity, EtherScene scene)
        {
            if (entity.HasComponent<CameraComponent>() && entity.HasComponent<TransformComponent>())
            {
                return new CameraEntity(entity, scene);
            }

            throw new Exception("Not a camera");
        }

        internal CameraEntity(EtherEntity entity, EtherScene scene) : base(entity, scene)
        {
            
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
            ref var transform = ref GetComponent<TransformComponent>();

            return _GetTransform(transform, _scene._graphicsDevice.Viewport);
        }

        public void Follow(EtherEntity entity)
        {
            AddComponent(new FollowComponent { 
                EntityUID = entity.GetUid(), 
                FollowType = FollowType.Instant,
            });
        }

        public void Follow(EtherEntity entity, in PIDDriveComponent pid)
        {
            AddComponent(new FollowComponent
            {
                EntityUID = entity.GetUid(),
                FollowType = FollowType.PID,
            });

            AddComponent(pid);
        }

        public void Unfollow()
        {
            if (HasComponent<FollowComponent>()) RemoveComponent<FollowComponent>();
            if (HasComponent<PIDDriveComponent>()) RemoveComponent<PIDDriveComponent>();
        }

    }
}
