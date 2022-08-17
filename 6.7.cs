class Program
{
    static void Main(string[] args)
    {
        Direction direction = new Direction();
        Train train = new Train();
        Ticet ticet = new Ticet();
        bool isWork = true;
        while (isWork)
        {
            Console.WriteLine("1. Назначить напровление. 2. Продать билеты. 3.Сформировать поезд 4. Отправить поезд.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    direction.SetDirection();
                    break;
                case "2":
                    ticet.SellTicets();
                    break;
                case "3":
                    train.OcupSeats();
                    break;
                case "4":
                    direction.SendTrain();
                    break;
                default:
                    Console.WriteLine("Неверная команда.");
                    break;

            }

        }
    }
}

class Direction : Train
{
    public static bool isCreate { get; private set; }

    private List<Station> _stations = new List<Station>();
    private string _firstStation;
    private string _lastStation;

    public Direction()
    {
        _stations.Add(new Station("Курская"));
        _stations.Add(new Station("Серп и Молот"));
        _stations.Add(new Station("Карачарово"));
        _stations.Add(new Station("Чухлинка"));
        _stations.Add(new Station("Кусково"));
        _stations.Add(new Station("Новогиреево"));
        _stations.Add(new Station("Реутов"));
        _stations.Add(new Station("Никольское"));
        _stations.Add(new Station("Салтыковская"));
        _stations.Add(new Station("Кучино"));
        _stations.Add(new Station("Ольгино"));
        _stations.Add(new Station("Железнодорожный"));
    }

    public void ShowStations()
    {
        foreach (var station in _stations)
        {
            Console.WriteLine(station.Name);
        }
    }

    public void SetDirection()
    {
        if (isCreate == false)
        {
            Console.Clear();
            ShowStations();
            Console.WriteLine("Укажите откуда хотите ехать.");
            _firstStation = GetStation(Console.ReadLine());
            Console.WriteLine("Укажите куда хотите ехать.");
            _lastStation = GetStation(Console.ReadLine());

            if (_firstStation == _lastStation)
            {
                isCreate = false;
                Console.WriteLine("Неверное направление. Попробуйте ещё.");
                SetDirection();
            }

            isCreate = true;
            ShowDirection();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Направление уже назначено.");
            ShowDirection();
        }
    }

    public void ShowDirection()
    {
        if (isCreate)
        {
            Console.WriteLine($"Направление: {_firstStation} - {_lastStation}.");
        }
    }

    private string GetStation(string input)
    {
        string stationName = "default";
        bool canGet = false;

        foreach (var station in _stations)
        {
            if (station.Name.ToLower().Contains(input) || station.Name.Contains(input))
            {
                stationName = station.Name;
                canGet = true;
            }
        }

        if (canGet == false)
        {
            Console.WriteLine("Попробуйте ещё раз.");
            stationName = GetStation(Console.ReadLine());
        }

        return stationName;
    }

    public override void SendTrain()
    {
        if (isCreate && isFull && isSelling)
        {
            isCreate = false;
            base.SendTrain();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Выполнены не все условия.");
        }
    }

}

class Station
{
    public string Name { get; private set; }

    public Station(string name)
    {
        Name = name;
    }

}

class Ticet
{
    public static int SoldSount { get; private set; }
    protected static bool isSelling;
    private Random _random = new Random();

    public void SellTicets()
    {
        if (!isSelling)
        {
            Console.Clear();
            SoldSount = _random.Next(25, 101);
            Console.WriteLine($"Было продано {SoldSount} билетов.");
            isSelling = true;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Билеты проданы.");
            Console.WriteLine($"Продано {SoldSount} билетов.");
        }
    }

}

class Seat
{
    public bool IsOcupped = false;
    public int Number { get; private set; }

    public Seat(int num)
    {
        Number = num;
    }

    private string Message()
    {
        string message = IsOcupped ? $"{Number}) Место занято." : $"{Number}) Место свободно.";
        return message;
    }

    public void ShowInfo()
    {
        Console.WriteLine(Message());
    }

}
class Train : Ticet
{
    public static bool isFull { get; private set; }

    protected List<Seat> _seats = new List<Seat>();
    private int _countSeats = 30;

    public void OcupSeats()
    {
        if (isSelling)
        {
            if (isFull == false)
            {
                AddSeats();
                for (int i = 0; i < SoldSount; i++)
                { 
                    _seats[i].IsOcupped = true;
                }
                foreach (var seat in _seats)
                {
                    seat.ShowInfo();
                }
                isFull = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Поезд сформирован. Собрано {Cars()} вагонов");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Сначала продайте билеты.");
        }
    }

    private int Seats()
    {
        return _countSeats * Cars();
    }

    private int Cars()
    {
        int cars = 0;
        for (int i = 0; (_countSeats * cars) < SoldSount; i++)
        {
            cars++;
        }
        return cars;
    }

    private void AddSeats()
    {
        for (int i = 0; i < Seats(); i++)
        {
            _seats.Add(new Seat(i + 1));
        }
    }

    public virtual void SendTrain()
    {
        Console.Clear();
        _seats.Clear();
        isFull = false;
        isSelling = false;
        Console.WriteLine("Поезд отправлен.");
    }

}
