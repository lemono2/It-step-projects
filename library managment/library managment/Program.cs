using System;
using System.Collections.Generic;
using System.Linq;

namespace library_managment
{
    class Program
    {
        static void Main(string[] args)
        {
            BookManager bookManager = new BookManager();

            while (true)
            {
                Console.WriteLine("\nThe Book Manager: ");
                Console.WriteLine("1. Add New Book");
                Console.WriteLine("2. Show All Books");
                Console.WriteLine("3. Search Books by Title");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddNewBook(bookManager);
                        break;
                    case 2:
                        bookManager.ShowAllBooks();
                        break;
                    case 3:
                        SearchBooksByTitle(bookManager);
                        break;
                    case 4:
                        Console.WriteLine("Exiting the program...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        static void AddNewBook(BookManager bookManager)
        {
            Console.Write("Enter the Title of the Book: ");
            string title = Console.ReadLine();
            Console.Write("Enter the Author of the Book: ");
            string author = Console.ReadLine();

            int year;
            do
            {
                Console.Write("Enter the Year of Release of the Book: ");
            } while (!int.TryParse(Console.ReadLine(), out year));

            bookManager.AddBook(title, author, year);
        }

        static void SearchBooksByTitle(BookManager bookManager)
        {
            Console.Write("Enter the Title to Search for: ");
            string searchTitle = Console.ReadLine();
            bookManager.SearchByTitle(searchTitle);
        }
    }

    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public int YearOfRelease { get; }

        public Book(string title, string author, int yearOfRelease)
        {
            Title = title;
            Author = author;
            YearOfRelease = yearOfRelease;
        }

        public override string ToString() => $"Title: {Title}, Author: {Author}, Year of Release: {YearOfRelease}";
    }

    public class BookManager
    {
        private List<Book> books;

        public BookManager()
        {
            books = new List<Book>();
            LoadDefaultBooks();
        }

        private void LoadDefaultBooks()
        {
            books.Add(new Book("Vefxistyaosani", "Shota Rustaveli", 1200));
            books.Add(new Book("The Great Gatsby", "F. Scott Fitzgerald", 1925)); 
        }

        public void AddBook(string title, string author, int yearOfRelease)
        {
            if (yearOfRelease > DateTime.Now.Year)
            {
                Console.WriteLine("Invalid year of release.");
                return;
            }

            books.Add(new Book(title, author, yearOfRelease));
            Console.WriteLine("Book added successfully.");
        }

        public void ShowAllBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            Console.WriteLine("List of all books:");
            foreach (var book in books)
            {
                Console.WriteLine(book);
            }
        }

        public void SearchByTitle(string title)
        {
            var matchingBooks = books.Where(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase)).ToList();

            if (matchingBooks.Count == 0)
            {
                Console.WriteLine("No matching books found.");
                return;
            }

            Console.WriteLine($"Matching books with title '{title}':");
            foreach (var book in matchingBooks)
            {
                Console.WriteLine(book);
            }
        }
    }
}
