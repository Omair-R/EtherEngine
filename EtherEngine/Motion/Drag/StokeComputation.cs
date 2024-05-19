using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Motion.Drag
{
    internal class StokeComputation : Drag
    {
        public float Acceleration { get; private set; }
        public float FrictionCoeff { get; private set; }

        public StokeComputation(float maxVelocity, float maxTime) : base(maxVelocity, maxTime) { }

        public override Vector2 ComputeVelocityUpdate(Vector2 currentVelocity, Vector2 motionDirection)
        {
            //TODO: Elapsed Time?
            return Acceleration * motionDirection - FrictionCoeff * currentVelocity;
        }

        protected override void UpdateFriction(float maxVelocity, float maxTime) => FrictionCoeff = 5 / maxTime;
        protected override void UpdateAcceleration(float maxVelocity, float maxTime) => Acceleration = FrictionCoeff * maxVelocity;

        
    }
}
