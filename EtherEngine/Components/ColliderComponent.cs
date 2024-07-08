using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Shapes;
using Microsoft.Xna.Framework;

namespace EtherEngine.Components
{

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
        public CollisionLayer Layer;
        public bool Enable;

        public ColliderComponent(CollisionLayer layer)
        {
            Layer = layer;
            Enable = true;
        }
    }

}
