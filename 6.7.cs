using System;
using System.Collections.Generic;

namespace CSharpLight
{
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
                        program.AssignDirection();
                        break;
                    case "2":
                        program.SellTicets();
                        break;
                    case "3":
                        program.FormTrain();
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
                return;
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
            if (IsCreated == false)
            {
                ShowStations();
                Console.WriteLine("Укажите откуда хотите ехать.");
                _firstStation = GetStation(Console.ReadLine());
                Console.WriteLine("Укажите куда хотите ехать.");
                _lastStation = GetStation(Console.ReadLine());

                IsCreated = _firstStation != _lastStation;

                if (IsCreated == false)
                {
                    Console.Clear();
                    Console.WriteLine("Неверное направление. Попробуйте ещё.");
                    IndicateStations();
                }
                ShowInformation();
            }
            else
            {
                Console.Clear();
                ShowInformation();
            }
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
                AddSeats();
                IsFull = true;
                Console.WriteLine($"Собрано {GetCars()} вагонов.\nМест занято {_seatsInCar} из {GetMaxSeats()}");
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Собрано {GetCars()} вагонов.\nМест занято {_seatsInCar} из {GetMaxSeats()}");
            }

        }

        private void AddSeats()
        {
            for (int i = 0; i < _soldCount; i++)
            {
                _seatsInCar++;
            }
        }

        private int GetMaxSeats()
        {
            return _maxCountSeats * GetCars();
        }

        private int GetCars()
        {
            int cars = 0;
            while((_maxCountSeats * cars) < _soldCount)
            {
                cars++;
            }
            return cars;
        }

    }
}
