using EtherEngine.Collision.Models;
using EtherEngine.Collision.Utils;
using EtherEngine.Shapes;
using System;


namespace EtherEngine.Collision
{
    public class RotatableQuadCollision : Collision<RotatableQuad>
    {

        public RotatableQuadCollision(RotatableQuad quad, CollisionLayer layer = null) : base(layer)
        {
            this._type = CollisionTypes.RotatableQuad;
            this.InnerShape = quad;
        }
        protected override bool CheckCircleCollision(ICollision other, out Contact contact)
        {
            if (other is CircleCollision otherCircle)
                return CollisionUtils.CircleOnRotatableQuadCollision(otherCircle.InnerShape, InnerShape, out contact, true);
            else throw new ArgumentException();
        }

        protected override bool CheckStaticQuadCollision(ICollision other, out Contact contact)
        {

            if (other is StaticQuadCollision otherQuad)
                return CollisionUtils.StaticQuadOnRotatableQuadCollision(otherQuad.InnerShape, InnerShape, out contact, true);
            else throw new ArgumentException();
        }

        protected override bool CheckRotatableQuadCollision(ICollision other, out Contact contact)
        {
            if (other is RotatableQuadCollision otherQuad)
                return CollisionUtils.RotatableQuadOnRotatableQuadCollision(InnerShape, otherQuad.InnerShape, out contact);
            else throw new ArgumentException();
        }

        protected override bool CheckPolygonCollision(ICollision other, out Contact contact)
        {
            if (other is PolygonCollision otherPolygon)
                return CollisionUtils.RotatableQuadOnPolygonCollision(InnerShape, otherPolygon.InnerShape, out contact);
            else throw new ArgumentException();
        }
    }
}
