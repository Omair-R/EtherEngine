using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content.Pipeline;
using Newtonsoft.Json;

namespace EtherEngine.LDTK.Content.Pipeline
{
    [ContentImporter(".ldtk", DisplayName = "LDtk Json Importer - EtherEngine", DefaultProcessor = "LdtkProcessor")]
    public class LdtkImporter : ContentImporter<string>
    {
        public override string Import(string filename, ContentImporterContext context)
        {
            return filename;
        }
    }
}
