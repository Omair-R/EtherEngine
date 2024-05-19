using System.Collections.Generic;


namespace EtherEngine.Collision.Models
{
    public class CollisionLayer
    {
        public HashSet<CollisionLayer> CollidingLayers { get; set; }

        public CollisionLayer(HashSet<CollisionLayer> collidingLayers) {
            CollidingLayers = collidingLayers;
        }

        public bool ShouldCollide(CollisionLayer layer) => CollidingLayers.Contains(layer);

    }
}
