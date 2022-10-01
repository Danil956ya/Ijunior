using System;
using System.Collections.Generic;

namespace CSharpLight
{
    class Program
    {
        static void Main(string[] args)
        {
            TrainProgram program = new TrainProgram();
            bool IsWork = true;

            while (IsWork)
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
        private bool _isDirection = false;
        private bool _isSelling = false;
        private bool _isOcupped = false;

        public void AssignDirection()
        {
            _direction.SetDirection();
            _isDirection = true;
        }

        public void SellTicets()
        {
            _office.SellTicets();
            _isSelling = true;
        }

        public void FormTrain()
        {
            if (_isSelling == false)
            {
                Console.Clear();
                Console.WriteLine("Сначала продайте билеты.");
            }
            else
            {
                _train.FormTrain(_office.SoldCount);
                _isOcupped = true;
            }
        }

        public void SendTrain()
        {
            if (IsComplete())
            {
                _direction = new Direction();
                _office = new TicetsOffice();
                _train = new Train();
                _isDirection = _direction.IsCreated;
                _isSelling = _office.IsSelling;
                _isOcupped = _train.IsFull;
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
            if (_isOcupped && _isSelling && _isDirection)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public void SetDirection()
        {
            if (IsCreated == false)
            {
                ShowStations();
                Console.WriteLine("Укажите откуда хотите ехать.");
                _firstStation = GetStation(Console.ReadLine());
                Console.WriteLine("Укажите куда хотите ехать.");
                _lastStation = GetStation(Console.ReadLine());

                IsCreated = _firstStation == _lastStation ? false : true;

                if (IsCreated == false)
                {
                    Console.Clear();
                    Console.WriteLine("Неверное направление. Попробуйте ещё.");
                    SetDirection();
                }
                ShowDirection();
            }
            else
            {
                Console.Clear();
                ShowDirection();
            }
        }

        public void ShowDirection()
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

        public void SetBoolIsSelling(bool isSelling)
        {
            IsSelling = isSelling;
        }

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

    class Seat
    {
        private bool _isOcupped = false;
        public int Number { get; private set; }

        public Seat(int number)
        {
            Number = number;
        }

        public void ShowInfo()
        {
            Console.WriteLine(GetMessage());
        }

        public void OcupSeat()
        {
            _isOcupped = true;
        }

        private string GetMessage()
        {
            string message = _isOcupped ? $"{Number}) Место занято." : $"{Number}) Место свободно.";
            return message;
        }


    }

    class Train
    {
        private List<Seat> _seats = new List<Seat>();
        private int _countSeats = 30;
        private int _count;
        public bool IsFull { get; private set; }

        public Train()
        {
            IsFull = false;
        }

        public void FormTrain(int SoldCount)
        {
            _count = SoldCount;
            if (IsFull == false)
            {
                AddSeats();
                for (int i = 0; i < SoldCount; i++)
                {
                    _seats[i].OcupSeat();
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
}
