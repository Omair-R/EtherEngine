using EtherUtils.Extensions;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK.Converters
{
    internal class Vector2Converter : JsonConverter<Vector2>
    {
        private void ReadAndCheck(ref JsonReader reader)
        {
            if (!reader.Read()) throw new InvalidOperationException("The array has to contain two elements.");
        }

        public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return Vector2.Zero;
            if (reader.TokenType == JsonToken.StartArray)
            {
                Vector2 v = new Vector2();

                ReadAndCheck(ref reader);
                v.X = serializer.Deserialize<float>(reader);

                ReadAndCheck(ref reader);
                v.Y = serializer.Deserialize<float>(reader);

                ReadAndCheck(ref reader);

                return v;
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                Vector2 v = new Vector2();

                ReadAndCheck(ref reader);
                ReadAndCheck(ref reader);
                v.X = serializer.Deserialize<float>(reader);

                ReadAndCheck(ref reader);
                ReadAndCheck(ref reader);
                v.Y = serializer.Deserialize<float>(reader);

                ReadAndCheck(ref reader);

                return v;
            }

            return Vector2.Zero;
        }

        public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
        {

            if (value is Vector2 vector)
            {
                serializer.Serialize(writer, new float[2]{vector.X, vector.Y});
            }

            throw new ArgumentException("The object passed was not a vector2");

        }

        public static readonly Vector2Converter Singleton = new Vector2Converter();
    }
}
