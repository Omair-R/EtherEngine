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

namespace Sandbox
{
    public class TestScene : EtherScene
    {
        private CollisionLayer objectLayer = new CollisionLayer(new HashSet<CollisionLayer>());
        private CollisionLayer playerLayer = new CollisionLayer(new HashSet<CollisionLayer>());

        public TestScene(GraphicsDevice graphicsDevice,
                         ContentManager contentManager,
                         GraphicsDeviceManager graphicsDeviceManager, LdtkLoader ldtkLoader) : base(graphicsDevice, contentManager, graphicsDeviceManager)
        {
            _systemManager.AddSystem(new RenderedLayerSystem(this));
            _systemManager.AddSystem(new FollowSystem(this));
            _systemManager.AddSystem(new AnimationSystem(this));
            _systemManager.AddSystem(new SpriteSystem(this));
            _systemManager.AddSystem(new InputSystem(this));
            _systemManager.AddSystem(new DragDriveSystem(this));
            _systemManager.AddSystem(new PIDDriveSystem(this));
            _systemManager.AddSystem(new MotionSystem(this));

            _systemManager.AddSystem(new GravitySystem(this));
            _systemManager.AddSystem(new CollisionGizmoSystem(this));
            _systemManager.AddSystem(new CollisionSystem(this));


            EtherEntity spriteEntity = entityManager.MakeEntity();
            _ = new Texture2D(graphicsDevice, 100, 100);

            Texture2D playerTexture = contentManager.Load<Texture2D>("fall");
            Texture2D animatedTexture = contentManager.Load<Texture2D>("Idle");

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
                Enable = true,
                Layer = playerLayer
            });
            spriteEntity.AddComponent(new ColliderShapeComponent { Shape = new Circle(spriteEntity.GetComponent<TransformComponent>().Position, 16) });
            spriteEntity.AddComponent(new CollisionGizmoComponent
            {
                Color = Color.Crimson,
                Alpha = 0.5f,
            });

            EtherEntity obsticle = entityManager.MakeEntity();
            obsticle.AddComponent(new SpriteComponent(playerTexture));
            obsticle.AddComponent(new TransformComponent { Position = new Vector2(300, 250), Scale = new Vector2(4, 4), Rotation = 0f });
            obsticle.AddComponent(new ColorComponent { Color = Color.Green });

            obsticle.AddComponent(new ColliderComponent 
            { 
                Enable = true,
                Layer = objectLayer
            });
            obsticle.AddComponent(new CollisionGizmoComponent{
               Color = Color.Crimson,
               Alpha = 0.5f,
            });
            obsticle.AddComponent(new ColliderShapeComponent { Shape = new Circle(obsticle.GetComponent<TransformComponent>().Position, 64) });


            var camera = new CameraEntity(this);
            //camera.Follow(spriteEntity, new PIDDriveComponent(128, 1, 5, 10, 1));
            camera.GetComponent<TransformComponent>().Scale = new Vector2(3, 3);
            camera.Follow(spriteEntity);
            MainCamera = camera;

            ldtkLoader.LoadLevel(this, "Green_hills", "walls", 2);
            ldtkLoader.TransferToScene();

            playerLayer.CollidingLayers.Add(ldtkLoader.CollisionLayer);
        }


    }
}
