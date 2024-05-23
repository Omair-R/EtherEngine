using EtherEngine.Camera;
using EtherEngine.Collision;
using EtherEngine.Collision.Models;
using EtherEngine.DrawBatch;
using EtherEngine.Input;
using EtherEngine.Motion;
using EtherEngine.Shapes;
using EtherEngine.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    internal class CollisionAndShape : ITestScene
    {
        private TexturedSprite _sprite;
        private CircleCollision _spriteCollision;

        private IMotion _motion;
        private KeyboardManager keyboardManager;

        private List<IShape> shapePool;
        private List<ICollision> collisions;

        private CollisionLayer objectLayer = new CollisionLayer(new HashSet<CollisionLayer>());
        private CollisionLayer playerLayer = new CollisionLayer(new HashSet<CollisionLayer>());

        private PositionalCamera camera;
        Vector2 HandleInput()
        {
            keyboardManager.Update();
            Vector2 outputDirection = Vector2.Zero;

            if (keyboardManager.IsPressed(Keys.Up))
                outputDirection.Y -= 1;

            if (keyboardManager.IsPressed(Keys.Down))
                outputDirection.Y += 1;

            if (keyboardManager.IsPressed(Keys.Left))
                outputDirection.X -= 1;

            if (keyboardManager.IsPressed(Keys.Right))
                outputDirection.X += 1;

            return outputDirection;
        }


        public void Initialize(GraphicsDevice graphicsDevice)
        {
            _sprite = new TexturedSprite(
                "Fall",
                new Vector2(400, 180),
                new Vector2(100, 100),
                Color.White);

            _motion = new PIDMotion(200f, 20, 2f, 5f, 3f);

            shapePool = new List<IShape>();
            collisions = new List<ICollision>();

            playerLayer.CollidingLayers.Add(objectLayer);

            camera = new PositionalCamera(_sprite.Center, 0f, 1.0f, graphicsDevice);

            keyboardManager = KeyboardManager.GetInstance;
        }

        public void PrintSomething(object sender, CollisionEventArgs args)
        {
            Debug.WriteLine("yoooo");
        }
        public void LoadContent(ContentManager content)
        {
            _sprite.Load(content);


            shapePool.Add(new Circle(new Vector2(60, 180), 30));
            shapePool.Add(new StaticQuad(new Vector2(30, 60), new Vector2(120, 300)));
            shapePool.Add(new RotatableQuad(300, 120, 100, 100, 2));
            shapePool.Add(new Polygon(new Vector2[]{ new Vector2(120, 60), new Vector2(60, 300), new Vector2(300, 400) }));

            collisions.Add(new CircleCollision(new Circle(new Vector2(60, 180), 30)));
            collisions.Add(new StaticQuadCollision(new StaticQuad(new Vector2(30, 60), new Vector2(120, 300)), objectLayer));
            collisions.Add(new RotatableQuadCollision(new RotatableQuad(300, 120, 100, 100, 2), objectLayer));
            collisions.Add(new PolygonCollision(new Polygon(new Vector2[] { new Vector2(120, 60), new Vector2(60, 300), new Vector2(300, 400) }), objectLayer));

            _spriteCollision = new CircleCollision(new Circle(_sprite.Center, 30), playerLayer);
            _spriteCollision.CollisionOccured += PrintSomething;

        }

        public void Update(GameTime gameTime)
        {
            Vector2 motionDirection = HandleInput();
            _sprite.Center = _motion.MoveWithDirection(_sprite.Center, motionDirection, gameTime);

            _spriteCollision.InnerShape.Center = _sprite.Center;

            foreach (var collision in collisions)
            {
                _spriteCollision.CheckCollision(collision, out Contact contact); 
                if (contact != null)
                    _sprite.Center -= contact.penetration * contact.collisionDirection;
            }

            _spriteCollision.InnerShape.Center = _sprite.Center;

            camera.MoveTo(_sprite.Center);

            Debug.WriteLine(_motion.ToString());
        }

        public void Draw(SpriteBatch spriteBatch, ShapeBatch _shapeBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera.GetTransformation());
            _sprite.Draw(spriteBatch);
            spriteBatch.End();

            _shapeBatch.Begin(transformMatrix: camera.GetTransformation());
            foreach( IShape shape in shapePool)
            {
                _shapeBatch.DrawShape(shape, Color.BlueViolet);
            }

            _shapeBatch.DrawCircle(_spriteCollision.InnerShape, Color.HotPink, 40);

            _shapeBatch.End();
        }
    }
}
