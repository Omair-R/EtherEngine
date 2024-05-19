using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace EtherEngine.Particle
{
    public class ParticleEmitter
    {
        public int Amount {  get; set; }
        public float IntervalSeconds { get; set; }
        public bool Active { get; set; }
        public EmittionInstruction EmittionInstruction { get; set; }

        private ParticlePool _particlePool;

        public ParticleEmitter(int amount, float interval, int capacity, EmittionInstruction instruction) 
        {
            Amount = amount;
            IntervalSeconds = interval;
            EmittionInstruction = instruction;
            _particlePool = new ParticlePool(capacity);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Particle p in _particlePool.ParticleList) 
                p.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in _particlePool.ParticleList)
                p.Draw(spriteBatch);
        }

        public void Emit()
        {
            _particlePool.Get(out Particle particle);

            throw new NotImplementedException();
        }
    }
}
