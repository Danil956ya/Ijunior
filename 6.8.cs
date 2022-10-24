namespace _1._2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Field field = new Field();
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Начинаем битву? \n1 - Да \n2 - Нет");
                string input = Console.ReadLine();

                if (input == "1")
                    field.Fight();
                else if (input == "2")
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

        private bool IsCreated()
        {
            Console.Clear();
            ChooseBattler(out _firstBattler, 1);
            ChooseBattler(out _secondBattler, 2);
            return _firstBattler != null && _secondBattler != null;
        }

        public void Fight()
        {
            if (IsCreated())
            {
                Console.Clear();
                Console.WriteLine($"Да начнётся битва! \n{_firstBattler.Name} vs {_secondBattler.Name}");
                while (_firstBattler.IsAlive() && _secondBattler.IsAlive())
                {
                    _firstBattler.MakeTurn(ref _secondBattler);
                    _secondBattler.MakeTurn(ref _firstBattler);
                    ShowStats();
                }
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

        private void ShowStats()
        {
            Console.WriteLine("------------------");
            Console.WriteLine($"{_firstBattler.Name}: {_firstBattler.Healt} оз.");
            Console.WriteLine($"{_secondBattler.Name}: {_secondBattler.Healt} оз.");
            Console.WriteLine("------------------");
        }

        public void ChooseBattler(out Battler battler, int number)
        {
            battler = null;
            while (battler == null)
            {
                Console.WriteLine($"Выбирете бойца {number}.");
                ShowBattlers();
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        battler = new Warrior();
                        break;
                    case "2":
                        battler = new Archer();
                        break;
                    case "3":
                        battler = new Druid();
                        break;
                    case "4":
                        battler = new Knight();
                        break;
                    case "5":
                        battler = new Mage();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверный ввод.");
                        break;
                }
            }

        }

        public void ShowBattlers()
        {
            int battlerNumber = 0;
            foreach (var battler in battlers)
            {
                battlerNumber++;
                Console.WriteLine(battlerNumber + " " + battler.Name);

            }
        }

    }

    class Battler
    {
        public string Name { get; protected set; }
        public int Damage { get; protected set; }
        public int Healt { get; protected set; }
        public int Armor { get; protected set; }
        public bool IsBlocking { get; protected set; }
        public bool InStun { get; protected set; }

        static private int _maxCooldown = 3;
        static private int _minCooldown = 0;
        private int _timeInStun = 2;
        private int _cooldown = _maxCooldown;
        private bool _isUseble;


        public void MakeTurn(ref Battler enemy)
        {
            const int MinTurn = 1;
            const int MaxTurn = 4;
            _isUseble = _cooldown <= _minCooldown;
            _cooldown = _isUseble ? _maxCooldown : _minCooldown;
            _cooldown -= 1;
            Random random = new Random();
            int stage = random.Next(MinTurn, MaxTurn);
            IsBlocking = stage == (int)Stage.Shield;

            if(InStun && _timeInStun > 0)
            {
                stage = 4;
                IsBlocking = false;
                _timeInStun--;
            }else
            {
                InStun = false;
            }

           
            switch (stage)
            {
                case (int)Stage.Attack:
                    AttackEnemy(ref enemy);
                    break;
                case (int)Stage.Special:
                    UseSpecial(ref enemy);
                    break;
                case (int)Stage.Shield:
                    UseShield(ref enemy);
                    break;
                case (int)Stage.Stun:
                    Console.WriteLine($"{Name} - в стане.");
                    break;
            }

        }

        public void InStuny()
        {
            InStun = !InStun;
        }

        public bool IsAlive()
        {
            return Healt > 0;
        }

        private void AttackEnemy(ref Battler enemy)
        {
            if (enemy.IsBlocking)
            {
                Console.WriteLine($"{Name} атакует, но {enemy.Name} заблокировал атаку. Урон - {Damage / enemy.Armor}.");
                enemy.Healt -= Damage / enemy.Armor;
            }
            else
            {
                TakeDamage(ref enemy);
            }
        }

        private void UseSpecial(ref Battler enemy)
        {
            if (_isUseble)
            {
                SpecialAttack(ref enemy);
            }
            else
            {
                Console.WriteLine($"{Name} - Не удалось выполнить супер атаку");
            }
        }

        private void UseShield(ref Battler enemy)
        {
            if (enemy.IsBlocking)
            {
                Console.WriteLine("Не время поднимать щиты!");
                AttackEnemy(ref enemy);
            }
            else
            {
                Shield();
            }
        }

        protected virtual void TakeDamage(ref Battler enemy)
        {
            enemy.Healt -= Damage;
        }

        protected virtual void SpecialAttack(ref Battler enemy)
        {

        }

        protected virtual void Shield()
        {
            Console.WriteLine(Name + " в блоке.");
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
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
        {
            Damage = _buffAttack;
            Console.WriteLine($"{Name} - Бросает стальной топор! Урон - {Damage}");
            base.TakeDamage(ref enemy);
            Damage = _damage;
        }

        protected override void TakeDamage(ref Battler enemy)
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
            base.TakeDamage(ref enemy);
            Damage = _damage;
        }

        protected override void Shield()
        {
            Console.WriteLine($"{Name} - Укрывается руками.");
        }

    }

    class Archer : Battler
    {
        private string _name = "Archer";
        private int _damage = 20;
        private int _healts = 40;
        private int _armor = 5;
        private int _vampiric = 1;
        private int _multiply = 3;

        public Archer()
        {
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
        {
            Damage = _damage + (enemy.Healt / enemy.Armor);
            _vampiric = ((enemy.Healt - enemy.Armor) / _multiply) >= 0 ? (enemy.Healt - enemy.Armor) / _multiply : 0;
            Console.WriteLine($"{Name} - Выпускает кровавую стрелу! Урон - {Damage}, Лечение - {_vampiric}");
            Healt += _vampiric;
            base.TakeDamage(ref enemy);
            Damage = _damage;
        }

        protected override void TakeDamage(ref Battler enemy)
        {
            base.TakeDamage(ref enemy);
            Console.WriteLine($"{Name} - выпускает стрелу! Урон - {Damage}");
        }

        protected override void Shield()
        {
            Console.WriteLine($"{Name} - Укрылся в тени.");
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
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
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

        protected override void TakeDamage(ref Battler enemy)
        {
            base.TakeDamage(ref enemy);
            Console.WriteLine($"{Name} - Атакует терновыми шипами. Урон - {Damage}");
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
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
        {
            const int MaxValue = 4;
            const int MinValue = 1;
            Random random = new Random();
            int cpecial = random.Next(MinValue, MaxValue);

            switch(cpecial)
            {
                case 1:
                    Damage = _firebollDamage;
                    Console.WriteLine($"{Name} - Вызывает фаерболл! Урон - {Damage}");
                    base.TakeDamage(ref enemy);
                    Damage = _damage;
                    break;
                case 2:
                    Healt += _buffheals;
                    Console.WriteLine($"{Name} - Лечит себя! Лечение - {_buffheals}");
                    break;
                case 3:
                    Console.WriteLine($"{Name} - Заморозил {enemy.Name}.");
                    enemy.InStuny();
                    break;
            }

        }

        protected override void TakeDamage(ref Battler enemy)
        {
            base.TakeDamage(ref enemy);
            Console.WriteLine($"{Name} - Атакует заклинанием! Урон - {Damage}");
        }

        protected override void Shield()
        {
            Console.WriteLine($"{Name} - Укрывается магическим щитом!");
        }

    }

    class Knight : Battler
    {
        private string _name = "Knight";
        private int _damage = 10;
        private int _healts = 90;
        private int _armor = 30;
        private int _buffArmor = 5;
        private int _countUseSpecial = 3;

        public Knight()
        {
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
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

        protected override void TakeDamage(ref Battler enemy)
        {
            base.TakeDamage(ref enemy);
            Console.WriteLine($"{Name} - Атакует мечом! Урон - {Damage}");
        }

    }

    enum Stage
    {
        Attack = 1,
        Special,
        Shield,
        Stun
    }
}
