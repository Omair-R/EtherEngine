using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Collision.Utils;
using EtherEngine.Core.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Collision
{
    public class StaticQuadCollisionHelper : CollisionHelper<StaticQuad>
    {
        protected static readonly Lazy<StaticQuadCollisionHelper> _lazyInstance = new Lazy<StaticQuadCollisionHelper>(
            () => new StaticQuadCollisionHelper()
        );
        public static StaticQuadCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }

        private StaticQuadCollisionHelper() { }

        public override bool CheckCircleCollision(in StaticQuad current, in Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnStaticQuadCollision(other, current, out contact, true);
        }

        public override bool CheckStaticQuadCollision(in StaticQuad current, in StaticQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnStaticQuadCollision(current, other, out contact);
        }

        public override bool CheckRotatableQuadCollision(in StaticQuad current, in RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnRotatableQuadCollision(current, other, out contact);
        }

        public override bool CheckPolygonCollision(in StaticQuad current, in Polygon other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnPolygonCollision(current, other, out contact);
        }
    }
}
