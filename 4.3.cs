using System;

namespace _4._3
{
    class Program
    {
        static void Main(string[] args)
        {
            GetNumber();
        }

        static int GetNumber()
        {
            int tempInt;
            Console.WriteLine("Введите строку.");
            string input = Console.ReadLine();

            while(int.TryParse(input, out tempInt) == false)
            {
                Console.WriteLine("Конвертация не удалась, введите число!");
                input = Console.ReadLine();
            }

            Console.WriteLine("Верно!");
            return tempInt;
        }
    }
}
