using EtherEngine.Utils.Validate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Components.Graphics
{
    public struct SpriteComponent
    {
        public Texture2D Texture;
        public Rectangle SrcRect = Rectangle.Empty;
        public float LayerDepth = 0f;
        public SpriteEffects Effect = SpriteEffects.None;

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
