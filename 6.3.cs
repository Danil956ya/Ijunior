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
                Console.WriteLine("Введите номер.\n1. Добавить игрока.\n2. Вывести список игроков.\n3. Убрать бан игрока.\n4. Забанить игрока.\n5. Удалить игрока.");
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
                        database.RemoveBan();
                        break;
                    case "4":
                        database.AddBan();
                        break;
                    case "5":
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

        public bool IsBanned
        {
            set
            {
                _isBanned = value;
            }
        }

        public Player(int level, string nickname, bool ban)
        {
            _level = level;
            _nickname = nickname;
            _isBanned = ban;
        }

        public void ShowStats(int id)
        {
            Console.WriteLine("{3}) Ник - {0}. Уровень - {1}. Статус бана - {2}", _nickname, _level, _isBanned, id);
        }

    }

    class Вatabase
    {
        private List<Player> players = new List<Player>();

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
                players.Add(new Player(level, nickname, isBanned));
            }
            else
            {
                Console.WriteLine("Неверно введены значения.");
            }
            Console.Clear();
        }

        public void RemoveBan()
        {
            if (players.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
            }
            else
            {
                Console.WriteLine("Введите номер игрока.");
                string id = Console.ReadLine();

                if (int.TryParse(id, out int idPlayer) == true)
                {
                    Player[] playersMas = players.ToArray();
                    playersMas[idPlayer].IsBanned = playersMas[idPlayer].IsBanned = false;
                }
                else
                {
                    Console.WriteLine("Неверно введены значения.");
                }
            }
        }

        public void AddBan()
        {
            if (players.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
            }
            else
            {
                Console.WriteLine("Введите номер игрока.");
                string id = Console.ReadLine();

                if (int.TryParse(id, out int idPlayer) == true)
                {
                    Player[] playersArray = players.ToArray();
                    playersArray[idPlayer].IsBanned = playersArray[idPlayer].IsBanned = true;
                }
                else
                {
                    Console.WriteLine("Неверно введены значения.");
                }
            }
        }

        public void ShowPlayers()
        {
            if (players.Count <= 0)
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
            }
            else
            {
                Console.Clear();
                foreach (var player in players)
                {
                    int indexPlayer = players.IndexOf(player);
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
                players.RemoveAt(indexPlayer);
            }
            else
            {
                Console.WriteLine("Неверно введены значения.");
            }
        }

    }
}
