using System;

namespace _2._8
{
    class Program
    {
        static void Main(string[] args)
        {
            string correctPassword = "privet";
            int tryCount = 3;

            while(tryCount-- != 0)
            {
                Console.WriteLine("Введите пароль!");
                string password = Console.ReadLine();

                if(password == correctPassword)
                {
                    Console.WriteLine("Secret message");
                    tryCount = 0;
                }
                else
                {
                    Console.WriteLine("try again");
                }
            }
        }
    }
}
