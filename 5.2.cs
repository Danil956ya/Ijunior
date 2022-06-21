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

        static void GetQueues(Dictionary<string, int> gusts)
        {
            gusts.Add("Вася", 250);
            gusts.Add("Даня", 280);
            gusts.Add("Настя", 260);
            gusts.Add("Вова", 230);
        }

        static void CalculateGust(Dictionary<string, int> gusts, string name, int number, ref int score)
        {
            gusts.Remove(name);
            score = score + number;
        }

        static void GetStart()
        {
            Dictionary<string, int> gusts = new Dictionary<string, int>();
            GetQueues(gusts);
            int score = 0;
            string tempName = "";
            int number = 0;

            while (gusts.Count > 0)
            {
                Console.WriteLine("Ваш счёт: " + score);

                foreach (var gust in gusts)
                {
                    Console.WriteLine("Гость - " + gust.Key + ", Сумма покупки на: " + gust.Value);
                    tempName = gust.Key;
                    number = gust.Value;
                }

                CalculateGust(gusts, tempName, number, ref score);
                Console.ReadKey();
                Console.Clear();
            }

            Console.WriteLine("Общая сумма покупки - " + score);
        }

    }
}
