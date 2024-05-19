using EtherEngine.Utils.Validate;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace EtherEngine.Sprite
{
    public class TextSprite
    {

        private FlagValidator _loadValidator;
        public Color Color { get; set; }
        public Vector2 Position { get; set; }
        public string Text { get; set; }

        private TextWriter _writer;

        public TextSprite(string text, string font, Vector2 position, Color color)
        {
            Text = text;
            Position = position;
            Color = color;

            _writer = new TextWriter(font);

            _loadValidator = new FlagValidator("The TextSprite was not loaded, please call the Load method first");
        }

        //A constructor that has the textwriter preloaded,
        //recommended to save memory, but involves keeping track of the memeory of the writer.
        public TextSprite(string text, TextWriter writer, Vector2 position, Color color)
        {
            Text = text;
            Position = position;
            Color = color;

            _writer = writer;

            _loadValidator = new FlagValidator("The TextSprite was not loaded, please call the Load method first");
            _loadValidator.Up();
        }

        public void Load(ContentManager contentManager)
        {
            _writer.Load(contentManager);
            _loadValidator.Up();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _loadValidator.Check();
            _writer.Write(Text, Position,Color, spriteBatch);
        }
    }
}
