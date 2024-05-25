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
            emittionInstruction = new EmittionInstruction()
            {
                Position = new Vector2(400, 300),
                Spread = new Vector2(100, 50),

                InitVelocity = new Vector2(0,0),
                InitVelocityVariance = 0,

                Acceleration = new Vector2(0, -1000),
                TangentialAcceleration = new Vector2(500, -500),
                Damping = 0.1f,

                Angle = 1.5f,
                AngleVariance = 2f,
                AngularVelocity = 1,

                ScaleBegin = 20,
                ScaleEnd = 5,
                ScaleVariance = 5,


                ColorBegin = Color.Yellow,
                ColorEnd = Color.Red,
                HueVairance = 1,

                AlphaBegin = 0.7f,
                AlphaEnd = 0.05f,
                AlphaVariance = 0.3f,

                LifeTime = 2,
                LifeTimeVariance = 0.8f,

            };

            texturedSprite = new TexturedSprite("Fall", Vector2.One,Vector2.One);
            emitter = new ParticleEmitter(texturedSprite, 50, 0.1f, 150, emittionInstruction);
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
