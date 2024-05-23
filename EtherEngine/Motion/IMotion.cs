using Microsoft.Xna.Framework;

namespace EtherEngine.Motion
{
    public interface IMotion
    {
        public float MaxVelocity { get; set; }
        public Vector2 MoveWithDirection(Vector2 position, Vector2 motionDirection, GameTime gameTime);
    }

    public abstract class Motion : IMotion
    {
        protected Vector2 _currentVelocity = Vector2.Zero;
        protected Axis _restricedAxis;
        protected float _maxVelocity;

        public virtual float MaxVelocity { get; set; }

        public virtual Vector2 GetCurrentVelocity() => _currentVelocity;

        protected virtual void SuppressMotion(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    _currentVelocity.X = 0f;
                    break;
                case Axis.Y:
                    _currentVelocity.Y = 0f;
                    break;
                case Axis.None:
                    break;

            }
        }

        public abstract Vector2 MoveWithDirection(Vector2 position, Vector2 motionDirection, GameTime gameTime);

        public override string ToString()
        {
            return _currentVelocity.ToString();
        }
    }

    
}
