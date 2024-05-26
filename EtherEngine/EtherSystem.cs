using Arch.Core;
using EtherEngine.DrawBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine
{
    public abstract class EtherSystem
    {
        protected readonly World _world;
        protected EtherSystem(World world)
        {
            _world = world;
        }
    }

    public abstract class UpdatableSystem : EtherSystem
    {
        protected UpdatableSystem(World world) : base(world) { }

        public abstract void Update(GameTime gameTime);
    }

    public abstract class DrawableSystem : EtherSystem
    {
        protected DrawableSystem(World world) : base(world) { }

        public abstract void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch);
    }
}
