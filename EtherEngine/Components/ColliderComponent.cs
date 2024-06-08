using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Shapes;

namespace EtherEngine.Components
{

    public struct CollisionGizmoComponent
    {
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
