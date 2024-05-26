using EtherEngine.DrawBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace EtherEngine
{
    internal class SystemManager
    {
        private List<DrawableSystem> drawableSystems = new List<DrawableSystem>();
        private List<UpdatableSystem> updatableSystems = new List<UpdatableSystem>();

        internal SystemManager()
        {

        }

        public void Update(GameTime gameTime) //TODO: Temporary.
        {
            foreach(var system in updatableSystems)
            {
                system.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch) //TODO: Temporary.
        {
            foreach (var system in drawableSystems)
            {
                system.Draw(spriteBatch, shapeBatch);
            }
        }

    }
}
