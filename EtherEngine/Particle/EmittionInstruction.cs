using Microsoft.Xna.Framework;

namespace EtherEngine.Particle
{
    public record EmittionInstruction
    {
        public Vector2 Position { get; set; }
        public Vector2 Spread { get; set; }
        
        public Vector2 InitVelocity { get; set; }
        public float InitVelocityVariance { get; set; }

        public Vector2 Acceleration { get; set; }
        public Vector2 TangentialAcceleration { get; set; }
        public float Damping { get; set; }

        public float Angle { get; set; }
        public float AngleVariance { get; set; }
        public float AngularVelocity { get; set; }

        public float ScaleBegin { get; set; }
        public float ScaleEnd { get; set; }
        public float ScaleVariance { get; set; }

        
        public Color ColorBegin { get; set; }
        public Color ColorEnd { get; set; }
        public float HueVairance { get; set; }

        public float AlphaBegin { get; set; }
        public float AlphaEnd { get; set; }
        public float AlphaVariance { get; set; }

        public float LifeTime { get; set; }
        public float LifeTimeVariance { get; set; }
        
    }
}
