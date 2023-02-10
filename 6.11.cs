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
        const int MaxCount = 10;
        private List<Fish> _fishs = new List<Fish>();

        public void ShowFuctionList(out bool isWork)
        {
            isWork = true;
            Console.Clear();
            ShowFish();
            Console.WriteLine("Выбирите команду");
            Console.WriteLine("1. Добавить рыбу\n2. Достать рыбу\n3. Выход");

            if (int.TryParse(Console.ReadLine(), out int result))
            {
                switch (result)
                {
                    case 1:
                        AddFish();
                        break;
                    case 2:
                        RemoveFish();
                        break;
                    case 3:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда - попробуйте ещё");
                        break;
                }
            }
            if (_fishs.Count > 0)
            {
                foreach (var fish in _fishs)
                {
                    fish.LiveAge();
                }
            }
        }

        private void AddFish()
        {
            if (_fishs.Count - 1 <= MaxCount)
            {
                int count = 0;
                Console.WriteLine("Выбирете рыбу, которую хотите добавить.");

                foreach (var fish in AvableFish())
                {
                    count++;
                    Console.WriteLine($"{count}: {fish.Name}");
                }

                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && result - 1 < AvableFish().Count && result > 0)
                {
                    _fishs.Add(AvableFish()[result - 1]);
                }
                else
                {
                    Console.WriteLine("Такой рыбы нету");
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
            ShowFish();
            string input = Console.ReadLine();

            if (int.TryParse(input, out int result) && result <= _fishs.Count && result > 0)
            {
                Console.WriteLine($"Вы достали рыбу {_fishs[result - 1].Name}");
                _fishs.Remove(_fishs[result - 1]);
            }
        }

        private void ShowFish()
        {
            Console.WriteLine("В аквариуме");
            Console.WriteLine("-----------");

            if (_fishs.Count != 0)
            {
                int count = 0;
                foreach (var fish in _fishs)
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

        private List<Fish> AvableFish()
        {
            List<Fish> fish = new List<Fish>();
            fish.Add(new Clown("Рыба Клоун"));
            fish.Add(new Pike("Щука"));
            fish.Add(new Carp("Карп"));
            fish.Add(new Fugu("Рыба Фугу"));
            return fish;
        }
    }

    class Fish
    {
        public string Name { get; private set; }
        public int Age { get; private set; }
        public int MaxAge { get; protected set; }

        public Fish(string name)
        {
            Name = name;
        }

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
        public Clown(string name) : base(name) { MaxAge = 5; }
    }

    class Pike : Fish
    {
        public Pike(string name) : base(name) { MaxAge = 20; }
    }

    class Carp : Fish
    {
        public Carp(string name) : base(name) { MaxAge = 15; }
    }

    class Fugu : Fish
    {
        public Fugu(string name) : base(name) { MaxAge = 10; }
    }
}
