using System;

public class Program
{
    static public void Main ()
    {
      string message = "hello i using c# codding";                        
      string[] words = message.Split();

      foreach(var word in words)
      {
	Console.WriteLine(word);
      }
    }
}
