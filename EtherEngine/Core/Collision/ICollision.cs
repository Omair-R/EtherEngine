using EtherEngine.Core.Collision.Models;
using EtherEngine.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Core.Collision
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
        public EtherEntity ParentEntity { get; set; }
        public bool Enable { get; set; }
        bool CheckCollision(ICollision other, out Contact contact);
    }
}
