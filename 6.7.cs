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
            traider.AddItems();
            player.AddMoney();

            while (isWork)
            {
                Console.WriteLine("Выбирете команду. 1.Показать предметы у торговца. 2.Купить предмет 3.Показать купленое. 4. Выход.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        traider.ShowTraiderItems();
                        break;
                    case "2":
                        player.BuyItem(traider);
                        break;
                    case "3":
                        player.ShowPlayerItems();
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

    class Player : Inventory
    {
        //private List<Product> _items = new List<Product>();
        public int Money { get; private set; }
        public void BuyItem(Traider traider)
        {
            if (traider.ProductsCount() > 0)
            {
                traider.ShowTraiderItems();
                Console.WriteLine("Выбирете продукт.");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && traider.CanSell(result, traider.ProductsCount(), out Product product))
                {
                    PlayerBag.Add(product);
                    traider.SellItem(result);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("У продавца закончились предметы.");
            }
        }

        public void AddMoney()
        {
            Money = 150;
        }

        public void ShowPlayerItems()
        {
            base.ShowItems(PlayerBag);
        }
        
    }

    class Traider : Inventory
    {
       // private List<Product> _products = new List<Product>();

        public void ShowTraiderItems()
        {
            base.ShowItems(TraiderBag);
        }

        public void AddItems()
        {
            TraiderBag.Add(new Product("Зелье", 2));
            TraiderBag.Add(new Product("Меч", 5));
            TraiderBag.Add(new Product("Броня", 15));
            TraiderBag.Add(new Product("Поножи", 5));
            TraiderBag.Add(new Product("Рукавицы", 5));
        }

        public void SellItem(int index)
        {
            if (CanSell(index, TraiderBag.Count, out Product product))
            {
                TraiderBag.Remove(product);
                Console.Clear();
            }
        }

        public bool CanSell(int index, int count, out Product product)
        {
            bool canSell = index <= count && index > 0;

            if (canSell)
            {
                product = TraiderBag[index - 1];
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
            int count = TraiderBag.Count;
            return count;
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

        public void ShowInfoInInventory()
        {
            Console.WriteLine($"Название: {Name}.");
        }

    }

    class Inventory
    {
        public List<Product> TraiderBag = new List<Product>();
        public List<Product> PlayerBag = new List<Product>();

        public virtual void ShowItems(List<Product> products)
        {

            Console.Clear();
            if (products.Count > 0)
            {
                int numberItem = 0;
                foreach (var product in products)
                {
                    numberItem++;
                    Console.Write(numberItem + ") ");
                    product.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("Нету предметов.");
            }

        }

    }

}
