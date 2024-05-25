using EtherEngine.Utils.Pattern;
using Microsoft.Xna.Framework.Input;


namespace EtherEngine.Input
{
    public sealed class KeyboardManager : LazySingleton<KeyboardManager>
    {
        private KeyboardState _keyboardState = Keyboard.GetState();
        private KeyboardState _prevKeyboardState = Keyboard.GetState();

        private KeyboardManager() { }

        public void Update()
        {
            this._prevKeyboardState = this._keyboardState;
            this._keyboardState = Keyboard.GetState();
        }
        public bool IsPressed(Keys key)
        {
            return this._keyboardState.IsKeyDown(key);
        }

        public bool IsClicked(Keys key)
        {
            return this._keyboardState.IsKeyDown(key) && !this._prevKeyboardState.IsKeyDown(key);
        }

        public bool IsReleased(Keys key)
        {
            return this._prevKeyboardState.IsKeyDown(key) && this._keyboardState.IsKeyUp(key);
        }

        public bool IsPressed(KeyMapper key) => IsPressed(key.InnerKey);

        public bool IsClicked(KeyMapper key) => IsClicked(key.InnerKey);

        public bool IsReleased(KeyMapper key) => IsReleased(key.InnerKey);
    }
}
