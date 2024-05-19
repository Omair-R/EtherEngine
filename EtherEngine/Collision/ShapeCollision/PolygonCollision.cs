using EtherEngine.Collision.Models;
using EtherEngine.Collision.Utils;
using EtherEngine.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Collision
{
    public class PolygonCollision : Collision<Polygon>
    {
        public PolygonCollision(Polygon polygon)
        {
            this.type = CollisionTypes.Polygon;
            this.InnerShape = polygon;
        }
        protected override bool CheckCircleCollision(ICollision other, out Contact contact)
        {
            if (other is CircleCollision otherCircle)
                return CollisionUtils.CircleOnPolygonCollision(otherCircle.InnerShape, InnerShape, out contact, true);
            else throw new ArgumentException();
        }

        protected override bool CheckStaticQuadCollision(ICollision other, out Contact contact)
        {

            if (other is StaticQuadCollision otherQuad)
                return CollisionUtils.StaticQuadOnPolygonCollision(otherQuad.InnerShape, InnerShape, out contact, true);
            else throw new ArgumentException();
        }

        protected override bool CheckRotatableQuadCollision(ICollision other, out Contact contact)
        {
            if (other is RotatableQuadCollision otherQuad)
                return CollisionUtils.RotatableQuadOnPolygonCollision(otherQuad.InnerShape, InnerShape, out contact, true);
            else throw new ArgumentException();
        }

        protected override bool CheckPolygonCollision(ICollision other, out Contact contact)
        {
            if (other is PolygonCollision otherPolygon)
                return CollisionUtils.PolygonOnPolygonCollision(InnerShape, otherPolygon.InnerShape, out contact);
            else throw new ArgumentException();
        }
    }
}
