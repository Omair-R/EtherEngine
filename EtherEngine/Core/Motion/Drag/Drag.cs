using Microsoft.Xna.Framework;

namespace EtherEngine.Core.Motion.Drag
{

    internal interface IDrag
    {
        public Vector2 ComputeVelocityUpdate(Vector2 currentVelocity, Vector2 motionDirection);
    }

    internal abstract class Drag : IDrag
    {
        protected float _maxVelocity;
        public float MaxVelocity
        {
            get { return _maxVelocity; }
            set
            {
                _maxVelocity = value;
                UpdateAcceleration(_maxVelocity, _reachTime);
            }
        }

        protected float _reachTime;

        public float ReachTime
        {
            get { return _reachTime; }
            set
            {
                _reachTime = value;
                UpdateFriction(_maxVelocity, _reachTime);
            }
        }

        public Drag(float maxVelocity, float maxTime)
        {
            UpdateFriction(maxVelocity, maxTime);
            UpdateAcceleration(maxVelocity, maxTime);
        }
        protected abstract void UpdateAcceleration(float maxVelocity, float maxTime);
        protected abstract void UpdateFriction(float maxVelocity, float maxTime);
        public abstract Vector2 ComputeVelocityUpdate(Vector2 currentVelocity, Vector2 motionDirection);
    }
}
