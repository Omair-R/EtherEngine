using Arch.Core;
using EtherEngine.Components;
using Microsoft.Xna.Framework;
using System;


namespace EtherEngine.Systems
{
    public class GravitySystem : UpdatableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<MotionComponent, GravityComponent>();
        public GravitySystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _scene._world.Query(in queryDescription, (ref MotionComponent motion, ref GravityComponent gravity) =>
            {
                motion.Velocity += gravity.Acceleration * dt;
            });
        }
    }
}
