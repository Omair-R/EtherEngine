using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models.Definitions
{
    public partial class EnumValueDefinition
    {
        /// <summary>
        /// Optional color
        /// </summary>
        [JsonProperty("color")]
        public int Color { get; set; }

        /// <summary>
        /// Enum value
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Optional tileset rectangle to represents this value
        /// </summary>
        [JsonProperty("tileRect")]
        public TilesetRectangle TileRect { get; set; }
    }
}
