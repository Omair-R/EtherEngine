using System;

namespace EtherEngine.Collision.Models
{
    public class CollisionEventArgs : EventArgs
    {
        public CollisionLayer layer;
        public Contact contact;
        public EtherEntity partentEntity;
        public EtherEntity collidingEntity;
    }

}
