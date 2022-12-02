using System.Data.SqlTypes;

namespace _1._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Market market = new Market();
            market.ServeClients();
        }
    }

    class Market
    {
        private Random _random = new Random();
        private List<Product> _products = new List<Product>();
        private Queue<Client> _clients = new Queue<Client>();

        public Market()
        {
            int minClients = 3;
            int maxClients = 10;
            int countClients = _random.Next(minClients, maxClients);
            _products.Add(new Product("Молоко", 50));
            _products.Add(new Product("Морковь", 20));
            _products.Add(new Product("Макароны", 35));
            _products.Add(new Product("Картошка", 20));
            _products.Add(new Product("Лук", 20));
            _products.Add(new Product("Печенье", 35));
            _products.Add(new Product("Шоколад", 60));
            _products.Add(new Product("Хлеб", 35));
            _products.Add(new Product("Доширак", 25));
            _products.Add(new Product("Гречка", 60));
            _products.Add(new Product("Рис", 60));
            _products.Add(new Product("Кола", 45));
            _products.Add(new Product("Фанта", 45));

            for (int i = 0; i < countClients; i++)
            {
                _clients.Enqueue(new Client(_products));
            }
        }

        public void ServeClients()
        {
            while (_clients.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"В очереди {_clients.Count} человек");
                var client = _clients.Peek();
                client.ShowStat();

                if (client.IsEnough == false)
                {
                    client.RemoveRandomProduct();
                }
                else
                {
                    _clients.Dequeue();
                    Console.WriteLine("\nКлиент оплатил покупки.");
                }

                Console.WriteLine("\nНажмите любую кнопку.");
                Console.ReadKey();
            }

            Console.WriteLine("Всех обслужили.");
        }

    }

    class Client
    {
        private Random _random = new Random();
        private List<Product> _products = new List<Product>();

        public int Money { get; private set; }
        public int Bagage { get; private set; }
        public bool IsEnough { get { return Money >= GetTotalPrice(); } }

        public Client(List<Product> products)
        {
            int minMoney = 100;
            int maxMoney = 500;
            int _minGoods = 5;
            int _maxGoods = 12;
            Money = _random.Next(minMoney, maxMoney);
            Bagage = _random.Next(_minGoods, _maxGoods);

            for (int i = 0; i < Bagage; i++)
            {
                int randomProduct = _random.Next(0, products.Count);
                _products.Add(products[randomProduct]);
            }
        }

        public void RemoveRandomProduct()
        {
            int randomGood = _random.Next(0, _products.Count);
            Console.WriteLine($"\nУбирает продукт - {_products[randomGood].Name}");
            _products.RemoveAt(randomGood);
        }

        public void ShowStat()
        {
            ShowGoods();
            Console.WriteLine($"\nСтоймость покупок - {GetTotalPrice()}    Денег у клиента - {Money}.");
        }

        private void ShowGoods()
        {
            foreach (var product in _products)
            {
                product.ShowStat();
            }
        }

        private int GetTotalPrice()
        {
            int total = 0;

            foreach (var good in _products)
            {
                total += good.Price;
            }

            return total;
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

        public void ShowStat()
        {
            Console.WriteLine($"{Name} - {Price}");
        }
    }
}
