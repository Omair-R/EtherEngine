using Arch.Core;
using Arch.Core.Extensions;
using Arch.Core.Utils;
using EtherEngine.Components;
using EtherEngine.Components.Graphics;
using EtherEngine.Components.Particles;
using EtherUtils.Random;
using Microsoft.Xna.Framework;

namespace EtherEngine.Systems
{
    public class ParticlesEmitterSystem : UpdatableSystem
    {
        RandomSinglton _Random;

        public ParticlesEmitterSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<ParticleEmitterComponent, ParticleInstructionComponent, SpriteComponent>();
            _Random = RandomSinglton.Instance;
        }

        private float ApplyVariance(in float value, in float variance)
        {
            float output = value + _Random.Randomizer.NextFloat(variance);
            output -= _Random.Randomizer.NextFloat(variance);
            return output;
        }

        private Vector2 ApplyVariance(in Vector2 value, in Vector2 variance)
        {
            Vector2 output = Vector2.Zero;
            output.X = value.X + _Random.Randomizer.NextFloat(variance.X);
            output.X -= _Random.Randomizer.NextFloat(variance.X);

            output.Y = value.Y + _Random.Randomizer.NextFloat(variance.Y);
            output.Y -= _Random.Randomizer.NextFloat(variance.Y);
            return output;
        }

        private Vector2 ApplyVariance(in Vector2 value, in float variance)
        {
            Vector2 output = Vector2.Zero;
            output.X = value.X + _Random.Randomizer.NextFloat(variance);
            output.X -= _Random.Randomizer.NextFloat(variance);

            output.Y = value.Y + _Random.Randomizer.NextFloat(variance);
            output.Y -= _Random.Randomizer.NextFloat(variance);

            return output;
        }

        private void Emit(in ParticleEmitterComponent emitter, in ParticleInstructionComponent instruction, in SpriteComponent sprite)
        {
            var archetype = new ComponentType[] //TODO: make a way to make archtype creation easier.
            {
                typeof(ParticleComponent),
                typeof(TransformComponent),
                typeof(MotionComponent),
                typeof(MasslessDriveComponent),
                typeof(ColorComponent),
                typeof(SpriteComponent)
            };

            _scene.EntityManager.Registry.Reserve(archetype, emitter.Amount);

            for ( int i = 0; i < emitter.Amount;  i++ )
            {
                var entity = _scene.EntityManager.Registry.Create(archetype);

                var scaleBegin = ApplyVariance(instruction.ScaleBegin, instruction.ScaleVariance);
                var alphaBegin = ApplyVariance(instruction.AlphaBegin, instruction.AlphaVariance);
                var colorBegin = instruction.ColorBegin; //TODO: use the hue property.

                entity.Set(new ParticleComponent
                {
                    ScaleBegin = scaleBegin,
                    ScaleEnd = ApplyVariance(instruction.ScaleEnd, instruction.ScaleVariance),

                    AngularVelocity = instruction.AngularVelocity,

                    ColorBegin = colorBegin,
                    ColorEnd = instruction.ColorEnd,

                    AlphaBegin = alphaBegin,
                    AlphaEnd = instruction.AlphaEnd,

                    LifeTime = ApplyVariance(instruction.LifeTime, instruction.LifeTimeVariance),
                    Age = 0.0f
                });

                entity.Set(new TransformComponent
                {
                    Position = ApplyVariance(instruction.Position, instruction.Spread),
                    Scale = new Vector2(scaleBegin, scaleBegin),
                    Rotation = ApplyVariance(instruction.Angle, instruction.AngleVariance)
                });

                entity.Set(new MotionComponent
                {
                    Velocity = ApplyVariance(instruction.InitVelocity, instruction.InitVelocityVariance),
                });

                entity.Set(new MasslessDriveComponent
                {
                    Acceleration = instruction.Acceleration,
                    FrictionCoff = instruction.Damping,
                });

                entity.Set(sprite);

                entity.Set(new ColorComponent
                {
                    Color = colorBegin,
                    Alpha = alphaBegin,
                });

            }
            
        }

        public override void Update(GameTime gameTime)
        {
            Query query = _scene.EntityManager.Registry.Query(queryDescription);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach ( ref Chunk chunk in query)
            {
                chunk.GetSpan<ParticleEmitterComponent, ParticleInstructionComponent, SpriteComponent>(out var emitters, out var instructions, out var sprites);

                foreach (int index in chunk)
                {
                    ref var emitter = ref emitters[index];
                    ref var instruction = ref instructions[index];
                    ref var sprite = ref sprites[index];

                    emitter.Timer.Update(dt);

                    if (emitter.Timer.IsTriggered)
                    {
                        Emit(emitter, instruction, sprite);
                    }
                }
            }
        }
    }
}
