using Microsoft.Xna.Framework;
using System;
using System.Diagnostics;

namespace EtherUtils.Model
{
    public class Timer
    {
        float _duration;
        float _remainingTime;
        bool _repeat;

        public bool IsFinished { get; private set; }
        public bool IsActive { get; private set; }

        public event EventHandler<EventArgs> JustFinished;

        public Timer(float duration, bool repeat=false)
        {
            _duration = duration;
            _remainingTime = duration;
            _repeat = repeat;

            IsFinished = false;
            IsActive = true;
            
        }
        public void Restart()
        {
            _remainingTime = _duration;
            IsActive = true;
            IsFinished = false;
        }

        public void Pause() => IsActive = false;
        public void UnPause() => IsActive = true;

        public float GetProgress() => _duration - _remainingTime;
        public float GetRemaining() => _remainingTime;

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Update(dt);
        }

        public void Update(float dt)
        {
            if (IsFinished && _repeat) Restart();

            if (!IsActive || IsFinished) return;

            _remainingTime -= dt;

            if (_remainingTime <= 0)
            {
                IsFinished = true;
                IsActive = false;
                EventUtils.Invoke(JustFinished, this, new EventArgs());
            }
        }
    }
}
