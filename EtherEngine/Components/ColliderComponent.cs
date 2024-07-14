using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Shapes;
using Microsoft.Xna.Framework;

namespace EtherEngine.Components
{
    public enum CollisionType
    {
        Static,
        Dynamic, 
        Trigger,
    }

    public struct CollisionGizmoComponent
    {
        public Color Color;
        public float Alpha;
    }

    public struct ColliderShapeComponent
    {
        public IShape Shape;
    }


    public struct ColliderComponent
    {
        public CollisionType CollisionType;
        public CollisionLayer Layer;
        public bool Enable;

        public ColliderComponent(CollisionLayer layer, CollisionType type)
        {
            Layer = layer;
            Enable = true;
            CollisionType = type;
        }
    }

}
