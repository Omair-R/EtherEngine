using Microsoft.Extensions.ObjectPool;

namespace EtherEngine.Core.Particle
{
    public class ParticleObjectPoolPolicy : IPooledObjectPolicy<Particle>
    {
        private ParticlePool _pool;
        public ParticleObjectPoolPolicy(ParticlePool pool)
        {
            _pool = pool;
        }
        public Particle Create()
        {
            return new Particle(_pool);
        }

        public bool Return(Particle obj)
        {
            return true;
        }
    }
}
