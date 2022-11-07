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
            if (SelectedBattlers())
            {
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
        }

        private List<Battler> CreateBattlers()
        {
            List<Battler> _battlers = new List<Battler>();
            _battlers.Add(new Warrior());
            _battlers.Add(new Archer());
            _battlers.Add(new Druid());
            _battlers.Add(new Knight());
            _battlers.Add(new Mage());
            return _battlers;
        }

        private Battler SelectBattler(int number)
        {
            Battler battler = null;

            while (battler == null)
            {
                Console.WriteLine($"Введите номер бойца №{number}, которого хотите выбрать.");
                ShowBattlers();
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result > 0 && result < CreateBattlers().Count + 1)
                {
                    return CreateBattlers()[result - 1];
                }
            }

            return battler;
        }

        private bool SelectedBattlers()
        {
            Console.Clear();
            _firstBattler = SelectBattler(1);
            _secondBattler = SelectBattler(2);
            return _firstBattler != null && _secondBattler != null;
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
            {
                UseSpecialAttack(enemy);
            }
            else
            {
                UseAttack(enemy);
            }
        }

        protected virtual void UseAttack(Battler enemy, int damage)
        {
            enemy.TakeDamage(damage);
        }

        protected virtual void UseAttack(Battler enemy)
        {
            enemy.TakeDamage(Damage);
        }

        protected virtual void UseSpecialAttack(Battler enemy)
        {

        }

        protected virtual void TakeDamage(int damage)
        {
            Healts -= damage;
        }

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
            base.UseAttack(enemy, _buffAttack);
        }

        protected override void UseAttack(Battler enemy)
        {
            int rageMin = 1;
            int rageMedium = 2;
            int rageMax = 3;
            int currentRage = rageMin;

            if (_maxHealts / rageMedium >= Healts)
                currentRage = rageMedium;
            else if (_maxHealts / rageMax >= Healts)
                currentRage = rageMax;

            switch (currentRage)
            {
                case 1:
                    Console.WriteLine($"{Name} - Бъёт кулаком! Урон - {Damage}");
                    break;
                case 2:
                    Damage += _buffRage * currentRage;
                    Console.WriteLine($"{Name} - Усиленно бъёт кулаком! Урон - {Damage}");
                    break;
                case 3:
                    Damage += _buffRage * currentRage;
                    Console.WriteLine($"{Name} - В ярости ударяет кулаком! Урон - {Damage}");
                    break;

            }
            base.UseAttack(enemy, Damage);
        }

        protected override void TakeDamage(int damage)
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

        protected override void UseSpecialAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - выпускает ледяную стрелу! Урон - {Damage + _buffDamage}");
            base.UseAttack(enemy, Damage + _buffDamage);
        }

        protected override void UseAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - выпускает стрелу! Урон - {Damage}");
            base.UseAttack(enemy);
        }

        protected override void TakeDamage(int damage)
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
            base.UseAttack(enemy);
        }

        protected override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Больно!");
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

        protected override void UseSpecialAttack(Battler enemy)
        {
            const int MaxValue = 3;
            const int MinValue = 1;
            Random random = new Random();
            int cpecial = random.Next(MinValue, MaxValue);

            switch (cpecial)
            {
                case 1:
                    Console.WriteLine($"{Name} - Вызывает фаерболл! Урон - {_firebollDamage}");
                    base.UseAttack(enemy, _firebollDamage);
                    break;
                case 2:
                    Healts += _buffheals;
                    Console.WriteLine($"{Name} - Лечит себя! Лечение - {_buffheals}");
                    break;
            }

        }

        protected override void UseAttack(Battler enemy)
        {
            Console.WriteLine($"{Name} - Атакует заклинанием! Урон - {Damage}");
            base.UseAttack(enemy);
        }

        protected override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            Console.WriteLine($"{Name} - Ауч!");
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
            base.UseAttack(enemy);
        }

        protected override void TakeDamage(int damage)
        {
                base.TakeDamage(damage);
                Console.WriteLine($"{Name} - Ай!");  
        }

    }

}
