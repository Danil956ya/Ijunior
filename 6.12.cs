namespace Zoo_In_Gloo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            bool isWork = true;
            zoo.ShowFunctionList(ref isWork);
        }
    }

    public class Zoo
    {
        private List<Aviary> _aviaries = new List<Aviary>();
        private const int CommandChoice = 1;
        private const int CommandExit = 2;

        public Zoo()
        {
            for (int i = 0; i < 6; i++)
            {
                _aviaries.Add(new Aviary());
            }
        }

        public void ShowFunctionList(ref bool isWork)
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
            int index = 0;
            Console.WriteLine("Укажите номер вальера");

            foreach(var i in _aviaries)
            {
                Console.WriteLine(++index + " Вальер");
            }

            if (int.TryParse(Console.ReadLine(), out int result) && result <= _aviaries.Count())
            {
                _aviaries[result - 1].ShowInfo();
            }
        }
    }


    public class Aviary
    {
        private List<Animal> _animals = new List<Animal>();

        public Aviary()
        {
            for (int i = 0; i < GetRandomNumber(); i++)
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

        private List<Animal> Avable()
        {
            List<Animal> animals= new List<Animal>();
            animals.Add(new Lion());
            animals.Add(new Turttle());
            animals.Add(new Giraffe());
            animals.Add(new Tiger());
            animals.Add(new Monkey());
            animals.Add(new Hippopotamus());
            animals.Add(new Crocodile());
            return animals;
        }

        private Animal GetRandomAnimal()
        {
            Random random = new Random();
            int index = random.Next(0, Avable().Count);
            return Avable()[index];
        }

        private int GetRandomNumber()
        {
            Random random= new Random();
            int number = random.Next(0, 10);
            return number;
        }
    }

    public class Animal
    {
        public Animal()
        {
            IsMale = GetAnimalSex();
            Name = GetRandomName();
            Sex = IsMale == true ? "Самец" : "Самка";
        }

        public string AnimalName { get; protected set; }
        public string Sex { get; protected set; }
        public string Name { get; protected set; }
        public string Voice { get; protected set; }
        public bool IsMale { get; protected set; }

        private Random _random = new Random();

        public void ShowStats()
        {
            Console.WriteLine($"{Sex}: {AnimalName} {Name} \nИздаёт звук: {Voice}\n");
        }

        private bool GetAnimalSex()
        {
            int number;
            number = _random.Next(0, 4);
            return number >= 2;
        }

        private string GetRandomName()
        {
            int index;
            string[] MalesNames = { "Danya", "Dima", "Vova", "Victor", "Melman", "Jenya", "Igor" };
            string[] FemalesNames = { "Alena", "Vika", "Katya", "Natasha", "Irina", "Elena", "Lisa" };
            string[] names;
            names = IsMale ? MalesNames : FemalesNames;
            index = _random.Next(names.Length);
            return names[index];
        }
    }

    public class Lion : Animal
    {
        public Lion()
        {
            AnimalName = IsMale ? "Лев" : "Львица";
            Voice = "Rrrrr";
        }
    }

    public class Turttle : Animal
    { 
        public Turttle()
        {
            AnimalName = "Черепаха";
            Voice = "KAWABANGA";
        }
    }

    public class Giraffe : Animal
    {
        public Giraffe()
        {
            AnimalName = "Жираф";
            Voice = $"Hello my name is {Name}";
        }
    }

    public class Tiger : Animal
    {
        public Tiger()
        {
            AnimalName = IsMale ? "Тигр" : "Тигрица";
            Voice = "RRRRRRR";
        }
    }

    public class Monkey : Animal
    {
        public Monkey()
        {
            AnimalName = "Обезьянка";
            Voice = "YyYyYy";
        }
    }

    public class Crocodile : Animal
    {
        public Crocodile()
        {
            AnimalName = "Крокодил";
            Voice = "AUW";
        }
    }

    public class Hippopotamus : Animal
    {
        public Hippopotamus()
        {
            AnimalName = "Бегемот";
            Voice = "Ui ua ua";
        }
    }
}
