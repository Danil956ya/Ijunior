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
                isWork = int.TryParse(input, out int tempInt);
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
                string inputIndex;
                int result;
                ShowPlayers();
                Console.WriteLine("Укажите номер игрока.");
                bool tryGetPlayer = TryGetPlayer(out inputIndex, out result);

                if (tryGetPlayer)
                {
                    Console.WriteLine("Укажите значение бана (true или false)");
                    string inputStatus = Console.ReadLine();

                    if (inputStatus == "true")
                    {
                        playerArray[result].Ban();
                    }
                    else if (inputStatus == "false")
                    {
                        playerArray[result].RemoveBan();
                    }
                    else
                    {
                        Console.WriteLine("Некоректно введено значение.");
                    }
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
                Player[] players = _players.ToArray();
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].ShowStats(i);
                }
            }
        }

        public void DeletePlayer()
        {
            string inputIndex;
            int result;
            if (_players.Count > 0)
            {
                ShowPlayers();
                Console.WriteLine("Укажите номер игрока.");
                bool tryGetPlayer = TryGetPlayer(out inputIndex, out result);

                if (tryGetPlayer)
                {
                    if (_players.Count > result)
                    {
                        _players.RemoveAt(result);
                    }
                    else
                    {
                        Console.WriteLine("Неверно введено значение.");
                    }
                }
                else
                {
                    Console.WriteLine("Неверно введены значения.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Список пуст.");
            }

        }

        private bool TryGetPlayer(out string userInput, out int result)
        {
            int playersCount = _players.Count - 1;
            userInput = " ";
            result = 0;
            userInput = Console.ReadLine();
            bool isGetPlayer = int.TryParse(userInput, out result);

            if (playersCount >= result && result >= 0)
            {
                return isGetPlayer;
            }
            else
            {
                isGetPlayer = false;
                return isGetPlayer;
            }

        }

    }
}
