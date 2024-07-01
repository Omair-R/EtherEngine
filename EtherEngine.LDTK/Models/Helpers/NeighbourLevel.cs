using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    /// <summary>
    /// Nearby level info
    /// </summary>
    public partial class NeighbourLevel
    {
        /// <summary>
        /// A lowercase string tipping on the level location (`n`orth, `s`outh, `w`est,
        /// `e`ast).<br/>  Since 1.4.0, this value can also be `<` (neighbour depth is lower), `>`
        /// (neighbour depth is greater) or `o` (levels overlap and share the same world
        /// depth).<br/>  Since 1.5.3, this value can also be `nw`,`ne`,`sw` or `se` for levels only
        /// touching corners.
        /// </summary>
        [JsonProperty("dir")]
        public string Dir { get; set; }

        /// <summary>
        /// Neighbour Instance Identifier
        /// </summary>
        [JsonProperty("levelIid")]
        public string LevelIid { get; set; }

    }
}
