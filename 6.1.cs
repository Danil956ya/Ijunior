using System;

namespace _6._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(100, 25, 10);
            player.ShowStats();
        }
    }

    class Player
    {
        private int _healt;
        private int _damage;
        private int _armor;

        public Player(int healt, int damage, int armor)
        {
            _healt = healt;
            _damage = damage;
            _armor = armor;
        }

        public void ShowStats()
        {
            Console.WriteLine("Healt: " + _healt + "\nDamage: " + _damage + "\nArmor: " + _armor);
        }

    }

}
