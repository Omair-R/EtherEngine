using EtherEngine.Utils.Validate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace EtherEngine.Sprite
{
    public class TextWriter
    {
        private string _fontName;
        private SpriteFont _font;

        private FlagValidator _loadValidator;

        public TextWriter(string font)
        {
            _fontName = font;
            _loadValidator = new FlagValidator("The textWritter was not loaded, please call the Load method first");
        }

        public void Load(ContentManager contentManager)
        {
            _font = contentManager.Load<SpriteFont>(_fontName);
            _loadValidator.Up();
        }

        public void Write(SpriteBatch spriteBatch, string text, Vector2 position, Color color, float rotation=0f, Vector2? origin =null, float scale=1f, float layerDepth= 1f)
        {
            _loadValidator.Check();

            Vector2 origin_ = Vector2.Zero;
            if (origin != null)
                origin_ = (Vector2)origin;

            spriteBatch.DrawString(_font, text, position, color, rotation, origin_, scale, SpriteEffects.None, layerDepth);
        }
    }
}
