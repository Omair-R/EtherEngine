using Arch.Core;
using EtherEngine.Core.DrawBatch;
using EtherEngine.Entities;
using EtherEngine.Managers;
using EtherEngine.Systems;
using EtherEngine.Systems.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine
{
    public abstract class EtherScene
    {
        public Game Game { get; init; }
        public EntityManager EntityManager { get; init; }
        public ContentManager ContentManager { get; init; }
        public GraphicsDevice GraphicsDevice { get; init; }
        public ShapeBatch ShapeBatch { get; protected set; }
        public SpriteBatch SpriteBatch { get; protected set; }
        public CameraEntity MainCamera {  get; set; }
        public bool IsPaused { get; private set; }

        protected readonly SystemManager _systemManager;

        public event EventHandler BeforeDraw;
        public event EventHandler AfterDraw;
        public event EventHandler BeforeUpdate;
        public event EventHandler AfterUpdate;

        protected EtherScene(Game game)
        { 
            Game = game;

            EntityManager = new EntityManager(this); //TODO: Kill all managers.

            ContentManager = Game.Content;
            GraphicsDevice = Game.GraphicsDevice;

            SpriteBatch = new SpriteBatch(GraphicsDevice);
            ShapeBatch  = new ShapeBatch(GraphicsDevice);

            _systemManager = new SystemManager();

        }
        public virtual void Initialize()
        {
        }

        public virtual void LoadContent()
        {
        }

        public virtual void UnloadContent()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
            _systemManager.Update(gameTime);
        }

        public virtual void Draw()
        {
            _systemManager.Draw(SpriteBatch, ShapeBatch);
        }

        public void TriggerEvent<T>(in T eventComponent, EtherSystem senderSystem) where T : struct, IEvent
        {
            senderSystem.TriggersEvents = true;
            EntityManager.Registry.Add(eventComponent.Sender, eventComponent);
        }

    }
}
