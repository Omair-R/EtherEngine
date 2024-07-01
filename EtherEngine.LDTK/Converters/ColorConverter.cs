using System;
using System.IO;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using EtherUtils.Extensions;
namespace EtherEngine.LDTK.Converters
{
    internal class ColorConverter : JsonConverter<Color>
    {

        public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return Color.Black;

            string value = serializer.Deserialize<string>(reader);

            return ColorExtensions.FromHexString(value);
        }

        public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
        {
            if (value is Color color)
            {
                serializer.Serialize(writer, color.ToHexString());
            }

            throw new ArgumentException("The object passed was not a color");

        }


        public static readonly ColorConverter Singleton = new ColorConverter();
    }
}
