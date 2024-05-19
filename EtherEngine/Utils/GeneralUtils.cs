using System;

namespace EtherEngine.Utils
{
    static public class GeneralUtils
    {
        public static void Swap<T>(ref T first, ref T second)
        {
            T temp = first;
            first = second;
            second = temp;
        }

        public static T[] RangeSubset<T>(this T[] array, int startIndex, int length)
        {
            T[] result = new T[length];
            Array.Copy(array, startIndex, result, 0, length);
            return result;
        }
    }
}
