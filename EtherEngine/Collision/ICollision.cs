using EtherEngine.Collision.Models;
using EtherEngine.Entity;
using EtherEngine.Shapes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Collision
{
    public enum CollisionTypes
    {
        StaticQuad,
        RotatableQuad,
        Circle,
        Polygon,
    }

    public interface ICollision
    {
        public event EventHandler<CollisionEventArgs> CollisionOccured;
        public CollisionTypes Type { get; }
        public CollisionLayer Layer { get; set; }
        public IEntity ParentEntity { get; set; }
        public bool Enable { get; set; }
        bool CheckCollision(ICollision other, out Contact contact);
    }
}
