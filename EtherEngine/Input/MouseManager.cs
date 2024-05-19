using EtherEngine.Utils.Pattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace EtherEngine.Input
{
    public enum MouseButtons
    {
        LeftButton,
        RightButton,
    }

    public sealed class MouseManager : LazySingleton<MouseManager>
    {
        private MouseState mouseState = Mouse.GetState();
        private MouseState prevMouseState = Mouse.GetState();

        private MouseManager() {}

        public void Update()
        {
            this.prevMouseState = this.mouseState;
            this.mouseState = Mouse.GetState();
        }

        private ButtonState GetButtonState(MouseButtons button, MouseState state)
        {
            switch (button)
            {
                case MouseButtons.LeftButton: return state.LeftButton;
                case MouseButtons.RightButton: return state.RightButton;
            }
            throw new IndexOutOfRangeException();
        }

        public bool IsPressed(MouseButtons button)
        {
            return GetButtonState(button, this.mouseState) == ButtonState.Pressed;
        }

        public bool IsClicked(MouseButtons button)
        {
            return GetButtonState(button, this.mouseState) == ButtonState.Pressed &&
                !(GetButtonState(button, this.prevMouseState) == ButtonState.Pressed);
        }

        public bool IsReleased(MouseButtons button)
        {
            return GetButtonState(button, this.prevMouseState) == ButtonState.Pressed &&
                !(GetButtonState(button, this.mouseState) == ButtonState.Pressed);
        }

        public Vector2 GetWindowMousePosition()
        {
            return new Vector2(mouseState.Position.X, mouseState.Position.Y);
        }

        public Vector2 GetWindowPreviousPosition()
        {
            return new Vector2(prevMouseState.Position.X, prevMouseState.Position.Y);
        }

        public Vector2 GetWorldMousePosition()
        {
            throw new NotImplementedException();
        }

    }
}
