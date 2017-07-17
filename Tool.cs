using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
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
    }

}
