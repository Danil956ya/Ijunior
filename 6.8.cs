using System.Runtime.InteropServices;

namespace LiteBattlers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field();
            bool isWork = true;
            string CommandFight = "1";
            string CommandExit = "2";

            while (isWork)
            {
                Console.WriteLine("Начинаем битву? \n1 - Да \n2 - Нет");
                string input = Console.ReadLine();

                if (input == CommandFight)
                    field.Fight();
                else if (input == CommandExit)
                    isWork = false;
                else
                    Console.WriteLine("Попробуйте ещё раз.");
            }
        }

    }

    class Field
    {
        private Battler _firstBattler;
        private Battler _secondBattler;
        private List<Battler> battlers = new List<Battler>();

        public Field()
        {
            battlers.Add(new Warrior());
            battlers.Add(new Archer());
            battlers.Add(new Druid());
            battlers.Add(new Knight());
            battlers.Add(new Mage());
        }

        public Battler SelectBattler(int number)
        {
            Battler battler = null;
            ResetList();

            while (battler == null)
            {
                Console.WriteLine($"Введите номер бойца №{number}, которого хотите выбрать.");
                ShowBattlers();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result > 0 && result < battlers.Count + 1)
                {
                    return battlers[result - 1];
                }
            }
            return battler;
        }

        public void Fight()
        {
            if (IsCreated())
            {
                Console.Clear();
                Console.WriteLine($"Да начнётся битва! \n{_firstBattler.Name} vs {_secondBattler.Name}");
                while (_firstBattler.IsAlive() && _secondBattler.IsAlive())
                {
                    _firstBattler.AttackEnemy(_secondBattler);
                    _secondBattler.AttackEnemy(_firstBattler);
                    ShowStats();
                }
                ShowResult();
            }
        }

        private bool IsCreated()
        {
            Console.Clear();
            _firstBattler = SelectBattler(1);
            _secondBattler = SelectBattler(2);
            return _firstBattler != null && _secondBattler != null;
        }

        private void ShowStats()
        {
            Console.WriteLine("------------------");
            Console.WriteLine($"{_firstBattler.Name}: {_firstBattler.Healt} оз.");
            Console.WriteLine($"{_secondBattler.Name}: {_secondBattler.Healt} оз.");
            Console.WriteLine("------------------");
        }

        private void ShowBattlers()
        {
            int battlerNumber = 0;
            foreach (var battler in battlers)
            {
                battlerNumber++;
                Console.WriteLine(battlerNumber + " " + battler.Name);

            }
        }

        private void ShowResult()
        {
            if (_firstBattler.IsAlive())
            {
                Console.WriteLine($"{_firstBattler.Name} - Побеждает!\n");
            }
            else if (_secondBattler.IsAlive())
            {
                Console.WriteLine($"{_secondBattler.Name} - Побеждает!\n");
            }
            else
            {
                Console.WriteLine("Ничья.\n");
            }
        }

        private void ResetList()
        {
            battlers.Clear();
            battlers.Add(new Warrior());
            battlers.Add(new Archer());
            battlers.Add(new Druid());
            battlers.Add(new Knight());
            battlers.Add(new Mage());
        }

    }

    class Battler
    {
        public string Name { get; protected set; }
        public int Damage { get; protected set; }
        public int Healt { get; protected set; }
        public int Armor { get; protected set; }

        private int _chanceAbility = 30;
        private int _rangeChance = 100;

        public bool IsAlive()
        {
            return Healt > 0;
        }

        public void AttackEnemy(Battler enemy)
        {
            Random random = new Random();
            int Ability = random.Next(_rangeChance);

            if (_chanceAbility <= Ability)
            {
                SpecialAttack(enemy);
            }
            else
            {
                Attack(enemy);
            }
        }

        protected virtual void Attack(Battler enemy)
        {
            enemy.TakeDamage(Damage);
        }

        protected virtual void SpecialAttack(Battler enemy)
        {

        }

        protected virtual void TakeDamage(int damage)
        {

        }

    }

    class Warrior : Battler
    {
        private string _name = "Warrior";
        private int _damage = 15;
        private int _healts = 45;
        private int _armor = 10;
        private int _buffAttack = 25;
        private int _buffRage = 3;

        public Warrior()
        {
            Name = _name;
            Damage = _damage;
            Healt = _healts;
            Armor = _armor;
        }

        protected override void SpecialAttack(Battler enemy)
        {
            Damage = _buffAttack;
            Console.WriteLine($"{Name} - Бросает стальной топор! Урон - {Damage}");
            base.Attack(enemy);
            Damage = _damage;
        }

        protected override void Attack(Battler enemy)
        {
            int rage = 1;

            if (_healts / 2 >= Healt)
                rage = 2;
            else if (_healts / 3 >= Healt)
                rage = 3;

            switch (rage)
            {
                case 1:
                    Console.WriteLine($"{Name} - Бъёт кулаком! Урон - {Damage}");
                    break;
                case 2:
                    Damage += _buffRage * rage;
                    Console.WriteLine($"{Name} - Усиленно бъёт кулаком! Урон - {Damage}");
                    break;
                case 3:
                    Damage += _buffRage * rage;
                    Console.WriteLine($"{Name} - В ярости ударяет кулаком! Урон - {Damage}");
                    break;

            }
            base.Attack(enemy);
            Damage = _damage;
        }

        protected override void TakeDamage(int damage)
        {
            Healt -= damage;
            Console.WriteLine($"{Name} - Аухг.");
        }

    }

    class Archer : Battler
    {
        private string _name = "Archer";
        private int _damage = 20;
        private int _healts = 40;
        private int _armor = 5;
        private int _chanceDodge = 15;
        private int _buffDamage = 10;
        private int _rangeChance = 100;

        public Archer()
        {
            Name = _name;
            Damage = _damage;
            Healt = _healts;
            Armor = _armor;
        }

        protected override void SpecialAttack(Battler enemy)
        {
            Damage += _buffDamage;
            Console.WriteLine($"{Name} - выпускает ледяную стрелу! Урон - {Damage}");
            base.Attack(enemy);
            Damage = _damage;
        }

        protected override void Attack(Battler enemy)
        {
            Console.WriteLine($"{Name} - выпускает стрелу! Урон - {Damage}");
            base.Attack(enemy);
        }

        protected override void TakeDamage(int damage)
        {
            Random random = new Random();
            int Dodge = random.Next(_rangeChance);

            if (_chanceDodge >= Dodge)
            {
                Console.WriteLine($"{Name} - Увернулся");
            }
            else
            {
                Healt -= damage;
                Console.WriteLine($"{Name} - Ай!");
            }
        }

    }

    class Druid : Battler
    {
        private string _name = "Druid";
        private int _damage = 20;
        private int _healts = 50;
        private int _armor = 5;
        private int _buffheals = 30;
        private int _maxHealts = 200;
        private int _minHealts = 0;

        public Druid()
        {
            Name = _name;
            Damage = _damage;
            Healt = _healts;
            Armor = _armor;
        }

        protected override void SpecialAttack(Battler enemy)
        {
            if (Healt >= _maxHealts)
            {
                Console.WriteLine($"{Name} - Слишком перелечил себя и умер!");
                Healt = _minHealts;
            }
            else
            {
                Healt += _buffheals;
                Console.WriteLine($"{Name} - Лечит себя целебной травой! Востановление - {_buffheals}");
            }
        }

        protected override void Attack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Атакует терновыми шипами. Урон - {Damage}");
            base.Attack(enemy);
        }

        protected override void TakeDamage(int damage)
        {
            Console.WriteLine($"{Name} - Больно!");
            Healt -= damage;
        }

    }

    class Mage : Battler
    {
        private string _name = "Mage";
        private int _damage = 20;
        private int _healts = 60;
        private int _armor = 5;
        private int _firebollDamage = 30;
        private int _buffheals = 20;

        public Mage()
        {
            Name = _name;
            Damage = _damage;
            Healt = _healts;
            Armor = _armor;
        }

        protected override void SpecialAttack(Battler enemy)
        {
            const int MaxValue = 3;
            const int MinValue = 1;
            Random random = new Random();
            int cpecial = random.Next(MinValue, MaxValue);

            switch (cpecial)
            {
                case 1:
                    Damage = _firebollDamage;
                    Console.WriteLine($"{Name} - Вызывает фаерболл! Урон - {Damage}");
                    base.Attack(enemy);
                    Damage = _damage;
                    break;
                case 2:
                    Healt += _buffheals;
                    Console.WriteLine($"{Name} - Лечит себя! Лечение - {_buffheals}");
                    break;
            }

        }

        protected override void Attack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Атакует заклинанием! Урон - {Damage}");
            base.Attack(enemy);
        }

        protected override void TakeDamage(int damage)
        {
            Healt -= damage;
            Console.WriteLine($"{Name} - Ауч!");
        }

    }

    class Knight : Battler
    {
        private string _name = "Knight";
        private int _damage = 10;
        private int _healts = 90;
        private int _armor = 5;
        private int _buffArmor = 5;
        private int _countUseSpecial = 3;

        public Knight()
        {
            Name = _name;
            Damage = _damage;
            Healt = _healts;
            Armor = _armor;
        }

        protected override void SpecialAttack(Battler enemy)
        {
            if (_countUseSpecial > 0)
            {
                Armor += _buffArmor;
                _countUseSpecial--;
                Console.WriteLine($"{Name} - Увеличивает свою броню!");
            }
            else
            {
                Console.WriteLine("Слишком много брони.");
            }
        }

        protected override void Attack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Атакует мечом! Урон - {Damage}");
            base.Attack(enemy);
        }

        protected override void TakeDamage(int damage)
        {
                Healt -= damage;
                Console.WriteLine($"{Name} - Ай!");  
        }

    }

}
