
namespace EtherEngine.Utils
{
    public class MinMax
    {
        public MinMax(float min, float max)
        {
            if (min > max)
            {
                Min = max;
                Max = min;
            }
            else
            {
                Min = min;
                Max = max;
            }
        }

        public float Min { get; set; }
        public float Max { get; set; }

    }
}
