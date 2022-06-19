using System;

namespace _4._5
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 3, 6, 1, 4, 6, 2 };
            OutputArray(array);
            Shuffle(array);
            OutputArray(array);
        }

        static void OutputArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
        }

        static void Shuffle(int[] array)
        {
            Random random = new Random();
            
            for (int i = array.Length - 1; i >= 0; i--)
            {
                int randomItem = random.Next(i);
                int shuffledElement = array[randomItem];
                array[randomItem] = array[i];
                array[i] = shuffledElement;
            }
        }
    }
}
