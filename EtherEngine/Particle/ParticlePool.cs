using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.ObjectPool;

namespace EtherEngine.Particle
{
    public class ParticlePool
    {
        public HashSet<Particle> ParticleList { get; }
        DefaultObjectPool<Particle> _pool;

        public ParticlePool(int capacity) {
            ParticleList = new HashSet<Particle>(capacity);
            _pool = new DefaultObjectPool<Particle>(new ParticleObjectPoolPolicy(this), capacity);
        }

        public void Get(out Particle particle)
        {
            particle = _pool.Get();
            ParticleList.Add(particle);
        }

        public void Return(in Particle particle)
        {
            _pool.Return(particle);
            //ParticleList.Remove(particle); //Relay the removal to the update method.
        }

        public bool Contains(in Particle particle)
        {
            return ParticleList.Contains(particle);
        }
    }
}
