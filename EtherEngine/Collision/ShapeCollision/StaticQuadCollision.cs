using EtherEngine.Collision.Models;
using EtherEngine.Collision.Utils;
using EtherEngine.Shapes;
using System;


namespace EtherEngine.Collision
{
    public class StaticQuadCollision : Collision<StaticQuad>
    {

        public StaticQuadCollision(StaticQuad quad, CollisionLayer layer = null) : base(layer)
        {
            this._type = CollisionTypes.StaticQuad;
            this.InnerShape = quad;
        }

        protected override bool CheckCircleCollision(ICollision other, out Contact contact)
        {
            if (other is CircleCollision otherCircle)
                return CollisionUtils.CircleOnStaticQuadCollision(otherCircle.InnerShape, InnerShape, out contact, true);
            else throw new ArgumentException();
        }

        protected override bool CheckStaticQuadCollision(ICollision other, out Contact contact)
        {

            if (other is StaticQuadCollision otherQuad)
                return CollisionUtils.StaticQuadOnStaticQuadCollision(InnerShape, otherQuad.InnerShape, out contact);
            else throw new ArgumentException();
        }

        protected override bool CheckRotatableQuadCollision(ICollision other, out Contact contact)
        {
            if (other is RotatableQuadCollision otherQuad)
                return CollisionUtils.StaticQuadOnRotatableQuadCollision(InnerShape, otherQuad.InnerShape, out contact);
            else throw new ArgumentException();
        }

        protected override bool CheckPolygonCollision(ICollision other, out Contact contact)
        {
            if (other is PolygonCollision otherPolygon)
                return CollisionUtils.StaticQuadOnPolygonCollision(InnerShape, otherPolygon.InnerShape, out contact);
            else throw new ArgumentException();
        }
    }
}
