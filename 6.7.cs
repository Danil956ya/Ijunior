class Program
{
    static void Main(string[] args)
    {
        TrainProgram program = new TrainProgram();
        bool _isWork = true;

        while (_isWork)
        {
            Console.WriteLine("1. Назначить направление. 2. Продать билеты. 3.Сформировать поезд 4. Отправить поезд.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    program.GetDirection();
                    break;
                case "2":   
                    program.SellTicets();
                    break;
                case "3":
                    program.OcupSeats();
                    break;
                case "4":
                    program.SendTrain();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Неверная команда.");
                    break;

            }

        }
    }
}

class TrainProgram
{
    private Direction _direction = new Direction();
    private Train _train = new Train();
    private TicetsOffice _office = new TicetsOffice();
    private bool _isDirection = false;
    private bool _isSelling = false;
    private bool _isOcupped = false;

    public void GetDirection()
    {
        _direction.SetDirection();
        _isDirection = true;
    }

    public void SellTicets  ()
    {
        _office.SellTicets();
        _isSelling = true;
    }

    public void OcupSeats()
    {
        if (_isSelling == false)
        {
            Console.Clear();
            Console.WriteLine("Сначала продайте билеты.");
        }
        else
        {
            _train.OcupSeats(_office.SoldCount);
            _isOcupped = true;
        }
    }

    public void SendTrain()
    {
        if (IsComplete())
        {
            _train.SendTrain();
            _isSelling = false;
            _office.IsSelling(_isSelling);
            _isOcupped = false;
            _train.IsFull(_isOcupped);
            _isDirection = false;
            _direction.IsCreated(_isDirection);
        }
        else
        {
            Console.WriteLine("Не все условия выполнены.");
        }
    }

    private bool IsComplete()
    {
        bool complete;
        if (_isOcupped && _isSelling && _isDirection)
        {
            complete = true;
            return complete;
        }
        else
        {
            complete = false;
            return complete;
        }
    }
}

class Direction
{
    private List<Station> _stations = new List<Station>();
    private string _firstStation;
    private string _lastStation;
    private bool _isCreated;

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

    public void IsCreated(bool created)
    {
        _isCreated = created;
    }

    public void SetDirection()
    {
        if (_isCreated == false)
        {
            Console.Clear();
            ShowStations();
            Console.WriteLine("Укажите откуда хотите ехать.");
            _firstStation = GetStation(Console.ReadLine());
            Console.WriteLine("Укажите куда хотите ехать.");
            _lastStation = GetStation(Console.ReadLine());

            if (_firstStation == _lastStation)
            {
                IsCreated(false);
                Console.WriteLine("Неверное направление. Попробуйте ещё.");
                SetDirection();
            }
            IsCreated(true);
            Console.Clear();
            ShowDirection();
        }
        else
        {
            ShowDirection();
        }
    }

    public void ShowDirection()
    {
        if (_isCreated)
        {
            Console.Clear();
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

}

class Station
{
    public string Name { get; private set; }

    public Station(string name)
    {
        Name = name;
    }

}

class TicetsOffice
{
    private bool _isSelling;
    private Random _random = new Random();
    private int _minCountSold = 25;
    private int _maxCountSold = 101;
    public int SoldCount { get; private set; }

    public void IsSelling(bool isSelling)
    {
        _isSelling = isSelling;
    }

    public void SellTicets()
    {
        if (_isSelling == false)
        {
            Console.Clear();
            SoldCount = _random.Next(_minCountSold, _maxCountSold);
            Console.WriteLine($"Было продано {SoldCount} билетов.");
            _isSelling = true;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Билеты проданы.");
            Console.WriteLine($"Продано {SoldCount} билетов.");
        }
    }

}

class Seat
{
    public bool IsOcupped = false;
    public int Number { get; private set; }

    public Seat(int number)
    {
        Number = number;
    }

    public void ShowInfo()
    {
        Console.WriteLine(GetMessage());
    }

    private string GetMessage()
    {
        string message = IsOcupped ? $"{Number}) Место занято." : $"{Number}) Место свободно.";
        return message;
    }


}

class Train
{
    private List<Seat> _seats = new List<Seat>();
    private int _countSeats = 30;
    private int _count;
    public bool IsFull { get; private set; }

    public void SendTrain()
    {
        Console.Clear();
        _seats.Clear();
        Console.WriteLine("Поезд отправлен.");
    }

    public void IsFull(bool isFull)
    {
        IsFull = isFull;
    }

    public void OcupSeats(int SoldCount)
    {
        _count = SoldCount;
        if (IsFull == false)
        {
            AddSeats();
            for (int i = 0; i < SoldCount; i++)
            {
                _seats[i].IsOcupped = true;
            }
            foreach (var seat in _seats)
            {
                seat.ShowInfo();
            }
            IsFull = true;
        }
        else
        {
            Console.Clear();
            Console.WriteLine($"Было собрано {GetCars()} вагонов.");
        }

    }

    private int GetSeats()
    {
        int seats = _countSeats * GetCars();
        return seats;
    }

    private int GetCars()
    {
        int cars = 0;
        for (int i = 0; (_countSeats * cars) < _count; i++)
        {
            cars++;
        }
        return cars;
    }

    private void AddSeats()
    {
        for (int i = 0; i < GetSeats(); i++)
        {
            _seats.Add(new Seat(i + 1));
        }
    }

}
