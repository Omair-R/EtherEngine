using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherEngine.LDTK.Converters
{
    internal class PointConverter : JsonConverter<Point>
    {
        private void ReadAndCheck(ref JsonReader reader)
        {
            if (!reader.Read()) throw new InvalidOperationException("The array has to contain two elements.");
        }
        public override Point ReadJson(JsonReader reader, Type objectType, Point existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return Point.Zero;
            if (reader.TokenType == JsonToken.StartArray)
            {
                Point v = new Point();

                ReadAndCheck(ref reader);
                v.X = serializer.Deserialize<int>(reader);

                ReadAndCheck(ref reader);
                v.Y = serializer.Deserialize<int>(reader);

                ReadAndCheck(ref reader);

                return v;
            }

            if (reader.TokenType == JsonToken.StartObject)
            {
                Point v = new Point();

                ReadAndCheck(ref reader);
                ReadAndCheck(ref reader);
                v.X = serializer.Deserialize<int>(reader);

                ReadAndCheck(ref reader);
                ReadAndCheck(ref reader);
                v.Y = serializer.Deserialize<int>(reader);

                ReadAndCheck(ref reader);

                return v;
            }

            return Point.Zero;
        }

        public override void WriteJson(JsonWriter writer, Point value, JsonSerializer serializer)
        {

            if (value is Point point)
            {
                serializer.Serialize(writer, new int[2] { point.X, point.Y });
            }

            throw new ArgumentException("The object passed was not a point");

        }

        public static readonly PointConverter Singleton = new PointConverter();
    }
}
