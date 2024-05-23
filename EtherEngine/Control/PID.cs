using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace EtherEngine.Control
{
    public class PID
    {
        public float ProportionalGain { get; set; }
        public float? IntegralGain { get; set; } = null;
        public float? DerivativeGain { get; set; } = null;

        public float Epsilon { get; set; } = 0.00004f;

        private float previousError = 0.0f;
        private float accumulatedIntegral = 0.0f;

        private bool enableDerivative = false;
        private bool enableIntegral = false;
        

        public PID(float proportionalGain, float? integralGain = null, float? derivativeGain = null) 
        {
            ProportionalGain = proportionalGain;
            if (integralGain != null)
            {
                enableIntegral = true;
                accumulatedIntegral = 0.0f; 
                IntegralGain = integralGain;
            }

            if (derivativeGain != null)
            {
                enableDerivative = true;
                previousError = 0.0f;
                DerivativeGain = derivativeGain;
            }
        }

        public float Update(float currentValue, float targetValue, GameTime gameTime)
        {
            float error = targetValue - currentValue;
            float gain = ProportionalGain * error; 

            if (enableDerivative)
            {
                float derivative = (error - previousError)/ ((float)gameTime.ElapsedGameTime.TotalSeconds + Epsilon); 
                gain += (float)DerivativeGain * derivative;
            }

            if (enableIntegral)
            {
                accumulatedIntegral += error * (float)gameTime.ElapsedGameTime.TotalSeconds;
                gain += (float)IntegralGain * accumulatedIntegral;
            }

            return gain;
        }
    }
}
