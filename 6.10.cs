using System.Diagnostics.Metrics;

namespace Ijunior
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field();
            field.ShowTwoArmy();
            field.Fight();
        }
    }
    class Field
    {
        private Army _armyRed = new Army("Red");
        private Army _armyBlue = new Army("Blue");
        private Solder _solderRed;
        private Solder _solderBlue;

        public void Fight()
        {
            while(_armyRed.GetArmyCount() > 0 && _armyBlue.GetArmyCount() > 0)
            {
                _solderRed = _armyRed.GetSolder();
                _solderBlue = _armyBlue.GetSolder();
                _solderRed.TakeDamage(_solderBlue.Damage);
                _solderRed.UseSpecial(_solderBlue);
                _solderBlue.TakeDamage(_solderRed.Damage);
                _solderBlue.UseSpecial(_solderRed);
                RemoveSolder();
                ShowTwoArmy();
                Console.ReadKey();
                Console.Clear();
            }
            ShowResult();
        }
        private void ShowResult()
        {
            if(_armyRed.GetArmyCount() <= 0)
            {
                Console.WriteLine("Победила армия синих");
            }
            if(_armyBlue.GetArmyCount() <= 0)
            {
                Console.WriteLine("Победила армия красных");
            }
        }

        public void ShowTwoArmy()
        {
            Console.WriteLine($"Армия {_armyBlue.Name} - кол-во: {_armyBlue.GetArmyCount()}");
            _armyBlue.ShowArmy();
            Console.WriteLine($"Армия {_armyRed.Name}  - кол-во: {_armyRed.GetArmyCount()}");
            _armyRed.ShowArmy();
        }

        private void RemoveSolder()
        {
            if(_solderBlue.IsAlive() == false)
            {
                _armyBlue.RemoveFromField(_solderBlue);
            }
            else if(_solderRed.IsAlive() == false)
            {
                _armyRed.RemoveFromField(_solderRed);
            }
        }
    }


    class Army
    {
        const int MaxCount = 10;
        const int MinCount = 5;
        public string Name { get; private set; }
        private List<Solder> _solders = new List<Solder>();
        private Random _random = new Random();

        public Army(string name)
        {
            Name = name;
            SetArmy();
        }

        public int GetArmyCount()
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

        private void SetArmy()
        {
            int armyCount = _random.Next(MinCount, MaxCount);

            for (int i = 0; i < armyCount; i++)
            {
                int rank = _random.Next(0, SoldersRank().Count);
                _solders.Add(SoldersRank()[rank]);
            }
        }

        private List<Solder> SoldersRank()
        {
            List<Solder> soldersRank = new List<Solder>();
            soldersRank.Add(new Melee());
            soldersRank.Add(new Range());
            soldersRank.Add(new Tank());
            return soldersRank;
        }

        public void ShowArmy()
        {
            foreach (var solder in _solders)
            {
                Console.WriteLine($"{solder.Name} - {solder.Healt}оз, {solder.Damage}ур");
            }
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
