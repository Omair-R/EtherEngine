using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models.Definitions
{
    /// <summary>
    /// IntGrid value group definition
    /// </summary>
    public partial class IntGridValueGroupDefinition
    {
        /// <summary>
        /// User defined color
        /// </summary>
        [JsonProperty("color")]
        public Color Color { get; set; }

        /// <summary>
        /// User defined string identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Group unique ID
        /// </summary>
        [JsonProperty("uid")]
        public int Uid { get; set; }
    }
}
