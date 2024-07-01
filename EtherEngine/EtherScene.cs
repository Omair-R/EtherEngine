using Arch.Core;
using EtherEngine.Core.DrawBatch;
using EtherEngine.Entities;
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

        public readonly SpriteBatch spriteBatch;
        public readonly ShapeBatch shapeBatch;
        public readonly GraphicsDeviceManager graphicsDeviceManager;
        public readonly ContentManager contentManager;

        public readonly EntityManager entityManager;
        protected readonly SystemManager _systemManager;

        public readonly GraphicsDevice _graphicsDevice;

        public CameraEntity MainCamera {  get; set; }

        public bool IsPaused { get; private set; }

        protected EtherScene(GraphicsDevice graphicsDevice,
                             ContentManager contentManager,
                             GraphicsDeviceManager graphicsDeviceManager)
        { 
            _world = World.Create(); //TODO: move this to manager
            entityManager = new EntityManager(this); //TODO: Kill all managers.
            this.graphicsDeviceManager = graphicsDeviceManager;
            spriteBatch = new SpriteBatch(graphicsDevice);
            shapeBatch = new ShapeBatch(graphicsDevice);
            this.contentManager = contentManager;

            _graphicsDevice = graphicsDevice;

            _systemManager = new SystemManager();

            EtherWorld world = new EtherWorld();
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
            _systemManager.Draw(spriteBatch, shapeBatch);
        }


    }
}
