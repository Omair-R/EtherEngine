
using Arch.Core;
using EtherEngine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Motion
{
    public class MassLessDriveSystem : UpdatableSystem
    {
        public MassLessDriveSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<MotionComponent,
                                                              MotionDirectionComponent,
                                                              MasslessDriveComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _scene.EntityManager.Registry.Query(in queryDescription, (ref MotionComponent motion,
                                                      ref MotionDirectionComponent input,
                                                      ref MasslessDriveComponent masslessDrive) =>
            {
                motion.Velocity += input.InputDirection * masslessDrive.Acceleration * dt - masslessDrive.FrictionCoff * motion.Velocity;
            });
        }
    }
}
