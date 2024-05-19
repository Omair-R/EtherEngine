using EtherEngine.Utils.Random;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine.Particle
{
    public class Particle
    {
        private ParticlePool _pool;
        public Texture2D Texture { get; set; } //TODO: Change this.
        public Vector2 Center { get; set; }
        public float Velocity { get; set; }
        public Vector2 Size { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color Color { get; set; }
        public float Alpha { get; set; }
        public float LifeTime { get; set; }
        public float RemainingTime { get; private set; }
        public bool Active { get; set; }
        public ulong RandomIdentifier { get; private set; }

        public Particle(ParticlePool pool)
        {
            RandomIdentifier = StaticRandom.Random.NextUInt64();
            _pool = pool;
        }

        public void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is Particle particle &&
                   RandomIdentifier == particle.RandomIdentifier;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(RandomIdentifier);
            return hash.ToHashCode();
        }
    }
}
