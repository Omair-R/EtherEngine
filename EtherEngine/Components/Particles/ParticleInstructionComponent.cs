using Microsoft.Xna.Framework;

namespace EtherEngine.Components.Particles
{
    public struct ParticleInstructionComponent
    {
        public Vector2 Position;
        public Vector2 Spread;

        public Vector2 InitVelocity;
        public float InitVelocityVariance;

        public Vector2 Acceleration;
        public float Damping;

        public float Angle;
        public float AngleVariance;
        public float AngularVelocity;

        public float ScaleBegin;
        public float ScaleEnd;
        public float ScaleVariance;

        public Color ColorBegin;
        public Color ColorEnd;
        public float HueVariance;

        public float AlphaBegin;
        public float AlphaEnd;
        public float AlphaVariance;

        public float LifeTime;
        public float LifeTimeVariance;

    }
}
