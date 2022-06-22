using System;
using System.Collections.Generic;

namespace _5._5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] firstArray = { 1, 2, 1};
            int[] secondArray = { 3, 2 };
            List<int> list = new List<int>();

            AddToList(list, firstArray.Length, firstArray);
            AddToList(list, secondArray.Length, secondArray);

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        static void AddToList(List<int> list, int arrayLength, int[] array)
        {
            for (int i = 0; i < arrayLength; i++)
            {
                if (list.Contains(array[i]) == false)
                {
                    list.Add(array[i]);
                }
            }
        }
    }
}
