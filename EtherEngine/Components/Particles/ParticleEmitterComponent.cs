namespace EtherEngine.Components.Particles
{
    public struct Timer
    {
        public float IntervalSeconds;

        public bool IsTriggered { get; private set; }

        private float _count;

        public Timer(float intervalSeconds)
        {
            IntervalSeconds = intervalSeconds;
            _count = 0f;
            IsTriggered = false;
        }

        public void Update(float dt)
        {
            _count += dt;

            if (_count > IntervalSeconds)
            {
                _count -= IntervalSeconds;
                IsTriggered = true;
            }
            else 
                IsTriggered = false;
        }

    }
    public struct ParticleEmitterComponent
    {
        public int Amount;
        public Timer Timer;
        public bool Repeat;
    }
}
