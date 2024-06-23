using Arch.Core;
using Microsoft.Xna.Framework;
using EtherEngine.Core.Motion;
using EtherEngine.Core.Motion.Drag;
using EtherEngine.Components;

namespace EtherEngine.Systems.Motion
{
    public class DragDriveSystem : UpdatableSystem
    {
        QueryDescription queryDescription = new QueryDescription().WithAll<MotionComponent, MotionDirectionComponent, DragDriveComponent>();

        public DragDriveSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;


            _scene._world.Query(in queryDescription, (ref MotionComponent motion, ref MotionDirectionComponent input, ref DragDriveComponent dragDrive) =>
            {
                IDrag drag = dragDrive.DragType switch
                {
                    DragTypes.StokesDrag =>
                         new StokeComputation(dragDrive.MaxVelocity,
                                              dragDrive.ReachTime),
                    DragTypes.QuadraticDrag =>
                         new QuadraticComputation(dragDrive.MaxVelocity,
                                                  dragDrive.ReachTime),
                    _ => null,
                };

                motion.Velocity += drag.ComputeVelocityUpdate(motion.Velocity, input.InputDirection) * dt;
            });
        }
    }
}
