using System;

namespace _5
{
    class Program
    {
        static void Main(string[] args)
        {
            int picture = 52;
            int pictureInLine = 3;
            int picturesLines = picture / pictureInLine;
            int pictureRemains = picture % pictureInLine;
            Console.WriteLine("Lines {0}, remains pictures {1}", picturesLines, pictureRemains);
        }
    }
}
