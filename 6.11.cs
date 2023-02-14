using System.ComponentModel.DataAnnotations;

namespace Fish_and_Chips
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            bool isWork = true;

            while (isWork)
            {
                aquarium.ShowFuctionList(out isWork);
            }
        }
    }

    class Aquarium
    {
        private const int CommandAddFish = 1;
        private const int CommandRemoveFish = 2;
        private const int CommandExit = 3;
        private const int MaxCount = 10;
        private List<Fish> _fishes = new List<Fish>();

        public void ShowFuctionList(out bool isWork)
        {
            isWork = true;
            Console.Clear();
            Live();
            ShowFishes();
            Console.WriteLine("Выбирите команду");
            Console.WriteLine($"{CommandAddFish}. Добавить рыбу\n{CommandRemoveFish}. Достать рыбу\n{CommandExit}. Выход");

            if (int.TryParse(Console.ReadLine(), out int result))
            {
                switch (result)
                {
                    case CommandAddFish:
                        AddFish();
                        break;
                    case CommandRemoveFish:
                        RemoveFish();
                        break;
                    case CommandExit:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда - попробуйте ещё");
                        break;

                }
            }
        }

        private void Live()
        {
            if (_fishes.Count > 0)
            {
                foreach (var fish in _fishes)
                {
                    fish.LiveAge();
                }
            }
        }

        private void AddFish()
        {
            if (_fishes.Count < MaxCount)
            {
                int maxCountFishes = 4;
                Console.WriteLine($"Выбирете рыбу, которую хотите добавить.");
                Console.WriteLine($"1. Рыба клоун\n2. Щука\n3. Карп\n4. Фугу");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && result - 1 < maxCountFishes && result > 0)
                {
                    switch (result)
                    {
                        case 1:
                            _fishes.Add(new Clown("Рыба Клоун", 5));
                            break;
                        case 2:
                            _fishes.Add(new Pike("Щука", 15));
                            break;
                        case 3:
                            _fishes.Add(new Carp("Карп", 10));
                            break;
                        case 4:
                            _fishes.Add(new Fugu("Фугу", 7));
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("В аквариуме слишком много рыб");
            }
        }

        private void RemoveFish()
        {
            Console.WriteLine("Выбирете рыбу которую хотите достать.");
            ShowFishes();
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result) && result <= _fishes.Count && result > 0)
            {
                Console.WriteLine($"Вы достали рыбу {_fishes[result - 1].Name}");
                _fishes.Remove(_fishes[result - 1]);
            }
        }

        private void ShowFishes()
        {
            Console.WriteLine("В аквариуме");
            Console.WriteLine("-----------");

            if (_fishes.Count != 0)
            {
                int count = 0;

                foreach (var fish in _fishes)
                {
                    count++;
                    if (fish.IsAlive())
                    {
                        Console.WriteLine($"{count} {fish.Name}: {fish.Age} лет");
                    }
                    else
                    {
                        Console.WriteLine($"{count} {fish.Name}: мертва.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Аквариум пуст.");
            }
            Console.WriteLine("-----------");
        }
    }

    class Fish
    {
        public Fish(string name, int maxAge)
        {
            Name = name;
            MaxAge = maxAge;
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public int MaxAge { get; protected set; }

        public void LiveAge()
        {
            Age++;
        }

        public bool IsAlive()
        {
            return Age <= MaxAge;
        }
    }

    class Clown : Fish
    {
        public Clown(string name, int maxAge) : base(name, maxAge) { }
    }

    class Pike : Fish
    {
        public Pike(string name, int maxAge) : base(name, maxAge) { }
    }

    class Carp : Fish
    {
        public Carp(string name, int maxAge) : base(name, maxAge) { }
    }

    class Fugu : Fish
    {
        public Fugu(string name, int maxAge) : base(name, maxAge) { }
    }
}
