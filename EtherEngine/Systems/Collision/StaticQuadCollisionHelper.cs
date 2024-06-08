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
    public class StaticQuadCollisionHelper : CollisionHelper
    {
        protected static readonly Lazy<StaticQuadCollisionHelper> _lazyInstance = new Lazy<StaticQuadCollisionHelper>(
            () => new StaticQuadCollisionHelper()
        );
        public static StaticQuadCollisionHelper Instance
        {
            get { return _lazyInstance.Value; }
        }

        private StaticQuadCollisionHelper() { }

        public override bool CheckCircleCollision(IShape current, Circle other, out Contact contact)
        {
            return CollisionUtils.CircleOnStaticQuadCollision(other, (StaticQuad)current, out contact, true);
        }

        public override bool CheckStaticQuadCollision(IShape current, StaticQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnStaticQuadCollision((StaticQuad)current, other, out contact);
        }

        public override bool CheckRotatableQuadCollision(IShape current, RotatableQuad other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnRotatableQuadCollision((StaticQuad)current, other, out contact);
        }

        public override bool CheckPolygonCollision(IShape current, Polygon other, out Contact contact)
        {
            return CollisionUtils.StaticQuadOnPolygonCollision((StaticQuad)current, other, out contact);
        }
    }
}
