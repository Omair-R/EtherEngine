using EtherEngine.Components;
using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Shapes;
using EtherUtils.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Collision.Helpers
{

    public interface ICollisionHelper
    {
        public bool CheckCollsion(in ColliderShapeComponent currentShapeComponent, in ColliderShapeComponent otherShapeComponent, out Contact contact);
    }

    public class CollisionHelper<T> : ICollisionHelper where T : IShape
    {
        protected CollisionHelper()
        {

        }

        public bool CheckCollsion(in ColliderShapeComponent currentShapeComponent, in ColliderShapeComponent otherShapeComponent, out Contact contact)
        {

            switch (otherShapeComponent.Shape) // Change to expression?
            {
                case Circle collider:
                    return CheckCircleCollision((T)currentShapeComponent.Shape, collider, out contact);

                case RotatableQuad collider:
                    return CheckRotatableQuadCollision((T)currentShapeComponent.Shape, collider, out contact);

                case StaticQuad collider:
                    return CheckStaticQuadCollision((T)currentShapeComponent.Shape, collider, out contact);

                case Polygon collider:
                    return CheckPolygonCollision((T)currentShapeComponent.Shape, collider, out contact);

                default:
                    throw new NotSupportedException();
            }

        }

        public virtual bool CheckCircleCollision(in T current, in Circle other, out Contact contact)
        {
            contact = null;
            return false;
        }
        public virtual bool CheckStaticQuadCollision(in T current, in StaticQuad other, out Contact contact)
        {
            contact = null;
            return false;
        }
        public virtual bool CheckRotatableQuadCollision(in T current, in RotatableQuad other, out Contact contact)
        {
            contact = null;
            return false;
        }
        public virtual bool CheckPolygonCollision(in T current, in Polygon other, out Contact contact)
        {
            contact = null;
            return false;
        }

    }
}
