using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolMono.Tween
{
    public delegate float TweenFunc(float t, float duration);
    
    public abstract class Tween<T>
    {
        public abstract T From { get; protected set; }
        public abstract T To { get; protected set; }
        public float Duration { get; protected set; }

        public readonly TweenType type;

        protected TweenFunc tweenFunc;
        protected DateTime staringTime;
        protected T changeValue;


        public bool IsStarted { get; protected set; }
        public bool IsFinished { get; protected set; }

        public Tween(TweenType tweenType)
        {
            this.tweenFunc = TweenMenu.MapTweenFunc(tweenType);

            this.IsStarted = false;
            this.IsFinished = false;
        }

        public virtual void Start(T from, T to, float duration)
        {
            this.From = from;
            this.To = to;
            this.Duration = duration;
            this.staringTime = DateTime.Now;

            IsStarted = true;
            IsFinished = false;
        }

        protected void PrepareTime(out float t)
        {
            t = (float)(DateTime.Now - this.staringTime).TotalMilliseconds * 0.001f;

            if (t > Duration)
            {
                t = Duration;
                IsStarted = false;
                IsFinished = true;
            }     
        }

        public abstract T Update();
    }


    public class TweenF : Tween<float>
    {
        public override float From { get; protected set; }
        public override float To { get; protected set; }

        public TweenF(TweenType tweenType)
            :base(tweenType) {}

        public override void Start(float from, float to, float duration)
        {
            base.Start(from, to, duration);
            this.changeValue = this.To - this.From;
        }
        public override float Update()
        {
            if (!IsStarted || IsFinished) return From; 
            PrepareTime(out float t);
            return this.changeValue * tweenFunc(t, Duration) + this.From;
        }
    }

    public class TweenV : Tween<Vector2>
    {
        public override Vector2 From { get; protected set; }
        public override Vector2 To { get; protected set; }

        public TweenV(TweenType tweenType)
            : base(tweenType) { }

        public override void Start(Vector2 from, Vector2 to, float duration)
        {
            base.Start(from, to, duration);
            this.changeValue = this.To - this.From;
        }

        public override Vector2 Update()
        {
            if (!IsStarted || IsFinished) return From;
            PrepareTime(out float t);
            return this.changeValue * tweenFunc(t, Duration) + From;
        }
    }


    public class TweenColor : Tween<Color>
    {
        public override Color From { get; protected set; }
        public override Color To { get; protected set; }

        private Vector4 fromColor;
        private Vector4 toColor;
        private Vector4 changeColor;

        public TweenColor(TweenType tweenType)
            : base(tweenType) {
            fromColor = Vector4.Zero;
            toColor = Vector4.Zero;
            changeColor = Vector4.Zero;
        }

        public override void Start(Color from, Color to, float duration)
        {
            base.Start(from, to, duration);

            from.Deconstruct(out fromColor.X, out fromColor.Y, out fromColor.Z);
            to.Deconstruct(out toColor.X, out toColor.Y, out toColor.Z);

            changeColor = toColor - fromColor;

        }

        public override Color Update()
        {
            if (!IsStarted || IsFinished) return From;
            PrepareTime(out float t);

            Vector4 result = changeColor * tweenFunc(t, Duration) +fromColor;
            result = Vector4.Clamp(result, Vector4.Zero, Vector4.One);
            return new Color(result.X, result.Y, result.Z, 1);
        }
    }

}
