using EtherEngine.Control;
using Microsoft.Xna.Framework;
using System;


namespace EtherEngine.Motion
{
    public class PIDMotion : Motion
    {
        private PID _pidX;
        private PID _pidY;
        private float _mass;

        public PIDMotion(float maxVelocity, float mass, float Kp, float? KI=null, float? KD= null) 
        {
            _pidX = new PID(Kp, KI, KD);
            _pidY = new PID(Kp, KI, KD);
            MaxVelocity = maxVelocity;
            _mass = mass;
        }

        public override Vector2 MoveWithDirection(Vector2 position, Vector2 motionDirection, GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 force = Vector2.Zero;

            force.X = _pidX.Update(_currentVelocity.X,
                        MaxVelocity * motionDirection.X,
                        gameTime);

            force.Y = _pidY.Update(_currentVelocity.Y,
                        MaxVelocity * motionDirection.Y,
                        gameTime);

            Vector2 accel = force/_mass; //To have a stable model, mass should be considered. 

            _currentVelocity += accel * elapsedTime;
            return position + _currentVelocity * elapsedTime;
        }
    }
}
