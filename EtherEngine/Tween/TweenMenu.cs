using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolMono.Tween
{
    public enum TweenType
    {
        Linear,
        EaseInQuad,
        EaseOutQuad,
        EaseInOutQuad,
    }

    static public class TweenMenu
    {

        public static TweenFunc MapTweenFunc(TweenType tweenType)
        {
            switch (tweenType)
            {
                case TweenType.Linear: return Linear;
                case TweenType.EaseInQuad: return EaseInQuad;
                case TweenType.EaseOutQuad: return EaseOutQuad;
                case TweenType.EaseInOutQuad: return EaseInOutQuad;
            }

            throw new NotImplementedException();
        }


        public static float Linear(float t, float duration)
        {
            return t/duration;
        }


        public static float EaseInQuad(float t, float duration)
        {
            t /= duration;
            return t * t;
        }


        public static float EaseOutQuad(float t, float duration)
        {
            t /= duration;
            return -t * (t-2);
        }

        public static float EaseInOutQuad(float t, float duration)
        {
            t /= duration/2;
            
            if (t < 1)//??
                return t * t /2;

            t -= 2;
            return -(t * t - 2) /2;
        }



    }
}
