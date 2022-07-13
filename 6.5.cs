using System;
using System.Collections.Generic;

namespace _6._5
{
    class Program
    {
        static void Main(string[] args)
        {
            BookStorage bookStorage = new BookStorage();
            bool isWork = true;
            while (isWork)
            {
                Console.WriteLine("Укажите команду. 1.Добавить книгу 2.Удалить книгу 3.Показать все книги. 4.Найти по категории. 5.Выход.");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        bookStorage.AddBook();
                        break;
                    case "2":
                        bookStorage.RemoveBook();
                        break;
                    case "3":
                        bookStorage.ShowAllBooks();
                        break;
                    case "4":
                        bookStorage.FindBook();
                        break;
                    case "5":
                        isWork = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверная команда");
                        break;
                }

            }
        }
    }
    class Book
    {
        public string Name { get; private set; }
        public string Autor { get; private set; }
        public int BookRelese { get; private set; }

        public Book(string name = "default", string autorh = "default", int year = 0)
        {
            Name = name;
            Autor = autorh;
            BookRelese = year;
        }
        public void ShowInfo()
        {
            Console.WriteLine($"|| Название: '{Name}'. || Автор: {Autor}. || Год выпуска: {BookRelese}. ||");
        }
    }
    class BookStorage
    {
        private List<Book> _books = new List<Book>();
        public void AddBook()
        {
            Console.WriteLine("Укажите название.");
            string name = Console.ReadLine();
            Console.WriteLine("Укажите автора.");
            string autor = Console.ReadLine();
            Console.WriteLine("укажите год выпуска книги.");
            string inputYear = Console.ReadLine();
            if (int.TryParse(inputYear, out int year))
            {
                _books.Add(new Book(name, autor, year));
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Повторите попытку.");
            }
        }
        public void RemoveBook()
        {
            ShowAllBooks();
            if (_books.Count > 0)
            {
                Console.WriteLine("Укажите номер книги, которую нужно удалить");
                string input = Console.ReadLine();
                if(int.TryParse(input,out int result) && result >= 1)
                {
                    _books.RemoveAt(result - 1);
                }
                else
                {
                    Console.WriteLine("Неверно введены значения.");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Список пуст");
            }
        }
        public void ShowAllBooks()
        {
            Console.Clear();
            if (_books.Count > 0)
            {
                int number = 0;
                foreach (var book in _books)
                {
                    number++;
                    Console.Write(number + ")" + " ");
                    book.ShowInfo();
                }
            }
            else
            {
                Console.WriteLine("Список пуст");
            }
        }
        public void FindBook()
        {
            Console.Clear();
            Console.WriteLine("Укажите по какой категории искать? \n1. Название. \n2. Автор. \n3. Год.");
            string value = Console.ReadLine();
            Console.WriteLine("Введите значение.");
            string input = Console.ReadLine();

            switch (value)
            {
                case "1":
                    foreach (var book in _books)
                    {
                        if (book.Name.Contains(input))
                        {
                            book.ShowInfo();
                        }
                    }
                    break;
                case "2":
                    foreach (var book in _books)
                    {
                        if (book.Autor.Contains(input))
                        {
                            book.ShowInfo();
                        }
                    }
                    break;
                case "3":
                    foreach (var book in _books)
                    {
                        if (book.BookRelese.ToString().Contains(input))
                        {
                            book.ShowInfo();
                        }
                    }
                    break;
            }

        }
    }
}
