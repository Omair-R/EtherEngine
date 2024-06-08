using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Collision.Utils;
using EtherEngine.Core.Shapes;
using System;

namespace EtherEngine.Systems.Collision
{
    public class RotatableQuadCollisionHelper : CollisionHelper
    {
        protected static readonly Lazy<RotatableQuadCollisionHelper> _lazyInstance = new Lazy<RotatableQuadCollisionHelper>(
            () => new RotatableQuadCollisionHelper()
        );
        public static RotatableQuadCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }
        private RotatableQuadCollisionHelper() { }

        public override bool CheckCircleCollision(IShape current, Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnRotatableQuadCollision(other, (RotatableQuad)current, out contact, true);
        }

        public override bool CheckStaticQuadCollision(IShape current, StaticQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnRotatableQuadCollision(other, (RotatableQuad)current, out contact, true);
        }

        public override bool CheckRotatableQuadCollision(IShape current, RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.RotatableQuadOnRotatableQuadCollision((RotatableQuad)current, other, out contact);
        }

        public override bool CheckPolygonCollision(IShape current, Polygon other, out Contact contact)
        {
            return CollisionUtils.RotatableQuadOnPolygonCollision((RotatableQuad)current, other, out contact);
        }
    }
}
