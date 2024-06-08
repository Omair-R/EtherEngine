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
    public class PIDDriveSystem : UpdatableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<MotionComponent, PIDDriveComponent>();
        public PIDDriveSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _scene._world.Query(in queryDescription, (ref MotionComponent motion,
                                                      ref InputComponent input,
                                                      ref PIDDriveComponent pid) =>
            {
                Vector2 force = Vector2.Zero;
                force.X = pid.PIDX.Update(motion.Velocity.X, pid.MaxVelocity * input.InputDirection.X, dt);
                force.Y = pid.PIDY.Update(motion.Velocity.Y, pid.MaxVelocity * input.InputDirection.Y, dt);

                Vector2 accel = force / pid.Mass;
                motion.Velocity += accel * dt;

            });
        }
    }
}
