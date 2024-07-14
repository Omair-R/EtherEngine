using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Entities;
using EtherEngine.Managers.Input;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace EtherEngine.Systems
{


    public class InputSystem : UpdatableSystem
    {
        public InputSystem(EtherScene scene) : base(scene)
        {
            queryDescription = new QueryDescription().WithAll<MotionDirectionComponent>().WithNone<FollowComponent>();
        }


        public override void Update(GameTime gameTime)
        {
            var keyboardManager = KeyboardManager.Instance;

            _scene.EntityManager.Registry.Query(in queryDescription, (ref MotionDirectionComponent inputs) =>
            {
                inputs.InputDirection = keyboardManager.HandleMovementInput();
            });
        }
    }

}
