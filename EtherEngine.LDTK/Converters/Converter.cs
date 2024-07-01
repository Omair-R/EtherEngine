using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EtherEngine.LDTK.Converters
{
    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                TileRenderModeConverter.Singleton,
                LayerTypeConverter.Singleton,
                EmbedAtlasConverter.Singleton,
                WorldLayoutConverter.Singleton,
                ColorConverter.Singleton,
                Vector2Converter.Singleton,
                PointConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
