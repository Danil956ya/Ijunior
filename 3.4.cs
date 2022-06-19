using System;

namespace _3._4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[0];
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Введите число, exit или sum: ");
                string message = Console.ReadLine();

                if (message != "sum" && message != "exit")
                {
                    int input = Convert.ToInt32(message);

                    foreach (int number in array)
                    {
                        Console.Write(number + " ");
                    }

                    int[] tempArray = new int[array.Length + 1];

                    for (int i = 0; i < array.Length; i++)
                    {
                        tempArray[i] = array[i];
                    }

                    tempArray[tempArray.Length - 1] = Convert.ToInt32(input);
                    array = tempArray;
                }

                else if (message == "sum")
                {
                    int arraySum = 0;
                    foreach (int number in array)
                    {
                        arraySum += number;
                    }
                    Console.WriteLine("Сумма массива: {0}", arraySum);
                    arraySum = 0;
                }
                else if (message == "exit")
                {
                    isWork = false;
                }
                else
                {
                    Console.WriteLine("Нет такой команды.");
                }
            }
        }
    }
}
