using EtherEngine.Components;
using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Shapes;
using EtherUtils.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Collision
{


    public class CollisionHelper
    {
        protected CollisionHelper()
        {

        }

        public bool CheckCollsion(ColliderShapeComponent currentShapeComponent, ColliderShapeComponent otherShapeComponent, out Contact contact)
        {

            switch (otherShapeComponent.Shape) // Change to expression?
            {
                case Circle collider:
                    return CheckCircleCollision(currentShapeComponent.Shape, collider, out contact);

                case RotatableQuad collider:
                    return CheckRotatableQuadCollision(currentShapeComponent.Shape, collider, out contact);

                case StaticQuad collider:
                    return CheckStaticQuadCollision(currentShapeComponent.Shape, collider, out contact);

                case Polygon collider:
                    return CheckPolygonCollision(currentShapeComponent.Shape, collider, out contact);

                default:
                    throw new NotSupportedException();
            }

        }

        public virtual bool CheckCircleCollision(IShape current, Circle other, out Contact contact)
        {
            contact = null;
            return false;
        }
        public virtual bool CheckStaticQuadCollision(IShape current, StaticQuad other, out Contact contact)
        {
            contact = null;
            return false;
        }
        public virtual bool CheckRotatableQuadCollision(IShape current, RotatableQuad other, out Contact contact)
        {
            contact = null;
            return false;
        }
        public virtual bool CheckPolygonCollision(IShape current, Polygon other, out Contact contact)
        {
            contact = null;
            return false;
        }

    }
}
