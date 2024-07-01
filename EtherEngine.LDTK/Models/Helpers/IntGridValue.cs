using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models
{
    /// <summary>
    /// IntGrid value instance
    /// </summary>
    public partial class IntGridValue
    {
        /// <summary>
        /// Coordinate ID in the layer grid
        /// </summary>
        [JsonProperty("coordId")]
        public int CoordId { get; set; }

        /// <summary>
        /// IntGrid value
        /// </summary>
        [JsonProperty("v")]
        public int V { get; set; }
    }
}
