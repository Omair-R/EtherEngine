using EtherUtils.Model;
using EtherUtils.Pattern;
using Microsoft.Xna.Framework;
using System.Collections.Generic;


namespace EtherEngine.Managers
{
    public class TimerManager : LazySingleton<TimerManager>
    {
        private List<Timer> timers = new List<Timer>();

        private TimerManager() 
        { 
        }

        public bool Register(Timer timer)
        {
            try
            {
                timers.Add(timer);
            } catch
            {
                return false;
            }

            return true;
            
        }

        public bool Unregister(Timer timer) => timers.Remove(timer);
        
        public void Clear() => timers.Clear();

        public void Update(GameTime gameTime)
        {
            float fGameTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach( Timer timer in timers)
            {
                timer.Update(fGameTime);
            }
        }
    }
}
