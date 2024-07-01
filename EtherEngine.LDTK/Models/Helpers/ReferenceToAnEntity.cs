using Newtonsoft.Json;

namespace EtherEngine.LDTK.Models
{
    /// <summary>
    /// This object describes the "location" of an Entity instance in the project worlds.
    ///
    /// IID information of this instance
    /// </summary>
    public partial class ReferenceToAnEntity
    {
        /// <summary>
        /// IID of the refered EntityInstance
        /// </summary>
        [JsonProperty("entityIid")]
        public string EntityIid { get; set; }

        /// <summary>
        /// IID of the LayerInstance containing the refered EntityInstance
        /// </summary>
        [JsonProperty("layerIid")]
        public string LayerIid { get; set; }

        /// <summary>
        /// IID of the Level containing the refered EntityInstance
        /// </summary>
        [JsonProperty("levelIid")]
        public string LevelIid { get; set; }

        /// <summary>
        /// IID of the World containing the refered EntityInstance
        /// </summary>
        [JsonProperty("worldIid")]
        public string WorldIid { get; set; }
    }
}
