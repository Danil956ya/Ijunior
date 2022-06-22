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
            List<int> numbers = new List<int>();

            AddToList(numbers, firstArray.Length, firstArray);
            AddToList(numbers, secondArray.Length, secondArray);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
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
