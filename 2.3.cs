using System;

namespace _2._3
{
    class Program
    {
        static void Main(string[] args)
        {
            int startNumber = 5;
            int endNumber = 96;
            int step = 7;

            for (int i = startNumber; i <= endNumber; i += step)
            {
                Console.WriteLine(i);
            }
        }
    }
}

