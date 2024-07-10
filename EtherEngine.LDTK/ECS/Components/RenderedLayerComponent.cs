using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK.ECS.Components
{
    public struct RenderedLayerComponent
    {
        public RenderTarget2D Texture;
        public Vector2 Scale;
        public int DrawOrder;
    }
}
