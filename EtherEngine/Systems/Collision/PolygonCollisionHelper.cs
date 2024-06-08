using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Collision.Utils;
using EtherEngine.Core.Shapes;
using System;

namespace EtherEngine.Systems.Collision
{
    public class PolygonCollisionHelper : CollisionHelper
    {
        protected static readonly Lazy<PolygonCollisionHelper> _lazyInstance = new Lazy<PolygonCollisionHelper>(
            () => new PolygonCollisionHelper()
        );
        public static PolygonCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }
        private PolygonCollisionHelper() { }
        public override bool CheckCircleCollision(IShape current, Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnPolygonCollision(other, (Polygon)current, out contact, true);
        }

        public override bool CheckStaticQuadCollision(IShape current, StaticQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnPolygonCollision(other, (Polygon)current, out contact, true);
        }

        public override bool CheckRotatableQuadCollision(IShape current, RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.RotatableQuadOnPolygonCollision(other, (Polygon)current, out contact, true);
        }

        public override bool CheckPolygonCollision(IShape current, Polygon other, out Contact contact)
        {
            return CollisionUtils.PolygonOnPolygonCollision((Polygon)current, other, out contact);
        }
    }
}
