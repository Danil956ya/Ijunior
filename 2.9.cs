using System;

namespace _2._9
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int minNumber = 0;
            int maxNumber = 27;
            int number = random.Next(minNumber, maxNumber);
            int minMultiplyNumber = 100;
            int maxMultiplyNumber = 999;
            int multiply = 0;

            for (int i = number; i <= maxMultiplyNumber; i += number)
            {
                multiply += i < minMultiplyNumber ? 0 : 1;          
            }

            Console.WriteLine("Your number is: {0} \nMultiple is: {1}",number,multiply);

        }
    }
}
