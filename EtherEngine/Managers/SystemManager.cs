using EtherEngine.Core.DrawBatch;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace EtherEngine.Managers
{
    public sealed class SystemManager
    {
        private List<DrawableSystem> drawableSystems = new List<DrawableSystem>();
        private List<UpdatableSystem> updatableSystems = new List<UpdatableSystem>();

        internal SystemManager()
        {

        }

        public void AddSystem(DrawableSystem system)
        {
            drawableSystems.Add(system);
        }

        public void AddSystem(UpdatableSystem system)
        {
            updatableSystems.Add(system);
        }

        public void RemoveSystem(DrawableSystem system)
        {
            drawableSystems.Remove(system);
        }

        public void RemoveSystem(UpdatableSystem system)
        {
            updatableSystems.Remove(system);
        }
        public void Update(GameTime gameTime) //TODO: Temporary.
        {
            foreach (var system in updatableSystems)
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
