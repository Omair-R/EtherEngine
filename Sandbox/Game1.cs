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
    public class Game1 : EtherGame
    {
        private GraphicsDeviceManager _graphics;

        private EtherGui _etherGui;
        private GuiBatch _guiBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            
        }

        protected override void Initialize()
        {
            _etherGui = new EtherGui(GraphicsDevice, Window);
            _guiBatch = new GuiBatch(_etherGui);
            CurrentScene = new TestScene(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            CurrentScene.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            CurrentScene.Draw();

            _guiBatch.Begin(gameTime);

            ImGuiLayout();

            _guiBatch.End();
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
