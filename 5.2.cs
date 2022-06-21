using System;
using System.Collections.Generic;

namespace _5._2
{
    class Program
    {
        static void Main(string[] args)
        {
            GetStart();
        }

        static void GetQueues(Queue<int> gust)
        {
            gust.Enqueue(123);
            gust.Enqueue(12515);
            gust.Enqueue(135136);
            gust.Enqueue(21424);
            gust.Enqueue(5335);
            gust.Enqueue(540);
        }

        static void CalculateGust(Queue<int> gusts, ref int score)
        {
            score = score + gusts.Peek();
            gusts.Dequeue();
        }

        static void GetStart()
        {
            Queue<int> gusts = new Queue<int>();
            GetQueues(gusts);
            int score = 0;

            while (gusts.Count > 0)
            {
                Console.WriteLine("Ваш счёт: " + score);

                foreach (var gust in gusts)
                {
                    Console.WriteLine("Сумма покупки на: " + gust.ToString());
                }
                CalculateGust(gusts, ref score);
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Общая сумма покупки - " + score);
        }

    }
}
