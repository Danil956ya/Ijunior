using System;

namespace _6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Сколько у вас золота?");
            int gold = Convert.ToInt32(Console.ReadLine());
            int diamondPrice = 5;
            int availables = gold / diamondPrice;
            Console.WriteLine("Сколько вам нужно алмазов? Один алзмаз стоит: {0}, вам доступно {1} алмазов", diamondPrice, availables);
            int diamonds = Convert.ToInt32(Console.ReadLine());
            gold = gold - diamonds * diamondPrice;

            Console.WriteLine("Ваше золото {0} \n Ваши алмазы {1}", gold, diamonds);
        }
    }
}
