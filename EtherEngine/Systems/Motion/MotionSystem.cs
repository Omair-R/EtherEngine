using Arch.Core;
using EtherEngine.Components;
using Microsoft.Xna.Framework;


namespace EtherEngine.Systems.Motion
{
    public class MotionSystem : UpdatableSystem
    {

        public MotionSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<TransformComponent, MotionComponent>();
        }

        public override void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _scene.EntityManager.Registry.Query(in queryDescription, (ref TransformComponent transform, ref MotionComponent motion) =>
            {
                transform.Position += motion.Velocity * dt;
            });

        }
    }
}
