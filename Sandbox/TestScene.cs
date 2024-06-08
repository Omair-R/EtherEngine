using EtherEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using EtherEngine.Core.Collision.Models;
using EtherEngine.Managers;
using EtherEngine.Core.Motion;
using EtherEngine.Components;
using EtherEngine.Systems;
using EtherEngine.Systems.Collision;
using EtherEngine.Core.Shapes;
using EtherEngine.Systems.Motion;
using EtherEngine.Components.Graphics;

namespace Sandbox
{
    public class TestScene : EtherScene
    {
        private CollisionLayer objectLayer = new CollisionLayer(new HashSet<CollisionLayer>());
        private CollisionLayer playerLayer = new CollisionLayer(new HashSet<CollisionLayer>());

        public TestScene(GraphicsDevice graphicsDevice,
                         ContentManager contentManager,
                         GraphicsDeviceManager graphicsDeviceManager) : base(graphicsDevice, contentManager, graphicsDeviceManager)
        {
            _systemManager.AddSystem(new AnimationSystem(this));
            _systemManager.AddSystem(new SpriteSystem(this));
            _systemManager.AddSystem(new InputSystem(this));
            _systemManager.AddSystem(new DragDriveSystem(this));
            _systemManager.AddSystem(new MotionSystem(this));

            _systemManager.AddSystem(new GravitySystem(this));
            _systemManager.AddSystem(new CollisionSystem(this));

            EtherEntity spriteEntity = _entityManager.MakeEntity();
            _ = new Texture2D(graphicsDevice, 100, 100);

            Texture2D playerTexture = contentManager.Load<Texture2D>("fall");
            Texture2D animatedTexture = contentManager.Load<Texture2D>("Idle");

            var animationManager = AnimationManager.Instance;

            animationManager.Register(spriteEntity);

            animationManager.Add(spriteEntity,
                                 new SpriteComponent(animatedTexture),
                                 new SpriteAnimationComponent("idle", 11, 1, 0, 0, 0.1f));

            spriteEntity.AddComponent(new TransformComponent { Position = new Vector2(30, 100), Scale = new Vector2(100, 100), Rotation = 0f});
            spriteEntity.AddComponent(new ColorComponent { Color = Color.White });

            spriteEntity.AddComponent(new MotionComponent { Velocity = Vector2.Zero });
            spriteEntity.AddComponent(new DragDriveComponent { MaxVelocity = 200, 
                                                                DragType = DragTypes.StokesDrag, 
                                                                ReachTime = 1 });
            spriteEntity.AddComponent(new InputComponent { InputDirection = Vector2.Zero });

            //spriteEntity.AddComponent(new GravityComponent { Acceleration = Vector2.One * 980 });

            playerLayer.CollidingLayers.Add(objectLayer);

            spriteEntity.AddComponent(new ColliderComponent
            {
                Enable = true,
                Layer = playerLayer
            });
            spriteEntity.AddComponent(new ColliderShapeComponent { Shape = new Circle(spriteEntity.GetComponent<TransformComponent>().Position, 70) });


            EtherEntity obsticle = _entityManager.MakeEntity();
            obsticle.AddComponent(new SpriteComponent(playerTexture));
            obsticle.AddComponent(new TransformComponent { Position = new Vector2(300, 250), Scale = new Vector2(200, 200), Rotation = 0f });
            obsticle.AddComponent(new ColorComponent { Color = Color.Green });

            obsticle.AddComponent(new ColliderComponent 
            { 
                Enable = true,
                Layer = objectLayer
            });
            obsticle.AddComponent(new ColliderShapeComponent { Shape = new Circle(obsticle.GetComponent<TransformComponent>().Position, 60) });
        }


    }
}
