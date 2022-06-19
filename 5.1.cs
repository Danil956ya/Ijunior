using System;
using System.Collections.Generic;

namespace _5._1
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMessage();
        }

        static void SendListWords(List<string> words)
        {
            Console.Clear();
            Console.WriteLine("Введите слово для определения его значения.");

            foreach (var word in words)
            {
                Console.WriteLine(word);
            }
        }

        static void SendMessage()
        {
            Dictionary<string, int> valueNumber = new Dictionary<string, int>();
            List<string> words = new List<string>();
            int index = 0;
            words.Add("Один");
            words.Add("Два");
            words.Add("Три");
            words.Add("Четыре");
            words.Add("Пять");

            foreach (var word in words)
            {
                valueNumber.Add(word, ++index);
            }

            SendListWords(words);
            FindValueOf(valueNumber);
        }

        static string FindValueOf(Dictionary<string,int> value)
        {
            string message = "";

            while (message != "выход")
            {
                message = Console.ReadLine();

                if (value.ContainsKey(message))
                { 
                    Console.WriteLine("Значение: " + value[message]);
                }
                else
                {
                    Console.WriteLine("Попробуйте ещё раз.");
                }
            }
            return message;
        }

    }
}
