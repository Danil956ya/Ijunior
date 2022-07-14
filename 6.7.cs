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
            traider.AddItems();

            while (true)
            {
                Console.WriteLine("Выбирете команду. 1.Показать предметы у торговца. 2.Купить предмет 3.Показать купленое.");
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
                        player.ShowBackpack();
                        break;
                }
            }
        }
    }

    class Player
    {
        private List<Product> _inBackpack = new List<Product>();
        public void BuyItem(Traider traider)
        {
            if (traider.ProductsCount() > 0)
            {
                traider.ShowItems();
                Console.WriteLine("Выбирете продукт.");
                int input = Convert.ToInt32(Console.ReadLine());
                if (traider.CanSell(input, traider.ProductsCount(),out Product product))
                {
                    _inBackpack.Add(product);
                    traider.SellItem(input);
                }
            }
            else
            {
                Console.WriteLine("У продавца закончились предметы.");
            }
        }

        public void ShowBackpack()
        {
            if (_inBackpack.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("В вашеи рюкзаке:");
                foreach (var item in _inBackpack)
                {
                    item.ShowInfo();
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("В рюкзаке пусто.");
            }
        }
    }

    class Traider
    {
        private List<Product> _products = new List<Product>();

        public void ShowItems()
        {
            Console.Clear();
            if (_products.Count > 0)
            {
                int numberItem = 0;
                foreach (var product in _products)
                {
                    numberItem++;
                    Console.Write(numberItem + ") ");
                    product.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("У продавца закончились предметы.");
            }

        }

        public void AddItems()
        {
            _products.Add(new Product("Зелье", 2));
            _products.Add(new Product("Меч", 5));
            _products.Add(new Product("Броня", 15));
            _products.Add(new Product("Поножи", 5));
            _products.Add(new Product("Рукавицы", 5));
        }

        public void SellItem(int index)
        {
            if (CanSell(index, _products.Count, out Product product))
            {
                _products.Remove(product);
                Console.Clear();
            }
        }

        public bool CanSell(int index, int count, out Product product)
        {
            bool canSell = index <= count && index > 0;

            if(canSell)
            {
                product = _products[index - 1];
            }
            else
            {
                product = null;
                Console.WriteLine("Неверно введено значение.");
            }
            return canSell;
        }

        public int ProductsCount()
        {
            int Count = _products.Count;
            return Count;
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
}
