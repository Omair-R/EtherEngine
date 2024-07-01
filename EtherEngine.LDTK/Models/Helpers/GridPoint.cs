using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    /// <summary>
    /// This object is just a grid-based coordinate used in Field values.
    /// </summary>
    public partial class GridPoint
    {
        /// <summary>
        /// X grid-based coordinate
        /// </summary>
        [JsonProperty("cx")]
        public int Cx { get; set; }

        /// <summary>
        /// Y grid-based coordinate
        /// </summary>
        [JsonProperty("cy")]
        public int Cy { get; set; }
    }
}
