using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    /// <summary>
    /// In a tileset definition, enum based tag infos
    /// </summary>
    public partial class EnumTagValue
    {
        [JsonProperty("enumValueId")]
        public string EnumValueId { get; set; }

        [JsonProperty("tileIds")]
        public int[] TileIds { get; set; }
    }
}
