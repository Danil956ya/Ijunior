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
        private List<Product> _products = new List<Product>();
        private Queue<Client> _clients = new Queue<Client>();
        private int _minClients = 3;
        private int _maxClients = 10;

        public Market()
        {
            Random random = new Random();
            int countClients = random.Next(_minClients, _maxClients);

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

                if (client.IsMoneyEnough() == false)
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
        private List<Product> _products = new List<Product>();
        private int _minGoods = 5;
        private int _maxGoods = 12;

        public int Money { get; private set; }
        public int Bagage { get; private set; }

        public Client(List<Product> products)
        {
            Random random = new Random();
            int minMoney = 100;
            int maxMoney = 500;
            Money = random.Next(minMoney, maxMoney);
            Bagage = random.Next(_minGoods, _maxGoods);

            for (int i = 0; i < Bagage; i++)
            {
                _products.Add(GetRandomGood(products));
            }
        }

        public Product GetRandomGood(List<Product> products)
        {
            Random random = new Random();
            int randomGood = random.Next(0, products.Count);
            return products[randomGood];
        }

        public bool IsMoneyEnough()
        {
            return Money >= GetTotalPrice();
        }

        public void RemoveRandomProduct()
        {
            Random random = new Random();
            int randomGood = random.Next(0, _products.Count - 1);
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
