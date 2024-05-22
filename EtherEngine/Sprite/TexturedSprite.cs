using EtherEngine.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace EtherEngine.Sprite
{
    public class TexturedSprite
    {
        private string _textureName;
        private Texture2D _texture;

        private Rectangle _srcRect;
        private float _layerDepth;

        public Vector2 Center { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }
        public SpriteEffects Effect { get; set; }

        private bool isLoaded;
        private bool isFlipped;

        public TexturedSprite(string textureName, 
            Rectangle srcRect,
            Vector2 center, 
            Vector2 scale, 
            Color color, 
            float rotation=0f, 
            float layerDepth = 0f,
            SpriteEffects spriteEffect= SpriteEffects.None) 
        {
            if (string.IsNullOrEmpty(textureName))
            {
                throw new ArgumentException($"'{nameof(textureName)}' cannot be null or empty.", nameof(textureName));
            }

            _textureName = textureName;
            _srcRect = srcRect;
            Center = center;
            Scale = scale;
            Rotation = rotation;
            Color = color;
            _layerDepth = layerDepth;
            Effect = spriteEffect;

            isLoaded = false;
            isFlipped = false;

        }

        public TexturedSprite(string textureName,
            Vector2 center,
            Vector2 scale,
            Color color,
            float rotation = 0f,
            float layerDepth = 0f,
            SpriteEffects spriteEffect = SpriteEffects.None)
            : this(textureName, Rectangle.Empty, center, scale, color,rotation, layerDepth, spriteEffect)
        { }

        public TexturedSprite(string textureName,
            Vector2 center,
            Vector2 scale, 
            float rotation = 0f, 
            float layerDepth = 0f,
            SpriteEffects spriteEffect = SpriteEffects.None)
            : this(textureName, Rectangle.Empty, center, scale, Color.White, rotation, layerDepth, spriteEffect)
        { }

        public StaticQuad GetStaticQuad() => new StaticQuad(Center, Scale.X, Scale.Y);
        public StaticQuad GetRotatableQuad() => new RotatableQuad(Center.X, Center.Y, Scale.X,Scale.Y, Rotation);
        public Rectangle GetDestinationRectangle() => new Rectangle((int)(Center.X - Scale.X / 2), 
                                                                    (int)(Center.Y - Scale.Y / 2), 
                                                                    (int)Scale.X, 
                                                                    (int)Scale.Y);
        public void FlipHorizontally()
        {
            if (isFlipped)
                Effect = SpriteEffects.None;
            else
            {
                Effect = SpriteEffects.FlipHorizontally;
                isFlipped = true;
            }    
        }
            
        virtual public void Load(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(_textureName);
            if (_srcRect == Rectangle.Empty)
                _srcRect = new Rectangle(0, 0, _texture.Width, _texture.Height);
            isLoaded = true;
        }

        virtual public void Draw(GraphicsResource spriteBatch)
        {
            Debug.Assert(isLoaded);

            var _spriteBatch = spriteBatch as SpriteBatch;

            Debug.Assert(_spriteBatch != null);

            _spriteBatch.Draw(
                _texture,
                GetDestinationRectangle(),
                _srcRect,
                Color,
                Rotation,
                new Vector2(0, 0),
                Effect,
                _layerDepth);
        }
    }
}
