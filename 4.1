using System;

namespace _4._1
{
    class Program
    {
        static void Main()
        {
            string[] fullname = new string[0];
            string[] career = new string[0];
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Выбирете пункт. \n1. Добавить досье.\n2. Вывести досье.\n3. Удалить досье.\n4. Найти по фамилии.\n5. Выход.");
                int input = Convert.ToInt32(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        AddDossier(ref fullname, ref career);
                        break;
                    case 2:
                        OutputDossiers(fullname, career);
                        break;
                    case 3:
                        DeleteDossiers(ref fullname, ref career);
                        break;
                    case 4:
                        FindDossiers(fullname, career);
                        break;
                    default:
                        isWork = false;
                        Console.WriteLine("Пока!");
                        Console.ReadKey();
                        break;
                }
            }

        }

        static void AddDossier(ref string[] fullname, ref string[] career)
        {
            Console.Clear();
            Console.WriteLine("Введите имя");
            string name = Console.ReadLine();
            Console.WriteLine("введите професиию.");
            string job = Console.ReadLine();
            Array.Resize(ref fullname, fullname.Length + 1);
            Array.Resize(ref career, career.Length + 1);
            fullname[fullname.Length - 1] = name;
            career[career.Length - 1] = job;
            Console.Clear();
            return;
        }
        static void OutputDossiers(string[] fullname, string[] career)
        {
            Console.Clear();
            if (fullname.Length == 0)
            {
                Console.WriteLine("В списке пока никого нету.");
            }
            else
            {
                Console.WriteLine("Имя и должность.");
            }

            for (int i = 0; i < fullname.Length; i++)
            {
                Console.WriteLine($"{i + 1}){fullname[i]} - {career[i]}");
            }
        }
        static void DeleteFromArray(ref string[] array, int index)
        {
            if (array.Length != 0)
            {
                string[] TempName = new string[array.Length - 1];

                for (int i = 0; i < index; i++)
                {
                    TempName[i] = array[i];
                }

                for (int i = index + 1; i < array.Length; i++)
                {
                    TempName[i - 1] = array[i];
                }

                array = TempName;
            }
            else
            {
                Console.WriteLine("Список пуст.");
            }
        }
        static void DeleteDossiers(ref string[] fullname, ref string[] job)
        {
            Console.Clear();
            Console.WriteLine("Введите номер досье.");
            int index = Convert.ToInt32(Console.ReadLine()) - 1;
            DeleteFromArray(ref fullname, index);
            DeleteFromArray(ref job, index);
        }
        static void FindDossiers(string[] arrays, string[] job)
        {
            Console.Clear();
            Console.WriteLine("Введите фамилию");
            string input = Console.ReadLine();

            for (int i = 0; i < arrays.Length; i++)
            {
                if (arrays[i].Contains(input))
                {
                    Console.WriteLine($"{i + 1}){arrays[i]} - {job[i]}");
                }
            }
        }
    }
}

