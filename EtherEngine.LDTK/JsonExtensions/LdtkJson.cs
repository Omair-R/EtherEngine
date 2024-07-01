using EtherEngine.LDTK.Converters;
using Newtonsoft.Json;

namespace EtherEngine.LDTK
{
    public partial class LdtkJson
    {
        public static LdtkJson FromJson(string json) => JsonConvert.DeserializeObject<LdtkJson>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this LdtkJson self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
