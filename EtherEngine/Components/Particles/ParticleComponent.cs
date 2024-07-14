using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Components.Particles
{

    public struct ParticleComponent
    {
        public float AngularVelocity;

        public float ScaleBegin;
        public float ScaleEnd;

        public Color ColorBegin;
        public Color ColorEnd;

        public float AlphaBegin;
        public float AlphaEnd;

        public float LifeTime;

        public float Age;

    }
}
