using EtherEngine.Core.DrawBatch;
using EtherEngine.Systems;
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
        private List<EventSystem> eventSystems = new List<EventSystem>();

        internal SystemManager()
        {

        }
        public void AddSystem(EventSystem system)
        {
            eventSystems.Add(system);
        }

        public void AddSystem(DrawableSystem system)
        {
            drawableSystems.Add(system);
        }

        public void AddSystem(UpdatableSystem system)
        {
            updatableSystems.Add(system);
        }
        public void RemoveSystem(EventSystem system)
        {
            eventSystems.Remove(system);
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
                if (!system.IsPaused)
                    system._Update(gameTime);
                if (system.TriggersEvents)
                    foreach (var eventSystem in eventSystems)
                        eventSystem.Handle();
            }
        }

        public void Draw(SpriteBatch spriteBatch, ShapeBatch shapeBatch) //TODO: Temporary.
        {
            foreach (var system in drawableSystems)
            {
                if (!system.IsPaused)
                    system._Draw(spriteBatch, shapeBatch);
                if (system.TriggersEvents)
                    foreach (var eventSystem in eventSystems)
                        eventSystem.Handle();
            }
        }

    }
}
