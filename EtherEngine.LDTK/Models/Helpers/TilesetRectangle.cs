using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    /// <summary>
    /// This object represents a custom sub rectangle in a Tileset image.
    /// </summary>
    public partial class TilesetRectangle
    {
        /// <summary>
        /// Height in pixels
        /// </summary>
        [JsonProperty("h")]
        public int H { get; set; }

        /// <summary>
        /// UID of the tileset
        /// </summary>
        [JsonProperty("tilesetUid")]
        public int TilesetUid { get; set; }

        /// <summary>
        /// Width in pixels
        /// </summary>
        [JsonProperty("w")]
        public int W { get; set; }

        /// <summary>
        /// X pixels coordinate of the top-left corner in the Tileset image
        /// </summary>
        [JsonProperty("x")]
        public int X { get; set; }

        /// <summary>
        /// Y pixels coordinate of the top-left corner in the Tileset image
        /// </summary>
        [JsonProperty("y")]
        public int Y { get; set; }
    }
}
