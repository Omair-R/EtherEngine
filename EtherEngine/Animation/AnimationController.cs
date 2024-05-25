using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using Microsoft.Xna.Framework.Content;

namespace EtherEngine.Animation
{
    public class AnimationController
    {
        private string _textureName;
        private Texture2D _texture;
        private SpriteEffect _spriteEffect;
        private Dictionary<string, Animation> _animationDict;
        private Animation _currentAnimation;

        public AnimationController(string textureName, SpriteEffect spriteEffect=null)
        {
            _textureName = textureName;
            _spriteEffect = spriteEffect;
            _animationDict = new Dictionary<string, Animation>();
        }

        public void Load(ContentManager contentManager)
        {
            _texture = contentManager.Load<Texture2D>(_textureName);
        }

        public void Add(string name, Animation animation)
        {
            _animationDict.Add(name, animation);
            _currentAnimation = _animationDict[name];
        }

        public Animation Remove(string name)
        {
            Animation removedAnimation = _animationDict[name];
            _animationDict.Remove(name);
            return removedAnimation;
        }

        public void Play(string name)
        {
            //TODO: Add safegaurd 
            Debug.Assert(_animationDict.ContainsKey(name));
            _currentAnimation = _animationDict[name];
            _currentAnimation.Start();
        }

        public void Update(GameTime gameTime)
        {
            if (_currentAnimation == null) {
                throw new ArgumentNullException("The controller has no animations added, please add an animation, then call the Play method.");
            }
            _currentAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) //FIXME: Temporary
        {
            _currentAnimation.Draw(spriteBatch, position);
        }
    }
}
