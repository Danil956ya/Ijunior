using System;

namespace _6._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer();
            Player player = new Player(2, 1);

            renderer.SetPosition(player.X, player.Y);

        }
    }
    class Player
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
    class Renderer
    {
        public void SetPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("Im here!");
        }

    }
}
