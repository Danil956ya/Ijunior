using System;

namespace _2._7
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите символ: ");
            string symbol = Console.ReadLine();
            int maxLenght = name.Length + 1;
            string symbols = symbol;

            for(int i = 0; i < maxLenght; i++)
            {
               symbols += symbol;
            }

            Console.WriteLine(symbols);
            Console.WriteLine(symbol + name + symbol);
            Console.WriteLine(symbols);
        }
    }
}
