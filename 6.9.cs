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
        private List<Good> _products = new List<Good>();
        private Queue<Client> _clients = new Queue<Client>();
        private int _minClients = 3;
        private int _maxClients = 10;

        public Market()
        {
            Random random = new Random();
            int countClients = random.Next(_minClients, _maxClients);

            _products.Add(new Good("Молоко", 50));
            _products.Add(new Good("Морковь", 20));
            _products.Add(new Good("Макароны", 35));
            _products.Add(new Good("Картошка", 20));
            _products.Add(new Good("Лук", 20));
            _products.Add(new Good("Печенье", 35));
            _products.Add(new Good("Шоколад", 60));
            _products.Add(new Good("Хлеб", 35));
            _products.Add(new Good("Доширак", 25));
            _products.Add(new Good("Гречка", 60));
            _products.Add(new Good("Рис", 60));
            _products.Add(new Good("Кола", 45));
            _products.Add(new Good("Фанта", 45));

            for (int i = 0; i < countClients; i++)
            {
                _clients.Enqueue(new Client());
            }
        }

        public void ServeClients()
        {

            foreach (var client in _clients)
            {

                client.AddGoods(_products);
            }

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
        private List<Good> _products = new List<Good>();
        private int _minMoney = 100;
        private int _maxMoney = 500;
        private int _minGoods = 5;
        private int _maxGoods = 12;

        public int Money { get; private set; }

        public Client()
        {
            Random random = new Random();
            Money = random.Next(_minMoney, _maxMoney);
        }

        public void RemoveRandomProduct()
        {
            Random random = new Random();
            int randomGood = random.Next(0, _products.Count - 1);
            Console.WriteLine($"\nУбирает продукт - {_products[randomGood].Name}");
            _products.RemoveAt(randomGood);
        }

        public bool IsMoneyEnough()
        {
            return Money >= GetTotalPrice();
        }

        public void ShowStat()
        {
            ShowGoods();
            Console.WriteLine($"\nСтоймость покупок - {GetTotalPrice()}    Денег у клиента - {Money}.");
        }

        public void AddGoods(List<Good> goods)
        {
            Random random = new Random();
            int randomGoods = random.Next(_minGoods,_maxGoods);

            for (int i = 0; i < randomGoods; i++)
            {
                _products.Add(goods[i].GetRandomGood(goods));
            }
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

            foreach(var good in _products)
            {
                total += good.Price;
            }
            return total;
        }
    }

    class Good
    {

        public string Name { get; private set; }
        public int Price { get; private set; }

        public Good() { }

        public Good(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public Good GetRandomGood(List<Good> goods)
        {
            Random random = new Random();
            int randomGood = random.Next(0, goods.Count);
            return goods[randomGood];
        }

        public void ShowStat()
        {
            Console.WriteLine($"{Name} - {Price}");
        }
    }

}
