using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Collision.Utils;
using EtherEngine.Core.Shapes;
using System;

namespace EtherEngine.Systems.Collision
{
    public class CircleCollisionHelper : CollisionHelper
    {
        protected static readonly Lazy<CircleCollisionHelper> _lazyInstance = new Lazy<CircleCollisionHelper>(
            () => new CircleCollisionHelper()
        );
        public static CircleCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }

        private CircleCollisionHelper() { }

        public override bool CheckCircleCollision(IShape current, Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnCircleCollision((Circle)current, other, out contact);
        }

        public override bool CheckStaticQuadCollision(IShape current, StaticQuad other, out Contact contact)
        {
            return CollisionUtils.CircleOnStaticQuadCollision((Circle)current, other, out contact);
        }

        public override bool CheckRotatableQuadCollision(IShape current, RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.CircleOnRotatableQuadCollision((Circle)current, other, out contact);
        }

        public override bool CheckPolygonCollision(IShape current, Polygon other, out Contact contact)
        {
            return CollisionUtils.CircleOnPolygonCollision((Circle)current, other, out contact);
        }
    }
}
