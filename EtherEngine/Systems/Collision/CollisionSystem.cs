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
        private QueryDescription transformQueryDiscription = new QueryDescription().WithAll<TransformComponent, ColliderShapeComponent>();

        private QueryDescription queryDescription = new QueryDescription().WithAll<ColliderComponent, ColliderShapeComponent>();


        public static event EventHandler<CollisionEventArgs> CollisionOccured;

        public void HandlePhysicsCollision(object sender, CollisionEventArgs e)
        {
            ref TransformComponent pTransform = ref e.partentEntity.GetComponent<TransformComponent>();
            pTransform.Position -= e.contact.penetration * e.contact.collisionDirection;
        }

        public void OnCollisionOccured(CollisionEventArgs e) => EventUtils.Invoke(CollisionOccured, this, e);

        public CollisionSystem(EtherScene scene) : base(scene)
        {
            CollisionOccured += HandlePhysicsCollision;
        }


        public override void Update(in GameTime gameTime)
        {
            _scene._world.Query(in transformQueryDiscription, (ref TransformComponent transform, ref ColliderShapeComponent shape) =>
            {
                shape.Shape.MoveCenter(transform.Position);
            });

            bool collisionHappened;

            ColliderComponent currentCollider;
            ColliderShapeComponent currentColliderShape;
            Entity currentEntity;

            _scene._world.Query(in queryDescription, (in Entity enitity, ref ColliderComponent collider, ref ColliderShapeComponent colliderShape) =>
            {
                var collisionHelper = GetCollisionHelper(colliderShape);

                currentCollider = collider;
                currentColliderShape = colliderShape;
                currentEntity = enitity;

                _scene._world.Query(in queryDescription, (in Entity otherEnitity, ref ColliderComponent otherCollider, ref ColliderShapeComponent otherShape) =>
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
                    }
                });
            });
        }


        public CollisionHelper GetCollisionHelper(ColliderShapeComponent ShapeComponent)
        {
            switch (ShapeComponent.Shape) // Change to expression?
            {
                case Circle:
                    return CircleCollisionHelper.Instance;

                case RotatableQuad:
                    return RotatableQuadCollisionHelper.Instance;

                case StaticQuad:
                    return StaticQuadCollisionHelper.Instance;

                case Polygon:
                    return PolygonCollisionHelper.Instance;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
