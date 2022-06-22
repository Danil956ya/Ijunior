using System;
using System.Collections.Generic;

namespace _5._4
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
                string input = Console.ReadLine();

                if (int.TryParse(input, out int number) == true)
                {
                    switch (number)
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
                else
                {
                    Console.Clear();
                    Console.WriteLine("Неверная команда.");
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

            if (dossiers.ContainsKey(name))
            {
                Console.WriteLine("Имя занято.");
            }
            else
            {
                dossiers.Add(name, job);
            }

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
            Console.WriteLine("Введите полное имя из доссье.");
            string input = Console.ReadLine();

            if (dossiers.ContainsKey(input))
            {
                dossiers.Remove(input);
            }
            else
            {
                Console.WriteLine("Досье не найдено.");
            }

        }

    }
}
