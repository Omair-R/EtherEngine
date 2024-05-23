using EtherEngine.DrawBatch;
using EtherEngine.Input;
using EtherEngine.Motion;
using EtherEngine.Shapes;
using EtherEngine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    public class MotionAndInput : ITestScene
    {
        private TexturedSprite _sprite;
        private IMotion _motion;
        private KeyboardManager keyboardManager;

        Vector2 HandleInput()
        {
            keyboardManager.Update();
            Vector2 outputDirection = Vector2.Zero;

            if (keyboardManager.IsPressed(Keys.Up))
                outputDirection.Y -= 1;

            if (keyboardManager.IsPressed(Keys.Down))
                outputDirection.Y += 1;

            if (keyboardManager.IsReleased(Keys.Left))
                outputDirection.X -= 1;

            if (keyboardManager.IsPressed(Keys.Right))
                outputDirection.X += 1;

            return outputDirection;
        }


        public void Initialize(GraphicsDevice graphicsDevice)
        {
            _sprite = new TexturedSprite(
                "Fall",
                new Vector2(60, 180),
                new Vector2(100, 100),
                Color.White);
            //_motion = new MasslessMotion(400, 1000, 0.1f);
            _motion = new PIDMotion(200f, 20, 2f, 5f, 3f);
            //_motion = new DragMotion(200f, 0.8f, DragTypes.QuadraticDrag);
            keyboardManager = KeyboardManager.GetInstance;
        }

        public void LoadContent(ContentManager content)
        {
            _sprite.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 motionDirection = HandleInput();
            _sprite.Center = _motion.MoveWithDirection(_sprite.Center, motionDirection, gameTime);

            Debug.WriteLine(_motion.ToString());
        }

        public void Draw(SpriteBatch spriteBatch, ShapeBatch _shapeBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            _sprite.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
