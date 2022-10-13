using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace _1._2
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

        public bool IsCreated()
        {
            if (_firstBattler == null && _secondBattler == null)
            {
                ChooseBattler(out _firstBattler, 1);
                ChooseBattler(out _secondBattler, 2);
                return true;
            }
            else
            {
                return false;
            }
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
                    Console.WriteLine(_secondBattler.Name + " " + _secondBattler.Healt);
                    Console.WriteLine(_firstBattler.Name + " " + _firstBattler.Healt);
                }
                if (_firstBattler.IsAlive())
                {
                    Console.WriteLine($"{_firstBattler.Name} - Побеждает!");
                }
                else if (_secondBattler.IsAlive())
                {
                    Console.WriteLine($"{_secondBattler.Name} - Побеждает!");
                }
                else
                {
                    Console.WriteLine("Ничья.");
                }
            }
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

        private bool _isUseble;
        private int _cooldown;
        private int _maxCooldown = 3;
        private int _minCooldown = 0;

        public void AttackEnemy(ref Battler enemy)
        {
            if (enemy.IsBlocking)
            {
                Console.WriteLine($"{Name} атакует, но {enemy.Name} заблокировал атаку. Урон - {Damage / enemy.Armor}.");
                enemy.Healt -= Damage / enemy.Armor;
            }
            else
            {
                Console.WriteLine($"{Name} - Наносит {Damage} урона.");
                TakeDamage(ref enemy);
            }
        }

        protected void TakeDamage(ref Battler enemy)
        {
            enemy.Healt -= Damage;
        }

        public void UseSpecial(ref Battler enemy)
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

        protected virtual void SpecialAttack(ref Battler enemy)
        {

        }

        public void MakeTurn(ref Battler enemy)
        {
            const int Attack = 1;
            const int Special = 2;
            const int Shield = 3;
            _isUseble = _cooldown <= _minCooldown;
            _cooldown = _isUseble ? _maxCooldown : _minCooldown;
            _cooldown -= 1;
            Random random = new Random();
            int point = random.Next(1, 4);
            IsBlocking = point == Shield;

            switch (point)
            {
                case Attack:
                    AttackEnemy(ref enemy);
                    break;
                case Special:
                    UseSpecial(ref enemy);
                    break;
                case Shield:
                    UseShield(ref enemy);
                    break;
            }

        }

        public void UseShield(ref Battler enemy)
        {
            if (enemy.IsBlocking)
            {
                Console.WriteLine("Не время поднимать щиты!");
                AttackEnemy(ref enemy);
            }
            else
            {
                Console.WriteLine(Name + " в блоке.");
            }
        }

        public bool IsAlive()
        {
            return Healt > 0;
        }

    }

    class Warrior : Battler
    {
        private string _name = "Warrior";
        private int _damage = 15;
        private int _healts = 50;
        private int _armor = 10;
        private int _buffAttack = 35;

        public Warrior()
        {
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
        {
            base.Damage = _buffAttack;
            Console.WriteLine($"{Name} - Бросает стальной топор! Урон - {Damage}");
            TakeDamage(ref enemy);
            base.Damage = _damage;
        }

    }

    class Archer : Battler
    {
        private string _name = "Archer";
        private int _damage = 20;
        private int _healts = 40;
        private int _armor = 5;

        public Archer()
        {
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
        {
            Damage = _damage + enemy.Armor;
            Console.WriteLine($"{Name} - Выпускает лунную стрелу! Урон - {Damage}");
            TakeDamage(ref enemy);
            Damage = _damage;
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
                Console.WriteLine($"{Name} - Лечит себя заклинанием! Востановление - {_buffheals}");
            }
        }

    }
    class Mage : Battler
    {
        private string _name = "Mage";
        private int _damage = 20;
        private int _healts = 40;
        private int _armor = 1;
        private int _firebollDamage = 30;

        public Mage()
        {
            base.Name = _name;
            base.Damage = _damage;
            base.Healt = _healts;
            base.Armor = _armor;
        }

        protected override void SpecialAttack(ref Battler enemy)
        {
            Damage = _firebollDamage;
            Console.WriteLine($"{Name} - Вызывает фаерболл! Урон - {Damage}");
            TakeDamage(ref enemy);
            Damage = _damage;
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

    }

}
