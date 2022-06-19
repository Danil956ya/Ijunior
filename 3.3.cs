using System;

namespace _3._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int arraySize = 30;
            int[] array = new int[arraySize];
            int minNumber = 1;
            int maxNumber = 9;

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minNumber, maxNumber);
                Console.Write(array[i] + " ");
            }

            Console.Write("\nСписок локальных максимумов: ");

            if (array[0] > array[1])
            {
                Console.Write(array[0] + " ");
            }

            for (int i = 1; i < array.Length - 1; ++i)
            {
                if (array[i - 1] < array[i] && array[i + 1] < array[i])
                {
                    Console.Write(array[i] + " ");
                }
            }

            if (array[array.Length - 1] > array[array.Length - 2])
            {
                Console.Write(array[array.Length - 1]);
            }
        }
    }
}
