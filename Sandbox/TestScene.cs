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
using EtherEngine.Entities;
using EtherEngine.Components.Relations;
using EtherEngine.LDTK;
using EtherEngine.LDTK.ECS.Systems;
using EtherEngine.Systems.Event;
using EtherEngine.Components.Particles;

namespace Sandbox
{
    public class TestScene : EtherScene
    {
        private CollisionLayer objectLayer = new CollisionLayer(new HashSet<CollisionLayer>());
        private CollisionLayer playerLayer = new CollisionLayer(new HashSet<CollisionLayer>());

        LdtkLoader ldtkLoader;

        public TestScene(Game game) : base(game)
        {
            _systemManager.AddSystem(new RenderedLayerSystem(this));
            _systemManager.AddSystem(new ParticlesEmitterSystem(this));
            _systemManager.AddSystem(new ParticleSystem(this));
            _systemManager.AddSystem(new FollowSystem(this));
            _systemManager.AddSystem(new AnimationSystem(this));
            _systemManager.AddSystem(new SpriteSystem(this));
            _systemManager.AddSystem(new InputSystem(this));
            _systemManager.AddSystem(new DragDriveSystem(this));
            _systemManager.AddSystem(new PIDDriveSystem(this));
            _systemManager.AddSystem(new MotionSystem(this));
            _systemManager.AddSystem(new KillEntitiyEventSystem(this));
            _systemManager.AddSystem(new GravitySystem(this));
            _systemManager.AddSystem(new CollisionGizmoSystem(this));
            _systemManager.AddSystem(new CollisionSystem(this));


            EtherEntity spriteEntity = EntityManager.MakeEntity();
            _ = new Texture2D(Game.GraphicsDevice, 100, 100);

            Texture2D playerTexture = Game.Content.Load<Texture2D>("fall");
            Texture2D animatedTexture = Game.Content.Load<Texture2D>("Idle");

            var animationManager = AnimationManager.Instance;

            animationManager.Register(spriteEntity);

            animationManager.Add(spriteEntity,
                                 new SpriteComponent(animatedTexture),
                                 new SpriteAnimationComponent("idle", 11, 1, 0, 0, 0.1f));

            spriteEntity.AddComponent(new TransformComponent { Position = new Vector2(50, 150), Scale = new Vector2(1, 1), Rotation = 0f});
            spriteEntity.AddComponent(new ColorComponent { Color = Color.White });

            spriteEntity.AddComponent(new MotionComponent { Velocity = Vector2.Zero });
            //spriteEntity.AddComponent(new DragDriveComponent { MaxVelocity = 64, 
            //                                                    DragType = DragTypes.StokesDrag, 
            //                                                    ReachTime = 2 });
            spriteEntity.AddComponent(new PIDDriveComponent(128, 1, 10));
            spriteEntity.AddComponent(new MotionDirectionComponent { InputDirection = Vector2.Zero });

            spriteEntity.AddComponent(new GravityComponent { Acceleration = new Vector2(0,1) * 980 });

            playerLayer.CollidingLayers.Add(objectLayer);

            spriteEntity.AddComponent(new ColliderComponent
            {
                CollisionType = CollisionType.Dynamic,
                Enable = true,
                Layer = playerLayer
            });
            spriteEntity.AddComponent(new ColliderShapeComponent { Shape = new Circle(spriteEntity.GetComponent<TransformComponent>().Position, 16) });
            spriteEntity.AddComponent(new CollisionGizmoComponent
            {
                Color = Color.Crimson,
                Alpha = 0.5f,
            });

            EtherEntity obsticle = EntityManager.MakeEntity();
            obsticle.AddComponent(new SpriteComponent(playerTexture));
            obsticle.AddComponent(new TransformComponent { Position = new Vector2(300, 250), Scale = new Vector2(4, 4), Rotation = 0f });
            obsticle.AddComponent(new ColorComponent { Color = Color.Green });

            obsticle.AddComponent(new ColliderComponent 
            { 
                CollisionType = CollisionType.Trigger,
                Enable = true,
                Layer = objectLayer
            });
            obsticle.AddComponent(new CollisionGizmoComponent{
               Color = Color.Crimson,
               Alpha = 0.5f,
            });
            obsticle.AddComponent(new ColliderShapeComponent { Shape = new Circle(obsticle.GetComponent<TransformComponent>().Position, 64) });

            EtherEntity particleEmitter = EntityManager.MakeEntity();

            particleEmitter.AddComponent(new ParticleEmitterComponent { 
                Repeat = true,
                Amount = 100,
                Timer = new Timer(0.5f),
            });
            particleEmitter.AddComponent(new ParticleInstructionComponent
            {
                 Position = new Vector2(140, 250),
                 Spread = new Vector2(30, 10),

                 InitVelocity = new Vector2(0, -100),
                 InitVelocityVariance = 3,

                 Acceleration = new Vector2(0, -50),
                 Damping = 30,

                 Angle = 0,
                 AngleVariance = 0,
                 AngularVelocity = 0,

                 ScaleBegin = 0.3f,
                 ScaleEnd = 0,
                 ScaleVariance = 0.1f,

                 ColorBegin = Color.Blue,
                 ColorEnd = Color.Gray,
                 HueVariance  = 0,

                 AlphaBegin = 0.6f,
                 AlphaEnd = 0,
                 AlphaVariance =0.2f,

                 LifeTime = 0.7f,
                 LifeTimeVariance = 0.3f,
            });
            particleEmitter.AddComponent(new SpriteComponent(playerTexture));

            var camera = new CameraEntity(this);
            //camera.Follow(spriteEntity, new PIDDriveComponent(128, 1, 5, 10, 1));
            camera.GetComponent<TransformComponent>().Scale = new Vector2(3, 3);
            camera.Follow(spriteEntity);
            MainCamera = camera;

            LdtkJson json = Game.Content.Load<LdtkJson>("test");
            //_ldtkRenderer = new LdtkRenderer(json, Content, GraphicsDevice, _spriteBatch);
            ldtkLoader = new LdtkLoader(json, this);

            ldtkLoader.LoadLevel("Green_hills", "walls", 2);
            ldtkLoader.TransferToScene();

            playerLayer.CollidingLayers.Add(ldtkLoader.CollisionLayer);
        }


    }
}
