using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK.Content.Pipeline
{
    [ContentProcessor(DisplayName = "Ldtk Json Processor - EtherEngine")]
    public class LdtkProcessor : ContentProcessor<string, LdtkJson>
    {
        public override LdtkJson Process(string input, ContentProcessorContext context)
        {
            string json = File.ReadAllText(input);

            if (String.IsNullOrEmpty(json)) 
                throw new InvalidContentException(json);

            return LdtkJson.FromJson(json);
        }
    }
}
