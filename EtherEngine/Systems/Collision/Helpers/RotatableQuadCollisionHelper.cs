using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Collision.Utils;
using EtherEngine.Core.Shapes;
using System;

namespace EtherEngine.Systems.Collision.Helpers
{
    public class RotatableQuadCollisionHelper : CollisionHelper<RotatableQuad>
    {
        protected static readonly Lazy<RotatableQuadCollisionHelper> _lazyInstance = new Lazy<RotatableQuadCollisionHelper>(
            () => new RotatableQuadCollisionHelper()
        );
        public static RotatableQuadCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }
        private RotatableQuadCollisionHelper() { }

        public override bool CheckCircleCollision(in RotatableQuad current, in Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnRotatableQuadCollision(other, current, out contact, true);
        }

        public override bool CheckStaticQuadCollision(in RotatableQuad current, in StaticQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnRotatableQuadCollision(other, current, out contact, true);
        }

        public override bool CheckRotatableQuadCollision(in RotatableQuad current, in RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.RotatableQuadOnRotatableQuadCollision(current, other, out contact);
        }

        public override bool CheckPolygonCollision(in RotatableQuad current, in Polygon other, out Contact contact)
        {
            return CollisionUtils.RotatableQuadOnPolygonCollision(current, other, out contact);
        }
    }
}
