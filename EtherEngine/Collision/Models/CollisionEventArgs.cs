using EtherEngine.Entity;
using System;

namespace EtherEngine.Collision.Models
{
    public class CollisionEventArgs : EventArgs
    {
        public CollisionLayer layer;
        public Contact contact;
        public IEntity partentEntity;
        public IEntity collidingEntity;
    }

}
