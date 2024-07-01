using EtherEngine.LDTK.Models.Instances;
using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    public partial class LdtkTableOfContentEntry
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("instancesData")]
        public LdtkTocInstanceData[] InstancesData { get; set; }
    }
}
