namespace LiteBattlers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field();
            const string CommandFight = "1";
            const string CommandExit = "2";
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine($"Начинаем битву? \n{CommandFight} - Да \n{CommandExit} - Нет");
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

        public void Fight()
        {
            _firstBattler = SelectBattler(1);
            _secondBattler = SelectBattler(2);

            Console.Clear();
            Console.WriteLine($"Да начнётся битва! \n{_firstBattler.Name} vs {_secondBattler.Name}");

            while (_firstBattler.IsAlive() && _secondBattler.IsAlive())
            {
                _firstBattler.Attack(_secondBattler);
                _secondBattler.Attack(_firstBattler);
                ShowStats();
            }

            ShowResult();
        }

        private List<Battler> CreateBattlers()
        {
            List<Battler> battlers = new List<Battler>();
            battlers.Add(new Warrior());
            battlers.Add(new Archer());
            battlers.Add(new Druid());
            battlers.Add(new Knight());
            battlers.Add(new Mage());
            return battlers;
        }

        private Battler SelectBattler(int number)
        {
            bool isWork = true;
            List<Battler> battlers = CreateBattlers();

            while (isWork)
            {
                Console.WriteLine($"Введите номер бойца №{number}, которого хотите выбрать.");
                ShowBattlers();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && result > 0 && result <= battlers.Count)
                {
                    return battlers[result - 1];
                    isWork = false;
                }
            }
            return null;
        }

        private void ShowStats()
        {
            Console.WriteLine("------------------");
            Console.WriteLine($"{_firstBattler.Name}: {_firstBattler.Health} оз.");
            Console.WriteLine($"{_secondBattler.Name}: {_secondBattler.Health} оз.");
            Console.WriteLine("------------------");
        }

        private void ShowBattlers()
        {
            int battlerNumber = 0;

            foreach (var battler in CreateBattlers())
            {
                battlerNumber++;
                Console.WriteLine(battlerNumber + " " + battler.Name);
            }
        }

        private void ShowResult()
        {

            if (_firstBattler.IsAlive())
                Console.WriteLine($"{_firstBattler.Name} - Побеждает!\n");
            else if (_secondBattler.IsAlive())
                Console.WriteLine($"{_secondBattler.Name} - Побеждает!\n");
            else
                Console.WriteLine("Ничья.\n");
        }
    }

    class Battler
    {
        private int _chanceAbility = 30;
        private int _rangeChance = 100;

        public string Name { get; protected set; }
        public int Damage { get; protected set; }
        public int Health { get; protected set; }
        public int Armor { get; protected set; }

        public bool IsAlive()
        {
            return Health > 0;
        }

        public void Attack(Battler enemy)
        {
            Random random = new Random();
            int ability = random.Next(_rangeChance);

            if (_chanceAbility <= ability)
                UseSpecialAttack(enemy);
            else
                UseAttack(enemy);
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        protected virtual void UseAttack(Battler enemy)
        {
            enemy.TakeDamage(Damage);
        }

        protected virtual void UseSpecialAttack(Battler enemy) { }
    }

    class Warrior : Battler
    {
        private int _buffAttack = 25;
        private int _buffRage = 3;
        private int _maxHealts = 45;

        public Warrior()
        {
            Name = "Warrior";
            Damage = 15;
            Health = 45;
            Armor = 10;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Аухг.");
        }

        protected override void UseSpecialAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Бросает стальной топор! Урон - {_buffAttack}");
            enemy.TakeDamage(_buffAttack);
        }

        protected override void UseAttack(Battler enemy)
        {
            const int RageMin = 1;
            const int RageMedium = 2;
            const int RageMax = 3;
            int currentRage = RageMin;
            int currentDamage = Damage;

            if (_maxHealts / RageMedium >= Health)
                currentRage = RageMedium;
            else if (_maxHealts / RageMax >= Health)
                currentRage = RageMax;

            switch (currentRage)
            {

                case RageMin:
                    Console.WriteLine($"{Name} - Бъёт кулаком! Урон - {currentDamage}");
                    break;
                case RageMedium:
                    currentDamage = _buffRage * currentRage;
                    Console.WriteLine($"{Name} - Усиленно бъёт кулаком! Урон - {currentDamage}");
                    break;
                case RageMax:
                    currentDamage = _buffRage * currentRage;
                    Console.WriteLine($"{Name} - В ярости ударяет кулаком! Урон - {currentDamage}");
                    break;

            }

            enemy.TakeDamage(currentDamage);
        }

    }

    class Archer : Battler
    {
        private int _chanceDodge = 15;
        private int _buffDamage = 10;
        private int _rangeChance = 100;

        public Archer()
        {
            Name = "Archer";
            Damage = 20;
            Health = 40;
            Armor = 5;
        }

        public override void TakeDamage(int damage)
        {
            Random random = new Random();
            int dodge = random.Next(_rangeChance);

            if (_chanceDodge >= dodge)
            {
                Console.WriteLine($"{Name} - Увернулся");
            }
            else
            {
                base.TakeDamage(damage);
                Console.WriteLine($"{Name} - Ай!");
            }
        }

        protected override void UseSpecialAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - выпускает ледяную стрелу! Урон - {Damage + _buffDamage}");
            enemy.TakeDamage(Damage + _buffDamage);
        }

        protected override void UseAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - выпускает стрелу! Урон - {Damage}");
            enemy.TakeDamage(Damage);
        }
    }

    class Druid : Battler
    {
        private int _buffheals = 30;
        private int _maxHealts = 200;
        private int _minHealts = 0;

        public Druid()
        {
            Name = "Druid";
            Damage = 20;
            Health = 50;
            Armor = 5;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Больно!");
        }

        protected override void UseSpecialAttack(Battler enemy)
        {

            if (Health >= _maxHealts)
            {
                Console.WriteLine($"{Name} - Слишком перелечил себя и умер!");
                Health = _minHealts;
            }
            else
            {
                Health += _buffheals;
                Console.WriteLine($"{Name} - Лечит себя целебной травой! Востановление - {_buffheals}");
            }
        }

        protected override void UseAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Атакует терновыми шипами. Урон - {Damage}");
            enemy.TakeDamage(Damage);
        }
    }

    class Mage : Battler
    {
        private const int SpellFireboll = 1;
        private const int SpellHeal = 2;

        private int[] _spells = { SpellFireboll, SpellHeal };

        private int _firebollDamage = 30;
        private int _buffheals = 20;

        public Mage()
        {
            Name = "Mage";
            Damage = 20;
            Health = 60;
            Armor = 5;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Ауч!");
        }

        protected override void UseSpecialAttack(Battler enemy)
        {
            Random random = new Random();
            int special = random.Next(_spells[0], _spells[1]);

            if (special == _spells[0])
            {
                Console.WriteLine($"{Name} - Вызывает фаерболл! Урон - {_firebollDamage}");
                enemy.TakeDamage(_firebollDamage);
            }
            else if (special == _spells[1])
            {
                Health += _buffheals;
                Console.WriteLine($"{Name} - Лечит себя! Лечение - {_buffheals}");
            }
        }

        protected override void UseAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Атакует заклинанием! Урон - {Damage}");
            enemy.TakeDamage(Damage);
        }
    }

    class Knight : Battler
    {
        private int _buffArmor = 5;
        private int _countUseSpecial = 3;

        public Knight()
        {
            Name = "Knight";
            Damage = 10;
            Health = 90;
            Armor = 5;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Ай!");
        }

        protected override void UseSpecialAttack(Battler enemy)
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

        protected override void UseAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Атакует мечом! Урон - {Damage}");
            enemy.TakeDamage(Damage);
        }
    }
}
