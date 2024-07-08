using EtherEngine.Components;
using EtherEngine.Entities;
using EtherEngine.LDTK.ECS.Components.Global;
using EtherEngine.LDTK.Models;
using EtherEngine.LDTK.Models.Definitions;
using EtherEngine.LDTK.Models.Instances;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK
{
    public class LdtkRenderer
    {
        private LdtkJson _root;
        private ContentManager _contentManager;
        private GraphicsDevice _graphicsDevice;
        private SpriteBatch _spriteBatch;

        private Texture2D _whiteTexture;

        private Dictionary<int, Texture2D> _tileTextures;
        private Dictionary<int, int> _tileSrcGridSize;

        private RenderTarget2D[] _renderedLayers;

        public LdtkRenderer(LdtkJson root, ContentManager content, GraphicsDevice device, SpriteBatch spriteBatch)
        {
            _root = root;
            _contentManager = content;
            _graphicsDevice = device;
            _spriteBatch = spriteBatch;

            _whiteTexture = new Texture2D(device, 1, 1);
            _whiteTexture.SetData(new[] { Color.White });

            _tileTextures = new Dictionary<int, Texture2D>();
            _tileSrcGridSize = new Dictionary<int, int>();

            MapTextures();
            _renderedLayers = LoadLevel("Green_hills"); ;
        }

        private void MapTextures()
        {
            foreach(var tileDef in _root.Defs.Tilesets)
            {
                Texture2D texture = _contentManager.Load<Texture2D>(tileDef.RelPath.Substring(0, tileDef.RelPath.Length-4));
                _tileTextures.Add(tileDef.Uid, texture);
                _tileSrcGridSize.Add(tileDef.Uid, tileDef.TileGridSize);
            }
        }

        private void HandleTiles(LayerInstance layer, TileInstance[] tiles)
        {
            foreach (var tile in tiles)
            {
                _spriteBatch.Draw(_tileTextures[layer.TilesetDefUid.Value],
                                        new Rectangle(
                                            tile.Px.X,
                                            tile.Px.Y,
                                            layer.GridSize,
                                            layer.GridSize), new Rectangle(
                                                tile.Src.X,
                                                tile.Src.Y,
                                                _tileSrcGridSize[layer.TilesetDefUid.Value],
                                                _tileSrcGridSize[layer.TilesetDefUid.Value]),
                                        new Color(Color.White, tile.A), 0, Vector2.Zero, (SpriteEffects)tile.F, 0f);
            }
        }


        private void HandleIntGrid(LayerInstance layer, LayerDefinition layerDefinition)
        {

            if (layer.AutoLayerTiles.Length == 0)
            {
                Dictionary<int, IntGridValueDefinition> intGridMap = new Dictionary<int, IntGridValueDefinition>();
                intGridMap = layerDefinition.IntGridValues.ToDictionary((x) => x.Value);
                for (int i = 0; i < layer.IntGridCsv.Length; i++)
                {
                    if (intGridMap.TryGetValue(layer.IntGridCsv[i], out var intGrid))
                    {
                        int x = (i % layer.GridBasedWidth) * layer.GridSize;
                        int y = (i / layer.GridBasedHeight) * layer.GridSize;
                        _spriteBatch.Draw(_tileTextures[intGrid.Tile.TilesetUid],
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
            }
            else
            {
                HandleTiles(layer, layer.AutoLayerTiles);
            }
        }


        public void Draw(CameraEntity camera=null)
        {
            _graphicsDevice.Clear(_root.BgColor);

            if (camera != null)
            {
                _spriteBatch.Begin(transformMatrix:camera.GetTransform(), samplerState:SamplerState.PointClamp);
            }else
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            }
            
            for(int i= 0; i < _root.Levels.Length; i++)
            {
                int size = _root.WorldGridWidth.Value;


                _spriteBatch.Draw(_whiteTexture, new Rectangle(_root.Levels[i].WorldX,
                                                              _root.Levels[i].WorldY,
                                                              _root.Levels[i].WidthInPixels,
                                                              _root.Levels[i].HeightInPixels), _root.Levels[i].BgColor);

                foreach(var target in _renderedLayers)
                {
                    _spriteBatch.Draw(target, Vector2.Zero, Color.White);
                }
                break;
            }

            _spriteBatch.End();
        }

        public RenderTarget2D[] LoadLevel(string identifer, CameraEntity camera=null)
        {
            //Green_hills
            Level level = _root.Levels.FirstOrDefault((x)=>x.Identifier == identifer);
            List<RenderTarget2D> renderedLayers = new();

            if (level == null)
            {
                throw new ArgumentNullException("level does not exist in world");
            }

            _graphicsDevice.Clear(_root.BgColor); //TODO: Turn this to a layer.

            if (camera != null)
            {
                _spriteBatch.Begin(transformMatrix: camera.GetTransform(),
                                   samplerState: SamplerState.PointClamp);
            }
            else
            {
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            }


            foreach (LayerInstance layer in level.LayerInstances)
            {
                RenderTarget2D renderTarget2D = new RenderTarget2D(_graphicsDevice,
                                                                   layer.GridBasedWidth * layer.GridSize,
                                                                   layer.GridBasedHeight * layer.GridSize,
                                                                   false,
                                                                   SurfaceFormat.Color,
                                                                   DepthFormat.None,
                                                                   0,
                                                                   RenderTargetUsage.PreserveContents);
                _graphicsDevice.SetRenderTarget(renderTarget2D);
                renderedLayers.Add(renderTarget2D);

                
                switch (layer.Type)
                {
                    case LayerType.Tiles:
                        HandleTiles(layer, layer.GridTiles);
                        break;
                    case LayerType.AutoLayer:
                        HandleTiles(layer, layer.AutoLayerTiles);
                        break;
                    case LayerType.Entities:

                        break;
                    case LayerType.IntGrid:
                        LayerDefinition layerDefinition = _root.Defs.Layers.FirstOrDefault(x => x.TilesetDefUid == layer.TilesetDefUid);
                        HandleIntGrid(layer, layerDefinition);
                        break;
                }
            }

            _spriteBatch.End();
            _graphicsDevice.SetRenderTarget(null);

            return renderedLayers.ToArray();
        }
        public Level LoadLevel(Guid iid)
        {
            throw new NotImplementedException();
        }

        public Level LoadLevel(Guid worldId, Guid iid)
        {
            throw new NotImplementedException();
        }

        public Level LoadLevel(Guid worldId, int index)
        {
            throw new NotImplementedException();
        }

        public TilesetComponent GetTilesetComponent(Guid iid)
        {
            throw new NotImplementedException();
        }

        public TilesetComponent GetTilesetComponent(string Identifier)
        {
            throw new NotImplementedException();
        }
    }
}
