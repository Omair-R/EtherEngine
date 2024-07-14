using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine
{
    public abstract class EtherGame : Game
    {
        public EtherScene CurrentScene { get; protected set;  }

        public EtherGame()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            CurrentScene?.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            CurrentScene?.LoadContent();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            CurrentScene?.UnloadContent();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }


    }
}
