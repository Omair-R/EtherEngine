using EtherEngine.DrawBatch;
using EtherEngine.Input;
using EtherEngine.Particle;
using EtherEngine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sandbox
{
    public class ParticleTest : ITestScene
    {
        ParticleEmitter emitter;
        EmittionInstruction emittionInstruction;
        KeyboardManager keyboardManager;
        TexturedSprite texturedSprite;
        
        public void Initialize(GraphicsDevice graphicsDevice)
        {
            keyboardManager = KeyboardManager.GetInstance;
            emittionInstruction = new EmittionInstruction(
                new Vector2(300, 300),
                new Vector2(150, 100),
                new Vector2(0, 0),
                0,
                new Vector2(-50, 0),
                0,
                2f,
                0.5f,
                1f,
                20,
                5,
                5,
                Color.White,
                Color.Green,
                2f,
                0.9f,
                0.05f,
                0.1f,
                1,
                0.5f);
            texturedSprite = new TexturedSprite("Fall", Vector2.One,Vector2.One);
            emitter = new ParticleEmitter(texturedSprite, 5, 0.1f, 150, emittionInstruction);
        }

        public void LoadContent(ContentManager content)
        {
            texturedSprite.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            keyboardManager.Update();
            if (keyboardManager.IsPressed(Keys.Up))
                emitter.Emit();
            emitter.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch, ShapeBatch _shapeBatch)
        {
            spriteBatch.Begin();
            emitter.Draw(spriteBatch);
            spriteBatch.End();
        }

    }
}
