using EtherEngine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace EtherEngine.Entity
{
    public interface IEntity: IDrawable, IUpdateable, IDisposable { }

    //Reimplementation of the GameComponent with some edits.
    public abstract class EtherEntity : DrawableGameComponent, IEntity
    {
        public SpriteBatch SpriteBatch { get; private set; }

        public EtherEntity(Game game, SpriteBatch spriteBatch) : base(game)
        {
            this.SpriteBatch = spriteBatch;
        }

        ~EtherEntity()
        {
            Dispose(false);
        }

        protected override abstract void Dispose(bool disposing);
        public override abstract void Update(GameTime gameTime);
        public override void Draw(GameTime gameTime)
        {
            if (SpriteBatch == null) throw new ArgumentNullException("There is no SpriteBatch to assist with drawing." +
                "If you utilize a different method of drawing," +
                "please override the Draw method not just _Draw.");
            _Draw(gameTime);
        }
        protected abstract void _Draw(GameTime gameTime);
    }


}
