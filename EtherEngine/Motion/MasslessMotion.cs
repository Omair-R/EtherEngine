using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace EtherEngine.Motion
{
    public class MasslessMotion : Motion
    {
        public float Acceleration { get; set; }

        private float _frictionCoeff;

        public float FrictionCoeff { get{
                return _frictionCoeff;
            } set {
                if (value > 1f)
                    _frictionCoeff = 1f;
                else 
                    _frictionCoeff = value;
            }
        }

        public MasslessMotion(float maxVelocity, float acceleration, float frictionCoeff)
        {
            Debug.Assert(frictionCoeff <= 1 && frictionCoeff > 0);
            MaxVelocity = maxVelocity;
            Acceleration = acceleration;
            FrictionCoeff = frictionCoeff;
        }

        public override Vector2 MoveWithDirection(Vector2 position, Vector2 motionDirection, GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _currentVelocity += motionDirection * Acceleration * elapsedTime - FrictionCoeff * _currentVelocity;
            return position + _currentVelocity * elapsedTime;
        }
    }
}
