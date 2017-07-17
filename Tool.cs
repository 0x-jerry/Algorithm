using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public struct Range
    {
        public int min;
        public int max;

        public Range(int _min, int _max)
        {
            min = _min;
            max = _max;
        }
    }

    public class Tool
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static string ArrayToString<T>(T[] array)
        {
            string s = "";
            foreach (var item in array)
            {
                s += item.ToString() + "  ";
            }
            return s;
        }

        public static T Min<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? b : a;
        }

        public static T Max<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        public static int MaxBit(int[] arr)
        {
            int max = arr[0];
            foreach (var item in arr)
            {
                if (max < item) max = item;
            }

            int bit = 0;
            while (max >= 1)
            {
                max /= 10;
                bit++;
            }
            
            return bit;
        }
    }

}
