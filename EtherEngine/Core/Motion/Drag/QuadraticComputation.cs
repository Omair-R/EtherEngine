using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Core.Motion.Drag
{
    internal class QuadraticComputation : Drag
    {
        public float Acceleration { get; private set; }
        public float FrictionCoeff { get; private set; }

        public QuadraticComputation(float maxVelocity, float reachTime) : base(maxVelocity, reachTime) { }

        public override Vector2 ComputeVelocityUpdate(Vector2 currentVelocity, Vector2 motionDirection)
        {
            Vector2 resultVelocity = Vector2.Zero;
            float magnitude = currentVelocity.Length();
            resultVelocity.X = Acceleration * motionDirection.X - FrictionCoeff * currentVelocity.X * magnitude;
            resultVelocity.Y = Acceleration * motionDirection.Y - FrictionCoeff * currentVelocity.Y * magnitude;
            return resultVelocity;
        }

        protected override void UpdateFriction(float maxVelocity, float maxTime) => FrictionCoeff = 5f / (maxTime * maxVelocity);
        protected override void UpdateAcceleration(float maxVelocity, float maxTime) => Acceleration = FrictionCoeff * maxVelocity * maxVelocity;

    }
}
