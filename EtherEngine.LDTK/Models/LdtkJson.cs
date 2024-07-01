using EtherEngine.LDTK.Models;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    /// <summary>
    /// This file is a JSON schema of files created by LDtk level editor (https://ldtk.io).
    ///
    /// This is the root of any Project JSON file. It contains:  - the project settings, - an
    /// array of levels, - a group of definitions (that can probably be safely ignored for most
    /// users).
    /// </summary>
    public partial class LdtkJson
    {
        /// <summary>
        /// Project background color
        /// </summary>
        [JsonProperty("bgColor")]
        public Color BgColor { get; set; }

        /// <summary>
        /// A structure containing all the definitions of this project
        /// </summary>
        [JsonProperty("defs")]
        public DefinitionCollection Defs { get; set; }

        /// <summary>
        /// If TRUE, one file will be saved for the project (incl. all its definitions) and one file
        /// in a sub-folder for each level.
        /// </summary>
        [JsonProperty("externalLevels")]
        public bool ExternalLevels { get; set; }

        /// <summary>
        /// Unique project identifier
        /// </summary>
        [JsonProperty("iid")]
        public string Iid { get; set; }

        /// <summary>
        /// File format version
        /// </summary>
        [JsonProperty("jsonVersion")]
        public string JsonVersion { get; set; }

        /// <summary>
        /// All levels. The order of this array is only relevant in `LinearHorizontal` and
        /// `linearVertical` world layouts (see `worldLayout` value).<br/>  Otherwise, you should
        /// refer to the `worldX`,`worldY` coordinates of each Level.
        /// </summary>
        [JsonProperty("levels")]
        public Level[] Levels { get; set; }

        /// <summary>
        /// All instances of entities that have their `exportToToc` flag enabled are listed in this
        /// array.
        /// </summary>
        [JsonProperty("toc")]
        public LdtkTableOfContentEntry[] Toc { get; set; }

        /// <summary>
        /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
        /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
        /// the change immediately.<br/><br/>  Height of the world grid in pixels.
        /// </summary>
        [JsonProperty("worldGridHeight")]
        public int? WorldGridHeight { get; set; }

        /// <summary>
        /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
        /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
        /// the change immediately.<br/><br/>  Width of the world grid in pixels.
        /// </summary>
        [JsonProperty("worldGridWidth")]
        public int? WorldGridWidth { get; set; }

        /// <summary>
        /// **WARNING**: this field will move to the `worlds` array after the "multi-worlds" update.
        /// It will then be `null`. You can enable the Multi-worlds advanced project option to enable
        /// the change immediately.<br/><br/>  An enum that describes how levels are organized in
        /// this project (ie. linearly or in a 2D space). Possible values: &lt;`null`&gt;, `Free`,
        /// `GridVania`, `LinearHorizontal`, `LinearVertical`
        /// </summary>
        [JsonProperty("worldLayout")]
        public WorldLayout? WorldLayout { get; set; }

        /// <summary>
        /// This array will be empty, unless you enable the Multi-Worlds in the project advanced
        /// settings.<br/><br/> - in current version, a LDtk project file can only contain a single
        /// world with multiple levels in it. In this case, levels and world layout related settings
        /// are stored in the root of the JSON.<br/> - with "Multi-worlds" enabled, there will be a
        /// `worlds` array in root, each world containing levels and layout settings. Basically, it's
        /// pretty much only about moving the `levels` array to the `worlds` array, aint with world
        /// layout related values (eg. `worldGridWidth` etc).<br/><br/>If you want to start
        /// supporting this future update easily, please refer to this documentation:
        /// https://github.com/deepnight/ldtk/issues/231
        /// </summary>
        [JsonProperty("worlds")]
        public World[] Worlds { get; set; }
    }
}
