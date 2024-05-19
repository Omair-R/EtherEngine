using EtherEngine.Utils.Pattern;
using Microsoft.Xna.Framework.Input;


namespace EtherEngine.Input
{
    public sealed class KeyboardManager : LazySingleton<KeyboardManager>
    {
        private KeyboardState keyboardState = Keyboard.GetState();
        private KeyboardState prevKeyboardState = Keyboard.GetState();

        private KeyboardManager() { }

        public void Update()
        {
            this.prevKeyboardState = this.keyboardState;
            this.keyboardState = Keyboard.GetState();
        }
        public bool IsPressed(Keys key)
        {
            return this.keyboardState.IsKeyDown(key);
        }

        public bool IsClicked(Keys key)
        {
            return this.keyboardState.IsKeyDown(key) && !this.prevKeyboardState.IsKeyDown(key);
        }

        public bool IsReleased(Keys key)
        {
            return this.prevKeyboardState.IsKeyDown(key) && this.keyboardState.IsKeyDown(key);
        }

        public bool IsPressed(KeyMapper key) => IsPressed(key.InnerKey);

        public bool IsClicked(KeyMapper key) => IsClicked(key.InnerKey);

        public bool IsReleased(KeyMapper key) => IsReleased(key.InnerKey);
    }
}
