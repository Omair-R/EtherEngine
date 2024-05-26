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
        Sprite texturedSprite;

        Texture2D texture;


        public void Initialize(GraphicsDevice graphicsDevice)
        {
            keyboardManager = KeyboardManager.Instance;
            texture = new Texture2D(graphicsDevice, 1, 1);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("fall");
            emittionInstruction = new EmittionInstruction()
            {
                Position = new Vector2(400, 300),
                Spread = new Vector2(100, 5),

                InitVelocity = new Vector2(0, 0),
                InitVelocityVariance = 0,

                Acceleration = new Vector2(0, -1000),
                TangentialAcceleration = new Vector2(0, 0),
                Damping = 0.2f,

                Angle = 1.5f,
                AngleVariance = 2f,
                AngularVelocity = 1,

                ScaleBegin = 30,
                ScaleEnd = 2,
                ScaleVariance = 5,


                ColorBegin = Color.Green,
                ColorEnd = Color.Red,
                HueVairance = 1,

                AlphaBegin = 0.8f,
                AlphaEnd = 0.05f,
                AlphaVariance = 0.5f,

                LifeTime = 1,
                LifeTimeVariance = 0.8f,

            };


            texturedSprite = new Sprite(texture, Vector2.One, Vector2.One);
            emitter = new ParticleEmitter(texturedSprite, 10000, 0.5f, 1000, emittionInstruction);
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
