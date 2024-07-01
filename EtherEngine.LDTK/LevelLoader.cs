using EtherEngine.LDTK.ECS.Components.Global;
using EtherEngine.LDTK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK
{
    public class LevelLoader
    {
        private LdtkJson _root;

        public LevelLoader(string json)
        {
            _root = LdtkJson.FromJson(json);
        }

        public LevelLoader(LdtkJson root)
        {
            _root = root;
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
