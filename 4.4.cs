using System;

namespace _4._4
{
    class Program
    {
        static void Main()
        {
            int playerPositionX;
            int playerPositionY;
            int playerDirectionX = 0;
            int playerDirectionY = 0;
            char[,] map = ReadMap(out playerPositionX, out playerPositionY);

            DrawMap(map);

            while (map[playerPositionX, playerPositionY] != 'X')
            {
                ChangeDirection(map, ref playerPositionX, ref playerPositionY, ref playerDirectionX, ref playerDirectionY);

                if (map[playerPositionX + playerDirectionX, playerPositionY + playerDirectionY] != '@')
                {
                    Move(ref playerPositionX, ref playerPositionY, playerDirectionX, playerDirectionY);
                }
            }
            Console.Clear();
            Console.WriteLine("Вы выйграли!");
        }

        static char[,] ReadMap(out int positionX, out int positionY)
        {
            positionX = 0;
            positionY = 0;
            Console.CursorVisible = false;

            char[,] map =
            {
               {'@','@','@','@','@','@','@','@','@','@','@','@',},
               {'@',' ',' ',' ',' ',' ',' ',' ',' ',' ','X','@',},
               {'@','@',' ',' ',' ',' ',' ',' ',' ',' ','@','@',},
               {'@','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','@',},
               {'@','@','@','@','@','@','@','@','@','@','@','@',}
            };

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == '#')
                    {
                        positionX = i;
                        positionY = j;
                    }
                }
            }
            return map;
        }
        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void ChangeDirection(char[,] map, ref int positionX, ref int positionY, ref int directionX, ref int directionY)
        {
            ConsoleKeyInfo charKey = Console.ReadKey();

            switch (charKey.Key)
            {
                case ConsoleKey.UpArrow:
                    directionX = -1;
                    directionY = 0;
                    break;
                case ConsoleKey.DownArrow:
                    directionX = 1;
                    directionY = 0;
                    break;
                case ConsoleKey.LeftArrow:
                    directionX = 0;
                    directionY = -1;
                    break;
                case ConsoleKey.RightArrow:
                    directionX = 0;
                    directionY = 1;
                    break;
            }
        }
        static void Move(ref int positionX, ref int positionY, int directionX, int directionY)
        {
            Console.SetCursorPosition(positionY, positionX);
            Console.Write(" ");
            positionX += directionX;
            positionY += directionY;
            Console.SetCursorPosition(positionY, positionX);
            Console.Write("#");
        }
    }
}
