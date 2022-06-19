using System;

namespace _3._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int sumOfSecondLine = 0;
            int productOfFirstColumn = 1;
            int[,] array = { {3,2,2,2 },{3,4,2,8 },{3,2,2,2} };

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
            for (int j = 0; j < array.GetLength(1); j++)
            {
                sumOfSecondLine += array[1, j];
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                productOfFirstColumn *= array[i, 0];
            }

            Console.WriteLine("Cумма второй строки: {0} , Произведение первого столбца: {1}",sumOfSecondLine,productOfFirstColumn);
        }
    }
}
