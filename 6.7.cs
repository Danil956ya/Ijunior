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
                    break;
                default:
                    Console.WriteLine("Неверная команда.");
                    break;

            }

        }
    }
}

class Direction
{
    private List<Station> _stations = new List<Station>();
    public bool isCreate { get; private set; }
    string firstStation;
    string lastStation;
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
        if (!isCreate)
        {
            Console.Clear();
            ShowStations();
            Console.WriteLine("Укажите откуда хотите ехать.");
            firstStation = GetStation(Console.ReadLine());
            Console.WriteLine("Укажите куда хотите ехать.");
            lastStation = GetStation(Console.ReadLine());
            if (firstStation == lastStation)
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
            Console.WriteLine($"Направление: {firstStation} - {lastStation}.");
        }
    }
    private string GetStation(string input)
    {
        string Station = "default";
        bool canGet = false;
        foreach (var station in _stations)
        {
            if (station.Name.ToLower().Contains(input) || station.Name.Contains(input))
            {
                Station = station.Name;
                canGet = true;
            }
        }
        if (!canGet)
        {
            Console.WriteLine("Попробуйте ещё раз.");
            Station = GetStation(Console.ReadLine());
        }
        return Station;
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
    public static int count { get; private set; }
    Random rnd = new Random();
    bool isSelling;

    public void SellTicets()
    {
        if (!isSelling)
        {
            Console.Clear();
            count = rnd.Next(25, 101);
            Console.WriteLine($"Было продано {count} билетов.");
            isSelling = true;
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Билеты проданы.");
            Console.WriteLine($"Продано {count} билетов.");
        }
    }

}
class Seat : Ticet
{
    public bool IsOcupped = false;
    public int number;
    public Seat(int num)
    {
        number = num;
    }
    private string mes()
    {
        string mesag = IsOcupped ? $"{number}) Место занято." : $"{number}) Место свободно.";
        return mesag;
    }
    public void ShowInfo()
    {
        Console.WriteLine(mes());
    }

}
class Train : Ticet
{
    protected List<Seat> _seats = new List<Seat>();
    int countSeats = 30;
    int Cars = 1;
    bool isFull;

    public void OcupSeats()
    {
        if (count > 0)
        {
            if (!isFull)
            {
                AddSeats();
                for (int i = 0; i < count; i++)
                {
                    _seats[i].number = i + 1;
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
                Console.WriteLine($"поезд сформирован. Собрано {Cars} вагонов");
            }
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Сначала продайте билеты.");
        }
    }
    public int CarSeats()
    {
        int seats = 30;
        if (count > countSeats)
        {
            Cars += 1;
            countSeats = Cars * seats;
        }
        return countSeats;
    }
    public void AddSeats()
    {
        for (int i = 0; i < CarSeats(); i++)
        {
            _seats.Add(new Seat(i + 1));
        }
    }
}
