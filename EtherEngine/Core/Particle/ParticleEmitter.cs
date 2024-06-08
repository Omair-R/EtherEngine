using EtherEngine.Utils.Random;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EtherEngine.Core.Particle
{
    public class ParticleEmitter
    {
        public int Amount { get; set; }
        public float IntervalSeconds { get; set; }
        public bool Active { get; set; }
        public EmittionInstruction EmittionInstruction { get; set; }

        private ParticlePool _particlePool;

        private readonly RandomSinglton _Random = RandomSinglton.Instance;
        private Sprite.Sprite _sprite;

        private float _count = 0;
        public ParticleEmitter(in Sprite.Sprite sprite, int amount, float interval, int capacity, EmittionInstruction instruction)
        {
            Amount = amount;
            IntervalSeconds = interval;
            EmittionInstruction = instruction;
            _particlePool = new ParticlePool(capacity);
            _sprite = sprite;
        }

        public void Update(GameTime gameTime)
        {
            _count -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach (Particle p in _particlePool.ParticleList)
            {
                Debug.Assert(p.Active);

                p.Update(gameTime);
                float timeRatio = 1 - p.RemainingTime / p.LifeTime;

                p.Size = MathHelper.Lerp(p.SizeBegin, EmittionInstruction.ScaleEnd, timeRatio);
                p.Color = Color.Lerp(p.ColorBegin, EmittionInstruction.ColorEnd, timeRatio);
                p.Alpha = MathHelper.Lerp(p.AlphaBegin, EmittionInstruction.AlphaEnd, timeRatio);

                p.CheckStillAlive();
            }

            _particlePool.ParticleList.RemoveWhere(p => p.RemainingTime <= 0);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Particle p in _particlePool.ParticleList)
                p.Draw(spriteBatch);
        }

        public void Emit()
        {
            if (_count > 0)
                return;
            for (int i = 0; i < Amount; i++)
            {
                _particlePool.Get(out Particle particle);
                particle.Sprite = _sprite;
                ApplyVariance(EmittionInstruction.Position, EmittionInstruction.Spread, out particle.Position);
                ApplyVariance(EmittionInstruction.InitVelocity, EmittionInstruction.InitVelocityVariance, out particle.Velocity);
                particle.Acceleration = EmittionInstruction.Acceleration;
                particle.TangentialAcceleration = EmittionInstruction.TangentialAcceleration;
                particle.Damping = EmittionInstruction.Damping;

                ApplyVariance(EmittionInstruction.Angle, EmittionInstruction.AngleVariance, out particle.Angle);
                particle.AngularVelocity = EmittionInstruction.AngularVelocity;

                ApplyVariance(EmittionInstruction.ScaleBegin, EmittionInstruction.ScaleVariance, out particle.Size);
                particle.SizeBegin = particle.Size;

                particle.Color = EmittionInstruction.ColorBegin;
                particle.ColorBegin = particle.Color;

                ApplyVariance(EmittionInstruction.AlphaBegin, EmittionInstruction.AlphaVariance, out particle.Alpha);
                particle.AlphaBegin = particle.Alpha;

                particle.LifeTime = EmittionInstruction.LifeTime + _Random.Randomizer.NextFloat(EmittionInstruction.LifeTimeVariance);
                particle.LifeTime -= _Random.Randomizer.NextFloat(EmittionInstruction.LifeTimeVariance);

                particle.Active = true;
            }
            _count = IntervalSeconds;
        }

        private void ApplyVariance(in Vector2 value, in Vector2 variance, out Vector2 output)
        {
            output.X = value.X + _Random.Randomizer.NextFloat(variance.X);
            output.X -= _Random.Randomizer.NextFloat(variance.X);

            output.Y = value.Y + _Random.Randomizer.NextFloat(variance.Y);
            output.Y -= _Random.Randomizer.NextFloat(variance.Y);
        }

        private void ApplyVariance(in Vector2 value, in float variance, out Vector2 output)
        {
            output.X = value.X + _Random.Randomizer.NextFloat(variance);
            output.X -= _Random.Randomizer.NextFloat(variance);

            output.Y = value.Y + _Random.Randomizer.NextFloat(variance);
            output.Y -= _Random.Randomizer.NextFloat(variance);
        }

        private void ApplyVariance(in float value, in float variance, out float output)
        {
            output = value + _Random.Randomizer.NextFloat(variance);
            output -= _Random.Randomizer.NextFloat(variance);
        }
    }
}
