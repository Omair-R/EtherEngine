
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
        QueryDescription queryDescription = new QueryDescription().WithAll<MotionComponent,
                                                                            InputComponent,
                                                                            MasslessDriveComponent>();
        public MassLessDriveSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _scene._world.Query(in queryDescription, (ref MotionComponent motion,
                                                      ref InputComponent input,
                                                      ref MasslessDriveComponent masslessDrive) =>
            {
                motion.Velocity += input.InputDirection * masslessDrive.Acceleration * dt - masslessDrive.FrictionCoff * motion.Velocity;
            });
        }
    }
}
