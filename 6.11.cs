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

        public Zoo()
        {
            int MaxArivary = 6;

            for (int i = 0; i < MaxArivary; i++)
            {
                _aviaries.Add(new Aviary());
            }
        }

        public void Work()
        {
            bool isWork = true;
            const int CommandChoice = 1;
            const int CommandExit = 2;

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

            for (int i = 0; i < _aviaries.Count; i++)
            {
                Console.WriteLine($"{i + 1} Вальер.");
            }

            if (int.TryParse(Console.ReadLine(), out int result) && result <= _aviaries.Count && result > 0) ;
            {
                _aviaries[result - 1].ShowInfo();
            }
        }
    }


    public class Aviary
    {
        private List<Animal> _animals = new List<Animal>();
        private List<Animal> _avableAnimals = new List<Animal>();

        public Aviary()
        {
            int MaxAnimalCount = 10;

            FillAnimalList();

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

        private void FillAnimalList()
        {
            _avableAnimals.Add(new Lion());
            _avableAnimals.Add(new Turttle());
            _avableAnimals.Add(new Giraffe());
            _avableAnimals.Add(new Tiger());
            _avableAnimals.Add(new Monkey());
        }
    }

    public abstract class Animal
    {
        private const int MaxRandomChance = 4;
        private const int MinRandomChance = 2;

        public Animal(string type, string voice)
        {
            IsMale = GetSex();
            Name = GetRandomName();
            Sex = IsMale ? "Самец" : "Самка";
            Type = type;
            Voice = voice;
        }

        public string Type { get; protected set; }
        public string Sex { get; protected set; }
        public string Name { get; protected set; }
        public string Voice { get; protected set; }
        public bool IsMale { get; protected set; }

        public abstract Animal Clone();

        public void ShowStats()
        {
            Console.WriteLine($"{Sex}: {Type} {Name} \nИздаёт звук: {Voice}\n");
        }

        private bool GetSex()
        {
            return Number.GetRandom(MaxRandomChance) >= MinRandomChance;
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
        public Lion() : base("Лев", "Rrrrr")
        {
            Type = IsMale ? "Лев" : "Львица";
        }

        public override Animal Clone()
        {
            return new Lion();
        }
    }

    public class Turttle : Animal
    {
        public Turttle() : base("Черепаха", "Qva") { }

        public override Animal Clone()
        {
            return new Turttle();
        }
    }

    public class Giraffe : Animal
    {
        public Giraffe() : base("Жираф", "DAdaDada") { }

        public override Animal Clone()
        {
            return new Giraffe();
        }
    }

    public class Tiger : Animal
    {
        public Tiger() : base("Тигр", "meyw")
        {
            Type = IsMale ? "Тигр" : "Тигрица";
        }

        public override Animal Clone()
        {
            return new Tiger();
        }
    }

    public class Monkey : Animal
    {
        public Monkey() : base("Обезьяна", "YYYyyyYYY") { }

        public override Animal Clone()
        {
            return new Monkey();
        }
    }
}
