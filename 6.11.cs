using System.ComponentModel.DataAnnotations;
using System.Data.Common;

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
        private List<Fish> _possibleFishes = new List<Fish>();

        public void ShowFuctionList(out bool isWork)
        {
            isWork = true;
            Console.Clear();
            Live();
            ShowFishes();
            Console.WriteLine("Выбирите команду");
            Console.WriteLine($"{CommandAddFish}. Добавить рыбу\n{CommandRemoveFish}. Достать рыбу\n{CommandExit}. Выход");

            if (GetNumber(out int result))
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
                int fishCount = 0;
                Console.WriteLine($"Выбирете рыбу, которую хотите добавить.");

                foreach(var fish in PossibleFishes())
                {
                    Console.WriteLine(++fishCount + " " + fish.Name);
                }

                if (GetNumber(out int result, maxCountFishes))
                {
                    _fishes.Add(PossibleFishes()[result - 1]);
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

            if (GetNumber(out int result) && result <= _fishes.Count && result > 0)
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

                    if (fish.IsAlive)
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

        private bool GetNumber(out int number, int maxFishes)
        {
            if (int.TryParse(Console.ReadLine(), out int result) && result - 1 < maxFishes && result > 0)
            {
                number = result;
                return true;
            }
            else
            {
                number = 0;
                return false;
            }
        }

        private bool GetNumber(out int number)
        {
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                number = result;
                return true;
            }
            else
            {
                number = 0;
                return false;
            }
        }

        private List<Fish> PossibleFishes()
        {
            List<Fish> fishs = new List<Fish>();
            fishs.Add(new Clown("Clown",5));
            fishs.Add(new Pike("Pike", 10));
            fishs.Add(new Carp("Carp", 7));
            fishs.Add(new Fugu("Fugu", 8));
            return fishs;
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

        public bool IsAlive => Age <= MaxAge;
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
