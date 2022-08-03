using System;
using System.Collections.Generic;

namespace _6._6
{
    class Program
    {
        static void Main(string[] args)
        {
            Traider traider = new Traider();
            Player player = new Player();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Ваш счёт: " + player.GetMoney());
                Console.WriteLine("Выбирете команду. 1.Показать предметы у торговца. 2.Купить предмет 3.Показать купленое. 4. Выход.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        traider.ShowItems();
                        break;
                    case "2":
                        player.BuyItem(traider);
                        break;
                    case "3":
                        player.ShowItems();
                        break;
                    case "4":
                        isWork = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверный ввод");
                        break;
                }
            }
        }

    }

    class Player : Human
    {
        private int _money;

        public Player()
        {
            _money = 150;
        }

        public void BuyItem(Traider traider)
        {
            if (traider.ToCount() > 0)
            {
                traider.ShowItems();
                Console.WriteLine("Выбирете продукт.");
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && traider.CanSell(result, traider.ToCount(),out Product product) && traider.GetPrice(result) <= _money)
                {
                    _money -= traider.GetPrice(result);
                    Bag.Add(product);
                    traider.SellItem(result);
                }
            }
            else
            {
                Console.Clear();
                IsEmpty();
            }
        }

        public int GetMoney()
        {
            int Money = _money;
            return Money;
        }

    }

    class Traider : Human
    {
        public Traider()
        {
            Bag.Add(new Product("Зелье", 2));
            Bag.Add(new Product("Меч", 5));
            Bag.Add(new Product("Броня", 15));
            Bag.Add(new Product("Поножи", 5));
            Bag.Add(new Product("Рукавицы", 5));
        }

        public void SellItem(int index)
        {
            if (CanSell(index, Bag.Count, out Product product))
            {
                Bag.Remove(product);
                Console.Clear();
            }
        }

        public bool CanSell(int index, int count, out Product product)
        {
            bool canSell = index <= count && index > 0;

            if(canSell)
            {
                product = Bag[index - 1];
            }
            else
            {
                product = null;
                Console.WriteLine("Неверно введено значение.");
            }
            return canSell;
        }

        public int GetCount()
        {
            int Count = Bag.Count;
            return Count;
        }

        public int GetPrice(int index)
        {
            int price = Bag[index - 1].Price;
            return price;
        }

    }

    class Product
    {
        public string Name { get; private set; }
        public int Price { get; private set; }

        public Product(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Название: {Name}. Цена - {Price}");
        }

    }

    class Human
    {
        protected List<Product> Bag = new List<Product>();

        public void ShowItems()
        {
            Console.Clear();
            if (Bag.Count > 0)
            {
                int numberItem = 0;
                foreach (var product in Bag)
                {
                    numberItem++;
                    Console.Write(numberItem + ") ");
                    product.ShowInfo();
                }
            }
            else
            {
                IsEmpty();
            }
        }

        public void IsEmpty()
        {
            Console.WriteLine("Нету предметов.");
        }
    
    }

}
