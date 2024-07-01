using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    /// <summary>
    /// In a tileset definition, user defined meta-data of a tile.
    /// </summary>
    public partial class TileCustomMetadata
    {
        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("tileId")]
        public int TileId { get; set; }
    }
}
