using System.Diagnostics.Metrics;

namespace Ijunior
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field();
            field.Fight();
        }
    }

    class Field
    {
        private Army _armyRed = new Army("Red");
        private Army _armyBlue = new Army("Blue");

        public void Fight()
        {
            while(_armyRed.GetSoldersCount() > 0 && _armyBlue.GetSoldersCount() > 0)
            {
                ShowArmys();
                Soldier soldierRed = _armyRed.GetSoldier();
                Soldier soldierBlue = _armyBlue.GetSoldier();
                soldierRed.TakeDamage(soldierBlue.Damage);
                soldierRed.UseSpecial(soldierBlue);
                soldierBlue.TakeDamage(soldierRed.Damage);
                soldierBlue.UseSpecial(soldierRed);
                RemoveSoldier(soldierRed);
                RemoveSoldier(soldierBlue);
                Console.ReadKey();
                Console.Clear();
            }
            ShowResult();
        }

        public void ShowArmys()
        {
            Console.WriteLine("_______________________");
            Console.WriteLine($"Армия {_armyBlue.Name} - кол-во: {_armyBlue.GetSoldersCount()}");
            _armyBlue.ShowInfo();
            Console.WriteLine("_______________________");
            Console.WriteLine($"Армия {_armyRed.Name}  - кол-во: {_armyRed.GetSoldersCount()}");
            _armyRed.ShowInfo();
            Console.WriteLine("_______________________");
        }

        private void ShowResult()
        {
            if(_armyRed.GetSoldersCount() <= 0)
            {
                Console.WriteLine("Победила армия синих");
            }
            else if(_armyBlue.GetSoldersCount() <= 0)
            {
                Console.WriteLine("Победила армия красных");
            }
        }

        private void RemoveSoldier(Soldier soldier)
        {
            if(soldier.IsAlive() == false)
            {
                _armyBlue.RemoveSoldier(soldier);
            }
        }
    }


    class Army
    {
        private const int MaxCount = 10;
        private const int MinCount = 5;
        private List<Soldier> _solders = new List<Soldier>();
        private Random _random = new Random();

        public Army(string name)
        {
            Name = name;
            CreateSolders();
        }

        public string Name { get; private set; }

        public int GetSoldersCount()
        {
            return _solders.Count;
        }

        public Soldier GetSoldier()
        {
            return _solders[_random.Next(0,_solders.Count)];
        }

        public void RemoveSoldier(Soldier soldier)
        {
            _solders.Remove(soldier);
        }

        public void ShowInfo()
        {
            foreach (var soldier in _solders)
            {
                Console.WriteLine($"{soldier.Name} - {soldier.Healt}оз, {soldier.Damage}ур");
            }
        }

        private void CreateSolders()
        {
            int armyCount = _random.Next(MinCount, MaxCount);

            for (int i = 0; i < armyCount; i++)
            {
                int rank = _random.Next(0, GetSoldersRank().Count);
                _solders.Add(GetSoldersRank()[rank]);
            }
        }

        private List<Soldier> GetSoldersRank()
        {
            List<Soldier> soldersRank = new List<Soldier>();
            soldersRank.Add(new Melee());
            soldersRank.Add(new Range());
            soldersRank.Add(new Tank());
            return soldersRank;
        }
    }

    class Soldier
    {
        public string Name { get; protected set; }
        public int Damage { get; protected set; }
        public int Healt { get; protected set; }

        public bool IsAlive()
        {
            return Healt > 0;
        }

        public void TakeDamage(int damage)
        {
            Healt -= damage;
            Console.WriteLine($"{Name} нанёс {damage} урона.");
        }

        public virtual void UseSpecial(Soldier soldier) { }
    }

    class Melee : Soldier
    {
        private int _buffHeal = 10;

        public Melee()
        {
            Name = "Солдат с лопатой";
            Damage = 10;
            Healt = 50;
        }

        public override void UseSpecial(Soldier soldier)
        {
            Console.WriteLine($"{Name} Перебинтовался");
            Healt += _buffHeal;
        }
    }

    class Range : Soldier
    {
        private int _buffDamage = 25;

        public Range()
        {
            Name = "Солдат с автоматом";
            Damage = 15;
            Healt = 45;
        }

        public override void UseSpecial(Soldier soldier)
        {
            Console.WriteLine($"{Name} Выстрелил бронебойным патроном");
            soldier.TakeDamage(_buffDamage);
        }
    }

    class Tank : Soldier
    {
        private int _buffDamage = 30;

        public Tank()
        {
            Name = "Танк";
            Damage = 25;
            Healt = 30;
        }

        public override void UseSpecial(Soldier soldier)
        {
            Console.WriteLine($"{Name} Выстрелил снарядом");
            soldier.TakeDamage(_buffDamage);
        }
    }
}
