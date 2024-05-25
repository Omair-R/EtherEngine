using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Utils
{
    public class Counter
    {
        float _duration;
        float _remainingTime;

        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsActive { get; private set; }

        public event EventHandler<EventArgs> Finished; 


        public Counter(float duration)
        {
            _duration = duration;
            _remainingTime = duration;

            IsStarted = false;
            IsFinished = false;
            IsActive = false;
        }
        public void Start()
        {
            _remainingTime = _duration;
            IsStarted = true;
            IsActive = true;
            IsFinished = false;
        }

        public void Pause() => IsActive = false;
        public void UnPause() => IsActive = true;

        public void Update(GameTime gameTime)
        {
            Debug.Assert(IsStarted);

            
            if (!IsActive) return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _remainingTime -= dt;

            if ( _remainingTime <= 0 )
            {
                IsFinished = true;
                IsActive = false;
                EventUtils.Invoke(Finished, this, new EventArgs());
            }
        }
    }
}
