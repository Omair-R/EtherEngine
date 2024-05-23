using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using EtherEngine.DrawBatch;
using Microsoft.Xna.Framework.Content;

namespace Sandbox
{
    public interface ITestScene
    {
        public void Initialize(GraphicsDevice graphicsDevice);

        public void LoadContent(ContentManager content);

        public void Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch, ShapeBatch _shapeBatch);

    }
}
