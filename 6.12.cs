namespace Zoo_In_Gloo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            zoo.Work();
        }
    }

    public static class Number
    { 
        private static Random _random = new Random();

        public static int GetRandom(int count)
        {
            return _random.Next(count);
        }
    }


    public class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();
        private const int CommandChoice = 1;
        private const int CommandExit = 2;
        private const int MaxArivary = 6;
        private bool isWork = true;

        public Zoo()
        {
            for (int i = 0; i < MaxArivary; i++)
            {
                _aviaries.Add(new Aviary());
            }
        }

        public void Work()
        {
            while (isWork)
            {
                Console.WriteLine($"{CommandChoice}. - Подойти к вальеру\n{CommandExit}. - выход");
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    switch (result)
                    {
                        case CommandChoice:
                            ChoiceAviary();
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

        private void ChoiceAviary()
        {
            Console.WriteLine("Укажите номер вальера");

            for (int i = 0; i < MaxArivary; i++)
            {
                Console.WriteLine($"{i + 1} Вальер.");
            }

            if (int.TryParse(Console.ReadLine(), out int result) && result <= _aviaries.Count && result > 0);
            {
                _aviaries[result - 1].ShowInfo();
            }
        }
    }


    public class Aviary
    {
        private List<Animal> _animals = new List<Animal>();
        private List<Animal> _avableAnimals = new List<Animal>() { new Lion("Лев", "Rrrrr"), new Turttle("Черепаха", "Qva") , new Giraffe("Жираф", "GeGeGe"), new Tiger("Тигр", "Rrrrrr"), new Monkey("Обезьяна", "YyYyYy"), new Hippopotamus("Бегемот", "UAAAA"), new Crocodile("Крокодил", "AaUuWw") };
        private const int MaxAnimalCount = 10;

        public Aviary()
        {
            for (int i = 0; i < Number.GetRandom(MaxAnimalCount); i++)
            {
                _animals.Add(GetRandomAnimal());
            }
        }

        public void ShowInfo()
        {
            Console.Clear();
            if (_animals.Count > 0)
            {
                Console.WriteLine($"В вальере - {_animals.Count} животных");
                foreach (var animal in _animals)
                {
                    animal.ShowStats();
                }
            }
            else
            {
                Console.WriteLine("Вальер пуст");
            }
        }

        private Animal GetRandomAnimal()
        {
            return _avableAnimals[Number.GetRandom(_avableAnimals.Count)].Clone();
        }
    }

    public abstract class Animal
    {
        public Animal(string animalName, string voice)
        {
            IsMale = GetAnimalSex();
            Name = GetRandomName();
            Sex = IsMale? "Самец" : "Самка";
            AnimalName = animalName;
            Voice = voice;
        }

        public abstract Animal Clone();
        public string AnimalName { get; protected set; }
        public string Sex { get; protected set; }
        public string Name { get; protected set; }
        public string Voice { get; protected set; }
        public bool IsMale { get; protected set; }

        private const int MaxChance = 4;
        private const int MinChance = 2;

        public void ShowStats()
        {
            Console.WriteLine($"{Sex}: {AnimalName} {Name} \nИздаёт звук: {Voice}\n");
        }

        private bool GetAnimalSex()
        {
            return Number.GetRandom(MaxChance) >= MinChance;
        }

        private string GetRandomName()
        {
            int index;
            string[] MalesNames = { "Danya", "Dima", "Vova", "Victor", "Melman", "Jenya", "Igor" };
            string[] FemalesNames = { "Alena", "Vika", "Katya", "Natasha", "Irina", "Elena", "Lisa" };
            string[] names;
            names = IsMale ? MalesNames : FemalesNames;
            index = Number.GetRandom(names.Length);
            return names[index];
        }
    }

    public class Lion : Animal
    {
        public Lion(string animalName, string voice) : base(animalName, voice) 
        {
            AnimalName = IsMale ? "Lev" : "Lviza";
        }

        public override Animal Clone()
        {
            return new Lion(AnimalName, Voice);
        }
    }

    public class Turttle : Animal
    {
        public Turttle(string animalName, string voice) : base(animalName, voice) { }

        public override Animal Clone()
        {
            return new Turttle(AnimalName, Voice);
        }
    }

    public class Giraffe : Animal
    {
        public Giraffe(string animalName, string voice) : base(animalName, voice) { }

        public override Animal Clone()
        {
            return new Giraffe(AnimalName, Voice);
        }
    }

    public class Tiger : Animal
    {
        public Tiger(string animalName, string voice) : base(animalName, voice)
        {
            AnimalName = IsMale ? "Тигр" : "Тигрица";
        }

        public override Animal Clone()
        {
            return new Tiger(AnimalName, Voice);
        }
    }

    public class Monkey : Animal
    {
        public Monkey(string animalName, string voice) : base(animalName, voice) { }

        public override Animal Clone()
        {
            return new Monkey(AnimalName, Voice);
        }
    }

    public class Crocodile : Animal
    {
        public Crocodile(string animalName, string voice) : base(animalName, voice) { }

        public override Animal Clone()
        {
            return new Crocodile(AnimalName, Voice);
        }
    }

    public class Hippopotamus : Animal
    {
        public Hippopotamus(string animalName, string voice) : base(animalName, voice) { }

        public override Animal Clone()
        {
            return new Hippopotamus(AnimalName,Voice);
        }
    }
}
