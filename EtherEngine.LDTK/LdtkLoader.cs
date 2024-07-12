using Arch.Core;
using EtherEngine.Components;
using EtherEngine.Core.Collision.Models;
using EtherEngine.Core.Shapes;
using EtherEngine.Entities;
using EtherEngine.LDTK.ECS.Components;
using EtherEngine.LDTK.Models;
using EtherEngine.LDTK.Models.Definitions;
using EtherEngine.LDTK.Models.Instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using static System.Formats.Asn1.AsnWriter;


namespace EtherEngine.LDTK
{
    public class LdtkLoader
    {
        private LdtkJson _root;
        private EtherScene _scene;

        private float _scale;
        private Texture2D _whiteTexture;

        private RenderTarget2D[] _renderedLayers;
        private Level _loadedLevel;
        private StaticQuad[] Colliders;

        public CollisionLayer CollisionLayer { get; private set; }
        public string CurrentLevel { get; private set; }

        public LdtkLoader(LdtkJson root)
        {
            _root = root;
            if (_root.ExternalLevels) //TODO: Add support for external levels.
                throw new NotImplementedException();

            CollisionLayer = new CollisionLayer(new HashSet<CollisionLayer>());
        }

        public string GetNeighbourIdentifier(string Dir) //Convert to enum
        {
            string identifier = _loadedLevel.Neighbours.FirstOrDefault(x => x.Dir == Dir).LevelIid;
            //return String.IsNullOrEmpty(identifier) ? throw new NullReferenceException("Neighbour does not exist.") : identifier; //hhmmmmm
            return identifier;
        }

        public void LoadLevel(EtherScene scene, string identifer, string collisionLayer= "", float scale=1f, bool useContentPipeline = true)
        {
            Level level = _root.Levels.FirstOrDefault((x) => x.Identifier == identifer);

            _scale = scale;
            _LoadLevel(scene, level, collisionLayer, useContentPipeline);

            
            CurrentLevel = level.Identifier;
            _loadedLevel = level;
        }

        public void LoadLevel(EtherScene scene, Guid Iid, string collisionLayer = "", float scale = 1f,  bool useContentPipeline = true)
        {
            Level level = _root.Levels.FirstOrDefault((x) => x.Iid == Iid.ToString());

            _scale = scale;
            _LoadLevel(scene, level, collisionLayer, useContentPipeline);

            
            CurrentLevel = level.Identifier;
            _loadedLevel = level;
        }

        public void LoadLevel(EtherScene scene, int Uid, string collisionLayer = "", float scale = 1f,  bool useContentPipeline = true)
        {
            Level level = _root.Levels.FirstOrDefault((x) => x.Uid == Uid);

            _scale = scale;
            _LoadLevel(scene, level, collisionLayer, useContentPipeline);

            
            CurrentLevel = level.Identifier;
            _loadedLevel = level;
        }

        // Connecting to a new scene results in the distruction of previously saved rendered layers.
        public void TransferToScene()
        {
            
            _scene.entityManager.DestroyEntities<RenderedLayerComponent>();
            _scene.entityManager.DestroyEntities<TileColliderComponent>();

            int order = 0;
            foreach (var layer in _renderedLayers)
            {
                EtherEntity entity =_scene.entityManager.MakeEntity();
                entity.AddComponent<RenderedLayerComponent>(new RenderedLayerComponent
                {
                    Texture = layer,
                    DrawOrder = order,
                    Scale = Vector2.One * _scale

                });
                order++;
            }

            foreach (var collider in Colliders)
            {
                EtherEntity entity = _scene.entityManager.MakeEntity();
                entity.AddComponent(new TileColliderComponent());
                entity.AddComponent(new ColliderComponent
                {
                    Enable = true,
                    Layer = CollisionLayer
                });
                entity.AddComponent(new CollisionGizmoComponent
                {
                    Color = Color.BlueViolet,
                    Alpha = 0.4f,
                });
                entity.AddComponent(new ColliderShapeComponent { Shape = collider });
                entity.AddComponent(new TransformComponent
                {
                    Position = collider.GetCenter(),
                    Scale = Vector2.One,
                    Rotation = 0f
                });

            }

            Colliders = null;
            _renderedLayers = null;
            _loadedLevel = null;
        }

