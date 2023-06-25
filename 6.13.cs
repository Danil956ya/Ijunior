using System.Data.Common;

namespace Car_On_Crup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            CarServes carServes = new CarServes();
            carServes.ServeClient();
        }
    }

    class CarServes
    {
        Warehouse warehouse = new Warehouse();
        Queue<Client> clients = new Queue<Client>();
        public CarServes()
        {
            OpenServes();
        }

        public void OpenServes()
        {
            clients.Enqueue(new Client());
            clients.Enqueue(new Client());
            clients.Enqueue(new Client());
            clients.Enqueue(new Client());
            clients.Enqueue(new Client());
        }
        public void ServeClient()
        {
            clients.Peek().ShowCar();
            ShowStorage();
        }

        public void ShowStorage()
        {
            warehouse.ShowDetails();
        }
    }

    class Warehouse
    {
        Random random = new Random();
        List<Detail> DetailsOnStorage = new List<Detail>();

        public Warehouse()
        {
            int index;
            for (int i = 0; i < 10; i++)
            {
                index = random.Next(AvableDetails().Count);
                DetailsOnStorage.Add(AvableDetails()[index]);
            }
        }

        public void ShowDetails()
        {
            Console.WriteLine("_________________\nДетали на складе.\n-----------------");
            foreach(var detail in DetailsOnStorage)
            {
                detail.ShowStatus();
            }
        }

        private List<Detail> AvableDetails()
        {
            List<Detail> details = new List<Detail>();
            details.Add(new Wheel_12());
            details.Add(new Wheel_13());
            details.Add(new ZMZ24());
            details.Add(new VAZ2107());
            details.Add(new VolgaRadiator());
            details.Add(new VAZRadiator());
            details.Add(new Battary(100));
            return details;
        }
    }

    class Client
    {
        Car car = new Car();
        Random random= new Random();

        public Client()
        {
            car = GetRandomCar();
        }

        private Car GetRandomCar()
        {
            int index;
            index = random.Next(AvableCars().Count);
            return AvableCars()[index];
        }
        private List<Car> AvableCars()
        {
            List<Car> cars = new List<Car>();
            cars.Add(new Volga());
            cars.Add(new Lada2107());
            return cars;
        }

        public void ShowCar()
        {
            car.ShowStat();
        }
    }

    class Car
    {
        public Car()
        {
            Details.Add(Engine);
            Details.Add(Wheel);
            Details.Add(Radiator);
        }

        public string Name { get; protected set; }
        public Engine Engine { get; protected set; }
        public Wheel Wheel { get; protected set; }
        public Radiator Radiator { get; protected set; }
        public Battary Battary { get; protected set; }

        private List<Detail> Details = new List<Detail>();
        private Random random = new Random();

        public void ShowStat()
        {
            Console.WriteLine($"[{Name}]\n{Engine.Name} [{Engine.DetailState}]\n{Wheel.Name} [{Wheel.DetailState}]\n{Radiator.Name} [{Radiator.DetailState}]\n{Battary.Name}: {Battary.BattaryStatus}%");
        }

        private void SetRandomDetailBreak()
        {
            int NeedIndex = random.Next(0, Details.Count);
            int index = 0;
            foreach(var detail in Details)
            {
                index++;
                if(index == NeedIndex)
                {
                    
                }
            }
        }
    }

    class Volga : Car
    {
        public Volga()
        {
            Name = "Волга - 24";
            Engine = new ZMZ24();
            Wheel = new Wheel_12();
            Radiator = new VolgaRadiator();
            Battary = new Battary();
        }
    }

    class Lada2107 : Car
    {
        public Lada2107()
        {
            Name = "ВАЗ - 2107";
            Engine = new VAZ2107();
            Wheel = new Wheel_13();
            Radiator = new VAZRadiator();
            Battary = new Battary();
        }
    }

    class Detail
    {
        public string Name { get; protected set; }
        public bool IsFixed { get; protected set; }
        public string DetailState { get; protected set; }
        public int PriceCost { get; protected set; }

        public Detail()
        {
            IsFixed = true;
            DetailState = IsFixed ? "Починено" : "Сломано";
        }

        public virtual void ShowStatus() { }
    }

    class Battary : Detail
    {
        public Battary(int status)
        {
            Name = "Аккумулятор";
            BattaryStatus = status;
        }

        public Battary()
        {
            Name = "Аккумулятор";
            BattaryStatus = GetBattaryStatus();
        }

        public int BattaryStatus { get; protected set; }

        private Random _random = new Random();
        private const int MaxCount = 100;

        public override void ShowStatus()
        {
            Console.WriteLine($"{Name}: {BattaryStatus}");
        }

        private int GetBattaryStatus()
        {
            int index = _random.Next(0, MaxCount);
            return index;
        }
    }

    class Wheel : Detail
    { 
        public Wheel()
        {
            Name = "Колеса";
            IsFixed = base.IsFixed;
        }
        public int Radius { get; protected set; }

        public override void ShowStatus()
        {
            Console.WriteLine($"{Name}");
        }
    }

    class Wheel_12 : Wheel
    { 
        public Wheel_12()
        {
            Radius = 12;
            Name = $"{base.Name}: {Radius} дюймов";
        }
    }

    class Wheel_13 : Wheel
    {
        public Wheel_13()
        {
            Radius = 13;
            Name = $"{base.Name}: {Radius} дюймов";
        }
    }

    class Radiator : Detail
    {
        public Radiator()
        {
            Name = "Радиатор";
        }
        public string SerialNumber { get; protected set; }
        public override void ShowStatus()
        {
            Console.WriteLine($"{Name}");
        }
    }

    class VolgaRadiator : Radiator
    { 
        public VolgaRadiator()
        {
            SerialNumber = "24-1301010-21";
            Name = $"{base.Name}: {SerialNumber}";
        }
    }

    class VAZRadiator : Radiator
    {
        public VAZRadiator()
        {
            SerialNumber = "2107-1301.012-60";
            Name = $"{base.Name}: {SerialNumber}";
        }
    }

    class Engine : Detail
    {
        public Engine()
        {
            Name = $"Движок";
        }
        public string EngineName { get; protected set; }
        public override void ShowStatus()
        {
            Console.WriteLine($"{Name}");
        }
    }

    class VAZ2107 : Engine
    {
        public VAZ2107()
        {
            EngineName = "VAZ 2107";
            Name = $"Движок: {EngineName}";
            PriceCost = 10;
        }
    }

    class ZMZ24 : Engine
    { 
        public ZMZ24()
        {
            EngineName = "ZMZ-24";
            Name = $"Движок: {EngineName}";
            PriceCost = 10;
        }
    }
}
