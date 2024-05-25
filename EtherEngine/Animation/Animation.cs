using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace EtherEngine.Animation
{
    public class Animation
    {
        private Texture2D _texture;

        private int _frameCount;
        private float _frameDuration;

        private int _currentFrame;
        private float _remainingTime;

        private Rectangle[] _sourceRectangles;

        private bool _isPaused;

        public Animation(Texture2D texture, int horizontalFrameCount, int verticalFrameCount, int row, float frameDuration, SpriteEffect spriteEffect = null) {
            _texture = texture;
            _currentFrame = 0;
            _frameDuration = frameDuration;
            _frameCount = horizontalFrameCount;
            _isPaused = false;

            Vector2 frameSize = new Vector2(texture.Width / horizontalFrameCount,
                                            texture.Height / verticalFrameCount);

            _sourceRectangles = new Rectangle[horizontalFrameCount];


            for ( int i = 0; i < horizontalFrameCount; i++ )
            {
                _sourceRectangles[i] = new Rectangle(
                        i * (int)frameSize.X, row * (int)frameSize.Y, (int)frameSize.X, (int)frameSize.Y);
            }

            _remainingTime = frameDuration;
        }

        public void Reset()
        {
            _currentFrame = 0;
            _remainingTime = _frameDuration;
        }

        public void Start()
        {
            Reset();
            _isPaused = false;
        }

        public void Pause() => _isPaused = true;
        public void UnPause() => _isPaused = false;

        public void Update(GameTime gameTime)
        {
            if (_isPaused) return;

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _remainingTime -= dt;

            if (_remainingTime <= 0)
            {
                _currentFrame = (_currentFrame + 1) % _frameCount;
                _remainingTime += _frameDuration;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position) //FIXME: Temporary
        {
            spriteBatch.Draw(_texture, position, _sourceRectangles[_currentFrame], Color.White);
        }


    }
}
