using System;

namespace _3._2
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfLines = 10;
            int numberOfColumns = 10;
            int minNumber = 1;
            int maxNumber = 9;
            int[,] array = new int[numberOfLines, numberOfColumns];
            Random random = new Random();
            int maxValue = 0;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(minNumber, maxNumber);

                    if (maxValue < array[i, j])
                    {
                        maxValue = array[i, j];
                    }

                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }

            Console.WriteLine("Максимальное число: {0}.", maxValue);

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j] == maxValue)
                    {
                        array[i, j] = 0;
                    }

                    Console.Write(array[i, j]);
                }
                Console.WriteLine();
            }

        }
    }
}
