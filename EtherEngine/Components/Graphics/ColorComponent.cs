using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace EtherEngine.Components.Graphics
{
    public struct ColorComponent
    {
        public Color Color = Color.White;
        public float Alpha = 1f;

        public ColorComponent()
        {
        }

        public Color GetActualColor()
        {
            return new Color(Color, Alpha);
        }
    }
}
