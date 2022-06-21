using System;
using System.Collections.Generic;

namespace _5._3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = new List<int>();
            string message = "";
            int tempInt;

            while (message != "exit")
            {
                Console.WriteLine("Введите число, exit или sum: ");
                message = Console.ReadLine();

                if (int.TryParse(message,out tempInt) == true)
                {
                    AddNumber(numbers, message);
                }
                else if (message == "sum")
                {
                    GetSum(numbers);
                }
                else
                {
                    Console.WriteLine("Нет такой команды.");
                }
            }
        }
        static void AddNumber(List<int> numbers, string message)
        {
            numbers.Add(Convert.ToInt32(message));
        }

        static void GetSum(List<int> numbers)
        {
            int sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }
            Console.WriteLine("Сумма массива: {0}", sum);
        }
    }
}
