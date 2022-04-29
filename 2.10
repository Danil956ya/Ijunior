using System;

namespace _2._10
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int number = random.Next(100);
            int multiplier = 2;
            int degree = 0;

            for(int multiply = 2; multiply != number; multiply *= multiplier)
            {
                degree++;

                if(multiply > number)
                {
                    Console.WriteLine("Number is: {0}, multiply is: {1}, degree is: {2}", number, multiply, degree);
                    break;
                }    

                Console.WriteLine("Number is: {0}, multiply is: {1}, degree is: {2}", number,multiply, degree);
            }
        }
    }
}
