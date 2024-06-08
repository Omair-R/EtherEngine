using Arch.Core;
using EtherEngine.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Systems.Motion
{
    public class MotionSystem : UpdatableSystem
    {

        QueryDescription queryDescription = new QueryDescription().WithAll<TransformComponent, MotionComponent>();
        public MotionSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _scene._world.Query(in queryDescription, (ref TransformComponent transform, ref MotionComponent motion) =>
            {
                transform.Position += motion.Velocity * dt;
            });

        }
    }
}
