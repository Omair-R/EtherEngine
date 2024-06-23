using EtherUtils;
using EtherUtils.Random;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine.Core.Particle
{
    public class Particle
    {
        private ParticlePool _pool;

        public Sprite.Sprite Sprite; //TODO: Change this.

        public Vector2 Position;
        public Vector2 Velocity;

        public Vector2 Acceleration;
        public Vector2 TangentialAcceleration;
        public float Damping;

        public float Angle;
        public float AngularVelocity;

        public float SizeBegin;
        public float Size;

        public Color ColorBegin;
        public Color Color;

        public float AlphaBegin;
        public float Alpha;

        private float _lifeTime;
        public float LifeTime
        {
            get => _lifeTime; set
            {
                _lifeTime = value;
                RemainingTime = _lifeTime;
            }
        }

        public float RemainingTime { get; private set; }
        public bool Active { get; set; }
        public Guid RandomIdentifier { get; private set; }

        public Particle(ParticlePool pool)
        {
            RandomIdentifier = Guid.NewGuid();
            _pool = pool;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var norm = MathUtils.Normalize(Velocity);
            Acceleration.X += TangentialAcceleration.Y * dt * -norm.Y;
            Acceleration.Y += TangentialAcceleration.X * dt * norm.X;
            Velocity += Acceleration * dt - Damping * Velocity;

            Position += Velocity * dt;

            Angle += AngularVelocity * dt;

            RemainingTime -= dt;
        }

        public void Draw(in SpriteBatch spriteBatch)
        {
            Sprite.Center = Position;
            Sprite.Color = Color;
            Sprite.Scale = new Vector2(Size, Size);
            Sprite.Rotation = Angle; //??
            Sprite.Alpha = Alpha;

            Sprite.Draw(spriteBatch);
        }

        public void CheckStillAlive()
        {
            if (RemainingTime <= 0)
            {
                _pool.Return(this);
                Active = false;
            }
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
