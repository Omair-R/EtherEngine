using EtherEngine.Camera;
using EtherEngine.Collision.Models;
using EtherEngine.Collision;
using EtherEngine.DrawBatch;
using EtherEngine.Input;
using EtherEngine.Motion;
using EtherEngine.Shapes;
using EtherEngine.Sprite;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolMono.Tween;
using static System.Net.Mime.MediaTypeNames;

namespace Sandbox
{
    public class TweenAndText: ITestScene
    {


        private List<IShape> shapePool;
        TextWriter writer;
        TextSprite text;
        TweenF tweenF;


        public void Initialize(GraphicsDevice graphicsDevice)
        {
            shapePool = new List<IShape>();
            writer = new TextWriter("ourFont");
            text = new TextSprite("Yoooo", "ourFont", new Vector2(10, 10), Color.BurlyWood);
            tweenF = new TweenF(TweenType.Linear);
            
        }
        public void LoadContent(ContentManager content)
        {

            shapePool.Add(new Circle(new Vector2(60, 180), 30));
            shapePool.Add(new StaticQuad(new Vector2(30, 60), new Vector2(120, 300)));
            shapePool.Add(new RotatableQuad(300, 120, 100, 100, 0));
            shapePool.Add(new Polygon(new Vector2[] { new Vector2(120, 60), new Vector2(60, 300), new Vector2(300, 400) }));


            writer.Load(content);
            text.Load(content);
        }

        public void Update(GameTime gameTime)
        {
            var rot = new RotatableQuad(300, 120, 100, 100, 0);
            if (tweenF.IsFinished || !tweenF.IsStarted)
                tweenF.Start(0, MathF.PI, 5);
            rot.Rotation = tweenF.Update(gameTime);
            shapePool[2] = rot;

            if (tweenF.IsFinished)
                text.Text = "Is finished :3";
        }

        public void Draw(SpriteBatch spriteBatch, ShapeBatch _shapeBatch)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            //writer.Write("Hello", new Vector2(300, 100), Color.Black, spriteBatch);
            text.Draw(spriteBatch);
            spriteBatch.End();

            _shapeBatch.Begin();
            foreach (IShape shape in shapePool)
            {
                _shapeBatch.DrawShape(shape, Color.BlueViolet);
            }
            _shapeBatch.End();
        }
    }
}
