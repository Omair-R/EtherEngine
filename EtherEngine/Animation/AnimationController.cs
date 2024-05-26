using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using Microsoft.Xna.Framework.Content;
using EtherEngine.Utils.Validate;

namespace EtherEngine.Animation
{
    public class AnimationController
    {
        private Texture2D _texture;
        private SpriteEffect _spriteEffect;
        private Dictionary<string, Animation> _animationDict;
        private Animation _currentAnimation;

        public AnimationController(Texture2D texture, SpriteEffect spriteEffect=null)
        {
            _texture = texture;
            _spriteEffect = spriteEffect;
            _animationDict = new Dictionary<string, Animation>();
        }

        public void Add(string name, int horizontalFrameCount, int verticalFrameCount, int row, float frameDuration)
        {
            Animation animation = new Animation(_texture, horizontalFrameCount, verticalFrameCount, row, frameDuration, _spriteEffect);
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
