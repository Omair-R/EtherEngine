using EtherEngine.Utils.Pattern;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;


namespace EtherEngine.Managers.Input
{
    public enum MouseButtons
    {
        LeftButton,
        RightButton,
    }

    public sealed class MouseManager : LazySingleton<MouseManager>
    {
        private MouseState _mouseState = Mouse.GetState();
        private MouseState _prevMouseState = Mouse.GetState();

        private MouseManager() { }

        public void Update()
        {
            _prevMouseState = _mouseState;
            _mouseState = Mouse.GetState();
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
            return GetButtonState(button, _mouseState) == ButtonState.Pressed;
        }

        public bool IsClicked(MouseButtons button)
        {
            return GetButtonState(button, _mouseState) == ButtonState.Pressed &&
                !(GetButtonState(button, _prevMouseState) == ButtonState.Pressed);
        }

        public bool IsReleased(MouseButtons button)
        {
            return GetButtonState(button, _prevMouseState) == ButtonState.Pressed &&
                !(GetButtonState(button, _mouseState) == ButtonState.Pressed);
        }

        public Vector2 GetWindowMousePosition()
        {
            return new Vector2(_mouseState.Position.X, _mouseState.Position.Y);
        }

        public Vector2 GetWindowPreviousPosition()
        {
            return new Vector2(_prevMouseState.Position.X, _prevMouseState.Position.Y);
        }

        public Vector2 GetWorldMousePosition()
        {
            throw new NotImplementedException();
        }

    }
}
