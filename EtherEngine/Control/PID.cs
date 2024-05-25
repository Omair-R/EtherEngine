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

        private float _previousError = 0.0f;
        private float _accumulatedIntegral = 0.0f;

        private bool _enableDerivative = false;
        private bool _enableIntegral = false;
        

        public PID(float proportionalGain, float? integralGain = null, float? derivativeGain = null) 
        {
            ProportionalGain = proportionalGain;
            if (integralGain != null)
            {
                _enableIntegral = true;
                _accumulatedIntegral = 0.0f; 
                IntegralGain = integralGain;
            }

            if (derivativeGain != null)
            {
                _enableDerivative = true;
                _previousError = 0.0f;
                DerivativeGain = derivativeGain;
            }
        }

        public float Update(float currentValue, float targetValue, GameTime gameTime)
        {
            float error = targetValue - currentValue;
            float gain = ProportionalGain * error; 

            if (_enableDerivative)
            {
                float derivative = (error - _previousError)/ ((float)gameTime.ElapsedGameTime.TotalSeconds + Epsilon); 
                gain += (float)DerivativeGain * derivative;
            }

            if (_enableIntegral)
            {
                _accumulatedIntegral += error * (float)gameTime.ElapsedGameTime.TotalSeconds;
                gain += (float)IntegralGain * _accumulatedIntegral;
            }

            return gain;
        }
    }
}
