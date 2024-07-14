using Arch.Core;
using EtherEngine.Core.DrawBatch;
using EtherEngine.Core.Motion.Drag;
using EtherUtils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine.Systems
{
    public abstract class EtherSystem
    {
        protected readonly EtherScene _scene;

        protected QueryDescription queryDescription;

        public EventHandler Before;

        public EventHandler After;

        public bool IsPaused { get; set; }

        public bool TriggersEvents { get; set; }

        protected EtherSystem(EtherScene scene)
        {
            _scene = scene;
        }

        public void Without<T>() where T : struct
        {
            queryDescription.WithNone<T>();
        }

        
    }

    public interface IEventSystem
    {

        void Handle();
    }

    public interface IUpdateableSystem
    {
        void Update(GameTime gameTime);

        void _Update(GameTime gameTime);
    }

    public interface IDrawableSystem
    {
        void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);

        void _Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);
    }

    public abstract class EventSystem : EtherSystem, IEventSystem
    {

        protected EventSystem(EtherScene scene) : base(scene) { }

        public abstract void Handle();
    }

    public abstract class UpdatableSystem : EtherSystem, IUpdateableSystem
    {
        protected UpdatableSystem(EtherScene scene) : base(scene) { }

        public abstract void Update(GameTime gameTime);

        public virtual void _Update(GameTime gameTime)
        {
            EventUtils.Invoke(Before, this, new EventArgs());
            Update(gameTime);
            EventUtils.Invoke(After, this, new EventArgs());
        }
    }

    public abstract class DrawableSystem : EtherSystem, IDrawableSystem
    {
        protected DrawableSystem(EtherScene scene) : base(scene) { }

        public abstract void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);

        public virtual void _Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch)
        {
            EventUtils.Invoke(Before, this, new EventArgs());
            Draw(spriteBatch, shapeBatch);
            EventUtils.Invoke(After, this, new EventArgs());
        }
    }

}
