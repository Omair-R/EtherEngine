using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;


namespace EtherEngine.Camera
{
    public class PositionalCamera : ICamera
    {
        protected Vector2 postion;
        protected float rotation;
        protected float zoom;

        protected Matrix transform;
        protected GraphicsDevice device;

        private bool ShouldUpdate;

        public PositionalCamera(Vector2 postion, float rotation, float zoom, GraphicsDevice device) 
        {
            Debug.Assert(zoom > 0);
            this.postion = postion;
            this.rotation = rotation;
            this.zoom = zoom;
            this.device = device;

            ShouldUpdate = true;
        }

        private void Update()
        {

            transform = Matrix.CreateTranslation(new Vector3(-this.postion.X, -this.postion.Y, 0)) *
                Matrix.CreateRotationZ(this.rotation) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(device.Viewport.Width / 2, device.Viewport.Height / 2, 0));

            // Alternative.
            //var _position = postion - new Vector2(device.Viewport.Width / 2, device.Viewport.Height / 2);
            ////var _position = Vector2.Transform(postion, Matrix.CreateTranslation(new Vector3(-device.Viewport.Width / 2, -device.Viewport.Height / 2, 0)));
            //transform = Matrix.CreateLookAt(new Vector3(_position, 0) , new Vector3(_position, -1) ,Vector3.Up);
        }

        public Matrix GetTransformation()
        {
            if (ShouldUpdate) this.Update();
            ShouldUpdate = false;

            return transform;
        }

        public void Move(Vector2 extent)
        {
            postion += extent;
            ShouldUpdate = true;
        }

        public void SetZoom(float zoom)
        {
            this.zoom = zoom;
            ShouldUpdate = true;
        }

        public void SetRotationRadians(float radians)
        {
            rotation = radians;
            ShouldUpdate = true;
        }

        public void SetRotationDegrees(float degrees)
        {
            rotation = degrees * MathF.PI/180;
            ShouldUpdate = true;
        }

        public void MoveTo(Vector2 target)
        {
            postion = target;
            ShouldUpdate = true;
        }
    }
}
