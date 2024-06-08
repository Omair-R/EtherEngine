using Arch.Core;
using EtherEngine.Core.DrawBatch;
using EtherEngine.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine
{
    public abstract class EtherScene
    {
        internal readonly World _world;

        public readonly SpriteBatch _spriteBatch;
        public readonly ShapeBatch _shapeBatch;
        public readonly GraphicsDeviceManager _graphicsDeviceManager;
        public readonly ContentManager _contentManager;

        public readonly EntityManager _entityManager;
        protected readonly SystemManager _systemManager;

        public bool IsPaused { get; private set; }

        protected EtherScene(GraphicsDevice graphicsDevice,
                             ContentManager contentManager,
                             GraphicsDeviceManager graphicsDeviceManager)
        { 
            _world = World.Create(); //TODO: move this to manager
            _entityManager = new EntityManager(this);
            _graphicsDeviceManager = graphicsDeviceManager;
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _shapeBatch = new ShapeBatch(graphicsDevice);
            _contentManager = contentManager;

            _systemManager = new SystemManager();
        }
        protected virtual bool BeginDraw() 
        {
            return true;
        }

        protected virtual void EndDraw() 
        {
        }

        protected virtual void BeginRun()
        {
        }

        protected virtual void EndRun()
        {
        }

        public virtual void Initialize()
        {
        }

        protected virtual void LoadContent()
        {
        }

        protected virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            _systemManager.Update(gameTime);
        }

        public virtual void Draw()
        {
            _systemManager.Draw(_spriteBatch, _shapeBatch);
        }


    }
}
