using System;
using System.Collections.Generic;

namespace CSharpLight
{
    class Program
    {
        static void Main(string[] args)
        {
            const string CommandAssign = "1";
            const string CommandSell = "2";
            const string CommandForm = "3";
            const string CommandSend = "4";
            TrainProgram program = new TrainProgram();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"{CommandAssign}. Назначить направление. {CommandSell}. Продать билеты. {CommandForm}.Сформировать поезд {CommandSend}. Отправить поезд.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case CommandAssign:
                        program.AssignDirection();
                        break;
                    case CommandSell:
                        program.SellTicets();
                        break;
                    case CommandForm:
                        program.FormTrain();
                        break;
                    case CommandSend:
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

        public void AssignDirection()
        {
            _direction.IndicateStations();
        }

        public void SellTicets()
        {
            _office.SellTicets();
        }

        public void FormTrain()
        {
            if (_office.IsSelling == false)
            {
                Console.Clear();
                Console.WriteLine("Сначала продайте билеты.");
            }
            else
            {
                _train.FillSeats(_office.SoldCount);
            }
        }

        public void SendTrain()
        {
            if (IsComplete())
            {
                _direction = new Direction();
                _office = new TicetsOffice();
                _train = new Train();
                Console.WriteLine("Поезд отправлен.");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Не все условия выполнены.");
            }
        }

        private bool IsComplete()
        {
            return _direction.IsCreated && _train.IsFull && _office.IsSelling;
        }
    }

    class Direction
    {
        private List<Station> _stations = new List<Station>();
        private string _firstStation;
        private string _lastStation;
        public bool IsCreated { get; private set; }

        public Direction()
        {
            IsCreated = false;
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

        public void IndicateStations()
        {
            while (IsCreated == false)
            {
                Console.Clear();
                ShowStations();
                Console.WriteLine("Укажите откуда хотите ехать.");
                _firstStation = GetStation(Console.ReadLine());
                Console.WriteLine("Укажите куда хотите ехать.");
                _lastStation = GetStation(Console.ReadLine());
                IsCreated = _firstStation != _lastStation;
                ShowInformation();
            }
            Console.Clear();
            ShowInformation();
        }

        public void ShowInformation()
        {
            if (IsCreated)
            {
                Console.Clear();
                Console.WriteLine($"Направление: {_firstStation} - {_lastStation}.");
            }
        }

        private string GetStation(string input)
        {
            string stationName = "default";

            foreach (var station in _stations)
            {
                if (station.Name.ToLower().Contains(input) || station.Name.Contains(input))
                {
                    Console.WriteLine($"Успешно. Выбрана станция - {station.Name}");
                    stationName = station.Name;
                    return stationName;
                }
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
        private Random _random = new Random();
        private int _minCountSold = 25;
        private int _maxCountSold = 101;
        public bool IsSelling { get; private set; }
        public int SoldCount { get; private set; }

        public void SellTicets()
        {
            if (IsSelling == false)
            {
                Console.Clear();
                SoldCount = _random.Next(_minCountSold, _maxCountSold);
                Console.WriteLine($"Было продано {SoldCount} билетов.");
                IsSelling = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Билеты проданы.");
                Console.WriteLine($"Продано {SoldCount} билетов.");
            }
        }

    }

    class Train
    {
        private int _seatsInCar;
        private int _maxCountSeats = 30;
        private int _soldCount;
        public bool IsFull { get; private set; }

        public Train()
        {
            IsFull = false;
        }

        public void FillSeats(int soldCount)
        {
            _soldCount = soldCount;
            if (IsFull == false)
            {
                _seatsInCar += _soldCount;
                IsFull = true;
                Console.WriteLine($"Собрано {GetCars()} вагонов.\nМест занято {_seatsInCar} из {GetMaxSeats()}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Собрано {GetCars()} вагонов.\nМест занято {_seatsInCar} из {GetMaxSeats()}");
            }

        }

        private int GetMaxSeats()
        {
            return _maxCountSeats * GetCars();
        }

        private int GetCars()
        {
            return _soldCount / _maxCountSeats + 1;
        }

    }
}
