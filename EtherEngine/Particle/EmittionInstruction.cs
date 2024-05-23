using Microsoft.Xna.Framework;

namespace EtherEngine.Particle
{
    public class EmittionInstruction
    {
        public Vector2 Position { get; set; }
        public Vector2 Spread { get; set; }
        
        public Vector2 InitVelocity { get; set; }
        public float InitVelocityVariance { get; set; }

        public Vector2 Acceleration { get; set; }
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



        public EmittionInstruction(Vector2 position,
                                   Vector2 spread,
                                   Vector2 initVelocity,
                                   float initVelocityVariance,
                                   Vector2 acceleration,
                                   float damping,
                                   float angle,
                                   float angleVariance,
                                   float angularVelocity,
                                   float scaleBegin,
                                   float scaleEnd,
                                   float scaleVariance,
                                   Color colorBegin,
                                   Color colorEnd,
                                   float hueVairance,
                                   float alphaBegin,
                                   float alphaEnd,
                                   float alphaVariance,
                                   float lifeTime,
                                   float lifeTimeVariance)
        {
            Position = position;
            Spread = spread;
            InitVelocity = initVelocity;
            InitVelocityVariance = initVelocityVariance;
            Acceleration = acceleration;
            Damping = damping;
            Angle = angle;
            AngleVariance = angleVariance;
            AngularVelocity = angularVelocity;
            ScaleBegin = scaleBegin;
            ScaleEnd = scaleEnd;
            ScaleVariance = scaleVariance;
            ColorBegin = colorBegin;
            ColorEnd = colorEnd;
            HueVairance = hueVairance;
            AlphaBegin = alphaBegin;
            AlphaEnd = alphaEnd;
            AlphaVariance = alphaVariance;
            LifeTime = lifeTime;
            LifeTimeVariance = lifeTimeVariance;
        }
    }
}
