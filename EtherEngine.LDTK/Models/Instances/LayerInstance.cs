﻿using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models.Instances
{
    public partial class LayerInstance
    {
        /// <summary>
        /// Layer definition identifier
        /// </summary>
        [JsonProperty("__identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Unique layer instance identifier
        /// </summary>
        [JsonProperty("iid")]
        public string Iid { get; set; }

        /// <summary>
        /// Reference the Layer definition UID
        /// </summary>
        [JsonProperty("layerDefUid")]
        public int LayerDefUid { get; set; }

        /// <summary>
        /// Reference to the UID of the level containing this layer instance
        /// </summary>
        [JsonProperty("levelId")]
        public int LevelId { get; set; }

        /// <summary>
        /// Layer type (possible values: IntGrid, Entities, Tiles or AutoLayer)
        /// </summary>
        [JsonProperty("__type")]
        public LayerType Type { get; set; }

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
        /// Grid size
        /// </summary>
        [JsonProperty("__gridSize")]
        public int GridSize { get; set; }

        /// <summary>
        /// Layer opacity as Float [0-1]
        /// </summary>
        [JsonProperty("__opacity")]
        public float Opacity { get; set; }

        /// <summary>
        /// Total layer X pixel offset, including both instance and definition offsets.
        /// </summary>
        [JsonProperty("__pxTotalOffsetX")]
        public int PxTotalOffsetX { get; set; }

        /// <summary>
        /// Total layer Y pixel offset, including both instance and definition offsets.
        /// </summary>
        [JsonProperty("__pxTotalOffsetY")]
        public int PxTotalOffsetY { get; set; }

        /// <summary>
        /// X offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
        /// the `LayerDef` optional offset, so you should probably prefer using `__pxTotalOffsetX`
        /// which contains the total offset value)
        /// </summary>
        [JsonProperty("pxOffsetX")]
        public int PxOffsetX { get; set; }

        /// <summary>
        /// Y offset in pixels to render this layer, usually 0 (IMPORTANT: this should be added to
        /// the `LayerDef` optional offset, so you should probably prefer using `__pxTotalOffsetX`
        /// which contains the total offset value)
        /// </summary>
        [JsonProperty("pxOffsetY")]
        public int PxOffsetY { get; set; }

        [JsonProperty("entityInstances")]
        public EntityInstance[] EntityInstances { get; set; }

        [JsonProperty("gridTiles")]
        public TileInstance[] GridTiles { get; set; }

        /// <summary>
        /// An array containing all tiles generated by Auto-layer rules. The array is already sorted
        /// in display order (ie. 1st tile is beneath 2nd, which is beneath 3rd etc.).<br/><br/>
        /// Note: if multiple tiles are stacked in the same cell as the result of different rules,
        /// all tiles behind opaque ones will be discarded.
        /// </summary>
        [JsonProperty("autoLayerTiles")]
        public TileInstance[] AutoLayerTiles { get; set; }

        /// <summary>
        /// A list of all values in the IntGrid layer, stored in CSV format (Comma Separated
        /// Values).<br/>  Order is from left to right, and top to bottom (ie. first row from left to
        /// right, followed by second row, etc).<br/>  `0` means "empty cell" and IntGrid values
        /// start at 1.<br/>  The array size is `__cWid` x `__cHei` cells.
        /// </summary>
        [JsonProperty("intGridCsv")]
        public int[] IntGridCsv { get; set; }

        /// <summary>
        /// This layer can use another tileset by overriding the tileset UID here.
        /// </summary>
        [JsonProperty("overrideTilesetUid")]
        public int? OverrideTilesetUid { get; set; }

        /// <summary>
        /// The definition UID of corresponding Tileset, if any.
        /// </summary>
        [JsonProperty("__tilesetDefUid")]
        public int? TilesetDefUid { get; set; }

        /// <summary>
        /// The relative path to corresponding Tileset, if any.
        /// </summary>
        [JsonProperty("__tilesetRelPath")]
        public string TilesetRelPath { get; set; }

        /// <summary>
        /// Layer instance visibility
        /// </summary>
        [JsonProperty("visible")]
        public bool Visible { get; set; }
    }
}
