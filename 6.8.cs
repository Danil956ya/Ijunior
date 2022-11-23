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
            Battler Battler = null;
            List<Battler> battlers = CreateBattlers();

            while(Battler == null)
            {
                Console.WriteLine($"Введите номер бойца №{number}, которого хотите выбрать.");
                ShowBattlers();
                string input = Console.ReadLine();

                if (int.TryParse(input, out int result) && result > 0 && result <= battlers.Count)
                    return battlers[result - 1];
            }

            return null;
        }

        private void ShowStats()
        {
            Console.WriteLine("------------------");
            Console.WriteLine($"{_firstBattler.Name}: {_firstBattler.Healts} оз.");
            Console.WriteLine($"{_secondBattler.Name}: {_secondBattler.Healts} оз.");
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
        public int Healts { get; protected set; }
        public int Armor { get; protected set; }

        public bool IsAlive()
        {
            return Healts > 0;
        }

        public void Attack(Battler enemy)
        {
            Random random = new Random();
            int Ability = random.Next(_rangeChance);

            if (_chanceAbility <= Ability)
                UseSpecialAttack(enemy);
            else
                UseAttack(enemy);
        }

        public virtual void TakeDamage(int damage)
        { 
            Healts -= damage; 
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
            Healts = 45;
            Armor = 10;
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

            if (_maxHealts / RageMedium >= Healts)
                currentRage = RageMedium;
            else if (_maxHealts / RageMax >= Healts)
                currentRage = RageMax;

            switch (currentRage)
            {

                case RageMin:
                    Console.WriteLine($"{Name} - Бъёт кулаком! Урон - {Damage}");
                    break;
                case RageMedium:
                    currentDamage = _buffRage * currentRage;
                    Console.WriteLine($"{Name} - Усиленно бъёт кулаком! Урон - {Damage}");
                    break;
                case RageMax:
                    currentDamage = _buffRage * currentRage;
                    Console.WriteLine($"{Name} - В ярости ударяет кулаком! Урон - {Damage}");
                    break;

            }

            enemy.TakeDamage(currentDamage);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Аухг.");
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
            Healts = 40;
            Armor = 5;
        }

        public override void TakeDamage(int damage)
        {
            Random random = new Random();
            int dodge = random.Next(_rangeChance);

            if (_chanceDodge >= dodge)
                Console.WriteLine($"{Name} - Увернулся");
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
            Healts = 50;
            Armor = 5;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Больно!");
        }

        protected override void UseSpecialAttack(Battler enemy)
        {

            if (Healts >= _maxHealts)
            {
                Console.WriteLine($"{Name} - Слишком перелечил себя и умер!");
                Healts = _minHealts;
            }
            else
            {
                Healts += _buffheals;
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
        private int _firebollDamage = 30;
        private int _buffheals = 20;

        public Mage()
        {
            Name = "Mage";
            Damage = 20;
            Healts = 60;
            Armor = 5;
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Ауч!");
        }

        protected override void UseSpecialAttack(Battler enemy)
        {
            const int MaxValue = 3;
            const int MinValue = 1;
            const int SpellFireboll = 1;
            const int SpellHeal = 2;
            Random random = new Random();
            int cpecial = random.Next(MinValue, MaxValue);

            switch (cpecial)
            {
                case SpellFireboll:
                    Console.WriteLine($"{Name} - Вызывает фаерболл! Урон - {_firebollDamage}");
                    enemy.TakeDamage(_firebollDamage);
                    break;
                case SpellHeal:
                    Healts += _buffheals;
                    Console.WriteLine($"{Name} - Лечит себя! Лечение - {_buffheals}");
                    break;
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
            Healts = 90;
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
