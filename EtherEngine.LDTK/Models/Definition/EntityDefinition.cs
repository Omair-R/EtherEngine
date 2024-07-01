using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models.Definitions
{
    public partial class EntityDefinition
    {

        /// <summary>
        /// Base entity color
        /// </summary>
        [JsonProperty("color")]
        public Color Color { get; set; }

        /// <summary>
        /// Pixel height
        /// </summary>
        [JsonProperty("height")]
        public int Height { get; set; }


        /// <summary>
        /// User defined unique identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// An array of 4 dimensions for the up/right/down/left borders (in this order) when using
        /// 9-slice mode for `tileRenderMode`.<br/>  If the tileRenderMode is not NineSlice, then
        /// this array is empty.<br/>  See: https://en.wikipedia.org/wiki/9-slice_scaling
        /// </summary>
        [JsonProperty("nineSliceBorders")]
        public int[] NineSliceBorders { get; set; }

        /// <summary>
        /// Pivot X coordinate (from 0 to 1.0)
        /// </summary>
        [JsonProperty("pivotX")]
        public float PivotX { get; set; }

        /// <summary>
        /// Pivot Y coordinate (from 0 to 1.0)
        /// </summary>
        [JsonProperty("pivotY")]
        public float PivotY { get; set; }


        /// <summary>
        /// An object representing a rectangle from an existing Tileset
        /// </summary>
        [JsonProperty("tileRect")]
        public TilesetRectangle TileRect { get; set; }

        /// <summary>
        /// An enum describing how the the Entity tile is rendered inside the Entity bounds. Possible
        /// values: `Cover`, `FitInside`, `Repeat`, `Stretch`, `FullSizeCropped`,
        /// `FullSizeUncropped`, `NineSlice`
        /// </summary>
        [JsonProperty("tileRenderMode")]
        public TileRenderMode TileRenderMode { get; set; }

        /// <summary>
        /// Tileset ID used for optional tile display
        /// </summary>
        [JsonProperty("tilesetId")]
        public int? TilesetId { get; set; }

        /// <summary>
        /// Unique Int identifier
        /// </summary>
        [JsonProperty("uid")]
        public int Uid { get; set; }

        /// <summary>
        /// This tile overrides the one defined in `tileRect` in the UI
        /// </summary>
        [JsonProperty("uiTileRect")]
        public TilesetRectangle UiTileRect { get; set; }

        /// <summary>
        /// Pixel width
        /// </summary>
        [JsonProperty("width")]
        public int Width { get; set; }
    }
}
