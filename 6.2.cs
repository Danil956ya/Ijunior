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

        public Player(int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
        }

    }
    class Renderer
    {
        public void SetPosition(int positionX, int positionY)
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.Write("Im here!");
        }

    }
}
