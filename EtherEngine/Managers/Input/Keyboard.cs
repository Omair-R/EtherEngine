using EtherEngine.Utils.Pattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace EtherEngine.Managers.Input
{
    public sealed class KeyboardManager : LazySingleton<KeyboardManager>
    {
        private KeyboardState _keyboardState = Keyboard.GetState();
        private KeyboardState _prevKeyboardState = Keyboard.GetState();

        private KeyboardManager() { }

        public Vector2 HandleMovementInput()
        {
            Update();
            Vector2 outputDirection = Vector2.Zero;

            if (IsPressed(Keys.Up))
                outputDirection.Y -= 1;

            if (IsPressed(Keys.Down))
                outputDirection.Y += 1;

            if (IsPressed(Keys.Left))
                outputDirection.X -= 1;

            if (IsPressed(Keys.Right))
                outputDirection.X += 1;

            return outputDirection;
        }


        public void Update()
        {
            _prevKeyboardState = _keyboardState;
            _keyboardState = Keyboard.GetState();
        }
        public bool IsPressed(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }

        public bool IsClicked(Keys key)
        {
            return _keyboardState.IsKeyDown(key) && !_prevKeyboardState.IsKeyDown(key);
        }

        public bool IsReleased(Keys key)
        {
            return _prevKeyboardState.IsKeyDown(key) && _keyboardState.IsKeyUp(key);
        }

        public bool IsPressed(KeyMapper key) => IsPressed(key.InnerKey);

        public bool IsClicked(KeyMapper key) => IsClicked(key.InnerKey);

        public bool IsReleased(KeyMapper key) => IsReleased(key.InnerKey);
    }
}
