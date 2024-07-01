using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace EtherEngine.Components.Graphics
{
    public struct SpriteComponent
    {
        public Texture2D Texture;
        public Rectangle SrcRect = Rectangle.Empty;
        public float LayerDepth = 0f;
        public SpriteEffects Effect = SpriteEffects.None;
        public float Alpha = 1f;

        public SpriteComponent(Texture2D texture)
        {
            Texture = texture;
            if (SrcRect == Rectangle.Empty)
                SrcRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
        }

        public SpriteComponent(Texture2D texture,
                               Rectangle srcRectangle)
        {
            Texture = texture;
            SrcRect = srcRectangle;
        }

        public void FlipHorizontally()
        {
            Effect ^= SpriteEffects.FlipHorizontally;
        }

        public void FlipVertically()
        {
            Effect ^= SpriteEffects.FlipVertically;
        }
    }
}
