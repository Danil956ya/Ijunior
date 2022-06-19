using System;

namespace _2._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minCount = 0;
            int maxCount = 100;
            int number = random.Next(minCount, maxCount);
            int lessMultiple = 3;
            int mostMultiple = 5;
            int result = number;

            for (int i = number; i <= maxCount; i++)
            {
                if (i % lessMultiple == 0 || i % mostMultiple == 0)
                {
                    result += i;
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine(result);     
        }
    }
}
