using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Collision.Utils;
using EtherEngine.Core.Shapes;
using System;

namespace EtherEngine.Systems.Collision
{
    public class CircleCollisionHelper : CollisionHelper<Circle>
    {
        protected static readonly Lazy<CircleCollisionHelper> _lazyInstance = new Lazy<CircleCollisionHelper>(
            () => new CircleCollisionHelper()
        );
        public static CircleCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }

        private CircleCollisionHelper() { }

        public override bool CheckCircleCollision(in Circle current, in Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnCircleCollision(current, other, out contact);
        }

        public override bool CheckStaticQuadCollision(in Circle current, in StaticQuad other, out Contact contact)
        {
            return CollisionUtils.CircleOnStaticQuadCollision(current, other, out contact);
        }

        public override bool CheckRotatableQuadCollision(in Circle current, in RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.CircleOnRotatableQuadCollision(current, other, out contact);
        }

        public override bool CheckPolygonCollision(in Circle current, in Polygon other, out Contact contact)
        {
            return CollisionUtils.CircleOnPolygonCollision(current, other, out contact);
        }
    }
}
