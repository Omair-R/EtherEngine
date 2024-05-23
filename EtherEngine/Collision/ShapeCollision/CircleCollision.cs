using EtherEngine.Collision.Models;
using EtherEngine.Collision.Utils;
using EtherEngine.Shapes;
using Microsoft.Xna.Framework;
using System;


namespace EtherEngine.Collision
{
    public class CircleCollision : Collision<Circle>
    {
        
        public CircleCollision(Circle circle, CollisionLayer layer=null): base(layer)
        {
            this.type = CollisionTypes.Circle;
            this.InnerShape = circle;
        }

        protected override bool CheckCircleCollision(ICollision other, out Contact contact)
        {
            if (other is CircleCollision otherCircle)
                return CollisionUtils.CircleOnCircleCollision(InnerShape, otherCircle.InnerShape, out contact);
            else throw new ArgumentException();
        }

        protected override bool CheckStaticQuadCollision(ICollision other, out Contact contact)
        {

            if (other is StaticQuadCollision otherQuad)
                return CollisionUtils.CircleOnStaticQuadCollision(InnerShape, otherQuad.InnerShape, out contact);
            else throw new ArgumentException();
        }

        protected override bool CheckRotatableQuadCollision(ICollision other, out Contact contact)
        {
            if (other is RotatableQuadCollision otherQuad)
                return CollisionUtils.CircleOnRotatableQuadCollision(InnerShape, otherQuad.InnerShape, out contact);
            else throw new ArgumentException();
        }

        protected override bool CheckPolygonCollision(ICollision other, out Contact contact)
        {
            if (other is PolygonCollision otherPolygon)
                return CollisionUtils.CircleOnPolygonCollision(InnerShape, otherPolygon.InnerShape, out contact);
            else throw new ArgumentException();
        }

        
    }
}
