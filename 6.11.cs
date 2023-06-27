using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Net;

namespace Fish_and_Chips
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Work();
        }
    }

    class Aquarium
    {
        const int MaxFishCount = 10;
        private List<Fish> _fishes = new List<Fish>();
        private List<Fish> _possibleFishes = new List<Fish>();

        public Aquarium()
        {
            FillFishList();
        }

        public void Work()
        {
            const int CommandAddFish = 1;
            const int CommandRemoveFish = 2;
            const int CommandExit = 3;
            bool isWork = true;

            while (isWork)
            {
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
        }

        private void Live()
        {
            foreach (var fish in _fishes)
            {
                fish.GrowOld();
            }
        }

        private void AddFish()
        {
            if (_fishes.Count < MaxFishCount)
            {
                Console.WriteLine($"Выбирете рыбу, которую хотите добавить.");
                ShowPossibleFishs();

                if (TryPeekFish(out Fish fish, _possibleFishes))
                {
                    _fishes.Add(fish);
                }
            }
            else
            {
                Console.WriteLine("В аквариуме слишком много рыб");
            }
        }

        private void RemoveFish()
        {
            if (_fishes.Count != 0)
            {
                Console.WriteLine("Выбирете рыбу которую хотите достать.");
                ShowFishes();

                if (TryPeekFish(out Fish fish, _fishes))
                {
                    Console.WriteLine($"Вы достали рыбу {fish.Name}");
                    _fishes.Remove(fish);
                }
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
                    Console.WriteLine($"{++count}. {fish.ShowStat()}");
                }
            }
            else
            {
                Console.WriteLine("Аквариум пуст.");
            }
            Console.WriteLine("-----------");
        }

        private void FillFishList()
        {
            _possibleFishes.Add(new Clown());
            _possibleFishes.Add(new Pike());
            _possibleFishes.Add(new Carp());
            _possibleFishes.Add(new Fugu());
        }

        private void ShowPossibleFishs()
        {
            int fishCount = 0;
            foreach (var fish in _possibleFishes)
            {
                Console.WriteLine($"{++fishCount} {fish.Name}");
            }
        }

        private bool TryPeekFish(out Fish fish, List<Fish> fishes)
        {
            if (int.TryParse(Console.ReadLine(), out int result) && result <= fishes.Count && result > 0)
            {
                if (fishes == _possibleFishes)
                {
                    fish = fishes[result - 1].Clone();
                    return true;
                }
                else
                {
                    fish = fishes[result - 1];
                    return true;
                }
            }
            fish = null;
            return fish != null;
        }
    }

    abstract class Fish
    {
        public Fish(string name, int maxAge)
        {
            Name = name;
            MaxAge = maxAge;
        }

        public abstract Fish Clone();

        public string Name { get; private set; }
        public int Age { get; private set; }
        public int MaxAge { get; protected set; }
        public bool IsAlive => Age <= MaxAge;

        public void GrowOld()
        {
            Age++;
        }

        public string ShowStat()
        {
            string status = IsAlive ? $"{Age} Лет" : "Мертва";
            return $"{Name} : {status}";
        }
    }

    class Clown : Fish
    {
        public Clown() : base("Clown", 8) { }

        public override Fish Clone()
        {
            return new Clown(Name, MaxAge);
        }
    }

    class Pike : Fish
    {
        public Pike() : base("Pike", 5) { }

        public override Fish Clone()
        {
            return new Pike(Name, MaxAge);
        }
    }

    class Carp : Fish
    {
        public Carp() : base("Carp", 10) { }

        public override Fish Clone()
        {
            return new Carp(Name, MaxAge);
        }
    }

    class Fugu : Fish
    {
        public Fugu() : base("Fugu", 4) { }

        public override Fish Clone()
        {
            return new Fugu(Name, MaxAge);
        }
    }
}
