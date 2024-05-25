using EtherEngine.Shapes;
using EtherEngine.Utils.Validate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace EtherEngine.Sprite
{
    public class Sprite
    {
        private string _textureName;
        private Texture2D _texture;

        private Rectangle _srcRect;
        private float _layerDepth;

        public Vector2 Center { get; set; }
        public Vector2 Scale { get; set; }
        public float Rotation { get; set; }
        public Color Color { get; set; }

        private float _alpha;
        public float Alpha { get => _alpha; set { 
                _alpha = value;
                Color = new Color(Color, _alpha);
            }
        }
        public SpriteEffects Effect { get; set; }

        private FlagValidator _loadValidator;

        public Flag FlippedH { get; private set; }
        public Flag FlippedV { get; private set; }

        public Sprite(string textureName, 
            Rectangle srcRect,
            Vector2 center, 
            Vector2 scale, 
            Color color, 
            float alpha=1f,
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
            Color = new Color(color,alpha);
            _layerDepth = layerDepth;
            Effect = spriteEffect;

            _loadValidator = new FlagValidator(new ContentLoadException("The sprite was not loaded yet, please load the sprite first."));

            FlippedH = new Flag();
            FlippedV = new Flag();
            FlippedH.FlagChanged += OnFlipHorizontally;
            FlippedV.FlagChanged += OnFlipVertically;
        }

        public Sprite(string textureName,
            Vector2 center,
            Vector2 scale,
            Color color,
            float alpha = 1f,
            float rotation = 0f,
            float layerDepth = 0f,
            SpriteEffects spriteEffect = SpriteEffects.None)
            : this(textureName, Rectangle.Empty, center, scale, color,alpha, rotation, layerDepth, spriteEffect)
        { }

        public Sprite(string textureName,
            Vector2 center,
            Vector2 scale,
            float alpha = 1f,
            float rotation = 0f,
            float layerDepth = 0f,
            SpriteEffects spriteEffect = SpriteEffects.None)
            : this(textureName, Rectangle.Empty, center, scale, Color.White, alpha, rotation, layerDepth, spriteEffect)
        { }

        public StaticQuad GetStaticQuad() => new StaticQuad(Center, Scale.X, Scale.Y);
        public StaticQuad GetRotatableQuad() => new RotatableQuad(Center.X, Center.Y, Scale.X,Scale.Y, Rotation);
        public Rectangle GetDestinationRectangle() => new Rectangle((int)(Center.X - Scale.X / 2), 
                                                                    (int)(Center.Y - Scale.Y / 2), 
                                                                    (int)Scale.X, 
                                                                    (int)Scale.Y);

        private void OnFlipHorizontally(object sender, EventArgs e)
        {
            if (FlippedH.GetState())
                Effect |= SpriteEffects.FlipHorizontally;
            else
                Effect = Effect & ~SpriteEffects.FlipHorizontally;
        }

        public void OnFlipVertically(object sender, EventArgs e)
        {
            if (FlippedV.GetState())
                Effect |= SpriteEffects.FlipVertically;
            else
                Effect = Effect & ~SpriteEffects.FlipVertically;
        }

        virtual public void Load(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(_textureName);
            if (_srcRect == Rectangle.Empty)
                _srcRect = new Rectangle(0, 0, _texture.Width, _texture.Height);
            _loadValidator.Up();
        }

        virtual public void Draw(SpriteBatch spriteBatch)
        {
            _loadValidator.Check();

            spriteBatch.Draw(
                _texture,
                GetDestinationRectangle(),
                _srcRect,
                Color,
                Rotation,
                new Vector2(Scale.X/2, Scale.Y/2),
                Effect,
                _layerDepth);
        }
    }
}
