using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Managers.Input;
using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;


namespace EtherEngine.Systems
{
    public class InputSystem : UpdatableSystem
    {
        private readonly QueryDescription queryDescription = new QueryDescription().WithAll<InputComponent>();

        public InputSystem(EtherScene scene) : base(scene)
        {
        }

        public override void Update(in GameTime gameTime)
        {
            var keyboardManager = KeyboardManager.Instance;

            _scene._world.Query(in queryDescription, (ref InputComponent inputs) =>
            {
                inputs.InputDirection = keyboardManager.HandleMovementInput();
            });
        }
    }
}
