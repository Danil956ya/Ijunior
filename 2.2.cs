using System;

namespace junior_2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string exitInput = "exit";
            string playerInput;
            bool isWork = true;

            while(isWork)
            {
                Console.Write("Программа работает, введите команду для выхода: ");
                playerInput = Console.ReadLine();
                isWork = playerInput != exitInput;
            }
        }
    }
}
