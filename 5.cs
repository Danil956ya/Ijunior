using System;

class Program
{
    static void Main(string[] args)
    {
        string name = "Danil";
        string surname = "Kalinin";
        Console.WriteLine("Your name is: {0} {1}", name, surname);
        string temp = name;
        name = surname;
        surname = temp;
        Console.WriteLine("Your name is: {0} {1}", name, surname);
    }
}
