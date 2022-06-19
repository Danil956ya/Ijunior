using System;

namespace _4._2
{
    class Program
    {
        static void Main()
        {
            int maxHealt = 10;
            Console.WriteLine("Выбирете сколько у вас жизней. Максимум жизней: {0}.", maxHealt);
            int healt = Convert.ToInt32(Console.ReadLine());
            DrawBar(healt, maxHealt);
        }

        static void DrawBar(int value, int maxValue)
        {
            string bar = "";

            if (value > maxValue || value < 0)
            {
                Console.WriteLine("Неверно введены значения.");
                return;
            }

            for (int i = 0; i < value; i++)
            {
                bar += '#';
            }

            Console.Write("[");
            Console.Write(bar);
            bar = "";

            for (int i = value; i < maxValue; i++)
            {
                bar += ' ';
            }

            Console.Write(bar + "]" + "\n");

        }
    }
}
