using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Collision.Utils;
using EtherEngine.Core.Shapes;
using System;

namespace EtherEngine.Systems.Collision
{
    public class PolygonCollisionHelper : CollisionHelper<Polygon>
    {
        protected static readonly Lazy<PolygonCollisionHelper> _lazyInstance = new Lazy<PolygonCollisionHelper>(
            () => new PolygonCollisionHelper()
        );
        public static PolygonCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }
        private PolygonCollisionHelper() { }
        public override bool CheckCircleCollision(in Polygon current,in Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnPolygonCollision(other, current, out contact, true);
        }

        public override bool CheckStaticQuadCollision(in Polygon current, in StaticQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnPolygonCollision(other, current, out contact, true);
        }

        public override bool CheckRotatableQuadCollision( in Polygon current, in RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.RotatableQuadOnPolygonCollision(other, current, out contact, true);
        }

        public override bool CheckPolygonCollision(in Polygon current, in Polygon other, out Contact contact)
        {
            return CollisionUtils.PolygonOnPolygonCollision(current, other, out contact);
        }
    }
}
