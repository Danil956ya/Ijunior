using System;
using System.Collections.Generic;

namespace _6._4
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            Player player = new Player();
            bool isWork = true;
            string userInput;

            while (isWork)
            {
                Console.WriteLine("Выбирете параметр. \n1. Вытянуть карту. \n2. Закончить игру и показать карты. \n3. Выход.");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1":
                        player.GetCard(deck);
                        break;
                    case "2":
                        player.FinishResult();
                        player.ShowCard();
                        isWork = false;
                        break;
                    case "3":
                        Console.WriteLine("Пока!");
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Неверная команда.");
                        break;
                }
            }
        }
    }
    class Card
    {
        public string Name { get; private set; }
        public int PowerPoint { get; private set; }

        public Card(string name, int powerPoint)
        {
            Name = name;
            PowerPoint = powerPoint;
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();
        private Random _random = new Random();

        public Deck()
        {
            _cards.Add(new Card("Card 1", GetPowerPoint()));
            _cards.Add(new Card("Card 2", GetPowerPoint()));
            _cards.Add(new Card("Card 3", GetPowerPoint()));
            _cards.Add(new Card("Card 4", GetPowerPoint()));
            _cards.Add(new Card("Card 5", GetPowerPoint()));
        }

        public bool TryGetCard(out Card card)
        {
            if (_cards.Count > 0)
            {
                card = _cards[GetNumberCard()];
                _cards.Remove(card);
                return true;
            }
            else
            {
                card = null;
                return false;
            }
        }
        private int GetPowerPoint()
        {
            int powerPoint;
            int minimalPowerPoint = 5;
            int maximumPowerPoint = 15;
            powerPoint = _random.Next(minimalPowerPoint, maximumPowerPoint);
            return powerPoint;
        }
        private int GetNumberCard()
        {
            int numberCard = 0;
            int minimalNumberCard = 0;
            int maximumNumberCard = 4;

            if (_cards.Count > maximumNumberCard)
            {
                numberCard = _random.Next(minimalNumberCard, maximumNumberCard);
                maximumNumberCard--;
            }

            return numberCard;
        }
    }
    class Player
    {
        private List<Card> _hand = new List<Card>();

        public void GetCard(Deck deck)
        {
            if (deck.TryGetCard(out Card card))
            {
                _hand.Add(card);
                Console.WriteLine("Ваши карты");
                ShowCard();
            }
            else
            {
                Console.WriteLine("В колоде законились карты");
            }
        }

        public void ShowCard()
        {
            for (int i = 0; i < _hand.Count; i++)
            {
                Console.WriteLine($"Карта: {_hand[i].Name}. Очков силы: {_hand[i].PowerPoint}");
            }
        }

        public void FinishResult()
        {
            int countPowerPoint = GetCountPoint();
            int scorePointToWin = 20;

            if (countPowerPoint >= scorePointToWin)
            {
                Console.WriteLine("Поздравляю вы выиграли!!!");
                Console.WriteLine($"У вас {countPowerPoint} очков.");
            }
            else
            {
                scorePointToWin -= countPowerPoint;
                Console.WriteLine("Вы проиграли.");
                Console.WriteLine($"До победы вам не хватило {scorePointToWin} очков");
            }
        }

        private int GetCountPoint()
        {
            int countPowerPoint = 0;

            for (int i = 0; i < _hand.Count; i++)
            {
                countPowerPoint += _hand[i].PowerPoint;
            }

            return countPowerPoint;
        }
    }

}
