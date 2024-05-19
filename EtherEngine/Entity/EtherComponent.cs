using EtherEngine.Utils;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.Entity
{
    public abstract class EtherComponent : GameComponent
    {
        public EtherComponent(Game game): base(game) { }
        public override abstract void Initialize();
        public override abstract void Update(GameTime gameTime);
        
    }
}
