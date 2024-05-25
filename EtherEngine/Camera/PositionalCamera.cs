using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;


namespace EtherEngine.Camera
{
    public class PositionalCamera : ICamera
    {
        protected Vector2 _postion;
        protected float _rotation;
        protected float _zoom;

        protected Matrix _transform;
        protected GraphicsDevice _device;

        private bool ShouldUpdate;

        public PositionalCamera(Vector2 postion, float rotation, float zoom, GraphicsDevice device) 
        {
            Debug.Assert(zoom > 0);
            this._postion = postion;
            this._rotation = rotation;
            this._zoom = zoom;
            this._device = device;

            ShouldUpdate = true;
        }

        private void Update()
        {

            _transform = Matrix.CreateTranslation(new Vector3(-this._postion.X, -this._postion.Y, 0)) *
                Matrix.CreateRotationZ(this._rotation) *
                Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(_device.Viewport.Width / 2, _device.Viewport.Height / 2, 0));

            // Alternative.
            //var _position = postion - new Vector2(device.Viewport.Width / 2, device.Viewport.Height / 2);
            ////var _position = Vector2.Transform(postion, Matrix.CreateTranslation(new Vector3(-device.Viewport.Width / 2, -device.Viewport.Height / 2, 0)));
            //transform = Matrix.CreateLookAt(new Vector3(_position, 0) , new Vector3(_position, -1) ,Vector3.Up);
        }

        public Matrix GetTransformation()
        {
            if (ShouldUpdate) this.Update();
            ShouldUpdate = false;

            return _transform;
        }

        public void Move(Vector2 extent)
        {
            _postion += extent;
            ShouldUpdate = true;
        }

        public void SetZoom(float zoom)
        {
            this._zoom = zoom;
            ShouldUpdate = true;
        }

        public void SetRotationRadians(float radians)
        {
            _rotation = radians;
            ShouldUpdate = true;
        }

        public void SetRotationDegrees(float degrees)
        {
            _rotation = degrees * MathF.PI/180;
            ShouldUpdate = true;
        }

        public void MoveTo(Vector2 target)
        {
            _postion = target;
            ShouldUpdate = true;
        }
    }
}
