using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models.Definitions
{
    /// <summary>
    /// IntGrid value definition
    /// </summary>
    public partial class IntGridValueDefinition
    {
        [JsonProperty("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Parent group identifier (0 if none)
        /// </summary>
        [JsonProperty("groupUid")]
        public int GroupUid { get; set; }

        /// <summary>
        /// User defined unique identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("tile")]
        public TilesetRectangle Tile { get; set; }

        /// <summary>
        /// The IntGrid value itself
        /// </summary>
        [JsonProperty("value")]
        public int Value { get; set; }
    }
}
