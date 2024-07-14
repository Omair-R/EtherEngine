using Arch.Core;
using EtherEngine.Components;
using Microsoft.Xna.Framework;
using System;


namespace EtherEngine.Systems
{
    public class GravitySystem : UpdatableSystem
    {
        public GravitySystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<MotionComponent, GravityComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _scene.EntityManager.Registry.Query(in queryDescription, (ref MotionComponent motion, ref GravityComponent gravity) =>
            {
                motion.Velocity += gravity.Acceleration * dt;
            });
        }
    }
}
