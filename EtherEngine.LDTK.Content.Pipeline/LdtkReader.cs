using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK.Content.Pipeline
{
    public class LdtkReader : ContentTypeReader<LdtkJson>
    {
        protected override LdtkJson Read(ContentReader input, LdtkJson existingInstance)
        {
            string json = input.ReadString();
            LdtkJson result = LdtkJson.FromJson(json);
            return result;
        }
    }
}
