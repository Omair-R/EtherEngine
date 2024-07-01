using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace EtherUtils.Extensions
{
    public static class ColorExtensions
    {
        public static Color FromHexString(string value)
        {

            if (value.StartsWith("#") && value.Length == 7)
            {
                string r = value.Substring(1, 2);
                string g = value.Substring(3, 2);
                string b = value.Substring(5, 2);

                if (!Int32.TryParse(r, NumberStyles.HexNumber, null, out int R) ||
                    !Int32.TryParse(g, NumberStyles.HexNumber, null, out int G) ||
                    !Int32.TryParse(b, NumberStyles.HexNumber, null, out int B))
                    throw new ArgumentException("Not a valid color string.");

                return new Color(R, G, B);
            }

            throw new ArgumentException("Not a valid color string.");
        }

        public static string ToHexString(this Color color)
        {
            string r = color.R.ToString("X");
            string g = color.G.ToString("X");
            string b = color.B.ToString("X");

            return "#" + r + g + b;
        }
    }
}
