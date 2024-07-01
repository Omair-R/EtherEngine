using EtherEngine.LDTK.Models.Definitions;
using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models
{
    /// <summary>
    /// If you're writing your own LDtk importer, you should probably just ignore *most* stuff in
    /// the `defs` section, as it contains data that are mostly important to the editor. To keep
    /// you away from the `defs` section and avoid some unnecessary JSON parsing, important data
    /// from definitions is often duplicated in fields prefixed with a float underscore (eg.
    /// `__identifier` or `__type`).  The 2 only definition types you might need here are
    /// **Tilesets** and **Enums**.
    ///
    /// A structure containing all the definitions of this project
    /// </summary>
    public partial class DefinitionCollection
    {
        [JsonProperty("entities")]
        public EntityDefinition[] Entities { get; set; }

        [JsonProperty("enums")]
        public EnumDefinition[] Enums { get; set; }

        [JsonProperty("externalEnums")]
        public EnumDefinition[] ExternalEnums { get; set; }

        [JsonProperty("layers")]
        public LayerDefinition[] Layers { get; set; }

        [JsonProperty("tilesets")]
        public TilesetDefinition[] Tilesets { get; set; }
    }
}
