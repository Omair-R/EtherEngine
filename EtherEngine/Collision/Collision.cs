using EtherEngine.Collision.Models;
using EtherEngine.Entity;
using EtherEngine.Shapes;
using EtherEngine.Utils;
using System;


namespace EtherEngine.Collision
{
    public abstract class Collision<Shape> : ICollision where Shape : IShape
    {
        public Shape InnerShape { get; set; }

        public event EventHandler<CollisionEventArgs> CollisionOccured;

        protected CollisionTypes _type;
        public CollisionTypes Type { 
            get { 
                return _type; 
            } 
        }
        public CollisionLayer Layer { get; set; }
        public IEntity ParentEntity { get; set; }
        public bool Enable { get; set; }

        public void OnIntersectionHappened(CollisionEventArgs e) => EventUtils.Invoke(CollisionOccured, this, e);

        public Collision(CollisionLayer layer)
        {
            Layer = layer;
        }

        public bool CheckCollision(ICollision other, out Contact contact)
        {

            if (Layer != null && !Layer.ShouldCollide(other.Layer))
            {
                contact = null;
                return false;
            }

            bool collisionHappened = false;

            switch (other.Type)
            {
                case CollisionTypes.Circle:
                    collisionHappened = CheckCircleCollision(other, out contact);
                    break;
                case CollisionTypes.StaticQuad:
                    collisionHappened = CheckStaticQuadCollision(other, out contact);
                    break;
                case CollisionTypes.RotatableQuad:
                    collisionHappened = CheckRotatableQuadCollision(other, out contact);
                    break;
                case CollisionTypes.Polygon:
                    collisionHappened = CheckPolygonCollision(other, out contact);
                    break;
                default:
                    throw new NotSupportedException();
            }

            if (collisionHappened)
            {
                OnIntersectionHappened(new CollisionEventArgs()
                {
                    collidingEntity = other.ParentEntity,
                    partentEntity = this.ParentEntity,
                    contact = contact,
                    layer = this.Layer,
                });
            }

            return collisionHappened;
        }
        abstract protected bool CheckCircleCollision(ICollision other, out Contact contact);
        abstract protected bool CheckStaticQuadCollision(ICollision other, out Contact contact);
        abstract protected bool CheckRotatableQuadCollision(ICollision other, out Contact contact);
        abstract protected bool CheckPolygonCollision(ICollision other, out Contact contact);


    }
}
