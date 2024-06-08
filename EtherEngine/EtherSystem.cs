using Arch.Core;
using EtherEngine.Core.DrawBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine
{
    public abstract class EtherSystem
    {
        protected readonly EtherScene _scene;
        protected EtherSystem(EtherScene scene)
        {
            _scene = scene;
        }
    }

    public interface IUpdateableSystem
    {
        void Update(in GameTime gameTime);
    }

    public interface IDrawableSystem
    {
        void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);
    }

    public abstract class UpdatableSystem : EtherSystem, IUpdateableSystem
    {
        protected UpdatableSystem(EtherScene scene) : base(scene) { }

        public abstract void Update(in GameTime gameTime);
    }

    public abstract class DrawableSystem : EtherSystem, IDrawableSystem
    {
        protected DrawableSystem(EtherScene scene) : base(scene) { }

        public abstract void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);
    }

    public abstract class UpdateableAndDrawableSystem : EtherSystem, IDrawableSystem, IUpdateableSystem
    {
        protected UpdateableAndDrawableSystem(EtherScene scene) : base(scene)
        {
        }

        public abstract void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);

        public abstract void Update(in GameTime gameTime);
    }
}
