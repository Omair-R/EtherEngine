using System;
using EtherEngine.Entities;

namespace EtherEngine.Core.Collision.Models
{
    public class CollisionEventArgs : EventArgs
    {
        public CollisionLayer layer;
        public Contact contact;
        public EtherEntity partentEntity;
        public EtherEntity collidingEntity;
    }

}
