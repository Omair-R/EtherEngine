using EtherEngine.Control;
using Microsoft.Xna.Framework;
using System;


namespace EtherEngine.Motion
{
    public class PIDMotion : Motion
    {
        public PID PID {  get; set; }

        public PIDMotion(float Kp, float? KI=null, float? KD= null) 
        {
            PID = new PID(Kp, KI, KD);
        }

        public override Vector2 MoveWithDirection(Vector2 position, Vector2 motionDirection, GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            float velocityUpdate = PID.Update(MathF.Max(_currentVelocity.X, _currentVelocity.Y),
                        MaxVelocity,
                        gameTime);

            _currentVelocity += motionDirection * velocityUpdate;
            return position + _currentVelocity * elapsedTime;
        }
    }
}
