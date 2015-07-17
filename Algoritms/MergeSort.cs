using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritms
{
    public static class Sorting
    {
        public static T[] InsertionSort<T>(this T[] array) where T : IComparable<T>
        {
            if (array == null)
            {
                return null;
            }

            for (int i = 1; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && array[j - 1].CompareTo(array[j]) > 0)
                {
                    T temp = array[j];
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                    j--;
                }
            }

            return array;
        }
    }
}
