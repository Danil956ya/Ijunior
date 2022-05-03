using System;

public class Program
{
    static public void Main ()
    {
       string input = Console.ReadLine();
	   int maxDepth = 0;
	   int depth = 0;
	   
	  for(int i = 0; i < input.Length; i++)
	   {
	   		if(input[i] == '(')
		        {
			    depth++;	
			}
			  
			if(input[i] == ')')
			{
			    depth--;
			}
			  
			if(maxDepth < depth)
			{
			  maxDepth = depth;
			}

			if(depth < 0)
			{
			  break;
			}
	   }
	   if(depth == 0)
	   {
	   	Console.WriteLine("Correct, depth: {0}", maxDepth);
	   }
	   else
	   {
	   	Console.WriteLine("is not correct");
	   }
    }
}

