using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Components.Graphics;
using EtherEngine.Components.Particles;
using EtherEngine.Systems.Event;
using Microsoft.Xna.Framework;


namespace EtherEngine.Systems
{
    public class ParticleSystem : UpdatableSystem
    {
        public ParticleSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<ParticleComponent, TransformComponent, ColorComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            Query query = _scene.EntityManager.Registry.Query(queryDescription);
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _scene.EntityManager.Registry.Query(
                in queryDescription, 
                (in Entity entity, 
                ref ParticleComponent particle, 
                ref TransformComponent transform, 
                ref ColorComponent color) => {
                    particle.Age += dt;
                    if (particle.Age > particle.LifeTime)
                    {
                        _scene.TriggerEvent<KillParticleComponent>(new KillParticleComponent { Sender = entity }, this);
                    }

                    float timeRatio = particle.Age / particle.LifeTime;

                    float scale = MathHelper.Lerp(particle.ScaleBegin, particle.ScaleEnd, timeRatio);
                    transform.Scale = Vector2.One * scale;
                    color.Color = Color.Lerp(particle.ColorBegin, particle.ColorEnd, timeRatio);
                    color.Alpha = MathHelper.Lerp(particle.AlphaBegin, particle.AlphaEnd, timeRatio);

                });


        }
    }
}
