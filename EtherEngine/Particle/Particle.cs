using EtherEngine.Sprite;
using EtherEngine.Utils.Random;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine.Particle
{
    public class Particle
    {
        private ParticlePool _pool;

        public TexturedSprite Sprite; //TODO: Change this.

        public Vector2 Position;
        public Vector2 Velocity;

        public Vector2 Acceleration;
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
        public float LifeTime{get => _lifeTime; set { 
                _lifeTime = value;
                RemainingTime = _lifeTime;
                Reset();
            } 
        }

        public float RemainingTime { get; private set; }
        public bool Active { get; set; }
        public Guid RandomIdentifier { get; private set; }

        public float Accumulated_time { get; private set; } = 0;

        public Particle(ParticlePool pool)
        {
            RandomIdentifier = Guid.NewGuid();
            _pool = pool;
        }

        public void Reset() => Accumulated_time = 0;

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Velocity += Acceleration * dt - Damping * Velocity;
            Position += Velocity * dt;

            Angle += AngularVelocity * dt;

            RemainingTime -= dt;
        }

        public void Draw(SpriteBatch spriteBatch)
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
