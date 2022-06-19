using System;

namespace junior_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What do you want to enter?");
            string text = Convert.ToString(Console.ReadLine());
            Console.WriteLine("How many times do you want to display on the screen?");
            int count = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i != count; i++)
            {
                Console.WriteLine("{0}", text);
            }  
        }
    }
}
