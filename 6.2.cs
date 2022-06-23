using System;

namespace _6._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer();
            Player player = new Player(2, 3);

            renderer.SetPosition(player.PositionX, player.PositionY);

        }
    }
    class Player
    {
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }

        public Player(int x, int y)
        {
            PositionX = x;
            PositionY = y;
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
