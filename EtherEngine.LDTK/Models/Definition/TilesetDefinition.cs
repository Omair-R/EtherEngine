using System.Collections.Generic;
using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models.Definitions
{
    /// <summary>
    /// The `Tileset` definition is the most important part among project definitions. It
    /// contains some extra informations about each integrated tileset. If you only had to parse
    /// one definition section, that would be the one.
    /// </summary>
    public partial class TilesetDefinition
    {
        /// <summary>
        /// Grid-based height
        /// </summary>
        [JsonProperty("__cHei")]
        public int GridBasedHeight { get; set; }

        /// <summary>
        /// Grid-based width
        /// </summary>
        [JsonProperty("__cWid")]
        public int GridBasedWidth { get; set; }


        /// <summary>
        /// An array of custom tile metadata
        /// </summary>
        [JsonProperty("customData")]
        public TileCustomMetadata[] CustomData { get; set; }

        /// <summary>
        /// If this value is set, then it means that this atlas uses an internal LDtk atlas image
        /// instead of a loaded one. Possible values: &lt;`null`&gt;, `LdtkIcons`
        /// </summary>
        [JsonProperty("embedAtlas")]
        public EmbedAtlas? EmbedAtlas { get; set; }

        /// <summary>
        /// Tileset tags using Enum values specified by `tagsSourceEnumId`. This array contains 1
        /// element per Enum value, which contains an array of all Tile IDs that are tagged with it.
        /// </summary>
        [JsonProperty("enumTags")]
        public EnumTagValue[] EnumTags { get; set; }

        /// <summary>
        /// User defined unique identifier
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Distance in pixels from image borders
        /// </summary>
        [JsonProperty("padding")]
        public int Padding { get; set; }

        /// <summary>
        /// Image height in pixels
        /// </summary>
        [JsonProperty("pxHei")]
        public int HeightInPixels { get; set; }

        /// <summary>
        /// Image width in pixels
        /// </summary>
        [JsonProperty("pxWid")]
        public int WidthInPixels { get; set; }

        /// <summary>
        /// Path to the source file, relative to the current project JSON file<br/>  It can be null
        /// if no image was provided, or when using an embed atlas.
        /// </summary>
        [JsonProperty("relPath")]
        public string RelPath { get; set; }


        /// <summary>
        /// Space in pixels between all tiles
        /// </summary>
        [JsonProperty("spacing")]
        public int Spacing { get; set; }

        /// <summary>
        /// An array of user-defined tags to organize the Tilesets
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Optional Enum definition UID used for this tileset meta-data
        /// </summary>
        [JsonProperty("tagsSourceEnumUid")]
        public int? TagsSourceEnumUid { get; set; }

        [JsonProperty("tileGridSize")]
        public int TileGridSize { get; set; }

        /// <summary>
        /// Unique Intidentifier
        /// </summary>
        [JsonProperty("uid")]
        public int Uid { get; set; }
    }
}
