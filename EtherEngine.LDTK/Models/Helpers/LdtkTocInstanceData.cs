using EtherEngine.LDTK.Models;
using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    public partial class LdtkTocInstanceData
    {
        /// <summary>
        /// An object containing the values of all entity fields with the `exportToToc` option
        /// enabled. This object typing depends on actual field value types.
        /// </summary>
        [JsonProperty("fields")]
        public dynamic Fields { get; set; }

        [JsonProperty("heiPx")]
        public int HeiPx { get; set; }

        /// <summary>
        /// IID information of this instance
        /// </summary>
        [JsonProperty("iids")]
        public ReferenceToAnEntity Iids { get; set; }

        [JsonProperty("widPx")]
        public int WidPx { get; set; }

        [JsonProperty("worldX")]
        public int WorldX { get; set; }

        [JsonProperty("worldY")]
        public int WorldY { get; set; }
    }
}
