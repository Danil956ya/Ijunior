using System;
using System.Collections.Generic;

namespace _6._3
{
    class Program
    {
        static void Main(string[] args)
        {
            Вatabase database = new Вatabase();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Введите номер.\n1. Добавить игрока.\n2. Вывести список игроков.\n3. Изменить статус бана.\n4. Удалить игрока.");
                string input = Console.ReadLine();
                isWork = int.TryParse(input, out int tempInt) == true;
                switch (input)
                {
                    case "1":
                        database.AddPlayer();
                        break;
                    case "2":
                        database.ShowPlayers();
                        break;
                    case "3":
                        database.ChangeStatus();
                        break;
                    case "4":
                        database.DeletePlayer();
                        break;
                    default:
                        Console.WriteLine("Такой команды нету.");
                        break;
                }
            }

        }
    }

    class Player
    {
        private int _level;
        private string _nickname;
        private bool _isBanned;


        public Player(int level = 0, string nickname = "unknown", bool ban = false)
        {
            _level = level;
            _nickname = nickname;
            _isBanned = ban;
        }

        public void ShowStats(int id)
        {
            Console.WriteLine("{3}) Ник - {0}. Уровень - {1}. Статус бана - {2}", _nickname, _level, _isBanned, id);
        }
        public void RemoveBan()
        {
            _isBanned = false;
        }
        public void Ban()
        {
            _isBanned = true;
        }

    }

    class Вatabase
    {
        private List<Player> _players = new List<Player>();

        public void AddPlayer()
        {
            Console.Clear();
            Console.WriteLine("Введите никнейм");
            string nickname = Console.ReadLine();
            Console.WriteLine("Введите уровень");
            string inputLevel = Console.ReadLine();
            Console.WriteLine("Укажите забанен ли игрок? (y/n)");
            string inputBan = Console.ReadLine();

            bool isBanned = inputBan == "y" ? true : false;

            if (int.TryParse(inputLevel, out int level) == true)
            {
                _players.Add(new Player(level, nickname, isBanned));
            }
            else
            {
                Console.WriteLine("Неверно введены значения.");
            }

            Console.Clear();
        }

        public void ChangeStatus()
        {
            if (_players.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
            }
            else
            {
                Player[] playerArray = _players.ToArray();
                ShowPlayers();
                Console.WriteLine("Введите номер игрока.");
                int inputIndex = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Укажите значение бана (true или false)");
                string inputStatus = Console.ReadLine();

                if(inputStatus == "true")
                {
                    playerArray[inputIndex].Ban();
                }
                else if(inputStatus == "false")
                {
                    playerArray[inputIndex].RemoveBan();
                }
                else
                {
                    Console.WriteLine("Некоректно введено значение.");
                }

            }
        }

        public void ShowPlayers()
        {
            if (_players.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
            }
            else
            {
                Console.Clear();
                foreach (var player in _players)
                {
                    int indexPlayer = _players.IndexOf(player);
                    player.ShowStats(indexPlayer);
                }
            }
        }

        public void DeletePlayer()
        {
            Console.WriteLine("Укажите номер игрока.");
            string inputIndex = Console.ReadLine();
            if (int.TryParse(inputIndex, out int indexPlayer) == true)
            {
                _players.RemoveAt(indexPlayer);
            }
            else
            {
                Console.WriteLine("Неверно введены значения.");
            }
        }

    }
}
