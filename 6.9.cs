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
        private Queue<Client> _clients = new Queue<Client>();
        private int _minClients = 3;
        private int _maxClients = 10;

        public Market()
        {
            Random random = new Random();
            int countClients = random.Next(_minClients, _maxClients);

            for (int i = 0; i < countClients; i++)
            {
                _clients.Enqueue(new Client());
            }

        }

        public void ServeClients()
        {
            while(_clients.Count > 0)
            {
                Console.Clear();
                Console.WriteLine($"В очереди {_clients.Count} человек");
                var client = _clients.Peek();
                client.ShowStat();

                if (client.BuyGoods() == false)
                {
                    client.RemoveRandomGood();
                }
                else
                {
                    _clients.Dequeue();
                    Console.WriteLine("Клиент оплатил покупки.");
                }
                Console.ReadKey();
            }
            Console.WriteLine("Всех обслужили.");
        }

    }

    class Client
    {
        private List<Good> _goods = new List<Good>();
        private int _minMoney = 100;
        private int _maxMoney = 500;

        public int Money { get; private set; }

        public Client()
        {
            Random random = new Random();
            Money = random.Next(_minMoney, _maxMoney);
            AddGoods();
        }

        public void RemoveRandomGood()
        {
            Random random = new Random();
            int randomGood = random.Next(1, _goods.Count - 1);
            Console.WriteLine($"Убирает продукт - {_goods[randomGood].Name}");
            _goods.RemoveAt(randomGood);
        }

        public bool BuyGoods()
        {
            return Money >= GoodsTotalPrice();
        }

        private void AddGoods()
        {
            Random random = new Random();
            int randomGoods = random.Next(4,11);
            for (int i = 0; i < randomGoods; i++)
            {
                _goods.Add(new Good().AddRandomGood());
            }
        }

        public void ShowStat()
        {
            Console.WriteLine($"Денег у клиента - {Money}.");
            ShowGoods();
            Console.WriteLine($"Стоймость покупок - {GoodsTotalPrice()}.");
            Console.WriteLine(BuyGoods().ToString());
        }

        private void ShowGoods()
        {
            foreach (var good in _goods)
            {
                good.ShowStat();
            }
        }

        private int GoodsTotalPrice()
        {
            int total = 0;
            foreach(var good in _goods)
            {
                total += good.Price;
            }
            return total;
        }

    }

    class Good
    {
        private List<Good> _goodslist = new List<Good>();

        public string Name { get; private set; }
        public int Price { get; private set; }

        public Good()
        {
            _goodslist.Add(new Good("Молоко", 50));
            _goodslist.Add(new Good("Морковь", 20));
            _goodslist.Add(new Good("Макароны", 35));
            _goodslist.Add(new Good("Картошка", 20));
            _goodslist.Add(new Good("Лук", 20));
            _goodslist.Add(new Good("Печенье", 35));
            _goodslist.Add(new Good("Шоколад", 60));
            _goodslist.Add(new Good("Хлеб", 35));
            _goodslist.Add(new Good("Доширак", 25));
            _goodslist.Add(new Good("Гречка", 60));
            _goodslist.Add(new Good("Рис", 60));
            _goodslist.Add(new Good("Кола", 45));
            _goodslist.Add(new Good("Фанта", 45));
        }

        public Good(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public Good AddRandomGood()
        {
            Random random = new Random();
            int rndGood = random.Next(0, _goodslist.Count);
            return _goodslist[rndGood];
        }

        public void ShowStat()
        {
            Console.WriteLine($"{Name} - {Price}");
        }

    }

}
