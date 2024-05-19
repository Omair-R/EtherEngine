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

        public void Write(string text, Vector2 position, Color color, SpriteBatch spriteBatch)
        {
            _loadValidator.Check();
            spriteBatch.DrawString(_font, text, position, color);
        }
    }
}
