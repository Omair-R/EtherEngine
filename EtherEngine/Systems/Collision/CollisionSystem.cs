using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Shapes;
using EtherEngine.Entities;
using EtherUtils;
using Microsoft.Xna.Framework;
using System;

namespace EtherEngine.Systems.Collision
{

    public class CollisionSystem : UpdatableSystem
    {
        private QueryDescription queryDescription = new QueryDescription().WithAll<TransformComponent, ColliderComponent, ColliderShapeComponent>();


        public static event EventHandler<CollisionEventArgs> CollisionOccured;


        public void OnCollisionOccured(CollisionEventArgs e) => EventUtils.Invoke(CollisionOccured, this, e);

        public CollisionSystem(EtherScene scene) : base(scene)
        {
        }


        public override void Update(in GameTime gameTime)
        {

            bool collisionHappened;

            ColliderComponent currentCollider;
            ColliderShapeComponent currentColliderShape;
            Entity currentEntity;

            ICollisionHelper collisionHelper;

            _scene._world.Query(in queryDescription, (in Entity enitity, ref TransformComponent transform,  ref ColliderComponent collider, ref ColliderShapeComponent colliderShape) =>
            {
                colliderShape.Shape.MoveCenter(transform.Position);

                collisionHelper = GetCollisionHelper(colliderShape);

                currentCollider = collider;
                currentColliderShape = colliderShape;
                currentEntity = enitity;

                _scene._world.Query(in queryDescription, (in Entity otherEnitity, ref TransformComponent otherTransform, ref ColliderComponent otherCollider, ref ColliderShapeComponent otherShape) =>
                {
                    if (currentEntity == otherEnitity) return;

                    if (currentCollider.Layer != null && !currentCollider.Layer.ShouldCollide(otherCollider.Layer)) return;

                    collisionHappened = collisionHelper.CheckCollsion(currentColliderShape, otherShape, out Contact contact);

                    if (collisionHappened)
                    {
                        OnCollisionOccured(new CollisionEventArgs()
                        {
                            collidingEntity = EtherEntity.Wrap(_scene, otherEnitity),
                            partentEntity = EtherEntity.Wrap(_scene, currentEntity),
                            contact = contact,
                            layer = currentCollider.Layer,
                        });

                        currentColliderShape.Shape.MoveCenter(currentColliderShape.Shape.GetCenter() - (contact.penetration * contact.collisionDirection)) ;
                        
                    }
                });

                transform.Position = currentColliderShape.Shape.GetCenter();

                colliderShape.Shape = currentColliderShape.Shape;
            });
        }


        public ICollisionHelper GetCollisionHelper(in ColliderShapeComponent ShapeComponent) => ShapeComponent.Shape switch // Change to expression?
            {
                Circle => CircleCollisionHelper.Instance,

                RotatableQuad => RotatableQuadCollisionHelper.Instance,

                StaticQuad => StaticQuadCollisionHelper.Instance,

                Polygon => PolygonCollisionHelper.Instance,

                _ => throw new NotSupportedException(),
            };

    }
}
