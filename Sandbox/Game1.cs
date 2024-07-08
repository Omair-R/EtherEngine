using EtherEngine;
using EtherEngine.LDTK;
using EtherGUI;
using ImGuiNET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace Sandbox
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private EtherScene _scene;
        private SpriteBatch _spriteBatch;
        //private ITestScene _scene;

        private EtherGui _etherGui;
        private GuiBatch _guiBatch;

        private LdtkRenderer _ldtkRenderer;
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

            _etherGui = new EtherGui(GraphicsDevice, Window);
            _guiBatch = new GuiBatch(_etherGui);

            //_scene.Initialize(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _scene = new TestScene(GraphicsDevice, Content, _graphics);

            LdtkJson json = Content.Load<LdtkJson>("test");
            _ldtkRenderer = new LdtkRenderer(json, Content, GraphicsDevice, _spriteBatch);
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
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            _ldtkRenderer.Draw(_scene.MainCamera);

            _guiBatch.Begin(gameTime);

            // Draw our UI
            ImGuiLayout();

            // Call AfterLayout now to finish up and draw all the things
            _guiBatch.End();


            _scene.Draw();
            base.Draw(gameTime);
        }

        protected virtual void ImGuiLayout()
        {
            ImGuiStylePtr style = ImGui.GetStyle();

            style.Alpha = 1.0f;

            _etherGui.FontStore.UseFont("archivo");

            ImGui.Begin("AnotherWindow", ImGuiWindowFlags.NoSavedSettings);
            ImGui.Text("Hello");
            ImGui.Text(string.Format("Application average {0:F3} ms/frame ({1:F1} FPS)", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));
            ImGui.End();

            //bool show_test_window = true;
            //ImGui.SetNextWindowPos(new ImVec2(650, 20), ImGuiCond.FirstUseEver);
            //ImGui.ShowDemoWindow(ref show_test_window);

            _etherGui.FontStore.PopFont();
        }
    }
}