        //TODO: make colors if no texture. + warning.
        private void _LoadLevel(EtherScene scene, Level level, string collisionLayer,  bool useContentPipeline)
        {
            _scene = scene;

            _whiteTexture = new Texture2D(_scene._graphicsDevice, 1, 1);
            _whiteTexture.SetData(new[] { Color.White });

            if (level == null)
            {
                throw new ArgumentNullException("level does not exist in world");
            }

            List<RenderTarget2D> renderedLayers = new();


            _scene.spriteBatch.Begin(samplerState: SamplerState.PointClamp);


            //TODO: bgImage.
            foreach (LayerInstance layer in level.LayerInstances)
            {
                
                if (!layer.Visible || layer.Type == LayerType.Entities) continue;

                string tilesetRelPath = layer.TilesetRelPath;

                if (layer.OverrideTilesetUid != null)
                {
                    tilesetRelPath = _root.Defs.Tilesets.FirstOrDefault((x)=> x.Uid== layer.OverrideTilesetUid.Value).RelPath;
                }

                Texture2D tilesetTexture = GetTilesetTexture(layer.TilesetRelPath, useContentPipeline);

                RenderTarget2D renderTarget2D = new RenderTarget2D(_scene._graphicsDevice,
                                                                   layer.GridBasedWidth * layer.GridSize,
                                                                   layer.GridBasedHeight * layer.GridSize,
                                                                   false,
                                                                   SurfaceFormat.Color,
                                                                   DepthFormat.None,
                                                                   0,
                                                                   RenderTargetUsage.PreserveContents);

                _scene._graphicsDevice.SetRenderTarget(renderTarget2D);
                renderedLayers.Add(renderTarget2D);


                switch (layer.Type)
                {
                    case LayerType.Tiles:
                        DrawTiles(layer, tilesetTexture, layer.GridTiles, collisionLayer);
                        break;
                    case LayerType.AutoLayer:
                        DrawTiles(layer, tilesetTexture, layer.AutoLayerTiles, collisionLayer);
                        break;
                    case LayerType.IntGrid:
                        LayerDefinition layerDefinition = _root.Defs.Layers.FirstOrDefault(x => x.TilesetDefUid == layer.TilesetDefUid);
                        DrawIntGridTiles(layer, tilesetTexture, layerDefinition, collisionLayer);
                        break;
                }

                _scene.spriteBatch.End();
                _scene._graphicsDevice.SetRenderTarget(null);
            }

            _renderedLayers = renderedLayers.ToArray();

        }

        //Chekc for collision.
        private void DrawTiles(LayerInstance layer, Texture2D tilesetTexture, TileInstance[] tiles, string collisionLayer)
        {
            bool collide = !string.IsNullOrEmpty(collisionLayer) && layer.Identifier == collisionLayer;
            List<StaticQuad> colliders = new List<StaticQuad>();

            foreach (var tile in tiles)
            {
                float x = (tile.Px.X + layer.PxTotalOffsetX);
                float y = (tile.Px.Y + layer.PxTotalOffsetY);

                if (collide)
                    colliders.Add(new StaticQuad(new Vector2(x * _scale,
                                                             y * _scale), 
                                                 new Vector2((x + layer.GridSize) * _scale,
                                                             (y + layer.GridSize) * _scale)));

                _scene.spriteBatch.Draw(tilesetTexture,
                                        new Rectangle(
                                            (int)x,
                                            (int)y,
                                            layer.GridSize,
                                            layer.GridSize), new Rectangle(
                                                tile.Src.X,
                                                tile.Src.Y,
                                                layer.GridSize,
                                                layer.GridSize),
                                        new Color(Color.White, tile.A * layer.Opacity), 0, Vector2.Zero, (SpriteEffects)tile.F, 0f);
            }

            if (collide)
                Colliders = colliders.ToArray();
        }

        private void DrawIntGridTiles(LayerInstance layer, Texture2D tilesetTexture, LayerDefinition layerDefinition, string collisionLayer)
        {
            Dictionary<int, IntGridValueDefinition> intGridMap = new Dictionary<int, IntGridValueDefinition>();
            intGridMap = layerDefinition.IntGridValues.ToDictionary((x) => x.Value);

            List<StaticQuad> colliders = new List<StaticQuad>();

            for (int i = 0; i < layer.IntGridCsv.Length; i++)
            {
                if (intGridMap.TryGetValue(layer.IntGridCsv[i], out var intGrid))
                {
                    int x = (i % layer.GridBasedWidth) * layer.GridSize * (int)_scale;
                    int y = (i / layer.GridBasedHeight) * layer.GridSize * (int)_scale;

                    if(intGrid.Identifier == collisionLayer && !string.IsNullOrEmpty(collisionLayer))
                        colliders.Add(new StaticQuad(new Vector2(x,
                                                                 y),
                                                     new Vector2(x + layer.GridSize * _scale,
                                                                 y + layer.GridSize * _scale)));
                    if (layer.AutoLayerTiles.Length == 0)
                        _scene.spriteBatch.Draw(tilesetTexture,
                                                new Rectangle(
                                                    x,
                                                    y,
                                                    layer.GridSize,
                                                    layer.GridSize), new Rectangle(
                                                        intGrid.Tile.X,
                                                        intGrid.Tile.Y,
                                                        intGrid.Tile.W,
                                                        intGrid.Tile.H),
                                                intGridMap[layer.IntGridCsv[i]].Color);
                }
            }

            Colliders = colliders.ToArray();

            if (layer.AutoLayerTiles.Length != 0)
            {
                DrawTiles(layer, tilesetTexture, layer.AutoLayerTiles, collisionLayer);
            }
        }

        private Texture2D GetTilesetTexture(string path, bool useContentPipeline=true)
        {
            if (useContentPipeline)
                return _scene.contentManager.Load<Texture2D>(path.Substring(0, path.Length - 4));
            
            throw new NotImplementedException();
        }

        private void MoveLoader(EtherScene _scene)
        {
            throw new NotImplementedException();
        }

    }
}
