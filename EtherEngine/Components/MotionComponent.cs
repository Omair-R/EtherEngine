using EtherEngine.Core.Control;
using EtherEngine.Core.Motion;
using Microsoft.Xna.Framework;

namespace EtherEngine.Components
{
    public struct MotionComponent
    {
        public Vector2 Velocity;
    }

    public struct GravityComponent
    {
        public Vector2 Acceleration;
    }

    public struct MasslessDriveComponent
    {
        public Vector2 Acceleration = Vector2.Zero;
        public float FrictionCoff = 0f;

        public MasslessDriveComponent(Vector2 acceleration, float friction)
        {
            Acceleration = acceleration;
            FrictionCoff = friction;
        }
    }
    public struct DragDriveComponent
    {
        public float MaxVelocity;
        public float ReachTime;
        public DragTypes DragType;
    }

    public struct PIDDriveComponent
    {
        public PID PIDX;
        public PID PIDY;
        public float MaxVelocity;
        public float Mass; //TODO: Temporary.
        public PIDDriveComponent(float maxVelocity, float mass, float Kp, float? Kd = null, float? ki = null)
        {
            PIDX = new PID(Kp, Kd, ki);
            PIDY = new PID(Kp, Kd, ki);
            Mass = mass;
            MaxVelocity = maxVelocity;
        }
    }
}
