using EtherEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sandbox
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private EtherScene _scene;
        private SpriteBatch _spriteBatch;
        //private ITestScene _scene;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            //_scene = new MotionAndInput();
            //_scene = new CollisionAndShape();
            //_scene = new TweenAndText();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //_scene.Initialize(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _scene = new TestScene(GraphicsDevice, Content, _graphics);
            // TODO: use this.Content to load your game content here
            //_scene.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _scene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _scene.Draw();
            base.Draw(gameTime);
        }
    }
}
