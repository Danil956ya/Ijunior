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
                Solder SolderRed = _armyRed.GetSolder();
                Solder SolderBlue = _armyBlue.GetSolder();
                SolderRed.TakeDamage(SolderBlue.Damage);
                SolderRed.UseSpecial(SolderBlue);
                SolderBlue.TakeDamage(SolderRed.Damage);
                SolderBlue.UseSpecial(SolderRed);
                RemoveSolder(SolderRed);
                RemoveSolder(SolderBlue);
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
            if(_armyBlue.GetSoldersCount() <= 0)
            {
                Console.WriteLine("Победила армия красных");
            }
        }

        private void RemoveSolder(Solder solder)
        {
            if(solder.IsAlive() == false)
            {
                _armyBlue.RemoveFromField(solder);
            }
        }
    }


    class Army
    {
        public string Name { get; private set; }
        private const int MaxCount = 10;
        private const int MinCount = 5;
        private List<Solder> _solders = new List<Solder>();
        private Random _random = new Random();

        public Army(string name)
        {
            Name = name;
            CreateSolders();
        }

        public int GetSoldersCount()
        {
            return _solders.Count;
        }

        public Solder GetSolder()
        {
            return _solders[_random.Next(0,_solders.Count)];
        }

        public void RemoveFromField(Solder solder)
        {
            _solders.Remove(solder);
        }

        public void ShowInfo()
        {
            foreach (var solder in _solders)
            {
                Console.WriteLine($"{solder.Name} - {solder.Healt}оз, {solder.Damage}ур");
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

        private List<Solder> GetSoldersRank()
        {
            List<Solder> soldersRank = new List<Solder>();
            soldersRank.Add(new Melee());
            soldersRank.Add(new Range());
            soldersRank.Add(new Tank());
            return soldersRank;
        }
    }

    class Solder
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

        public virtual void UseSpecial(Solder solder) { }
    }

    class Melee : Solder
    {
        private int _buffHeal = 10;

        public Melee()
        {
            Name = "Солдат с лопатой";
            Damage = 10;
            Healt = 50;
        }

        public override void UseSpecial(Solder solder)
        {
            Console.WriteLine($"{Name} Перебинтовался");
            Healt += _buffHeal;
        }
    }

    class Range : Solder
    {
        private int _buffDamage = 25;

        public Range()
        {
            Name = "Солдат с автоматом";
            Damage = 15;
            Healt = 45;
        }

        public override void UseSpecial(Solder solder)
        {
            Console.WriteLine($"{Name} Выстрелил бронебойным патроном");
            solder.TakeDamage(_buffDamage);
        }
    }

    class Tank : Solder
    {
        private int _buffDamage = 30;

        public Tank()
        {
            Name = "Танк";
            Damage = 25;
            Healt = 30;
        }

        public override void UseSpecial(Solder solder)
        {
            Console.WriteLine($"{Name} Выстрелил снарядом");
            solder.TakeDamage(_buffDamage);
        }
    }
}
