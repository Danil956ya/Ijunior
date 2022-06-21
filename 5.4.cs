using System;
using System.Collections.Generic;

namespace _4._1
{
    class Program
    {
        static void Main()
        {
            Dictionary<string, string> dossiers = new Dictionary<string, string>();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Выбирете пункт. \n1. Добавить досье.\n2. Вывести досье.\n3. Удалить досье.\n4. Выход.");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        AddDossier(dossiers);
                        break;
                    case 2:
                        OutputDossiers(dossiers);
                        break;
                    case 3:
                        DeleteDossiers(dossiers);
                        break;
                    default:
                        isWork = false;
                        Console.WriteLine("Пока!");
                        Console.ReadKey();
                        break;
                }
            }

        }

        static void AddDossier(Dictionary<string, string> dossiers)
        {
            Console.Clear();
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("введите професиию.");
            string job = Console.ReadLine();
            dossiers.Add(name, job);
            Console.Clear();
            return;
        }
        static void OutputDossiers(Dictionary<string, string> dossiers)
        {
            Console.Clear();
            if (dossiers.Count <= 0)
            {
                Console.WriteLine("В списке пока никого нету.");
            }
            else
            {
                Console.WriteLine("Имя и должность.");
                foreach (var dossier in dossiers)
                {
                    Console.WriteLine(dossier);
                }
            }
        }

        static void DeleteDossiers(Dictionary<string, string> dossiers)
        {
            Console.Clear();
            Console.WriteLine("Введите имя из доссье.");
            string input = Console.ReadLine();

            if (dossiers.ContainsKey(input))
            {
                dossiers.Remove(input);
            }
        }

    }
}
