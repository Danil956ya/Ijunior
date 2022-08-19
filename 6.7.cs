class Program
{
    static void Main(string[] args)
    {
        TrainProgram program = new TrainProgram();
        bool isWork = true;
        while (isWork)
        {
            Console.WriteLine("1. Назначить направление. 2. Продать билеты. 3.Сформировать поезд 4. Отправить поезд.");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    program.SetDirection();
                    break;
                case "2":
                    program.SellTikets();
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
    Direction direction = new Direction();
    Train train = new Train();
    TicetsOffice office = new TicetsOffice();
    private bool _isDirection = false;
    private bool _isSelling = false;
    private bool _isOcupped = false;

    public void SetDirection()
    {
        direction.SetDirection();
        _isDirection = true;
    }

    public void SellTikets()
    {
        office.SellTicets();
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
            train.OcupSeats(office.SoldCount);
            _isOcupped = true;
        }
    }
    public void SendTrain()
    {
        if (IsComplete())
        {
            train.SendTrain();
            _isSelling = false;
            office.SetSelling(_isSelling);
            _isOcupped = false;
            train.SetFull(_isOcupped);
            _isDirection = false;
            direction.SetCreated(_isDirection);
        }
        else
        {
            Console.WriteLine("Не все условия выполнены.");
        }
    }
    private bool IsComplete()
    {
        bool dada;
        if (_isOcupped && _isSelling && _isDirection)
        {
            dada = true;
            return dada;
        }
        else
        {
            dada = false;
            return dada;
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

    public void SetCreated(bool created)
    {
        _isCreated = created;
    }

    public virtual void SetDirection()
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
                SetCreated(false);
                Console.WriteLine("Неверное направление. Попробуйте ещё.");
                SetDirection();
            }
            SetCreated(true);
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

    public void SetSelling(bool isSelling)
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

    public Seat(int num)
    {
        Number = num;
    }

    public void ShowInfo()
    {
        Console.WriteLine(Message());
    }

    private string Message()
    {
        string message = IsOcupped ? $"{Number}) Место занято." : $"{Number}) Место свободно.";
        return message;
    }


}

class Train
{
    protected List<Seat> _seats = new List<Seat>();
    private int _countSeats = 30;
    private int _count;
    public bool IsFull { get; private set; }

    public void SendTrain()
    {
        Console.Clear();
        _seats.Clear();
        Console.WriteLine("Поезд отправлен.");
    }

    public void SetFull(bool isFull)
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
            Console.WriteLine($"Было собрано {Cars()} вагонов.");
        }

    }

    private int Seats()
    {
        int seats = _countSeats * Cars();
        return seats;
    }

    private int Cars()
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
        for (int i = 0; i < Seats(); i++)
        {
            _seats.Add(new Seat(i + 1));
        }
    }

}
