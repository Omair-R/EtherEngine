using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK.Content.Pipeline
{
    public class LdtkWriter : ContentTypeWriter<LdtkJson>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "LdtkReader, EtherEngine.LDTK.Content.Pipeline";
        }

        protected override void Write(ContentWriter output, LdtkJson value)
        {
            output.Write(value.ToJson());
        }
    }
}
