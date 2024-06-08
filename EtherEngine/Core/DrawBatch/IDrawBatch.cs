using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Core.DrawBatch
{
    public interface IDrawBatch
    {
        public void Begin(RasterizerState rasterizerState = null,
                Effect effect = null,
                Matrix? transformMatrix = null);

        public void End();


    }
}
